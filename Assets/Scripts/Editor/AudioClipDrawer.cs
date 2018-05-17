using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CanEditMultipleObjects, CustomPropertyDrawer(typeof(EnumAttribute))]
public class AudioClipDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumAttribute range = attribute as EnumAttribute;

        AudioManager _am = UnityEngine.Object.FindObjectOfType<AudioManager>();
        List<String> objectTypes = new List<String>();
        // create a list
        // get the pool managers enum

        objectTypes.Add("None");
      
        for (int i = 0; i < _am.clips.Count; i++)
        {
            objectTypes.Add(_am.clips[i].clipName);
        }
        // set the list 

        var sel = objectTypes.IndexOf(property.stringValue);

        int newSelect = EditorGUI.Popup(position, sel < 0 ? sel = 0 : sel, objectTypes.ToArray());
        property.stringValue = objectTypes[newSelect];
        
    }
}

