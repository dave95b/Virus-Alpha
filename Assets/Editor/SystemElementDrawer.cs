using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(SystemElement))]
//public class SystemElementDrawer : PropertyDrawer {

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);
        
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;
        
//        var hackRect = new Rect(position.x, position.y, 30, position.height);
//        var destroyRect = new Rect(position.x + 35, position.y, 50, position.height);
//        var conquerRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

//        EditorGUI.PropertyField(hackRect, property.FindPropertyRelative("HackCost"), GUIContent.none);
//        EditorGUI.PropertyField(destroyRect, property.FindPropertyRelative("DestroyCost"), GUIContent.none);
//        EditorGUI.PropertyField(conquerRect, property.FindPropertyRelative("ConquerCost"), GUIContent.none);

//        // Set indent back to what it was
//        EditorGUI.indentLevel = indent;

//        EditorGUI.EndProperty();
//    }
//}
