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
			if(
				!other.gameObject.GetComponent<QuestManager>().QuestActive &&
				GameObject.FindObjectOfType<QuestWindow>() == null
			)
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
			else if(other.gameObject.GetComponent<QuestManager>().questState == QuestManager.QuestStates.Delivery)
			{
				other.gameObject.GetComponent<QuestManager>().StopQuest();
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log("Ship exits QuestTrigger");
		_activated = false;
	}
}
