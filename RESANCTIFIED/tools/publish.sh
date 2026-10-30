#!/bin/sh
# Builds self-contained desktop releases (no .NET install needed on the target PC), one per runtime identifier.
# Output: artifacts/<rid>/, e.g. artifacts/win-x64/Resanctified.exe and artifacts/linux-x64/Resanctified,
# each with the Content folder next to it.
# Usage: tools/publish.sh [rid...]   (run from RESANCTIFIED/; default: win-x64 linux-x64)
set -e
[ $# -gt 0 ] || set -- win-x64 linux-x64
for rid in "$@"; do
    dotnet publish src/Resanctified.Desktop -c Release -r "$rid" --self-contained -o "artifacts/$rid"
done
