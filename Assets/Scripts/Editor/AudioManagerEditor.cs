using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CanEditMultipleObjects, CustomPropertyDrawer(typeof(ClipDrawer))]
public class AudioManagerEditor : PropertyDrawer
{ 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      
        AudioManager _am = UnityEngine.Object.FindObjectOfType<AudioManager>();

        for (int i = 0; i < _am.clips.Count; i++)
        {
            if(_am.clips[i].audioClip != null)
            _am.clips[i].clipName = _am.clips[i].audioClip.name;
        }
    }
}