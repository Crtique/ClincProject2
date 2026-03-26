using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public PlayerControl movement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            movement.enabled = false;
            GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.EndGame();
            }
        }
    }
}
