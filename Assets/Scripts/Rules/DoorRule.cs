using System.Collections;
using System.Collections.Generic;
using Liar.Rules.Traits;
using UnityEngine;

namespace Liar.Rules
{
	public interface IDoor : INounThing<Traits.ColliderState>
	{
	}

	public class DoorRule : SpecificRule<Traits.ColliderState, IDoor>
	{
		protected override ColliderState GetOpposite( ColliderState other )
		{
			ColliderState opposite = new ColliderState();
			switch ( other.State )
			{
				default:
					opposite.State = EColliderState.Invalid;
					break;

				case EColliderState.Immaterial:
					opposite.State = EColliderState.Solid;
					break;

				case EColliderState.Solid:
					opposite.State = EColliderState.Immaterial;
					break;
			}

			return opposite;
		}
	}
}