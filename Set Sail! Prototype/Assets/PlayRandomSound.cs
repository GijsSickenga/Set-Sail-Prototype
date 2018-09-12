using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
	private void Start()
	{
		List<AudioSource> sounds = GetComponents<AudioSource>().ToList();
		sounds[Random.Range(0, sounds.Count)].Play();
	}
}
