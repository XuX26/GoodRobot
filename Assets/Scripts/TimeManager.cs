using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Gameplay
{
	public class TimeManager : Utility.SingletonMono<TimeManager>
	{
		private float m_fixedTimeStep = -1;
		private Coroutine m_timeScaleRoutine = null;

		public void SetTimeScale( float scale, float transitionTime )
		{
			if ( m_timeScaleRoutine != null )
			{
				StopCoroutine( m_timeScaleRoutine );
				m_timeScaleRoutine = null;
			}

			m_timeScaleRoutine = StartCoroutine( SetScaleOverTime_Coroutine( scale, transitionTime ) );
		}

		private void SetTimeScale_Internal( float newScale )
		{
			Time.timeScale = newScale;
			Time.fixedDeltaTime = m_fixedTimeStep * newScale;
		}

		protected override void Awake()
		{
			base.Awake();

			m_fixedTimeStep = Time.fixedDeltaTime;
		}

		private IEnumerator SetScaleOverTime_Coroutine( float newScale, float transitionTime )
		{
			float timer = 0;
			if ( transitionTime <= 0 )
			{
				// Fake the completion of the coroutine since we want to apply the scale immediately ...
				timer = 1;
			}

			float startScale = Time.timeScale;
			float endScale = newScale;

			while ( timer < 1 )
			{
				timer += Time.unscaledDeltaTime / transitionTime;
				float scale = Mathf.Lerp( startScale, endScale, timer );

				SetTimeScale_Internal( scale );
				yield return null;
			}

			SetTimeScale_Internal( endScale );
			m_timeScaleRoutine = null;
		}
	}
}