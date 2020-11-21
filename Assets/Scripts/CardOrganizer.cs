using UnityEngine;

public class CardOrganizer : MonoBehaviour
{
	public static void DisplayCards(Card originalCard, HealthBar healthBar, TextMesh timeLabel, int gridRows, int gridCols, float offsetX, float offsetY, int[] numbers, Sprite[] images)
    {
        // Position of the time label, the health bar and the first card
        Vector3 originalCardStartPos = originalCard.transform.localPosition;
        Vector3 healthBarStartPos = healthBar.transform.localPosition;
		Vector3 timeLabelStartPos = timeLabel.transform.localPosition;

		if (PlayerPrefs.GetString("LastGameModeSelected") == "Hard")
        {
            // Adjust the time label and health bar's positions
            timeLabel.transform.localPosition = new Vector3(timeLabelStartPos.x, timeLabelStartPos.y + 15f, timeLabelStartPos.z);
            healthBar.transform.localPosition = new Vector3(healthBarStartPos.x, healthBarStartPos.y + 15f, healthBarStartPos.z);

            // Scale the first card and adjust its position to make the spawned cards fit the screen
            originalCard.transform.localScale = new Vector3(60, 60, originalCard.transform.localScale.z);
            originalCard.transform.localPosition = new Vector3(originalCardStartPos.x + 50f, healthBarStartPos.y - 90f, healthBarStartPos.z);
        }

		originalCardStartPos = originalCard.transform.position;

		// Place the cards in the predefined grid
		for (int i = 0; i < gridCols; i++)
		{
			for (int j = 0; j < gridRows; j++)
			{

				Card card; // Container reference fro either the original card or the copies

				// Original card for the first grid space
				if (i == 0 && j == 0)
				{
					card = originalCard;
				}
				else
				{
					card = Instantiate(originalCard) as Card;
				}

				// Set the spawned cards as children of the main canvas
				card.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

				// Next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);

				float posX = (offsetX * i) + originalCardStartPos.x;
				float posY = -(offsetY * j) + originalCardStartPos.y;
				card.transform.position = new Vector3(posX, posY, originalCardStartPos.z);
			}
		}
	}	
}
