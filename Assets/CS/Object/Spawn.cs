using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public struct Points
    {
        public Transform[] spawnPoints; // ���� ����Ʈ
    }

    public Points[] points;             // ���� ����Ʈ, 0 0 �� ������ ��ǥ
    
    public GameObject[] enemys;         // ���� ��
    public GameObject[] patterns;       // ���� ����

    // ���� ���� ����
    public uint spawnCount;                    // ������ óġ �� + ������ ���� ��, ��� �� óġ �� ���� ������ �����ϵ��� �ϱ� ����
    public bool isSpawnStart;                  // ���� ����

    void Start()
    {
        spawnCount = 0;
        isSpawnStart = true;
    }

    void Update()
    {
        if (GameManager.GM.isGameStop)
        {
            StopAllCoroutines();
            return;
        }

        // ��� �� óġ �� �ٽ� �� ���� ����
        if (GameManager.GM.destroyCount >=  spawnCount)
        {
            isSpawnStart = true;
            Debug.Log("ASdadfgg");
        }

        if (isSpawnStart)
        {
            StartCoroutine(SpawnEnemyWave(times: 3, plusPos: 1f, delay: 0.3f, enemyType: 0,
                                             enemyLine: 1, pointLine: 0,
                                          endEnemyLine: 1, endPointLine: 1));
        }
    }

    IEnumerator SpawnEnemyWave(int times, float plusPos, float delay, int enemyType,
                               int enemyLine, int pointLine, int endEnemyLine, int endPointLine)
    {
        spawnCount = GameManager.GM.destroyCount + (uint)times;
        isSpawnStart = false;

        float tempPos = 0f;
        for (int i = 0; i < times; i++)
        {
            var enemy = Instantiate(enemys[enemyType],
                points[enemyLine].spawnPoints[pointLine].position,
                Quaternion.identity);

            enemy.GetComponent<Enemy>().endPos =
                points[endEnemyLine].spawnPoints[endPointLine].position + new Vector3(tempPos, 0, 0);

            tempPos += plusPos;
            yield return new WaitForSeconds(delay);
        }
        yield return null;
    }

    public void SpawnPattern(int patternType, int enemyLine, int pointLine)
    {
        var temp = Instantiate(patterns[patternType], points[enemyLine].spawnPoints[pointLine].position, Quaternion.identity);
        var pattern = temp.GetComponent<Pattern>();
    }
}
