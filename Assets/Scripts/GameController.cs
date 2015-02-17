using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class GameController : MonoBehaviour
{

	public GameObject spellingBox;
	public int lives = 5;
	public int tempLives = 5;
	public int correct = 0;
	public List<Word> dictionary = new List<Word>();
	public List<Word> missedWords = new List<Word>();

	public float generationTimer = 0f;
	public float waitTime = 3f;

	int selectableCount = 0;

	void Start()
	{
		Populate(dictionary);
		generationTimer = 0f;
		tempLives = lives;
	}

	void Update()
	{
		generationTimer += Time.deltaTime;

		if (generationTimer >= waitTime)
		{
			generationTimer = 0f;
			//generate new SpellingBox
			int index = Random.Range(0, dictionary.Count);
			GameObject newSpellingBox = Instantiate(spellingBox) as GameObject;
			newSpellingBox.GetComponent<SpellingBox>().word = dictionary[index];
			newSpellingBox.transform.SetParent(GameObject.Find("MainCanvas").transform);

			Vector3 temp = new Vector3(Screen.width / 2f, Screen.height, 0);
			temp.x = Random.Range((Screen.width / 4f), 3 * (Screen.width / 4f));
			newSpellingBox.GetComponent<RectTransform>().position = temp;

		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			selectableCount -= 1;
			if (selectableCount < 0)
			{
				selectableCount = Selectable.allSelectables.Count - 1;
			}
			Selectable.allSelectables[selectableCount].Select();
		}

		if(tempLives != lives)
		{ 
			tempLives = lives;
			//for(int i = 0; i < missedWords.Count; i++)
				GameObject.FindGameObjectWithTag("MissedWords").GetComponent<Text>().text += "\n" + missedWords[missedWords.Count - 1].correctSpelling;
		}
	}

	void Populate(List<Word> inDictionary)
	{

		TextAsset txt = Resources.Load("wordlist") as TextAsset;
		StringReader content = new StringReader(txt.text);
		string line = content.ReadLine();
		while (line != null)
		{
			string[] words = line.Split(' ');
			dictionary.Add(new Word(words[0], words[1]));
			line = content.ReadLine();
		}

	}
}

public class Word
{
	public string correctSpelling;
	public string incorrectSpelling;
	public Word(string inputCorrectString, string inputIncorrectString)
	{
		correctSpelling = inputCorrectString;
		incorrectSpelling = inputIncorrectString;
	}
}
