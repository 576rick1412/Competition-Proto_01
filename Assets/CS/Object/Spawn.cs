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

    public Points[] points;         // 생성 포인트, 0 0 은 스포너 좌표

    // 스폰 관련 설정
    uint spawnCount;                // 원래의 처치 수 + 생성된 적의 수, 모든 적 처치 시 다음 스폰이 가능하도록 하기 위함
    bool spawnStart;                // 스폰 시작
    public int spawnIndex;          // 스폰 인덱스

    public WaveSetting[] WS;        // 적 생성 순서 지정

    void Awake()
    {
        spawnCount = 0;
        spawnStart = true;
        spawnIndex = 0;
    }

    void Update()
    {
        if (GameManager.GM.isGameStop)
        {
            StopAllCoroutines();
            return;
        }

        if (spawnIndex >= WS.Length)
        {
            StopAllCoroutines();
            return;
        }

        if (spawnStart)
        {
            // 적이 쏜 총알 전부 제거
            foreach(var i in FindObjectsOfType<Bullet>())
            {
                var BS = i.GetComponent<Bullet>();

                if(BS.isTargetToPlayer)
                {
                    i.GetComponent<Bullet>().damage = 0;
                    i.GetComponent<Bullet>().isSelfDes = true;
                }
            }

            if (WS[spawnIndex].isWindowPopup)
            {
                StartCoroutine(WindowPopup());
                return;
            }

            if (WS[spawnIndex].isEnemyWave)
            {
                StartCoroutine(SpawnEnemyWave());
            }
            else
            {
                StartCoroutine(SpawnMeteo());
            }
        }
        // 모든 적 처치 시 다시 적 스폰 시작
        else if (GameManager.GM.destroyCount >= spawnCount)
        {
            spawnStart = true;
            Debug.Log("스폰 재시작!");
        }
    }

    IEnumerator WindowPopup()
    {
        spawnStart = false;
        uint tempCount = spawnCount;
        spawnCount = 100000;
        GameManager.GM.stageCount++;

        yield return new WaitForSeconds(2f);
        Instantiate(WS[spawnIndex].objectType, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("sfagdsfhsh");
        yield return new WaitForSeconds(5f);

        spawnIndex++;
        spawnStart = true;
        spawnCount = tempCount;

        yield return null;
    }

    IEnumerator SpawnEnemyWave()
    {
        spawnCount = GameManager.GM.destroyCount + (uint)WS[spawnIndex].times;
        spawnStart = false;

        float tempPos = 0f;
        for (int i = 0; i < WS[spawnIndex].times; i++)
        {
            var enemy = Instantiate(WS[spawnIndex].objectType,
                points[WS[spawnIndex].enemyLine].spawnPoints[WS[spawnIndex].pointLine].position,
                Quaternion.identity);
            
            enemy.GetComponent<Enemy>().endPos =
                points[WS[spawnIndex].endEnemyLine].spawnPoints[WS[spawnIndex].endPointLine].position + new Vector3(tempPos, 0, 0);

            tempPos += WS[spawnIndex].plusPos;
            yield return new WaitForSeconds(WS[spawnIndex].delay);
        }

        spawnIndex++;
        yield return null;
    }

    IEnumerator SpawnMeteo()
    {
        spawnStart = false;
        uint tempCount = spawnCount;
        spawnCount = 100000;

        float ranPosX = 3.5f;
        for (int i = 0; i < WS[spawnIndex].times; i++)
        {
            Instantiate(WS[spawnIndex].objectType,
                        points[0].spawnPoints[1].position + new Vector3(Random.Range(-ranPosX, ranPosX), 0, 0),
                        Quaternion.identity);

            yield return new WaitForSeconds(WS[spawnIndex].delay);
        }
        yield return new WaitForSeconds(WS[spawnIndex].delay * 5);

        spawnIndex++;
        spawnStart = true;
        spawnCount = tempCount;

        yield return null;
    }

    /*
    public void SpawnPattern()
    {
        spawnStart = false;

        var temp = Instantiate(WS[spawnIndex].objectType, points[WS[spawnIndex].enemyLine].spawnPoints[WS[spawnIndex].pointLine].position, Quaternion.identity);
        var pattern = temp.GetComponent<Pattern>();

        spawnIndex++;
    }
    */

    [System.Serializable]
    public struct WaveSetting
    {
        [Header("스폰 타입")]
        public bool isWindowPopup;      // 윈도우 생성
        public bool isEnemyWave;        // 생성 타입

        [Header("생성할 오브젝트")]
        public GameObject objectType;   // 생성할 오브젝트

        [Header("시작 지점")]
        public int enemyLine;           // 생성 라인
        public int pointLine;           // 생성 지점

        [Header("최종 시점")]
        public int endEnemyLine;        // 이동될 라인
        public int endPointLine;        // 이동될 지점

        [Header("오브젝트 설정")]
        public int times;               // 생성반복 횟수
        public float plusPos;           // 생성할때마다 옆으로 얼마만큼 이동할지
        public float delay;             // 생성 간격
    }
}
