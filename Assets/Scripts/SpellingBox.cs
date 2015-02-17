using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SpellingBox : MonoBehaviour
{

	public GameObject lives;
	public GameObject correct;
	public float correctSpellingChance = 0.5f;
	public float downwardMovementSpeed = 1f;
	public string answer;
	public string inputAnswer;
	public Word word;

	private float timer = 0f;


	void Start()
	{
		timer = 0f;
		answer = word.correctSpelling;

		lives = GameObject.FindGameObjectWithTag("Lives");
		correct = GameObject.FindGameObjectWithTag("Correct");

		if (Random.Range(0f, 1f) < correctSpellingChance)
		{
			this.transform.FindChild("Placeholder").GetComponent<Text>().text = word.correctSpelling;
		}
		else
		{
			this.transform.FindChild("Placeholder").GetComponent<Text>().text = word.incorrectSpelling;
		}
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (transform.position.y < -15)
		{
			if (transform.Find("InputText").GetComponent<Text>().text == "")
			{
				inputAnswer = transform.Find("Placeholder").GetComponent<Text>().text;
			}
			else
			{
				inputAnswer = transform.Find("InputText").GetComponent<Text>().text;
			}

			if (inputAnswer == answer)
			{
				GameObject.Find("GameController").GetComponent<GameController>().correct += 1;
				this.correct.GetComponent<Text>().text = "Correct: " + GameObject.Find("GameController").GetComponent<GameController>().correct;
			}
			else
			{
				GameObject.Find("GameController").GetComponent<GameController>().lives -= 1;
				this.lives.GetComponent<Text>().text = "Lives: " + GameObject.Find("GameController").GetComponent<GameController>().lives;
				GameObject.Find("GameController").GetComponent<GameController>().missedWords.Add(new Word(word.correctSpelling, inputAnswer));
			}

			Destroy(this.gameObject);
		}

		// Move SpellingBox down slowly
		Vector3 currentPosition = transform.position;
		currentPosition = new Vector3(currentPosition.x, currentPosition.y - downwardMovementSpeed, currentPosition.z);
		transform.position = currentPosition;
	}
}
