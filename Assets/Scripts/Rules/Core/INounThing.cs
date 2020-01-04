using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Rules
{
	public interface INounThing<Trait>
	{
		void Apply( Trait newTrait );
		
		void AddNounTag();
		void RemoveNounTag();
	}
}