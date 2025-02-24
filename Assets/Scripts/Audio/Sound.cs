using UnityEngine.Audio;
using UnityEngine;
using System.Globalization;

[System.Serializable] // allows this custom class to appear in the inspector
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] // add sliders to the volume
    public float volume;
    [Range(0.1f, 3f)] // add sliders to the pitch
    public float pitch;

    public bool loop; // for theme

    [HideInInspector]
    public AudioSource source;
}
