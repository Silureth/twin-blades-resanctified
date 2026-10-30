# Resanctified

A port of Twin Blades to .NET 10 and MonoGame DesktopGL, for Windows and Linux.

## Building a release (run from `RESANCTIFIED/`)

```sh
tools/publish.sh                 # Windows and Linux (win-x64, linux-x64)
tools/publish.sh linux-x64       # one platform; any .NET runtime ID works
```

Each release lands in `artifacts/<rid>/` with the `Content` folder next to the executable. Releases are
self-contained: the target PC does not need .NET installed. `artifacts/` is not in git.

## Running

| Platform | Command |
|---|---|
| Linux | `artifacts/linux-x64/Resanctified` |
| Windows | `artifacts\win-x64\Resanctified.exe` |

- The game finds its content next to the executable, so it can be started from any folder or by
  double-clicking.
- Keep the whole release folder together. On Linux it also bundles SDL2 and OpenAL.
- Ship the Linux release as a `.tar.gz`, for example `tar -czf Resanctified-linux-x64.tar.gz -C artifacts linux-x64`.
  A zip made on Windows drops the executable bit; `chmod +x Resanctified` restores it.
- Saves and settings go to `~/.local/share/Resanctified` on Linux and `%LOCALAPPDATA%\Resanctified` on Windows.

## Command-line options

`--window <width>x<height>` sets the initial window size (default 960x640), for example
`artifacts/linux-x64/Resanctified --window 1440x960`.

The other switches (`--capture`, `--click`, `--key`, `--exit-after`) script screenshots and input for
automated checks. Running the game with a malformed switch prints the full list; see also
`src/Resanctified.Desktop/DesktopDebugOptions.cs`.

## Other tools

- `tools/hd/`: the HD texture pack pipeline, see [tools/hd/README.md](tools/hd/README.md).
- `tools/convert-music.sh`: re-creates `Content/sounds/*.ogg` from the original `.wma` music.
