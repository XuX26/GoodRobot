﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Utility
{
	public class Singleton<T> where T : new()
	{
		public static bool Exists { get { return m_instance != null; } }
		public static T Instance
		{
			get
			{
				if ( m_instance == null )
				{
					m_instance = new T();
				}
				return m_instance;
			}
		}

		private static T m_instance = default;

		private Singleton()
		{
			// Intentionally blank ...
		}
	}
}