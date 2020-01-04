using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Rules
{
	public abstract class SpecificRule<T, N> : BaseRule 
		where T : ITraitName
		where N : INounThing<T>
	{
		public override IEnumerable Traits { get { return m_traits; } }

		[Space]
		[SerializeField] protected T[] m_traits = default;

		protected T m_activeTrait = default;
		
		public override void SetActiveTrait( int traitIdx )
		{
			m_activeTraitElement = traitIdx;

			T newTrait = m_traits[traitIdx];
			SetActiveTrait_Internal( newTrait );
		}

		public override ITraitName GetActiveTraitName()
		{
			return m_activeTrait;
		}

		/// <summary>
		/// Child classes should override <see cref="ApplyRule_Internal(T)"/> instead.
		/// </summary>
		public override void ApplyRule()
		{
			T appliedTrait = m_invert
				? GetOpposite( m_activeTrait )
				: m_activeTrait;

			ApplyRule_Internal( appliedTrait );
		}

		protected abstract T GetOpposite( T other );

		private void SetActiveTrait_Internal( T newTrait )
		{
			T prevTrait = m_activeTrait;
			m_activeTrait = newTrait;

			ApplyRule();

			if ( prevTrait == null || !prevTrait.Equals( m_activeTrait ) )
			{
				InvokeOnRuleTraitChangedEvent();
			}
		}

		protected virtual void ApplyRule_Internal( T newTrait )
		{
			IEnumerable nounThings = NounContainer.GetAllNounsOfType<N, T>();
			foreach ( N thing in nounThings )
			{
				thing.Apply( newTrait );
			}
		}
	}
}