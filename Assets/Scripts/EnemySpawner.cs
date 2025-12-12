using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] SpawnModes spawnModes;
    [SerializeField] GameObject[] enemyTypes;

    [SerializeField] float spawnTime = 5f;

    public enum SpawnModes{
        Line,
        Points
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());        
    }

    IEnumerator SpawnWave()
    {
        int select_formation = Random.Range(0, 3);
        if (select_formation == 0)
        {
            StartCoroutine(LineSpawning());
        }
        else if (select_formation == 1)
        {
            StartCoroutine(RandomPointSpawning());
        } else
        {
            StartCoroutine(AllPointSpawning());
        }
        
        yield return new WaitForSeconds(spawnTime);

        yield return SpawnWave();
    }

    IEnumerator LineSpawning()
    {
        GameObject selected_enemy = enemyTypes[Random.Range(0, 2)];
        Vector3 lineTop = spawnLineTop.position;
        Vector3 lineBottom = spawnLineBottom.position;
        for (int i = 0; i < 4; i++)
        {
            float t = Random.Range(0f, 1f);
            Vector3 startPos = Vector3.Lerp(lineTop, lineBottom, t);
            Instantiate(selected_enemy, startPos, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RandomPointSpawning()
    {
        GameObject selected_enemy = enemyTypes[Random.Range(0, 2)];
        int numPoints = spawnPoints.Length;
        int j = Random.Range(0, numPoints);

        Vector3 startPos = spawnPoints[j].position;

        Instantiate(selected_enemy, startPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator AllPointSpawning()
    {
        GameObject selected_enemy = enemyTypes[Random.Range(0, 2)];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int numPoints = spawnPoints.Length;
            int j = Random.Range(0, numPoints);

            Vector3 startPos = spawnPoints[j].position;

            Instantiate(selected_enemy, startPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
