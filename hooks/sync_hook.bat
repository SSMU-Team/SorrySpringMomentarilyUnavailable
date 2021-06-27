::Open as admin for release dll synchronisation
pushd %~dp0

call python ./sync_hook.py

popd
pause