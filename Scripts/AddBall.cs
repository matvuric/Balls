using UnityEngine;

public class AddBall : MonoBehaviour
{
    private BallLauncher ballLauncher;

    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        ballLauncher.AddBall();
    }
}
