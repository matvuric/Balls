using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private AddBall addBallPrefab;
    [SerializeField] private HorizontalHit horHitPrefab;
    [SerializeField] private VerticalHit vertHitPrefab;

    private readonly List<Brick> bricksSpawned = new List<Brick>();
    private readonly List<AddBall> addBallSpawned = new List<AddBall>();
    private readonly List<HorizontalHit> horHitSpawned = new List<HorizontalHit>();
    private readonly List<VerticalHit> vertHitSpawned = new List<VerticalHit>();

    public List<Brick> BricksSpawned => bricksSpawned;
    
    private int _rowsSpawned = 0;
    private int _bricksInRow = 0;
    private int _bonusesInRow = 0;
    private readonly float _distanceBetweenBricks = 9.5f;
    private readonly int _width = 9;

    private void OnEnable()
    {
        SpawnBricks();
    }

    internal void SpawnBricks()
    {
        for (int i = 0; i < _width; i++)
        {
            int randomNum = Random.Range(0, 10);

            if ((randomNum < 3) && (_bricksInRow < 4))
            {
                var brick = Instantiate(brickPrefab, GetPosition(i), Quaternion.identity);
                int hp = Random.Range(1, 4) + _rowsSpawned;
                _bricksInRow++;

                brick.SetHealthValue(hp);
                bricksSpawned.Add(brick);
            }

            int bonusChoice = Random.Range(0, 3);

            if ((randomNum > 3) && (_bonusesInRow < 1) && (Random.Range(0, 10) < 3))
            {
                if (bonusChoice == 0)
                {
                    var bonus = Instantiate(addBallPrefab, GetPosition(i), Quaternion.identity);
                    addBallSpawned.Add(bonus);
                    _bonusesInRow++;
                }
                if (bonusChoice == 1)
                {
                    var bonus = Instantiate(horHitPrefab, GetPosition(i), Quaternion.identity);
                    horHitSpawned.Add(bonus);
                    _bonusesInRow++;
                }
                if (bonusChoice == 2)
                {
                    var bonus = Instantiate(vertHitPrefab, GetPosition(i), Quaternion.identity);
                    vertHitSpawned.Add(bonus);
                    _bonusesInRow++;
                }
            }
        }
        CheckRow(_bricksInRow);
    }

    private void CheckRow(int quantity)
    {
        if (quantity == 0)
        {
            SpawnBricks();
        }
        else
        {
            _rowsSpawned++;
            _bricksInRow = 0;
            _bonusesInRow = 0;
            RowMoveDown();
        }
    }

    private void RowMoveDown()
    {
        foreach (var brick in bricksSpawned)
        {
            if (brick != null)
            {
                brick.transform.position += Vector3.down * _distanceBetweenBricks;
            }
        }

        foreach (var bonus in addBallSpawned)
        {
            if (bonus != null)
            {
                bonus.transform.position += Vector3.down * _distanceBetweenBricks;
            }
        }

        foreach (var bonus in horHitSpawned)
        {
            if (bonus != null)
            {
                bonus.transform.position += Vector3.down * _distanceBetweenBricks;
            }
        }

        foreach (var bonus in vertHitSpawned)
        {
            if (bonus != null)
            {
                bonus.transform.position += Vector3.down * _distanceBetweenBricks;
            }
        }
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * _distanceBetweenBricks;
        return position;
    }

    internal void ClearListOfBricks(Brick br)
    {
        bricksSpawned.Remove(br);
    }
}
