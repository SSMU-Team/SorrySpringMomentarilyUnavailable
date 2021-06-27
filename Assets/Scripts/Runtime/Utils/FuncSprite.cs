
using UnityEngine;

namespace Utility
{
	/// <summary>
	/// Utilities functions for Sprite management.
	/// </summary>
	public static class FuncSprite
	{
		public static Sprite ConvertTextureToSprite(Texture2D texture)
		{
			return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 400);
		}
	}
}
