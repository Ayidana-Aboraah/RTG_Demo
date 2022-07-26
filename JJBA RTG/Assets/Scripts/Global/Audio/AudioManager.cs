using System;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(StandDictionary))]
public sealed class AudioManager : MonoBehaviour
{
	public string bgMusic;
	public Sound[] sounds;

	public static AudioManager manager;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject); //This also allows other manager scripts to not have to be deleted

		if(manager == null) manager = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	private void Start()
	{
		Play(bgMusic);
	}

	public void Play(string name)
	{
		if (name == "") return;
		
		Sound s = Array.Find(sounds, sound => sound.name == name);
		
		if (s == null){
			Debug.LogWarning(name + " not found, try checking spelling in audio");
			return;
		}

		s.source.Play();
	}
}
