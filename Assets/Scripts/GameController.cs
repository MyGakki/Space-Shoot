using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scocerText;
    public Text gameOverText;
    public Text restartText;

    private int scocer;
    private bool gameOver;
    private bool restart;

    void Start()
    {
        gameOverText.text = "";
        gameOver = false;
        restartText.text = "";
        restart = false;
        StartCoroutine(spawnWaves());
        scocer = 0;
        UpdateScocer();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over";
    }
    void UpdateScocer()
    {
        scocerText.text = "Scocer: " + scocer;
    }

    public void addScocer(int value)
    {
        scocer += value;
        UpdateScocer();
    }

    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver) {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

                if (gameOver)
                {
                    restart = true;
                    restartText.text = "Press 'R' to restart!";
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
