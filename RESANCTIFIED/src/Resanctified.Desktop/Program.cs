using System;
using Resanctified.Desktop;
using Resanctified.Game;

DesktopDebugOptions debugOptions;
try
{
    debugOptions = DesktopDebugOptions.Parse(args);
}
catch (ArgumentException ex)
{
    Console.Error.WriteLine(ex.Message);
    Console.Error.WriteLine(DesktopDebugOptions.USAGE);
    return 2;
}

using var game = new GameApplication();
game.Components.Add(new DesktopHost(game, debugOptions));
if (debugOptions.KeyPresses.Count > 0)
    game.Components.Add(new DesktopKeyScript(game, debugOptions.KeyPresses));
game.Run();
return 0;
