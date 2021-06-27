::Exemple of setting up Unity Environment for build scripts, need also to set %UNITY_FOLDER% in Path
pushd %~dp0
setx UNITY_FOLDER "C:\Program Files\Unity\Hub\Editor\2020.3.4f1\Editor"
popd
pause