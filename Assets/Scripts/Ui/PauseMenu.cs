using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Liar.Ui
{
	public class PauseMenu : MonoBehaviour
	{
		[SerializeField] private TopMenu m_menuController = default;

		[Space]
		[SerializeField] private Button m_resumeButton = default;
		[SerializeField] private Button m_quitButton = default;
		
		public void Show( bool show )
		{
			gameObject.SetActive( show );

			//float timeScale = show
			//	? 0
			//	: 1;

			//Gameplay.TimeManager.Instance.SetTimeScale( timeScale, 0 );
		}

		private void OnEnable()
		{
			Gameplay.TimeManager.Instance.SetTimeScale( 0, 0 );
		}

		private void OnDisable()
		{
			Gameplay.TimeManager.Instance.SetTimeScale( 1, 0 );
		}

		private void Resume()
		{
			m_menuController.TogglePauseMenu();
		}

		private void Quit()
		{
			GameManager.Instance.LoadMainMenu();
		}

		private void Awake()
		{
			m_resumeButton.onClick.AddListener( Resume );
			m_quitButton.onClick.AddListener( Quit );
		}
	}
}