using System.Collections;
using System.Collections.Generic;
using Liar.Rules.Traits;
using UnityEngine;

namespace Liar.Gameplay
{
	public class Spike : MonoBehaviour, Rules.ISpike
	{
		[SerializeField] private bool m_isDeadly = true;

		private BoxCollider2D m_collider = null;

		public void Apply( BinaryTrait newTrait )
		{
			ToggleDeadliness( newTrait.Value );
		}

		public void AddNounTag()
		{
			NounContainer.AddNounThing<Rules.ISpike>( this );
		}

		public void RemoveNounTag()
		{
			NounContainer.RemoveNounThing<Rules.ISpike>( this );
		}

		private void OnTriggerEnter2D( Collider2D collision )
		{
			PlayerController player = collision.attachedRigidbody?.GetComponent<PlayerController>();
			if ( player == null ) { return; }
			
			player.Lose();
        }

		private void ToggleDeadliness( bool isDeadly )
		{
			m_isDeadly = isDeadly;

			if ( isDeadly )
			{
				m_collider.isTrigger = true;
			}
			else
			{
				m_collider.isTrigger = false;
			}
		}

		private void Awake()
		{
			AddNounTag();

			m_collider = GetComponentInChildren<BoxCollider2D>();
			ToggleDeadliness( m_isDeadly );
		}

		private void OnDestroy()
		{
			RemoveNounTag();
		}
	}
}