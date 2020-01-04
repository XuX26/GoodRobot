using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Utility
{
	public class SfxClip : MonoBehaviour
	{
		[SerializeField] private AudioClip m_clip = default;
		[SerializeField] private bool m_randomizePicth = true;

		public void PlaySfx()
		{
			AudioManager.Instance.PlaySfx( m_clip, m_randomizePicth );
		}
	}
}