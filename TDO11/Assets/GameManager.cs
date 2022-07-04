using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverUI;
   

    public SceneFader sceneFader;

    public GameObject completeLevelUI;

    private void Start()
    {
        gameIsOver = false;
    }
    void Update()
    {
        //if (Input.GetKeyDown("l"))
        //{
        //    EndGame();
        //}

        if (gameIsOver)
        {
            return;
        }
        if (PlayerStats.lives <= 0)
        {

            EndGame();
        }
    }
    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
        //Debug.Log("Game Over !");
    }

    public void WinLevel()
    {
        //Debug.Log("NIVEAU TERMINE ! BRAVO");
        gameIsOver = true;
        completeLevelUI.SetActive(true);
       
    }
}
