using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(AudioDataSO))]
public class AudioDataSOEditor : Editor
{
    private bool showList = false;
    private bool showText = false;

    public override void OnInspectorGUI()
    {
        AudioDataSO audioData = (AudioDataSO)target;

        EditorGUILayout.LabelField("Unique ID", audioData.uniqueID);

        audioData.audioContentType = (AudioContentType)EditorGUILayout.EnumPopup("Audio Content Type", audioData.audioContentType);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Show List"))
        {
            showList = true;
            showText = false;
        }
        if (GUILayout.Button("Show Text"))
        {
            showList = false;
            showText = true;
        }
        if (GUILayout.Button("Hide All"))
        {
            showList = false;
            showText = false;
        }
        EditorGUILayout.EndHorizontal();

        if (showList)
        {
            List<AudioClipData> currentList = null;
            switch (audioData.audioContentType)
            {
                case AudioContentType.Dangerous:
                    currentList = audioData.dangerousAudioClips;
                    break;
                case AudioContentType.Friendly:
                    currentList = audioData.friendlyAudioClips;
                    break;
                case AudioContentType.Neutral:
                    currentList = audioData.neutralAudioClips;
                    break;
            }

            if (currentList != null)
            {
                EditorGUILayout.LabelField("Audio Clips");
                for (int i = 0; i < currentList.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    currentList[i].audioClip = (AudioClip)EditorGUILayout.ObjectField(currentList[i].audioClip, typeof(AudioClip), false);
                    currentList[i].volume = EditorGUILayout.Slider(currentList[i].volume, 0f, 1f);
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Audio Clip"))
                {
                    currentList.Add(new AudioClipData());
                }
            }
        }

        if (showText)
        {
            EditorGUILayout.LabelField("Long Text");
            EditorGUILayout.TextArea(audioData.longText);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(audioData);
        }
    }
}