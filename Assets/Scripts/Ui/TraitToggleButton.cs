using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Liar.Ui
{
	public class TraitToggleButton : Button
	{
		[Header( "Modifiers" )]
		[SerializeField] private bool m_invert = false;

		[Header( "Elements" )]
		[SerializeField] private TMP_Text m_labelElement = default;

		[Header( "References" )]
		[SerializeField] private Sprite m_redSprite = default;
		[SerializeField] private Sprite m_blueSprite = default;

		[Header( "SFX References" )]
		[SerializeField] private Utility.SfxClip m_trueSfxClick = default;
		[SerializeField] private Utility.SfxClip m_falseSfxClick = default;

		private Rules.BaseRule m_rule = null;
		private Image m_graphic = null;
		private GraphicColorPingPong m_graphicGlow = null;

		public void Init( Rules.BaseRule rule )
		{
			m_rule = rule;

			Rules.ITraitName traitName = rule.GetActiveTraitName();
			SetLabel( traitName.TraitName );
			ToggleSprite( rule.IsInverted );

			rule.OnRuleTraitToggledEvent += OnRuleToggled;
		}

		protected override void DoStateTransition( SelectionState state, bool instant )
		{
			base.DoStateTransition( state, instant );

			if ( state == SelectionState.Disabled )
			{
				m_graphicGlow?.StopPingPong();
			}
			else
			{
				m_graphicGlow?.StartPingPong();
			}
		}

		public void ToggleSprite()
		{
			ToggleSprite( !m_invert );
		}

		public override void OnPointerClick( PointerEventData eventData )
		{
			base.OnPointerClick( eventData );

			if ( interactable )
			{
				Utility.SfxClip sfxClip = m_invert
					? m_falseSfxClick
					: m_trueSfxClick;

				sfxClip?.PlaySfx();
			}

			if ( interactable && m_rule != null )
			{
				Rules.RulesManager.Instance.ToggleAllRules();
			}
		}

		private void OnRuleToggled( Rules.BaseRule rule )
		{
			ToggleSprite( rule.IsInverted );
		}

		private void SetLabel( string label )
		{
			m_labelElement.text = label;
		}

		private void ToggleSprite( bool invert )
		{
			m_invert = invert;

			m_graphic.sprite = invert 
				? m_redSprite
				: m_blueSprite;
		}

		protected override void Awake()
		{
			m_graphic = targetGraphic as Image;
			m_graphicGlow = m_graphic?.GetComponent<GraphicColorPingPong>();

			ToggleSprite( m_invert );
		}
	}


	#region Eidotr / Tools
#if UNITY_EDITOR
	/// <summary>
	/// Overriding Unity's base button editor.
	/// </summary>
	[UnityEditor.CustomEditor(typeof(TraitToggleButton))]
	public class TraitToggleButtonEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
	#endregion
}