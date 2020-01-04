using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Liar.Ui
{
	[RequireComponent( typeof( TMP_Text ) )]
	public class LevelNameElement : MonoBehaviour
	{
		private TMP_Text m_nameElement = null;

		private void Start()
		{
			Scene activeScene = SceneManager.GetActiveScene();

			m_nameElement.text = $"Level {activeScene.buildIndex}";
		}

		private void Awake()
		{
			m_nameElement = GetComponent<TMP_Text>();
		}
	}
}