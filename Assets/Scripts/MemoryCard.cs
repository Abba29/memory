using UnityEngine;

public class MemoryCard : MonoBehaviour {
	
	[SerializeField] private GameObject cardBack;
	[SerializeField] private GameManager controller;

	private int _id;

	// Getter function for the object's _id
	public int id {
		get {return _id;}
	}

	// Function used by other scripts to pass new sprites to this object
	public void SetCard(int id, Sprite image) {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}

	// Function called when the object is clicked
	public void OnMouseDown()
	{
		// Check the GameController's canReveal property to make sure only two cards are revealed at the same time
		if (cardBack.activeSelf && controller.canReveal)
		{
			cardBack.SetActive(false);
			controller.CardRevealed(this); // Notify the controller when the card is revealed
		}
	}

	// Public method so that GameController can hide the card again
	public void Unreveal() {
		cardBack.SetActive(true);
	}
}
