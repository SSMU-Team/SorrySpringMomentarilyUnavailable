using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using CustomEvents;

using UnityEngine;
using UnityEngine.SceneManagement;

using Utility.Singleton;

using static UnityEngine.InputSystem.InputAction;

namespace SceneManagement
{

	/// <summary>
	/// Level structure, with the levels to load, and unload.
	/// </summary>
	[System.Serializable]
	public struct Level
	{
		public string level_name; // For Debug purpose
		public List<int> levels;
		public List<int> levels_exclude;
	}

	/// <summary>
	/// Handle the loadings of scenes and the transitions display between. Mainly async operations.
	/// </summary>
	public class LevelManager : SingletonBehaviour<LevelManager>
	{
		#region Inspector
#pragma warning disable 0649


		[Header("Index build scene")]

		[SerializeField]
		private Level m_static;

		[SerializeField]
		private Level m_mainMenu;

		[SerializeField]
		private Level m_ui;

		[Header("Index game scene")]

		[SerializeField]
		int[] m_universeIndex;

		[SerializeField]
		private Level[] m_games;

#pragma warning restore 0649
		#endregion

		#region Events
#pragma warning disable 0649
		[Header("Events")]

		[SerializeField]
		private SimpleEvent m_sceneReadyEvent;

#pragma warning restore 0649
		#endregion

		private Animator m_transition;
		private CinemachineVirtualCamera m_camTransition;
		private bool m_isLoadingScreen;

		private int m_restartIndex = -1;

		/// <summary>
		/// Queue for loadings scene jobs.
		/// </summary>
		private List<AsyncOperation> m_loadingJobs = new List<AsyncOperation>();

		private void Start()
		{
			m_transition = GetComponentInChildren<Animator>();
			m_camTransition = GetComponentInChildren<CinemachineVirtualCamera>();
			AddJobs(m_static);
			StartCoroutine(LoadLevel(m_mainMenu));

			foreach(Level g in m_games)
			{
				g.levels.AddRange(m_ui.levels);
				g.levels_exclude.AddRange(m_mainMenu.levels);
			}
		}

		#region Callbacks

		public void OnLoadMainMenu()
		{
			m_restartIndex = -1;
			StartCoroutine(LoadLevel(m_mainMenu));
		}

		public void OnLoadGame(int level)
		{
			m_restartIndex = level;
			StartCoroutine(LoadLevel(m_games[m_restartIndex]));
		}

		public void OnInputShortcutRestart(CallbackContext ctx)
		{
			if(ctx.performed && m_restartIndex >= 0)
			{
				Debug.Log("Restart shortcut");
				StartCoroutine(LoadLevel(m_games[m_restartIndex]));
			}
		}

		public void OnRestart()
		{
			if(m_restartIndex >= 0)
			{
				Debug.Log("Restart");
				StartCoroutine(LoadLevel(m_games[m_restartIndex]));
			}
		}

		public void OnRestartDelayed(float time)
		{
			if(m_restartIndex >= 0)
			{
				Debug.Log("Restart delayed");
				StartCoroutine(OnRestartDelayedCoroutine(time));
			}
		}

		IEnumerator OnRestartDelayedCoroutine(float time)
		{
			yield return new WaitForSeconds(time);
			StartCoroutine(LoadLevel(m_games[m_restartIndex]));
		}

		#endregion

		/// <summary>
		/// Begin transition load screen.
		/// </summary>
		public IEnumerator BeginFadeScren()
		{
			m_transition.SetBool("Load", true);
			yield return new WaitForSecondsRealtime(0.0f);
			m_camTransition.enabled = true;
			yield return new WaitForSecondsRealtime(0.0f);
		}

		/// <summary>
		/// End transition load screen.
		/// </summary>
		public IEnumerator EndFadeScreen()
		{
			yield return new WaitForSecondsRealtime(0.0f);
			m_transition.SetBool("Load", false);
			m_camTransition.enabled = false;
			yield return new WaitForSecondsRealtime(0.0f);
		}

		/// <summary>
		/// Load a particular level with fade screen.
		/// </summary>
		/// <param name="level"> The level to load.</param>
		private IEnumerator LoadLevel(Level level)
		{
			//if(m_loadingJobs.Count == 0)
			//{
			//	yield return BeginFadeScren();
			//}

			m_isLoadingScreen = true;

			AddJobs(level);

			while(m_loadingJobs.Count != 0 && m_isLoadingScreen)
			{
				yield return null;
			}

			//yield return EndFadeScreen();
			m_isLoadingScreen = false;
			m_sceneReadyEvent.Invoke();
		}

		/// <summary>
		/// Add jobs for a complete level loading.
		/// </summary>
		/// <param name="level"></param>
		private void AddJobs(Level level)
		{
			foreach(int l in level.levels_exclude)
			{
				UnloadSceneLoaded(l);
			}

			//Unload game scene in all cases (restart, change level, menu, etc.)
			foreach(Level g in m_games)
			{
				foreach(int l in g.levels)
				{
					UnloadSceneLoaded(l);
				}
			}

			foreach(int l in level.levels)
			{
				LoadSceneUnloaded(l);
			}
		}

		private void UnloadSceneLoaded(int buildIndex)
		{
			if(SceneManager.GetSceneByBuildIndex(buildIndex).isLoaded)
			{
				for(int i = 0; i < SceneManager.sceneCount; i++)
				{
					Scene scene = SceneManager.GetSceneAt(i);
					if(scene.buildIndex == buildIndex)
					{
						AsyncOperation unload = SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
						unload.completed += OnSceneJobDone;
						m_loadingJobs.Add(unload);
						break;
					}
				}
			}
		}

		private void LoadSceneUnloaded(int buildIndex)
		{
			if(!SceneManager.GetSceneByBuildIndex(buildIndex).isLoaded)
			{
				AsyncOperation load = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
				load.completed += OnSceneJobDone;
				m_loadingJobs.Add(load);
			}
		}

		private void OnSceneJobDone(AsyncOperation job)
		{
			m_loadingJobs.Remove(job);
		}
	}
}
