using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Liar.Ui
{
	[RequireComponent( typeof( Graphic ) )]
	public class GraphicColorPingPong : MonoBehaviour
	{
		[SerializeField] private bool m_interactableGlow = true;
		[SerializeField, Tooltip( "Will not be scalable with the cycle duration field." )]
		private bool m_useGlobalTime = true;

		[Space]
		[SerializeField, Min( 0.001f )] private float m_pingPongCycleDuration = 1;
		[SerializeField] private Color m_startColor = Color.white;
		[SerializeField] private Color m_endColor = Color.black;

		private Graphic m_graphic = null;
		private float m_localTimer = 0;
		private bool m_pingPong = false;

		public void StartPingPong()
		{
			m_pingPong = true;
		}

		public void StopPingPong()
		{
			m_pingPong = false;
		}

		private void Update()
		{
			if ( !m_pingPong ) { return; }

			float duration = m_pingPongCycleDuration / 2f;
			m_localTimer += Time.unscaledDeltaTime / duration;

			float timer = m_useGlobalTime ? Time.time : m_localTimer;

			float lerpValue = Mathf.PingPong( timer, 1 );
			Color newColor = Color.Lerp( m_startColor, m_endColor, lerpValue );

			m_graphic.color = newColor;
		}

		private void Awake()
		{
			m_graphic = GetComponent<Graphic>();

			Selectable selectable = m_graphic.GetComponent<Selectable>();
			if ( selectable != null && m_interactableGlow )
			{
				StartPingPong();
			}
		}
	}
}