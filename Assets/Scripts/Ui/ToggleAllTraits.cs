using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Gameplay
{
	public class ToggleAllTraits : MonoBehaviour
	{
		public void Toggle()
		{
			Rules.RulesManager.Instance.ToggleAllRules();
		}
	}
}