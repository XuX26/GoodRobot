using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Liar.Ui
{
	public class TimeScaleButton : EventTrigger
	{
		[SerializeField] private float m_timeScale = 3;
		[SerializeField] private float m_transitionTimespan = 0.75f;

		private bool m_isScaling = false;

		public override void OnPointerDown( PointerEventData eventData )
		{
			base.OnPointerDown( eventData );

			if ( m_isScaling ) { return; }
			m_isScaling = true;

			Gameplay.TimeManager.Instance.SetTimeScale( m_timeScale, m_transitionTimespan ); 
		}

		public override void OnPointerUp( PointerEventData eventData )
		{
			base.OnPointerUp( eventData );

			m_isScaling = false;
			Gameplay.TimeManager.Instance.SetTimeScale( 1, m_transitionTimespan );
		}

		public override void OnDeselect( BaseEventData eventData )
		{
			base.OnDeselect( eventData );

			m_isScaling = false;
			Gameplay.TimeManager.Instance.SetTimeScale( 1, m_transitionTimespan );
		}
	}


#if UNITY_EDITOR
	[UnityEditor.CustomEditor( typeof( TimeScaleButton ) )]
	public class TimeScaleButtonEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}