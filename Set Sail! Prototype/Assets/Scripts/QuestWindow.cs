using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour {

	[SerializeField]
	private Text title;
	[SerializeField]
	private Text description;
	[SerializeField]
	private Text confirmText;

	[SerializeField]
	private GameObject enemyShipPrefab;
	public float fieldWidth;
	public float fieldHeight;

	private GameObject player = null;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		if(player == null)
		{
			Debug.LogError("QuestWindow couldn't find player with tag 'Player'. Destroying self.");
			DestroyImmediate(this.gameObject);
		}
	}

	void Start()
	{
		transform.SetParent(FindObjectOfType<Canvas>().transform, false);
		GetComponent<RectTransform>().localPosition = Vector3.zero;
	}

	void Update()
	{
		if(player != null)
		{
			if(Input.GetKeyDown(player.GetComponent<ShipControls>().confirmButton))
			{
				// Spawn a random enemy ship
				player.GetComponent<QuestManager>().StartQuest(
					Instantiate(
						enemyShipPrefab, 
						new Vector3(Random.Range(fieldWidth * -0.5f, fieldWidth * 0.5f), 0, Random.Range(fieldHeight * -0.5f, fieldHeight * 0.5f)),
						new Quaternion()
						)
				);

				DestroyImmediate(this.gameObject);
			}
		}
	}

	public void UpdateText(string title, string description)
	{
		transform.position = Vector3.zero;

		this.title.text = "QUEST: " + title;
		this.description.text = description;

		if(player != null)
		{
			this.confirmText.text = "Press " + player.GetComponent<ShipControls>().confirmButton.ToString() + " to confirm";
		}
	}
}
