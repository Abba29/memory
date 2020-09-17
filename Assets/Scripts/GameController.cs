using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool gameRunning;
	
	private int gridRows, gridCols;
	private float offsetX, offsetY;

	private int[] numbers;

	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	
	private GameObject[] cardsBack;

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int matches = 0;

	public TextMesh timeLabel;
	
	public HealthBar healthBar;
	private int maxHealth, currentHealth;

	public GameObject loseMenu, winMenu;

	// Use this for initialization
	void Start() {

		gameRunning = true;

		Vector3 timeLabelStartPos = timeLabel.transform.localPosition;
		Vector3 healthBarStartPos = healthBar.transform.localPosition;
		Vector3 cardStartPos = originalCard.transform.localPosition;

		if (PlayerPrefs.GetString("LastGameModeSelected") == "Easy")
		{
			EasyGameInitialization();
        } else
        {
			HardGameInitialization();

			// Adjust the position of the first card to make the spawned cards fit in the screen size
			timeLabel.transform.localPosition = new Vector3(timeLabelStartPos.x, timeLabelStartPos.y + 15f, timeLabelStartPos.z);
			healthBar.transform.localPosition = new Vector3(healthBarStartPos.x, healthBarStartPos.y + 15f, healthBarStartPos.z);

			originalCard.transform.localScale = new Vector3(60, 60, originalCard.transform.localScale.z);
			originalCard.transform.localPosition = new Vector3(cardStartPos.x + 50f, healthBarStartPos.y - 90f, healthBarStartPos.z);
		}

		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		cardStartPos = originalCard.transform.position;

		// place cards in a grid
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				MemoryCard card;

				// use the original for the first grid space
				if (i == 0 && j == 0) {
					card = originalCard;
				} else {
					card = Instantiate(originalCard) as MemoryCard;
				}

				card.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

				// next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);

				float posX = (offsetX * i) + cardStartPos.x;
				float posY = -(offsetY * j) + cardStartPos.y;
				card.transform.position = new Vector3(posX, posY, cardStartPos.z);
			}
		}

		cardsBack = GameObject.FindGameObjectsWithTag("CardBack");

		foreach (GameObject c in cardsBack)
		{
			c.SetActive(false);
		}

		// Cover all cards after 2s and starts the timer
		Invoke("coverAllCards", 2.0f);
	}

	// Knuth shuffle algorithm
	private int[] ShuffleArray(int[] numbers) {
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++ ) {
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card) {
		if (_firstRevealed == null) {
			_firstRevealed = card;
		} else {
			_secondRevealed = card;
			StartCoroutine(CheckMatch());
		}
	}
	
	private IEnumerator CheckMatch() {

		// increment matches if the cards match
		if (_firstRevealed.id == _secondRevealed.id) {

			yield return new WaitForSeconds(0.5f);

			_firstRevealed.gameObject.SetActive(false);
			_secondRevealed.gameObject.SetActive(false);

			matches++;
		}
		// otherwise turn them back over after 1s pause
		else {
			yield return new WaitForSeconds(1.0f);

			_firstRevealed.Unreveal();
			_secondRevealed.Unreveal();

			currentHealth -= 1;
			healthBar.SetHealth(currentHealth);

			if (currentHealth == 0)
			{
				Timer.instance.EndTimer();

				gameRunning = false;
				loseMenu.SetActive(true);

				PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
				Debug.Log("GamesPlayed: " + PlayerPrefs.GetInt("GamesPlayed"));
			}
		}
		
		_firstRevealed = null;
		_secondRevealed = null;

		CheckWin(matches);
	}

	public bool canReveal
	{
		get { return _secondRevealed == null; }
	}

	public void EasyGameInitialization()
    {
		maxHealth = 3;

		gridRows = 2;
		gridCols = 4;
		offsetX = 1.5f;
		offsetY = 1.8f;

		// create shuffled list of cards
		numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3 };
		numbers = ShuffleArray(numbers);
	}

	public void HardGameInitialization()
	{
		maxHealth = 5;

		gridRows = 4;
		gridCols = 4;
		offsetX = 1.2f;
		offsetY = 1.2f;

		// create shuffled list of cards
		numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
		numbers = ShuffleArray(numbers);
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

	public void coverAllCards()
	{
		foreach (GameObject c in cardsBack)
		{
			c.SetActive(true);
			Timer.instance.BeginTimer();
		}
	}
}
