# Resanctified

A non-commercial preservation port of **Twin Blades**, a Windows Phone 7 game released in 2010, to .NET 10
and MonoGame DesktopGL, for Windows and Linux. The original game can no longer be bought or installed; this
project keeps it playable on current PCs.

## Copyright and notice

- **Twin Blades** and all of its original code, art, music, sounds and text are ©2010 Press Start Studio.
  All rights reserved. They belong to their respective rights holders, not to this project.
- This project is **not affiliated with, endorsed by or connected to** Press Start Studio, its former owners,
  its publisher, Microsoft or any other rights holder.
- It is **free and non-commercial**: it is not sold, it carries no ads and no money is made from it.
- **No license is granted.** This repository has no open-source license, and nothing in it gives anyone the
  right to reuse the original game's code or assets.
- The upscaled textures in `RESANCTIFIED/Content/hd/` are derived from the original art and belong to its
  rights holders like the rest of it.
- Windows Phone, Xbox and Microsoft are trademarks of Microsoft Corporation. Other names are trademarks of
  their respective owners.

**Rights holders:** if you own rights in Twin Blades and want this project changed or taken down, open an
issue or contact the repository owner through GitHub, and it will be done promptly and without dispute.

## Repository layout

- `RESANCTIFIED/`: the port (engine, game, desktop host, tests, content).
- `_ARCHIVE_/`: an abandoned 2022 Android attempt, kept for reference.

## Building a release (run from `RESANCTIFIED/`)

```sh
tools/publish.sh                 # Windows and Linux (win-x64, linux-x64)
tools/publish.sh linux-x64       # one platform; any .NET runtime ID works
```

Each release lands in `RESANCTIFIED/artifacts/<rid>/` with the `Content` folder next to the executable.
Releases are self-contained: the target PC does not need .NET installed. `artifacts/` is not in git.

## Running

| Platform | Command (from `RESANCTIFIED/`) |
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
[DesktopDebugOptions.cs](RESANCTIFIED/src/Resanctified.Desktop/DesktopDebugOptions.cs).

## Other tools

- `RESANCTIFIED/tools/hd/`: the HD texture pack pipeline, see [its README](RESANCTIFIED/tools/hd/README.md).
- `RESANCTIFIED/tools/convert-music.sh`: re-creates `Content/sounds/*.ogg` from the original `.wma` music.
