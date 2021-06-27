echo "Proto..."
Unity.exe -quit -batchmode -projectPath "../" -executeMethod "Builder.BuildProto"  -logfile "build-proto.log"
if [ $? -eq 0 ]; then
    echo "Build proto complete"
    rm -r "../Build/Windows/SSMU/SSMU_BackUpThisFolder_ButDontShipItWithYourGame"
    ISCC.exe "../Installer/installer.iss" > intaller_proto.log
    if [ $? -eq 0 ]; then
        echo "Installer proto done."
    else
        echo "FAIL Installer proto"
    fi
else
    echo "FAIL Build proto"
fi