using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0.1f, 1f)] float spawnTime = 1f;
    [SerializeField][Range(0, 50)] int poolSize = 10;

    GameObject[] enemyPool;

    void Awake()
    {
        PopulatePool();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObJectInPool();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void PopulatePool()
    {
        enemyPool = new GameObject[poolSize];

        for (int i = 0; i < enemyPool.Length; i++)
        {
                enemyPool[i] = Instantiate(enemyPrefab, transform);
                enemyPool[i].SetActive(false);
        }
    }

    void EnableObJectInPool()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].activeInHierarchy == false)
            {
                enemyPool[i].SetActive(true);
                return; // break loop once enabled an object
            }
        }
    }
}
