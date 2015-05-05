using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;
	private float nextAsteroid;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start()
	{
		gameOver = false;
		restart = false;

		restartText.text = "";
		gameOverText.text = "";

		score = 0;
		UpdateScore ();

		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {

			for (int i = 0; i < hazardCount; i++) {
				SpawnAsteroid ();
				yield return new WaitForSeconds (spawnWait);
			}

			if (gameOver) {
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}

			yield return new WaitForSeconds(waveWait);
		}
	}

	void SpawnAsteroid()
	{
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (hazard, spawnPosition, spawnRotation);
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString ();
	}

	public void UpdateScore(int newScore)
	{
		score += newScore;
		UpdateScore ();
	}

	public void GameOver()
	{
		gameOver = true;
		string gameOverString = "Game over!\nYou earned " + score.ToString () + " points.\n";
		if (score > 0) {
			gameOverString += "Great job!";
		} else {
			gameOverString += "You need practice :-(";
		}
		gameOverText.text = gameOverString;
	}
}
