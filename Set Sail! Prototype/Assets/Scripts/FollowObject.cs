using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
	[SerializeField]
	private GameObject target;

	private Vector3 offset;

	public float mouseSensitivityScalar = 3f;
	public float scrollSpeedScalar = 10f;
	public float scrollRecoveryTime = 1f;
	public float scrollSpeedDrag = 10f;
	public float distanceScrollSpeedMult = 10f;

	public float minimumDistance = 20f;
	public float maximumDistance = 500f;

	private float mouseWheelDisplacement = 0;

	void Start()
	{
		if(target == null)
		{
			Debug.LogError("FollowObject doesn't have an object to follow.");
		}
		else
		{
			// Look at target object.
            transform.LookAt(target.transform.position);

            // Cache starting offset from target object.
            offset = transform.position - target.transform.position;
		}

		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target == null)
			return;

		// Get horizontal mouse displacement since last frame.
        float mouseDisplacement = Input.GetAxis("Mouse X");

		// Rotate around target with amount scaled by mouse displacement.
        transform.RotateAround(target.transform.position, Vector3.up, mouseDisplacement * mouseSensitivityScalar);

        // Get mousewheel displacement since last frame.
        mouseWheelDisplacement -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeedScalar * (1 / (maximumDistance - minimumDistance) * offset.magnitude);

		// Scale offset distance with mousewheel displacement.
		offset = offset.normalized * Mathf.Clamp(offset.magnitude + mouseWheelDisplacement, minimumDistance, maximumDistance);

        // Smooth zoom back to 0.
        mouseWheelDisplacement = Mathf.Lerp(mouseWheelDisplacement, 0, scrollSpeedDrag / scrollRecoveryTime * Time.deltaTime);

		// Translate position based on target position.
		transform.position = target.transform.position - transform.forward * offset.magnitude;
    }
}
