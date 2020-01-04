using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Liar.Gameplay
{
	public class WinLoseTrigger : MonoBehaviour, Rules.IWinLose
	{
		[SerializeField] private bool m_isWin = true;

		[Header( "References " )]
		[SerializeField] private Utility.SfxClip m_winSfx = default;
		[SerializeField] private Utility.SfxClip m_loseSfx = default;

		public void Apply( Rules.Traits.BinaryTrait newTrait )
		{
			m_isWin = newTrait.Value;
		}

		public void AddNounTag()
		{
			NounContainer.AddNounThing<Rules.IWinLose>( this );
		}

		public void RemoveNounTag()
		{
			NounContainer.RemoveNounThing<Rules.IWinLose>( this );
		}

		private void OnTriggerEnter2D( Collider2D collision )
		{
			PlayerController player = collision.attachedRigidbody?.GetComponent<PlayerController>();
			if ( m_isWin )
			{
				player.Win();
				m_winSfx?.PlaySfx();
            }
			else
			{
				player.Lose();
				m_loseSfx.PlaySfx();
			}
		}

		private void Awake()
		{
			AddNounTag();
		}

		private void OnDestroy()
		{
			RemoveNounTag();
		}
	}
}