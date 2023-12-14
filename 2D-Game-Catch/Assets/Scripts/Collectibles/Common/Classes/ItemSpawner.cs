using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRangeX = 5f;
    [SerializeField] private float spawnerSpeed = 2f;
    [SerializeField] private GameObject itemPrefab;
    
    private float nextSpawnTime;
    
    private void Update()
    {
        MoveSpawner();
        SpawnItems();
    }

    private void MoveSpawner()
    {
        transform.Translate(Vector3.right * spawnerSpeed * Time.deltaTime);

        if (transform.position.x > spawnRangeX)
            transform.position = new Vector3(-spawnRangeX, transform.position.y, transform.position.z);
    }

    private void SpawnItems()
    {
        if (!(Time.time >= nextSpawnTime)) return;

        nextSpawnTime = Time.time + spawnInterval;

        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
