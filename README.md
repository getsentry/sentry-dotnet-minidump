<p align="center">
  <a href="https://sentry.io" target="_blank" align="center">
    <img src="https://sentry-brand.storage.googleapis.com/sentry-logo-black.png" width="280">
  </a>
  <br />
</p>

Experimental Sentry Native SDK for .NET 
===========

This is an experiment to bundle [`sentry-native`](https://github.com/getsentry/sentry-native) with [Google's crashpad](https://chromium.googlesource.com/crashpad/crashpad/) and distribute via NuGet with a .NET API to initialize.

Windows, macOS and Linux are the initial goals.

It allows capturing minidumps of .NET applications caused by native libraries or the .NET runtime itself.

If this is useful to you, help us in building this on [Discord, #dotnet](https://discord.gg/Ww9hbqr).

![dotnet native crash](.github/dotnet-native-crash.png)

# Build from source

If you want to build all artifacts from source, the steps involved are:

## Init submodules

Run [setup.sh](setup.sh) on macOS or Linux or [setup.bat](setup.bat) on Windows
This will initialize all git submodules.

## Build native dependencies

Under [sentry-native](sentry-native):
Run [build.sh](sentry-native/build.sh) on macOS or Linux or [build.bat](sentry-native/build.bat).

Ths will build `sentry-native`, and copy the generated native library and the crashpad_handler to the relevant folder under [sentry-native/sentry-native-artifacts](sentry-native/sentry-native-artifacts). These are bundled in the NuGet package.

On Linux, make sure to install openssl lib, otherwise `crashpad` will fail to compile.
For example, on Ubuntu: `sudo apt-get install libssl-dev`

## Resources

* [![Documentation](https://img.shields.io/badge/documentation-sentry.io-green.svg)](https://docs.sentry.io/platforms/dotnet/)
* [![Forum](https://img.shields.io/badge/forum-sentry-green.svg)](https://forum.sentry.io/c/sdks)
* [![Discord](https://img.shields.io/discord/621778831602221064)](https://discord.gg/Ww9hbqr)
* [![Stack Overflow](https://img.shields.io/badge/stack%20overflow-sentry-green.svg)](http://stackoverflow.com/questions/tagged/sentry)
* [![Twitter Follow](https://img.shields.io/twitter/follow/getsentry?label=getsentry&style=social)](https://twitter.com/intent/follow?screen_name=getsentry)