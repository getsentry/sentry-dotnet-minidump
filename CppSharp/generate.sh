#!/bin/bash
set -e

pushd CppSharp-build

export cppsharp=CppSharp.CLI.dll
if [ ! -f "$cppsharp" ]; then
    echo "$cppsharp does not exist. Did you build CppSharp?"
    exit 1
fi

export artifacts=../../sentry-native/sentry-native-artifacts
export header=$artifacts/sentry.h
if [ ! -f "$header" ]; then
    echo "$header does not exist. Did you build sentry-native?"
    exit 2
fi

dotnet $cppsharp \
    -L $artifacts \
    -o "../sentry" \
    --on "sentry" \
    -d \
    -a x64 \
    -g "csharp" \
    -cs \
    -c \
    -v \
    $header
