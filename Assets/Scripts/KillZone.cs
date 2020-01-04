using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Gameplay
{
	public class KillZone : MonoBehaviour
	{
		private void OnTriggerEnter2D( Collider2D collision )
		{
			PlayerController player = collision.attachedRigidbody?.GetComponent<PlayerController>();
			player?.Lose();
		}
	}
}