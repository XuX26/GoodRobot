using System.Collections;
using System.Collections.Generic;
using Liar.Rules;
using Liar.Rules.Traits;
using UnityEngine;

namespace Liar.Gameplay
{
	public class Platform : MonoBehaviour, IPlatform
	{
		private BoxCollider2D m_collider = null;

		public void Apply( ColliderState newTrait )
		{
			switch ( newTrait.State )
			{
				default:
				case EColliderState.Invalid:
					Debug.Log( $"Something went wrong with the collider's state!", this );
					break;

				case EColliderState.Solid:
					m_collider.enabled = true;
					break;

				case EColliderState.Immaterial:
					m_collider.enabled = false;
					break;
			}
		}

		public void AddNounTag()
		{
			NounContainer.AddNounThing<IPlatform>( this );
		}

		public void RemoveNounTag()
		{
			NounContainer.RemoveNounThing<IPlatform>( this );
		}

		private void Awake()
		{
			m_collider = GetComponentInChildren<BoxCollider2D>();

			AddNounTag();
		}

		private void OnDestroy()
		{
			RemoveNounTag();
		}
	}
}