#!/bin/sh
# Re-creates Content/sounds/*.ogg from the original WP7 music (.wma, kept in _ARCHIVE_).
# MonoGame DesktopGL cannot play WMA songs, so the music ships as Ogg Vorbis loaded by AudioManager.
# The committed files were made with ffmpeg 7.0 (libvorbis), quality 5 (~160 kb/s, 44.1 kHz stereo), no metadata tags.
# Usage: tools/convert-music.sh [source-dir]   (run from RESANCTIFIED/)
set -e
SRC="${1:-../_ARCHIVE_/TNun/Content/Sounds}"
DST="Content/sounds"
for name in cutscene music_01 music_02 music_03 music_04 music_credits music_end music_splash; do
    ffmpeg -hide_banner -loglevel error -y -i "$SRC/$name.wma" -vn -map_metadata -1 -c:a libvorbis -q:a 5 "$DST/$name.ogg"
    echo "$DST/$name.ogg"
done
