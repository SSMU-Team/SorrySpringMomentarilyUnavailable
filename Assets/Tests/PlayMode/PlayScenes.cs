using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using NUnit.Framework;

using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayScenes
{
	string[] m_paths_scenes;

	[OneTimeSetUp]
	public void Init()
	{
		string[] guids = AssetDatabase.FindAssets("t:Scene", new string[] { "Assets/Data/Scenes" });
		m_paths_scenes = Array.ConvertAll<string, string>(guids, AssetDatabase.GUIDToAssetPath);
		m_paths_scenes = Array.FindAll(m_paths_scenes, File.Exists);
	}

	[UnitySetUp]
	public void BeforeTest()
	{
	}

	[UnityTearDown]
	public void AfterTest()
	{
	}

	[OneTimeTearDown]
	public void End()
	{
	}

	[UnityTest]
	public IEnumerator PlayAllScenes()
	{
		// Remove start scene test
		AssetDatabase.DeleteAsset(SceneManager.GetActiveScene().path);
		// Test loading of all scenes
		foreach(string path in m_paths_scenes)
		{
			Debug.Log("### Test Scene " + path);
			yield return EditorSceneManager.LoadSceneAsyncInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single));
			yield return new WaitForSeconds(5.0f);
			Debug.Log("### EndTest Scene " + path);
		}
		Debug.Log("EndTest PlayAllScenes.");
	}
}
