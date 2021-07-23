using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;

    private Spawner spawner;
    private BallReturn ballReturn;
    private SpriteRenderer ballLauncherRenderer;
    private LineRenderer lineRenderer;

    private readonly List<Ball> balls = new List<Ball>();

    private Vector3 _startPoint;
    private Vector3 _startDrag;
    private Vector3 _endDrag;

    private int _ballsReady = 0;
    private int _ballsAdded = 1;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        ballReturn = FindObjectOfType<BallReturn>();
        lineRenderer = GetComponent<LineRenderer>();
        ballLauncherRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CreateBall();
    }

    private void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -1;

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(LaunchBalls());
        }
    }

    private void CreateBall()
    {
        for (int i = 0; i < _ballsAdded; i++)
        {
            var ball = Instantiate(ballPrefab);
            balls.Add(ball);
            _ballsReady++;
        }
    }

    internal void ReturnBall()
    {
        _ballsReady++;

        if (_ballsReady == balls.Count)
        {
            CreateBall();
            spawner.SpawnBricks();
        }
    }

    private void SetStartPoint(Vector3 worldPoint)
    {
        _startPoint = worldPoint;

        lineRenderer.SetPosition(0, _startPoint);
    }

    private void SetEndPoint(Vector3 worldPoint)
    {
        if (worldPoint.y > _startPoint.y)
        {
            Vector3 pointOffset = worldPoint - _startPoint;
            Vector3 endPoint = transform.position + pointOffset;

            lineRenderer.SetPosition(1, endPoint);
        }
    }

    private void StartDrag(Vector3 worldPosition)
    {
        _startDrag = worldPosition;
        SetStartPoint(transform.position);
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        _endDrag = worldPosition;
        Vector3 direction = _endDrag - _startDrag;
        SetEndPoint(transform.position - direction);
        ClearLine(false);
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 direction = _endDrag - _startDrag;
        direction.Normalize();
        ClearLine(true);
        ClearBallLauncher(true);
        _ballsAdded = 0;
        ballReturn.ClearPositions();
        foreach (var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);

            yield return new WaitForSeconds(0.15f);
        }
        _ballsReady = 0;
    }

    internal void ClearBallLauncher(bool visible)
    {
        ballLauncherRenderer.enabled = !visible;
    }

    private void ClearLine(bool visible)
    {
        lineRenderer.enabled = !visible;
    }

    internal void AddBall()
    {
        _ballsAdded++;
    }
}