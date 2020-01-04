using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liar.Rules.Traits;

namespace Liar.Rules
{
	public interface IPlatform : INounThing<Traits.ColliderState>
	{
	}

	public class PlatformRule : SpecificRule<Traits.ColliderState, IPlatform>
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