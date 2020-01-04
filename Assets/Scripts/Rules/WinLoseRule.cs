using System.Collections;
using System.Collections.Generic;
using Liar.Rules.Traits;
using UnityEngine;

namespace Liar.Rules
{
	public interface IWinLose : INounThing<BinaryTrait>
	{
	}

	public class WinLoseRule : BinaryRule<IWinLose>
	{
	}
}