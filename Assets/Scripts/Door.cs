using System.Collections;
using System.Collections.Generic;
using Liar.Rules.Traits;
using UnityEngine;

namespace Liar.Gameplay
{
	public class Door : MonoBehaviour, Rules.IDoor
	{
		[Header( "Modifiers" )]
		[SerializeField] private bool m_isClosed = true;

		[Header( "References" )]
		[SerializeField] private SpriteRenderer m_closedRenderer = default;
		[SerializeField] private Collider2D m_collider = default;

		public void Apply( ColliderState newTrait )
		{
			bool isClosed = false;

			switch ( newTrait.State )
			{
				default:
				case EColliderState.Invalid:
					Debug.Log( $"Something went wrong with the door's state!", this );
					break;

				case EColliderState.Solid:
					isClosed = true;
					break;

				case EColliderState.Immaterial:
					isClosed = false;
					break;
			}

			SetDoorState( isClosed );
		}

		public void AddNounTag()
		{
			NounContainer.AddNounThing<Rules.IDoor>( this );
		}

		public void RemoveNounTag()
		{
			NounContainer.RemoveNounThing<Rules.IDoor>( this );
		}

		public void SetDoorState( bool isClosed )
		{
			m_isClosed = isClosed;

			m_collider.enabled = m_isClosed;
			m_closedRenderer.gameObject.SetActive( m_isClosed );
		}

		private void Awake()
		{
			AddNounTag();
			SetDoorState( m_isClosed );
		}

		private void OnDestroy()
		{
			RemoveNounTag();
		}
	}
}