using UnityEngine;

namespace Utility
{
	/// <summary>
	/// Timer to use in Update Loop.
	/// Begin when created.
	/// </summary>
	public struct TimerUpdate
	{
		private float Delay { get; set; }
		private float Timer { get; set; }

		public bool IsFinish => Timer == 0 && Delay != 0;

		public void Init(float delay)
		{
			Delay = delay;
			ResetTimer();
		}

		public void ResetTimer()
		{
			Timer = Delay;
		}

		public void UpdateTimer()
		{
			if(Timer > 0)
			{
				Timer -= Time.deltaTime;
			}
			else
			{
				Timer = 0;
			}
		}
	}
}