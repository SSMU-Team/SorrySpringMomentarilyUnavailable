namespace Utility
{
	public static class MenuUtilities
	{
		public static void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         UnityEngine.Application.OpenURL(webplayerQuitURL);
#else
         UnityEngine.Application.Quit();
#endif
		}
	}
}