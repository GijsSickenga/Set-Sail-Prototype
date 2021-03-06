﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

	private GameObject questTarget;
	private bool questActive = false;
	public bool QuestActive
	{
		get {return questActive;}
	}

	public enum QuestStates {Fight, Loot, Delivery, Inactive};
	public QuestStates questState = QuestStates.Inactive;

	[SerializeField]
	private GameObject questParent;
	[SerializeField]
	private Text questHeadline;
	[SerializeField]
	private Text questSubtext;

	private string vesselHeadline = "Vessel Spotted!";
	private string vesselMessage = "Travel towards a vessel to the % of you";

	private string crateHeadline = "Bring The Loot";
	private string crateMessage = "Pick up the crate and bring the loot back";
	
	// Update is called once per frame
	void Update () 
	{
		if(questActive)
		{
			if(questState == QuestStates.Fight && questTarget != null)
			{
				// Got to replace the message in a local variable cus we no change template
				string theSubtext = vesselMessage;
				questSubtext.text = theSubtext.Replace("%", DirectionToTarget());
			}
		}
	}

	public void StartQuest(GameObject newQuestTarget)
	{
		questActive = true;
		questTarget = newQuestTarget;
		questState = QuestStates.Fight;

		questHeadline.text = vesselHeadline;
		questSubtext.text = vesselMessage + DirectionToTarget();
		questParent.SetActive(true);
	}

	public void ProgressQuest()
	{
		if(questState == QuestStates.Fight)
		{
			questState = QuestStates.Loot;
			questHeadline.text = crateHeadline;
			questSubtext.text = crateMessage;
		}
		else if(questState == QuestStates.Loot)
		{
			questState = QuestStates.Delivery;
		}
	}

	public void StopQuest()
	{
		if(questActive)
		{
			questActive = false;
			questTarget = null;
			questState = QuestStates.Inactive;
			questParent.SetActive(false);
		}
	}

	private string DirectionToTarget()
	{
		string direction = "";

		// North/South
		if(questTarget.transform.position.z >= transform.position.z)
			direction += "north";
		else
			direction += "south";

		if(questTarget.transform.position.x >= transform.position.x)
			direction += "east";
		else
			direction += "west";

		return direction;
	}
}
