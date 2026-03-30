using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.EndGame();
            }
        }
    }
}