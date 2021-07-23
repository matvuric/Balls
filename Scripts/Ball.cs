using UnityEngine;

public class Ball : MonoBehaviour
{
    private readonly float _speed = 200;
    private Rigidbody2D rbBall;

    private void Awake()
    {
        rbBall = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rbBall.velocity = rbBall.velocity.normalized * _speed;
    }
}
