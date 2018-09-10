using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
	
	[SerializeField]
	private GameObject toFollow;

	private Vector3 origPosition;

	void Start()
	{
		origPosition = this.transform.position;
		if(toFollow == null)
			Debug.LogError("FollowObject doesn't have an object to follow.");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(toFollow == null)
			return;

		Vector3 toFollowPosition = toFollow.transform.position;
		toFollowPosition.x += origPosition.x;
		toFollowPosition.y += origPosition.y;
		toFollowPosition.z += origPosition.z;
		this.transform.position = toFollowPosition;
	}
}
