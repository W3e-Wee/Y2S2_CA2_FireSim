using UnityEngine;
using UnityEngine.Audio;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-09
// Description	: Template when adding sounds to Audio Manager;
//---------------------------------------------------------------------------------

[System.Serializable]
public class Sounds
{
	public string name;
    public AudioClip clip;

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

	public bool loop;
    public bool playOnAwake;

    [HideInInspector] public AudioSource source;
}


