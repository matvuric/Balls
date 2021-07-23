using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    private BallLauncher ballLauncher;

    private List<Vector3> positions = new List<Vector3>();

    private Vector3 position;

    private float positionY;

    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void Start()
    {
        positionY = ballLauncher.transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballLauncher.ReturnBall();
        collision.collider.gameObject.SetActive(false);

        position = collision.collider.gameObject.transform.position;
        position.y = positionY;
        positions.Add(position);
        ballLauncher.transform.position = positions[0];
        ballLauncher.ClearBallLauncher(false);
    }

    internal void ClearPositions()
    {
        positions.Clear();
    }
}