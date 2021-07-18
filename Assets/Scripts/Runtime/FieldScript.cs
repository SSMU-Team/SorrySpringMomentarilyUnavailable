using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FieldScript : MonoBehaviour
{
	public Vector3 space;

	[ContextMenu("Place vegetables")]
	void PlaceChildren()
	{
		Vector3 localpos = Vector3.zero;
		for(int i = 0; i < transform.childCount; i++)
		{
			localpos += space;
			transform.GetChild(i).transform.localPosition = localpos;
		}
	}
}
