using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float changeRateOfEachEenmy; //Set different for each enemy

    private float timeUntillSpawan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetTimeUnitllSpawn(); // Set the time at the scene first loads 
    }

    // Update is called once per frame
    void Update()
    {
        timeUntillSpawan -= Time.deltaTime; // Reduce the time 
        if (timeUntillSpawan <= 0)
        {
            // Once it reached 0, then spawn enemy
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUnitllSpawn(); 
        }

    }

    private void SetTimeUnitllSpawn()
    {
        float minSpawnTime = GameManager.Instance.enemyMinSpawnTime;
        float maxSpawnTime = GameManager.Instance.enemyMaxSpawnTime;
        timeUntillSpawan = Random.Range(minSpawnTime, maxSpawnTime); // Random between min-max spawn time 
    }

    public void EnemySpawnerLevelUp()
    {
        GameManager.Instance.enemyMinSpawnTime -= changeRateOfEachEenmy;
        GameManager.Instance.enemyMaxSpawnTime -= changeRateOfEachEenmy;
    }
}
