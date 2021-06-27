using UnityEngine;


namespace Visit.Utility
{
	/// <summary>
	/// Handle special layers for culling and personnalize their maximum distance.
	/// Change on the camera associated.
	/// </summary>
	[RequireComponent(typeof(Camera))]
	[DisallowMultipleComponent]
	public class CameraLayerCuillingDistance : MonoBehaviour
	{
		/// <summary>
		/// A layer and his distance associated.
		/// </summary>
		[System.Serializable]
		private struct LayerCull
		{
#pragma warning disable 0649

			public int layer;
			public float distance;

#pragma warning restore 0649
		}

		#region Inspector
#pragma warning disable 0649

		[SerializeField]
		private LayerCull[] m_layerCulls;

#pragma warning restore 0649
		#endregion

		private void Start()
		{
			Camera camera = GetComponent<Camera>();
			float[] distances = new float[32];
			foreach(LayerCull cull in m_layerCulls)
			{
				distances[cull.layer] = cull.distance;
			}
			camera.layerCullDistances = distances;
		}
	}
}