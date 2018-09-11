using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempParticle : MonoBehaviour
{
	private ParticleSystem _particleSystem;

	private void Start()
	{
		StartCoroutine(Die());
	}

	private IEnumerator Die()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
		yield break;
	}
}
