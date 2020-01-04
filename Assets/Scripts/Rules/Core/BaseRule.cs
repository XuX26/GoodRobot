using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Rules
{
	/// <summary>
	/// Serializable version of <see cref="SpecificRule{T, N}"/>.
	/// </summary>
	public abstract class BaseRule : MonoBehaviour
	{
		public virtual bool IsInteractable { get { return m_interactable; } }
		public virtual bool IsHidden { get { return m_hidden; } }
		public virtual bool IsInverted { get { return m_invert; } }
		public virtual string RuleName { get { return m_ruleName; } }
		public virtual int ActiveRuleIndex { get { return m_activeTraitElement; } }
		public abstract IEnumerable Traits { get; }

		public event System.Action<BaseRule> OnRuleTraitChangedEvent;
		public event System.Action<BaseRule> OnRuleTraitToggledEvent;

		[SerializeField] protected bool m_interactable = true;
		[SerializeField] protected bool m_hidden = false;

		[Space]
		[SerializeField] protected bool m_invert = false;
		[SerializeField] protected string m_ruleName = "New Rule";
		[SerializeField] protected int m_activeTraitElement = default;

		protected int m_initialTraitElement = -1;
		protected bool m_initialInvertMode = false;
		
		public abstract void SetActiveTrait( int traitIdx );
		public abstract void ApplyRule();
		public abstract ITraitName GetActiveTraitName();

		public void Toggle()
		{
			m_invert = !m_invert;

			SetActiveTrait( m_activeTraitElement );

			OnRuleTraitToggledEvent?.Invoke( this );
		}

		/// <summary>
		/// Sets the <see cref="m_activeTraitElement"/> to our <see cref="m_initialTraitElement"/> and calls <see cref="SetActiveTrait(int)"/>.
		/// </summary>
		public virtual void ResetTrait()
		{
			m_invert = m_initialInvertMode;
			OnRuleTraitToggledEvent?.Invoke( this );

			SetActiveTrait( m_initialTraitElement );
		}
		
		protected void InvokeOnRuleTraitChangedEvent()
		{
			OnRuleTraitChangedEvent?.Invoke( this );
		}

		protected virtual void Awake()
		{
			m_initialTraitElement = m_activeTraitElement;
			m_initialInvertMode = m_invert;
		}
	}
}