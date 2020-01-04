using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Rules
{
	public class RulesManager : Utility.SingletonMono<RulesManager>
	{
		public int RuleCount
		{
			get
			{
				int ruleCount = 0;
				foreach ( BaseRule rule in m_rules )
				{
					if ( rule.IsHidden ) { continue; }
					++ruleCount;
				}

				return ruleCount;
			}
		}
		public IEnumerable BaseRules
		{
			get
			{
				foreach ( BaseRule rule in m_rules )
				{
					if ( rule.IsHidden ) { continue; }
					yield return rule;
				}
			}
		}

		public event System.Action<RulesManager> OnRulesInitializationComplete = default;

		[SerializeField] private float m_slomoSpeed = 0.15f;
		[SerializeField] private float m_slomoTransitionTime = 0.65f;

		private BaseRule[] m_rules = default;

		public void ResetAllRules()
		{
			// Reset ALL traits even for non-interactable/hidden rules ...
			foreach ( BaseRule rule in m_rules )
			{
				rule.ResetTrait();
			}
		}

		public void ToggleAllRules()
		{
			// ONLY toggle traits for interactable/non-hidden rules ...
			foreach ( BaseRule rule in BaseRules )
			{
				if ( rule.IsInteractable )
				{
					rule.Toggle();
				}
			}
		}

		public void SlomoTime()
		{
			Gameplay.TimeManager.Instance.SetTimeScale( m_slomoSpeed, m_slomoTransitionTime );
		}

		public void ReturnTimeSpeed()
		{
			Gameplay.TimeManager.Instance.SetTimeScale( 1, m_slomoTransitionTime );
		}

		protected override void Awake()
		{
			base.Awake();

			m_rules = GetComponentsInChildren<BaseRule>( false );
		}

		private void Start()
		{
			// Initialize all rules ...
			ResetAllRules();

			OnRulesInitializationComplete?.Invoke( this );
		}
	}
}