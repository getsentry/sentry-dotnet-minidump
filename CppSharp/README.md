CppSharp currently only include pre-built artifacts for Windows.
Also, there are lots of changes since their last release, so building from source here.

The [build.sh](build.sh) is basically the steps taken from the GitHub actions steps. There are [docs here though](CppSharp/blob/master/docs/GettingStarted.md).
At the end of building, the output is copied to [CppSharp-build](CppSharp-build) and checked-in.

A [PR was merged already](https://github.com/mono/CppSharp/pull/1541) to add prebuilt native bits for macOS and Linux.  
Once that's shipped, this directory can be deleted in favor of the NuGet package.
