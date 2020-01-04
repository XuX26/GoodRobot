using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Rules
{
	public interface ISpike : INounThing<Traits.BinaryTrait>
	{
	}

	public class SpikeRule : BinaryRule<ISpike>
	{
	}
}