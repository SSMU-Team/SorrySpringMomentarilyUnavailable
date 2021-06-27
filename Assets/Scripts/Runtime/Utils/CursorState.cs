using UnityEngine;

namespace Utility
{
	/// <summary>
	/// Handle the cursor logic. Extend the actual Unity logic of cursor.
	/// </summary>
	public static class CursorState
	{
		private static CursorLockMode m_previous_mode;
		private static bool m_previous_visibility;

		/// <summary>
		/// Set the cursor state.
		/// </summary>
		/// <param name="mode"> CursorLockMode </param>
		/// <param name="isVisible"> if mouse is visible. </param>
		public static void SetCursorState(CursorLockMode mode, bool isVisible)
		{
			m_previous_mode = Cursor.lockState;
			m_previous_visibility = Cursor.visible;

			Cursor.lockState = mode;
			Cursor.visible = isVisible;
		}

		/// <summary>
		/// Get and set the previous cursor state.
		/// </summary>
		public static void PreviousCursorState()
		{
			CursorLockMode previous_mode = Cursor.lockState;
			bool previous_visibility = Cursor.visible;

			Cursor.lockState = m_previous_mode;
			Cursor.visible = m_previous_visibility;

			m_previous_mode = previous_mode;
			m_previous_visibility = previous_visibility;
		}
	}
}