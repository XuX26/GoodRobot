using System.Collections;
using System.Collections.Generic;
using Liar.Rules.Traits;
using UnityEngine;
using UnityEngine.Serialization;

namespace Liar.Rules
{
	public class BinaryRule<N> : SpecificRule<BinaryTrait, N>
		where N : INounThing<BinaryTrait>
	{
		public override int ActiveRuleIndex { get { return m_trait.Value ? 1 : 0; } }
		public override IEnumerable Traits
		{
			get
			{
				m_dummyTrait.Value = ConvertIndex( 0 );
				yield return m_dummyTrait;

				m_dummyTrait.Value = ConvertIndex( 1 );
				yield return m_dummyTrait;
			}
		}

		[Header( "Binary Rule Modifiers" )]
		[Space]
		[SerializeField] private string m_negativeName = "Off";
		[SerializeField] private string m_positiveName = "On";

		private Traits.BinaryTrait m_trait = null;
		/// <summary>
		/// Used exclusively with <see cref="Traits"/> to save on memory.
		/// </summary>
		private Traits.BinaryTrait m_dummyTrait = new Traits.BinaryTrait();

		public override void SetActiveTrait( int traitIdx )
		{
			bool prevValue = m_trait.Value;
			m_trait.Value = ConvertIndex( traitIdx );

			// Override index since we know we only have one trait which toggles values ...
			int desiredTraitIdx = traitIdx;
			traitIdx = 0;

			base.SetActiveTrait( traitIdx );
			m_activeTraitElement = desiredTraitIdx;

			if ( prevValue != m_trait.Value )
			{
				InvokeOnRuleTraitChangedEvent();
			}
		}

		protected override BinaryTrait GetOpposite( BinaryTrait other )
		{
			BinaryTrait opposite = new BinaryTrait() { Value = !other.Value };
			opposite.SetOffName( m_negativeName );
			opposite.SetOnName( m_positiveName );

			return opposite;
		}

		private bool ConvertIndex( int index )
		{
			return (index & 1) >= 1;
		}

		protected override void Awake()
		{
			base.Awake();

			// Override our traits to ensure we only have one which toggles ...
			// ...

			m_trait = new Traits.BinaryTrait() { Value = ConvertIndex( m_activeTraitElement ) };
			m_trait.SetOffName( m_negativeName );
			m_trait.SetOnName( m_positiveName );

			m_traits = new Traits.BinaryTrait[1]
			{
				m_trait
			};


			m_dummyTrait.SetOffName( m_negativeName );
			m_dummyTrait.SetOnName( m_positiveName );
		}
	}
}