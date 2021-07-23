using System.Collections;
using UnityEngine;

public class BackgroundAnim : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;

    private Vector3 _targetPos;
    public Vector3 TargetPos => _targetPos;
    
    private int _cameraWidth;
    private int _cameraHeight;
    private readonly bool TurnOff;

    private void Start()
    {
        _cameraWidth = Camera.main.pixelWidth;
        _cameraHeight = Camera.main.pixelHeight;
        StartCoroutine(PositionReset());
    }

    private void SpawnerPosition()
    {
        int side = Random.Range(0, 4);
        int previousSide = 0;

        if (previousSide == side)
        {
            SpawnerPosition();
        }
        if (side == 0)
        {
            transform.position = new Vector3(Random.Range(0, _cameraWidth), _cameraHeight * 1.05f);
            _targetPos = transform.position + Vector3.down * Camera.main.pixelHeight * 1.5f;
        }
        else if (side == 1)
        {
            transform.position = new Vector3(_cameraWidth * 1.1f, Random.Range(0, _cameraHeight));
            _targetPos = transform.position + Vector3.left * Camera.main.pixelWidth * 2f;
        }
        else if (side == 2)
        {
            transform.position = new Vector3(Random.Range(0, _cameraWidth), -_cameraHeight * 0.05f);
            _targetPos = transform.position + Vector3.up * Camera.main.pixelHeight * 1.5f;
        }
        else
        {
            transform.position = new Vector3(-_cameraWidth * 0.1f, Random.Range(0, _cameraHeight));
            _targetPos = transform.position + Vector3.right * Camera.main.pixelWidth * 2f;
        }
    }

    private IEnumerator PositionReset()
    {
        while (!TurnOff)
        {
            SpawnerPosition();
            CreateCube();

            yield return new WaitForSeconds(1.5f);
        }
    }

    private void CreateCube()
    {
        Instantiate(cubePrefab, transform.position, Quaternion.identity);
    }
}
