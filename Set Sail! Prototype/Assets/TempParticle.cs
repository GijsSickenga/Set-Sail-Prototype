using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempParticle : MonoBehaviour
{
	private ParticleSystem _particleSystem;
	public float deathDelay = 5f;

	private void Start()
	{
		StartCoroutine(Die());
	}

	private IEnumerator Die()
	{
		yield return new WaitForSeconds(deathDelay);
		Destroy(gameObject);
		yield break;
	}
}
