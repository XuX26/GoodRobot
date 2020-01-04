using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Utility
{
	public class LevelInitializer : SingletonMono<LevelInitializer>
	{
		public static bool IsInitialized { get { return Instance != null ? Instance.m_initialized : true; } }
		public static float InitTimespan { get { return Instance != null ? Instance.m_initializeDelay : 0; } }

		[Header( "References" )]
		[SerializeField] private GameManager m_gameManagerprefab = default;

		[Header( "Modifiers" )]
		[SerializeField] private float m_fadeInDuration = 1.5f;
		[SerializeField] private float m_initializeDelay = 2f;

		private bool m_initialized = false;

		protected override void Awake()
		{
			base.Awake();


			if ( !GameManager.Exists )
			{
				Instantiate( m_gameManagerprefab );
			}


			StartCoroutine( Initialize_Coroutine() );
		}

		private IEnumerator Initialize_Coroutine()
		{
			FadeTransition.Instance.FadeIn( m_fadeInDuration );

			if ( m_initializeDelay > 0 ) { yield return new WaitForSeconds( m_initializeDelay ); }

			m_initialized = true;
		}
	}
}