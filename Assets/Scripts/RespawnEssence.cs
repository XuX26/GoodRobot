using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Gameplay
{
	public class RespawnEssence : MonoBehaviour
	{
		public float FlyDuration { get { return m_flyDuration; } }

		[Header( "Modifiers" )]
		[SerializeField] private float m_flyDuration = 1;
		[SerializeField] private AnimationCurve m_flyCurve = default;
		[SerializeField] private float m_minHeight = 2;

		[Header( "References" )]
		[SerializeField] private ParticleSystem m_partiles = default;
		[SerializeField] private SpriteRenderer m_renderer = default;

		public float FlyToTarget( Vector3 startPos, Vector3 endPos )
		{
			if ( m_renderer != null )
			{
				m_renderer.enabled = true;
			}
			m_partiles.Play();

			StartCoroutine( Fly_Coroutine( startPos, endPos ) );
			return m_flyDuration;
		}

		public void Stop()
		{
			if ( m_renderer != null )
			{
				m_renderer.enabled = false;
			}
			m_partiles.Stop();
		}

		private IEnumerator Fly_Coroutine( Vector3 startPos, Vector3 endPos )
		{
			float timer = 0;
			while ( timer < 1 )
			{
				timer += Time.deltaTime / m_flyDuration;

				Vector3 linearPos = Vector3.LerpUnclamped( startPos, endPos, timer );
				float heightOffset = m_flyCurve.Evaluate( timer ) * m_minHeight;

				Vector3 newPos = linearPos + Vector3.up * heightOffset;
				transform.position = newPos;
				yield return null;
			}
		}
	}
}