using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Liar
{
	public class EnumFlagAttribute : PropertyAttribute
	{
	}


#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
	public class EnumFlagAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
			Enum targetEnum = (Enum)fieldInfo.GetValue( property.serializedObject.targetObject );
			

			EditorGUI.BeginProperty( position, label, property );
			Enum enumNew = EditorGUI.EnumFlagsField( position, label, targetEnum );
			property.intValue = (int)Convert.ChangeType( enumNew, targetEnum.GetType() );
			EditorGUI.EndProperty();
		}

		static T GetBaseProperty<T>( SerializedProperty prop )
		{
			// Separate the steps it takes to get to this property
			string[] separatedPaths = prop.propertyPath.Split( '.' );

			// Go down to the root of this serialized property
			System.Object reflectionTarget = prop.serializedObject.targetObject as object;
			// Walk down the path to get the target object
			foreach ( var path in separatedPaths )
			{
				System.Reflection.FieldInfo fieldInfo = reflectionTarget.GetType().GetField( path );
				reflectionTarget = fieldInfo.GetValue( reflectionTarget );
			}
			return (T)reflectionTarget;
		}
	}
#endif
}