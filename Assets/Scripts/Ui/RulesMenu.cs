using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Liar.Ui
{
	public class RulesMenu : MonoBehaviour
	{
		[SerializeField] private bool m_staggerRuleSpawning = true;
		[SerializeField] private float m_spawnDelayOffset = 0.25f;

		[Header( "References" )]
		[SerializeField] private Image m_rulesDisableOverlay = default;
		[SerializeField] private RuleElement m_ruleElementPrefab = default;
		[SerializeField] private RectTransform m_ruleContainer = default;

		private RuleElement CreateRuleElement( Rules.BaseRule newRule )
		{
			RuleElement result = Instantiate( m_ruleElementPrefab, m_ruleContainer );
			result.gameObject.SetActive( true );
			return result;
		}

		private void EnableRuleInteraction( bool isEnabled )
		{
			m_rulesDisableOverlay.raycastTarget = !isEnabled;
		}

		private IEnumerator Start()
		{
			if ( m_staggerRuleSpawning )
			{
				EnableRuleInteraction( false );
			}

			// Wait a frame to avoid creation of rule manager singleton while it's destroying from previous scene ...
			yield return null;

			float ruleSpawnDelay = Utility.LevelInitializer.InitTimespan / Rules.RulesManager.Instance.RuleCount;

			if ( m_staggerRuleSpawning )
			{
				yield return new WaitForSeconds( ruleSpawnDelay );
			}

			int numRules = Rules.RulesManager.Instance.RuleCount;
			foreach ( Rules.BaseRule rule in Rules.RulesManager.Instance.BaseRules )
			{
				--numRules;

				RuleElement newElement = CreateRuleElement( rule );
				newElement.Init( rule );

				if ( m_staggerRuleSpawning && numRules > 0 )
				{
					yield return new WaitForSeconds( ruleSpawnDelay - m_spawnDelayOffset );
				}
			}
			
			EnableRuleInteraction( true );
		}
	}
}