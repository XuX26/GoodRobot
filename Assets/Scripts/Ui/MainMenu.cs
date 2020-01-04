using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Liar.Ui
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private float m_defaultGravity = -7;
		[SerializeField] private Rules.GravityRule m_gravityRule = default;

		public void LoadNextLevel()
		{
			GameManager.Instance.LoadNextLevel();
		}

		private void Awake()
		{
			Physics2D.gravity = Vector3.up * m_defaultGravity;
			//m_gravityRule.ResetTrait();
		}
	}
}