﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Liar.Ui
{
	[RequireComponent( typeof( CanvasGroup ) )]
	public class CanvasGroupWrapper : MonoBehaviour
	{
		private CanvasGroup m_canvasGroup = null;
		private Coroutine m_fadeRoutine = null;

		public void FadeOut( float timespan )
		{
			CrossFade( 0, timespan );
		}

		public void FadeIn( float timespan )
		{
			CrossFade( 1, timespan );
		}

		public void CrossFade( float targetAlpha, float timespan )
		{
			StopFade();
			m_fadeRoutine = StartCoroutine( CrossFade_Coroutine( targetAlpha, timespan ) );
		}

		public void StopFade()
		{
			if ( m_fadeRoutine != null )
			{
				StopCoroutine( m_fadeRoutine );
				m_fadeRoutine = null;
			}
		}

		private void Awake()
		{
			m_canvasGroup = GetComponent<CanvasGroup>();
		}

		private IEnumerator CrossFade_Coroutine( float targetAlpha, float timespan )
		{
			float timer = 0;
			if ( timespan <= 0 )
			{
				timer = 1;
			}

			float start = m_canvasGroup.alpha;

			while ( timer < 1 )
			{
				timer += Time.deltaTime / timespan;
				float newAlpha = Mathf.Lerp( start, targetAlpha, timer );

				m_canvasGroup.alpha = newAlpha;
				yield return null;
			}

			m_canvasGroup.alpha = targetAlpha;
			m_fadeRoutine = null;
		}
	}
}