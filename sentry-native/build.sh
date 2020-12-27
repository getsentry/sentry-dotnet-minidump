#!/bin/bash
set -e

case "$(uname -s)" in
   Darwin) export os='osx' ;;
   Linux) export os='linux' ;;
   *)
     echo 'Operating System not supported'
     exit -1 
     ;;
esac

case "$(uname -m)" in
   x86_64)
     export arch='x64'
     ;;
   x86)
     export arch='x86'
     ;;
     # TODO: Add as needed
   *)
     echo 'Architecture not supported'
     exit -1 
     ;;
esac

pushd sentry-native
cmake -B build -D SENTRY_BACKEND=crashpad -D SENTRY_TRANSPORT=none
cmake --build build --config Release --parallel
cmake --install build --prefix install
popd

export destination=sentry-native-artifacts/$os-$arch
mkdir -p $destination
echo copying files to $destination

cp sentry-native/install/bin/crashpad_handler $destination
# Will override on each build and we can diff. Shouldn't change if on the same sha
# header isn't going to be packed but is used to generate the bindings with CppSharp
cp sentry-native/install/include/sentry.h $destination/..

if [ $os == osx ]; then
    cp sentry-native/install/lib/libsentry.dylib $destination
    cp -r sentry-native/install/lib/libsentry.dylib.dSYM $destination
elif [ $os == linux ]; then
    # TODO: Symbols
    cp sentry-native/install/lib/libsentry.so $destination
else
    echo -e "$os isn't supported"
    exit -2
fi
