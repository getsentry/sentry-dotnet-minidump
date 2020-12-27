#!/bin/bash
set -e

pushd CppSharp/
# Assumes macOS, x64
export PLATFORM=x64
build/build.sh generate -platform $PLATFORM
build/build.sh download_llvm -platform $PLATFORM
build/build.sh restore -platform $PLATFORM
build/build.sh -platform $PLATFORM -build_only
build/test.sh -platform $PLATFORM
build/build.sh prepack -platform $PLATFORM
popd

export destination=CppSharp-build
rm -rf $destination
cp -rp CppSharp/bin/Release_x64/ $destination

pushd $destination