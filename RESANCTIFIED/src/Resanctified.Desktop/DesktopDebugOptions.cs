using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Resanctified.Desktop
{
    /// <summary>
    /// Command-line switches for automated checks (screenshots, scripted taps and key presses, timed exit).
    /// All times are seconds of game time; tap positions are virtual screen coordinates (480x320, Y up).
    /// </summary>
    public sealed class DesktopDebugOptions
    {
        public const string USAGE =
            "Options:\n" +
            "  --capture <seconds> <file.png>   save the virtual screen after <seconds> (repeatable)\n" +
            "  --capture-window <s> <file.png>  same, but the whole window including letterbox bars\n" +
            "  --window <width>x<height>        initial window size (default 960x640)\n" +
            "  --click <x>,<y>@<seconds>        tap at virtual coords, Y up (repeatable)\n" +
            "  --key <Key>@<seconds>[:<hold>]   hold a key (XNA Keys name, e.g. Right, LeftControl, W)\n" +
            "                                   for <hold> seconds, default 0.1 (repeatable)\n" +
            "  --exit-after <seconds>           quit once <seconds> have passed and captures are written";

        public sealed record Capture(double Seconds, string FilePath, bool WholeWindow);

        public sealed record Click(double Seconds, Vector2 Position);

        public sealed record KeyPress(double Seconds, double HoldSeconds, Keys Key);

        // Long enough to span several frames, so a tap is never lost between two updates.
        private const double DEFAULT_KEY_HOLD_SECONDS = 0.1;

        public IReadOnlyList<Capture> Captures { get; private set; } = Array.Empty<Capture>();

        public IReadOnlyList<Click> Clicks { get; private set; } = Array.Empty<Click>();

        public IReadOnlyList<KeyPress> KeyPresses { get; private set; } = Array.Empty<KeyPress>();

        public double? ExitAfterSeconds { get; private set; }

        public Point? WindowSize { get; private set; }

        /// <summary>
        /// Throws <see cref="ArgumentException"/> with a readable message when a known switch is malformed.
        /// Unknown arguments are only logged: launchers (Steam, shortcuts, macOS -psn_*) add their own,
        /// and the game must still start.
        /// </summary>
        public static DesktopDebugOptions Parse(string[] args)
        {
            List<Capture> captures = new List<Capture>();
            List<Click> clicks = new List<Click>();
            List<KeyPress> keyPresses = new List<KeyPress>();
            DesktopDebugOptions options = new DesktopDebugOptions();
            for (int index = 0; index < args.Length; ++index)
            {
                switch (args[index])
                {
                    case "--capture":
                        captures.Add(new Capture(ParseSeconds(Next(args, ref index)), Next(args, ref index), false));
                        break;
                    case "--capture-window":
                        captures.Add(new Capture(ParseSeconds(Next(args, ref index)), Next(args, ref index), true));
                        break;
                    case "--window":
                        options.WindowSize = ParseWindowSize(Next(args, ref index));
                        break;
                    case "--click":
                        clicks.Add(ParseClick(Next(args, ref index)));
                        break;
                    case "--key":
                        keyPresses.Add(ParseKeyPress(Next(args, ref index)));
                        break;
                    case "--exit-after":
                        options.ExitAfterSeconds = ParseSeconds(Next(args, ref index));
                        break;
                    default:
                        Console.WriteLine("Ignoring unknown argument: " + args[index]);
                        break;
                }
            }
            options.Captures = captures.OrderBy(c => c.Seconds).ToList();
            options.Clicks = clicks.OrderBy(c => c.Seconds).ToList();
            options.KeyPresses = keyPresses.OrderBy(k => k.Seconds).ToList();
            return options;
        }

        private static string Next(string[] args, ref int index)
        {
            if (index + 1 >= args.Length)
                throw new ArgumentException("Missing value after " + args[index]);
            return args[++index];
        }

        private static double ParseSeconds(string text)
        {
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double seconds) || seconds < 0.0)
                throw new ArgumentException("Invalid time in seconds: " + text);
            return seconds;
        }

        // Format: <width>x<height>
        private static Point ParseWindowSize(string text)
        {
            string[] parts = text.Split('x');
            if (parts.Length != 2
                || !int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out int width)
                || !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out int height)
                || width <= 0 || height <= 0)
                throw new ArgumentException("Invalid window size, expected <width>x<height>: " + text);
            return new Point(width, height);
        }

        // Format: <x>,<y>@<seconds>
        private static Click ParseClick(string text)
        {
            string[] timeSplit = text.Split('@');
            string[] coords = timeSplit[0].Split(',');
            if (timeSplit.Length != 2 || coords.Length != 2
                || !float.TryParse(coords[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x)
                || !float.TryParse(coords[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y))
                throw new ArgumentException("Invalid click, expected <x>,<y>@<seconds>: " + text);
            return new Click(ParseSeconds(timeSplit[1]), new Vector2(x, y));
        }

        // Format: <Key>@<seconds>[:<holdSeconds>]
        private static KeyPress ParseKeyPress(string text)
        {
            string[] timeSplit = text.Split('@');
            // Enum.TryParse also takes numbers; only key names are meant here.
            if (timeSplit.Length != 2
                || int.TryParse(timeSplit[0], out _)
                || !Enum.TryParse(timeSplit[0], true, out Keys key)
                || !Enum.IsDefined(key)
                || key == Keys.None)
                throw new ArgumentException("Invalid key, expected <Key>@<seconds>[:<holdSeconds>] with an XNA Keys name: " + text);
            string[] holdSplit = timeSplit[1].Split(':');
            if (holdSplit.Length > 2)
                throw new ArgumentException("Invalid key, expected <Key>@<seconds>[:<holdSeconds>]: " + text);
            double hold = holdSplit.Length == 2 ? ParseSeconds(holdSplit[1]) : DEFAULT_KEY_HOLD_SECONDS;
            return new KeyPress(ParseSeconds(holdSplit[0]), hold, key);
        }
    }
}
