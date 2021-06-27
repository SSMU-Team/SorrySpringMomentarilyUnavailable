using UnityEngine;

using Utility.Singleton;

public class PlayerReference : SingletonBehaviour<PlayerReference>
{
	public static GameObject PlayerGameObject { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		PlayerGameObject = gameObject;
	}
}
