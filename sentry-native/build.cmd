pushd sentry-native
cmake -B build -D SENTRY_BACKEND=crashpad -D SENTRY_TRANSPORT=none
cmake --build build --config Release --parallel
cmake --install build --prefix install
popd

set destination=sentry-native-artifacts/win-x64

cp sentry-native/install/bin/crashpad_handler.exe %destination%
if %errorlevel% neq 0 exit /b %errorlevel%
cp sentry-native/install/bin/sentry.dll %destination%
if %errorlevel% neq 0 exit /b %errorlevel%

rem modified CMakeLists.txt from the checked out sha as:
rem https://github.com/getsentry/sentry-native/pull/439
rem in order to get pdbs in Release mode
rem install isn't copying the pdb though:
rem https://github.com/getsentry/sentry-native/issues/440
cp sentry-native/build/Release/sentry.pdb %destination%
if %errorlevel% neq 0 exit /b %errorlevel%
