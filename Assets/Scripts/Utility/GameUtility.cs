using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar
{
	public enum EDirection
	{
		None,

		Up, Right, Down, Left
	}

	public enum EColliderState
	{
		Invalid,

		Solid, Immaterial
	}

	[System.Flags]
	public enum EOrientationMode
	{
		Everything = -1,
		Nothing = 0,

		Position =	1 << 0,
		Rotation =	1 << 1,
		Scale =		1 << 2
	}

	public static class GameUtility 
	{
		public static Vector2 GetDirection( EDirection direction )
		{
			switch ( direction )
			{
				default:
				case EDirection.None: return Vector2.zero;

				case EDirection.Up: return Vector2.up;
				case EDirection.Down: return Vector2.down;
				case EDirection.Left: return Vector2.left;
				case EDirection.Right: return Vector2.right;
			}
		}
	}
}