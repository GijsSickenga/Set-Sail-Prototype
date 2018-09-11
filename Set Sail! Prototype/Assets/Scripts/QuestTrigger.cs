using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

	public float speedThreshold = 0.5f;
	public GameObject prefabQuestWindow;
	public Transform sceneCanvas;
	private bool _activated = false;

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player" && !_activated)
		{
			if( other.gameObject.transform.GetComponent<ShipControls>().sailVelocity < speedThreshold &&
				!other.gameObject.transform.GetComponent<QuestManager>().QuestActive)
			{
				_activated = true;
				GameObject questWindow = Instantiate(prefabQuestWindow);
				if(questWindow != null)
				{
					questWindow.SetActive(true);
					questWindow.transform.SetParent(sceneCanvas);

					// Change quest windows text
					questWindow.GetComponent<QuestWindow>().UpdateText(
						"Destroy Enemy Vessel", 
						"An enemy vessel has been spotted! Take it down before they reach this outpost."
					);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log("Ship exits QuestTrigger");
		_activated = false;
	}
}
