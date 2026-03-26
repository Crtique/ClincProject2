using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    private void Start()
    {
        if( gameOver != null)
        {
            gameOver.SetActive(false);
        }
        Time.timeScale = 1.0f;
    }
    public void EndGame()
    {
        if (gameOver != null)
        {
            gameOver.SetActive(true);
        }
        Time.timeScale = 0f;
    }
}
