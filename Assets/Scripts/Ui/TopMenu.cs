using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Liar.Ui
{
	public class TopMenu : MonoBehaviour
	{
		[Header( "References" )]
		[SerializeField] private Button m_resetButton = default;
		[SerializeField] private Button m_pauseButton = default;

		[Space]
		[SerializeField] private PauseMenu m_pauseMenu = default;

		private bool m_isPaused = false;

		public void TogglePauseMenu()
		{
			m_isPaused = !m_isPaused;
			m_pauseMenu.Show( m_isPaused );
		}

		private void QuickReset()
		{
			GameManager.Instance.ResetPlayerAndRules();
		}

		private void Awake()
		{
			m_resetButton.onClick.AddListener( QuickReset );
			m_pauseButton.onClick.AddListener( TogglePauseMenu );
		}

		private void Start()
		{
			m_isPaused = false;
			m_pauseMenu.Show( false );
		}
	}
}