using System.Collections;
using System.Collections.Generic;
using Liar.Rules.Traits;
using UnityEngine;

namespace Liar.Rules
{
	public interface IGravityChange : INounThing<Traits.DirectionTrait>
	{
	}

	public class GravityRule : SpecificRule<Traits.DirectionTrait, IGravityChange>
	{
		[Header("Gravity Rules")]
		[SerializeField] private float m_gravity = 9.8f;

		protected override void ApplyRule_Internal( Traits.DirectionTrait newTrait )
		{
			Physics2D.gravity = GetGravity( newTrait.Value );

			base.ApplyRule_Internal( newTrait );
		}

		protected override DirectionTrait GetOpposite( DirectionTrait other )
		{
			DirectionTrait opposite = new DirectionTrait();
			switch ( other.Value )
			{
				default:
					opposite.Value = EDirection.None;
					break;

				case EDirection.Up:
					opposite.Value = EDirection.Down;
					break;

				case EDirection.Down:
					opposite.Value = EDirection.Up;
					break;

				case EDirection.Left:
					opposite.Value = EDirection.Right;
					break;

				case EDirection.Right:
					opposite.Value = EDirection.Left;
					break;
			}

			return opposite;
		}

		private Vector2 GetGravity( EDirection direction )
		{
			Vector2 gravityDir = GameUtility.GetDirection( direction );
			return gravityDir * m_gravity;
		}
	}
}