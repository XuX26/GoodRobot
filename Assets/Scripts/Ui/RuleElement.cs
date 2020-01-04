using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Liar.Ui
{
	public class RuleElement : MonoBehaviour
	{
		[Header( "References" )]
		[SerializeField] private TMP_Text m_nounLabel = default;
		[SerializeField] private TMP_Dropdown m_traitDropdown = default;
		[SerializeField] private TraitToggleButton m_traitToggleButton = default;

		private Rules.BaseRule m_rule = null;

		public void Init( Rules.BaseRule rule )
		{
			m_rule = rule;
			m_rule.OnRuleTraitChangedEvent += UpdateDropdownValue;

			// Listen for GUI events ...
			m_traitDropdown.onValueChanged.AddListener( OnTraitClicked );

			EventTrigger eventTrigger = m_traitDropdown.GetComponent<EventTrigger>();
			EventTrigger.Entry clickEntry = new EventTrigger.Entry();
			clickEntry.eventID = EventTriggerType.PointerClick;
			clickEntry.callback.AddListener( OnDropdownOpened );
			eventTrigger.triggers.Add( clickEntry );


			// Initialize all UI elements ...
			SetUiElements( rule );

			UpdateDropdownValue( rule );


			// Interactable?
			m_traitDropdown.interactable = rule.IsInteractable;
			m_traitToggleButton.interactable = rule.IsInteractable;
		}

		private void OnTraitClicked( int traitIdx )
		{
			m_rule.SetActiveTrait( traitIdx );

			Rules.RulesManager.Instance.ReturnTimeSpeed();
		}

		private void OnDropdownOpened( BaseEventData data )
		{
			Rules.RulesManager.Instance.SlomoTime();
		}

		private void UpdateDropdownValue( Rules.BaseRule rule )
		{
			// Set dropdown to initial rule's value ...
			m_traitDropdown.SetValueWithoutNotify( m_rule.ActiveRuleIndex );
		}

		private void SetUiElements( Rules.BaseRule rule )
		{
			// Label ...
			m_nounLabel.text = rule.RuleName;


			// Dropdown element ...
			m_traitDropdown.ClearOptions();
			foreach ( Rules.ITraitName traitName in rule.Traits )
			{
				m_traitDropdown.options.Add( new TMP_Dropdown.OptionData( traitName.TraitName ) );
			}


			// Button element ...
			m_traitToggleButton.Init( rule );
		}
	}
}