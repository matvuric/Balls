using System.Linq;
using UnityEngine;

public class HorizontalHit : MonoBehaviour
{
    private Spawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);

        foreach (Brick br in spawner.BricksSpawned.Where(br => br.transform.position.y == transform.position.y))
        {
            br.UpdateHealth(1);
        }
    }
}
