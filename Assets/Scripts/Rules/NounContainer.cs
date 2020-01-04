using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Liar
{
	public class NounContainer : Utility.SingletonMono<NounContainer>
	{
		private Dictionary<System.Type, List<object>> m_nouns = new Dictionary<System.Type, List<object>>();
		
		public static IEnumerable<N> GetAllNounsOfType<N, T>() where N : Rules.INounThing<T>
		{
			if ( Instance == null ) { return null; }


			System.Type type = typeof( N );

			List<object> nouns = null;
			if ( !Instance.m_nouns.TryGetValue( type, out nouns ) )
			{
				Debug.LogError( $"No nouns of type [<b>{type.Name}</b>] found in scene!\n" +
					$"Be sure to call 'AddNounTag'/'RemoveNounTag' in 'Awake'/'OnDestroy' respectively!", Instance );

				// Return an empty list to avoid null errors ...
				return new List<N>();
			}

			return nouns.Cast<N>();
		}
		
		public static void AddNounThing<N>( N newThing ) 
		{
			if ( Instance == null ) { return; }


			System.Type type = typeof( N );

			List<object> nouns = null;
			if ( Instance.m_nouns.TryGetValue( type, out nouns ) )
			{
				nouns.Add( newThing );
			}
			else
			{
				Instance.m_nouns.Add( type, new List<object>() { newThing } );
			}
		}

		public static void RemoveNounThing<N>( N oldThing )
		{
			if ( Instance == null ) { return; }


			System.Type type = typeof( N );

			List<object> nouns = null;
			if ( Instance.m_nouns.TryGetValue( type, out nouns ) )
			{
				nouns.Remove( oldThing );
			}
		}
	}
}