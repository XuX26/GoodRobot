using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Rules
{
	public interface ITraitName
	{
		string TraitName { get; }
	}
}

namespace Liar.Rules.Traits
{
	[System.Serializable]
	public class DirectionTrait : ITraitName
	{
		public string TraitName { get { return DisplayName; } }

		public string DisplayName = "Direction";
		public EDirection Value = EDirection.None;
	}
	

	public class BinaryTrait : ITraitName
	{
		public string TraitName { get { return Value ? m_onName : m_offName; } }

		public bool Value = true;

		private string m_onName = "On?";
		private string m_offName = "Off?";

		public void SetOnName( string on )
		{
			m_onName = on;
		}

		public void SetOffName( string off )
		{
			m_offName = off;
		}
	}


	[System.Serializable]
	public class ColliderState : ITraitName
	{
		public string TraitName { get { return DisplayName; } }

		public string DisplayName = "State";
		public EColliderState State = EColliderState.Invalid;
	}
}