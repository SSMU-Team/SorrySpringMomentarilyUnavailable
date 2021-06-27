
using UnityEngine;

namespace CustomEvents
{
	public abstract class ScriptableEvent : ScriptableObject
	{
		public string NameEvent => name.Replace("Event", "");
	}
}