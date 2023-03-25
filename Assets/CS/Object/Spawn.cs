using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public struct Points
    {
        public Transform[] spawnPoints; // 스폰 포인트
    }

    public Points[] points;             // 생성 포인트, 0 0 은 스포너 좌표
    
    public GameObject[] enemys;         // 생성 적
    public GameObject[] patterns;       // 생성 패턴

    // 스폰 관련 설정
    public uint spawnCount;                    // 원래의 처치 수 + 생성된 적의 수, 모든 적 처치 시 다음 스폰이 가능하도록 하기 위함
    public bool isSpawnStart;                  // 스폰 시작

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

        // 모든 적 처치 시 다시 적 스폰 시작
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
