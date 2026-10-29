#!/bin/sh
# Builds a self-contained Windows x64 release (no .NET install needed on the target PC).
# Output: artifacts/win-x64/Resanctified.exe, with the Content folder next to it.
# Usage: tools/publish-windows.sh   (run from RESANCTIFIED/)
set -e
dotnet publish src/Resanctified.Desktop -c Release -r win-x64 --self-contained -o artifacts/win-x64
