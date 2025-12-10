using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] SpawnMode spawnMode;

    public enum SpawnMode{
        Line,
        Points
    }
    // Start is called before the first frame update
    void Start()
    {
        if (spawnMode == SpawnMode.Line)
        {
            StartCoroutine(LineSpawning());
        } else {
            int numPoints = spawnPoints.Length;
            int j = Random.Range(0, numPoints);

            Vector3 startPos = spawnPoints[j].position;

            Instantiate(enemyPrefab, startPos, Quaternion.identity);
        }
        
    }

    IEnumerator LineSpawning()
    {
        Vector3 lineTop = spawnLineTop.position;
        Vector3 lineBottom = spawnLineBottom.position;
        for (int i = 0; i < 4; i++)
        {
            float t = Random.Range(0f, 1f);
            Vector3 startPos = Vector3.Lerp(lineTop, lineBottom, t);
            Instantiate(enemyPrefab, startPos, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
