using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;

    private BackgroundAnim backgroundAnim;

    private void Start()
    {
        backgroundAnim = FindObjectOfType<BackgroundAnim>();
        StartCoroutine(MoveCube());
    }

    private IEnumerator MoveCube()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, backgroundAnim.TargetPos, i / 200);

            yield return null;
        }
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
