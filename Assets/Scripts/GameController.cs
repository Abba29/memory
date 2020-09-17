using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool gameRunning; // Variable used to avoid being able to pause the game when finished (lose/won)
	
	private int gridRows, gridCols;
	private float offsetX, offsetY;

	private int[] numbers;

	[SerializeField] private MemoryCard originalCard; // Reference for the first card in the scene
	[SerializeField] private Sprite[] images; // Array for references to the sprite assets
	
	private GameObject[] cardsBack;

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int matches = 0;

	public TextMesh timeLabel;
	
	public HealthBar healthBar;
	private int maxHealth, currentHealth;

	public GameObject loseMenu, winMenu;

	void Start() {

		gameRunning = true;

		// Position of the time label, the health bar and the first card
		Vector3 timeLabelStartPos = timeLabel.transform.localPosition;
		Vector3 healthBarStartPos = healthBar.transform.localPosition;
		Vector3 cardStartPos = originalCard.transform.localPosition;

		if (PlayerPrefs.GetString("LastGameModeSelected") == "Easy")
		{
			EasyGameInitialization();
        } 
		else
        {
			HardGameInitialization();

			// Adjust the time label and health bar's positions
			timeLabel.transform.localPosition = new Vector3(timeLabelStartPos.x, timeLabelStartPos.y + 15f, timeLabelStartPos.z);
			healthBar.transform.localPosition = new Vector3(healthBarStartPos.x, healthBarStartPos.y + 15f, healthBarStartPos.z);

			// Scale the first card and adjust its position to make the spawned cards fit the screen
			originalCard.transform.localScale = new Vector3(60, 60, originalCard.transform.localScale.z);
			originalCard.transform.localPosition = new Vector3(cardStartPos.x + 50f, healthBarStartPos.y - 90f, healthBarStartPos.z);
		}

		// Set the health to its maximum
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		// Updated position of the first card
		cardStartPos = originalCard.transform.position;

		// Place the cards in the predefined grid
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				
				MemoryCard card; // Container reference fro either the original card or the copies

				// Original card for the first grid space
				if (i == 0 && j == 0) {
					card = originalCard;
				} 
				else 
				{
					card = Instantiate(originalCard) as MemoryCard;
				}

				// Set the spawned cards as children of the main canvas
				card.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

				// Next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);

				float posX = (offsetX * i) + cardStartPos.x;
				float posY = -(offsetY * j) + cardStartPos.y;
				card.transform.position = new Vector3(posX, posY, cardStartPos.z);
			}
		}

		// Array for references to the cards' backs
		cardsBack = GameObject.FindGameObjectsWithTag("CardBack");

		// Show all cards by disabling all the cards' backs
		foreach (GameObject c in cardsBack)
		{
			c.SetActive(false);
		}

		// Cover all cards after 2s and starts the timer
		Invoke("coverAllCards", 2.0f);
	}


	/*
	 * GAME LOGIC FUNCTIONS
	 */

	// Getter function that returns false if there's already a second card revealed
	public bool canReveal
	{
		get { return _secondRevealed == null; }
	}
	
	// Store card objects in one of the two card variables dependending on if the first variable is already occupied
	public void CardRevealed(MemoryCard card) {
		if (_firstRevealed == null) {
			_firstRevealed = card;
		} 
		else 
		{
			_secondRevealed = card;
			
			// Call the coroutine when both cards are revealed
			StartCoroutine(CheckMatch());
		}
	}
	
	private IEnumerator CheckMatch() {

		// Compare the IDs of the two reveal card, incrementing 'matches' if they match
		if (_firstRevealed.id == _secondRevealed.id) {

			yield return new WaitForSeconds(0.5f);

			// Matching cards disappear after 0.5s
			_firstRevealed.gameObject.SetActive(false);
			_secondRevealed.gameObject.SetActive(false);

			matches++;
		}
		else 
		{
			
			yield return new WaitForSeconds(1.0f);

			// Unreveal the cards if they do not match after 1s
			_firstRevealed.Unreveal();
			_secondRevealed.Unreveal();

			// Decrease health because of the error
			currentHealth -= 1;
			healthBar.SetHealth(currentHealth);

			if (currentHealth == 0)
			{
				Timer.instance.EndTimer();

				gameRunning = false;
				loseMenu.SetActive(true);

				PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
				
				//Debug.Log("GamesPlayed: " + PlayerPrefs.GetInt("GamesPlayed"));
			}
		}
		
		// Clear out the variables whether or not a match was made
		_firstRevealed = null;
		_secondRevealed = null;

		CheckWin(matches);
	}

	public void CheckWin(int matches)
	{
		if (matches == 4 && PlayerPrefs.GetString("LastGameModeSelected") == "Easy")
		{
			Timer.instance.EndTimer();

			Timer.instance.CheckBestTime();

			gameRunning = false;
			winMenu.SetActive(true);

			PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
			PlayerPrefs.SetInt("EasyGamesWon", PlayerPrefs.GetInt("EasyGamesWon") + 1);

			//Debug.Log("GamesPlayed: " + PlayerPrefs.GetInt("GamesPlayed"));
			//Debug.Log("EasyGamesWon: " + PlayerPrefs.GetInt("EasyGamesWon"));
		}
		else if (matches == 8)
		{
			Timer.instance.EndTimer();

			Timer.instance.CheckBestTime();

			gameRunning = false;
			winMenu.SetActive(true);

			PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
			PlayerPrefs.SetInt("HardGamesWon", PlayerPrefs.GetInt("HardGamesWon") + 1);

			//Debug.Log("GamesPlayed: " + PlayerPrefs.GetInt("GamesPlayed"));
			//Debug.Log("HardGamesWon: " + PlayerPrefs.GetInt("HardGamesWon"));
		}
	}


	/*
	 * GAME INIZIALIZATION FUNCTIONS
	 */

	public void EasyGameInitialization()
    {
		maxHealth = 3;

		// Values for how many grid spaces and how far apart to place them
		gridRows = 2;
		gridCols = 4;
		offsetX = 1.5f;
		offsetY = 1.8f;

		// Create shuffled list of cards
		numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3 };
		numbers = ShuffleArray(numbers);
	}

	public void HardGameInitialization()
	{
		maxHealth = 5;

		// Values for how many grid spaces and how far apart to place them
		gridRows = 4;
		gridCols = 4;
		offsetX = 1.2f;
		offsetY = 1.2f;

		// Create shuffled list of cards
		numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
		numbers = ShuffleArray(numbers);
	}

	// Function created just to be called by the 'Invoke' method 
	public void coverAllCards()
	{
		foreach (GameObject c in cardsBack)
		{
			c.SetActive(true);
			
			// Starts the timer
			Timer.instance.BeginTimer();
		}
	}


	/*
	 * UTILITY FUNCTION
	 */

	// An implementation of the Knuth shuffle algorithm (also known as Fisher-Yates)
	private int[] ShuffleArray(int[] numbers)
	{
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++)
		{
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}
}
