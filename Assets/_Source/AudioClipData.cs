using UnityEngine;

[System.Serializable]
public class AudioClipData
{
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
}