using Resanctified.Desktop;
using Resanctified.Game;
using System;

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
game.Run();
return 0;
