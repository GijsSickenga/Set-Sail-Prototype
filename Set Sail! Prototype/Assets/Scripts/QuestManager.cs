using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

	private GameObject questTarget;
	private bool questActive = false;

	[SerializeField]
	private GameObject questParent;
	[SerializeField]
	private Text questHeadline;
	[SerializeField]
	private Text questSubtext;

	private string vesselHeadline = "Vessel Spotted!";
	private string vesselMessage = "Travel towards a vessel to the % of you.";
	
	// Update is called once per frame
	void Update () 
	{
		if(questActive)
		{
			Debug.Log("Quest is active");
			if(questTarget == null)
			{
				StopQuest();
				return;
			}

			// Got to replace the message in a local variable cus we no change template
			string theSubtext = vesselMessage;
			questSubtext.text = theSubtext.Replace("%", DirectionToTarget());
		}
	}

	public void StartQuest(GameObject newQuestTarget)
	{
		questActive = true;
		questTarget = newQuestTarget;

		questHeadline.text = vesselHeadline;
		questSubtext.text = vesselMessage + DirectionToTarget();
		questParent.SetActive(true);
	}

	public void StopQuest()
	{
		if(questActive)
		{
			questActive = false;
			questTarget = null;
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
