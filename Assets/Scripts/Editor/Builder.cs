
using UnityEditor;

using UnityEngine;

public static class Builder
{
	static void BuildGraph()
	{
		Debug.Log("### BUILDING ###");
		UnityEditor.Build.Reporting.BuildReport report = BuildPipeline.BuildPlayer(
			new[] { "Assets/Data/Scenes/Graph.unity" },
			"Build/Windows/SSMU_Graph/SSMU.exe",
			BuildTarget.StandaloneWindows64,
			BuildOptions.Development);

		Debug.Log("###   DONE   ###");

		Debug.Log(report);
	}

	static void BuildProto()
	{
		Debug.Log("### BUILDING ###");
		UnityEditor.Build.Reporting.BuildReport report = BuildPipeline.BuildPlayer(
			new[] { "Assets/Data/Scenes/Proto-0.2.unity" },
			"Build/Windows/SSMU/SSMU.exe",
			BuildTarget.StandaloneWindows64,
			BuildOptions.Development);

		Debug.Log("###   DONE   ###");

		Debug.Log(report);
	}
}
