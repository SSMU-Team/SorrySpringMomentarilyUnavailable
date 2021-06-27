echo "Graph..."
Unity.exe -quit -batchmode -projectPath "../" -executeMethod "Builder.BuildGraph"  -logfile "build-graph.log"
if [ $? -eq 0 ]; then
    echo "Build graph complete"
    rm -r "../Build/Windows/SSMU_Graph/SSMU_BackUpThisFolder_ButDontShipItWithYourGame"
    ISCC.exe "../Installer/installer_graph.iss" > intaller_graph.log
    if [ $? -eq 0 ]; then
        echo "Installer graph done."
    else
        echo "FAIL Installer graph"
    fi
else
    echo "FAIL Build graph"
fi