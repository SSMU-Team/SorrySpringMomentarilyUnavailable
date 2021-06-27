using System;

using UnityEngine;

namespace Utility
{
	/// <summary>
	/// Utilities function for sorting array of monobehaviours components by positions.
	/// </summary>
	public static class SortUtilities
	{
		/// <summary>
		/// Compare function.
		/// Order Scripts components by Y, then Z, then -X axes
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns>https://docs.microsoft.com/fr-fr/dotnet/api/system.collections.icomparer.compare?view=netcore-3.1</returns>
		private static int CompareByPosition(MonoBehaviour A, MonoBehaviour B)
		{
			//null greater than non null.
			if(A == null)
			{
				return B == null ? 0 : 1;
			}

			if(B == null)
			{
				return -1;
			}

			float ya = A.transform.position.y;
			float yb = B.transform.position.y;
			if(ya.CompareTo(yb) == 0)
			{
				float za = A.transform.position.z;
				float zb = B.transform.position.z;
				if(za.CompareTo(zb) == 0)
				{
					float xa = A.transform.position.x;
					float xb = B.transform.position.x;
					if(xa.CompareTo(xb) == 0)
					{
						return 0;
					}
					return xa.CompareTo(xb);
				}
				return -za.CompareTo(zb);
			}
			return ya.CompareTo(yb);
		}

		/// <summary>
		/// Compare function.
		/// Order Scripts components by Y, then Z, then -X axes in local position
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns>https://docs.microsoft.com/fr-fr/dotnet/api/system.collections.icomparer.compare?view=netcore-3.1</returns>
		private static int CompareByLocalPosition(MonoBehaviour A, MonoBehaviour B)
		{
			//null greater than non null.
			if(A == null)
			{
				return B == null ? 0 : 1;
			}

			if(B == null)
			{
				return -1;
			}

			float ya = A.transform.localPosition.y;
			float yb = B.transform.localPosition.y;
			if(ya.CompareTo(yb) == 0)
			{
				float za = A.transform.localPosition.z;
				float zb = B.transform.localPosition.z;
				if(za.CompareTo(zb) == 0)
				{
					float xa = A.transform.localPosition.x;
					float xb = B.transform.localPosition.x;
					if(xa.CompareTo(xb) == 0)
					{
						return 0;
					}
					return xa.CompareTo(xb);
				}
				return -za.CompareTo(zb);
			}
			return ya.CompareTo(yb);
		}


		/// <summary>
		/// Sort the MonoBehaviour components from the actual position of GameObjects attached.
		/// Order first by Y, then Z, then -X axes. The null components are the last ones.
		/// </summary>
		/// <param name="array"> The array to sort.</param>
		public static void SortByPosition(MonoBehaviour[] array)
		{
			Array.Sort(array, CompareByPosition);
		}


		/// <summary>
		/// Sort the MonoBehaviour components from the actual local position of GameObjects attached.
		/// Order first by Y, then Z, then -X axes. The null components are the last ones.
		/// </summary>
		/// <param name="array"> The array to sort.</param>
		public static void SortByLocalPosition(MonoBehaviour[] array)
		{
			Array.Sort(array, CompareByLocalPosition);
		}
	}

}
