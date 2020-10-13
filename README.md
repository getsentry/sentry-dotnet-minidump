git submodule update --init --recursive

xcodebuild -project sentry-cocoa/Sentry.xcodeproj -target Sentry -sdk iphonesimulator -configuration Release build

> xcrun -sdk iphoneos lipo -info sentry-cocoa/build/Release-iphonesimulator/Sentry.framework/Sentry
Architectures in the fat file: sentry-cocoa/build/Release-iphonesimulator/Sentry.framework/Sentry are: i386 x86_64 arm64 