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

    public Points[] points;         // ���� ����Ʈ, 0 0 �� ������ ��ǥ

    // ���� ���� ����
    uint spawnCount;                // ������ óġ �� + ������ ���� ��, ��� �� óġ �� ���� ������ �����ϵ��� �ϱ� ����
    bool spawnStart;                // ���� ����
    public int spawnIndex;          // ���� �ε���

    public WaveSetting[] WS;        // �� ���� ���� ����

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
            // ���� �� �Ѿ� ���� ����
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
        // ��� �� óġ �� �ٽ� �� ���� ����
        else if (GameManager.GM.destroyCount >= spawnCount)
        {
            spawnStart = true;
            Debug.Log("���� �����!");
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
        [Header("���� Ÿ��")]
        public bool isWindowPopup;      // ������ ����
        public bool isEnemyWave;        // ���� Ÿ��

        [Header("������ ������Ʈ")]
        public GameObject objectType;   // ������ ������Ʈ

        [Header("���� ����")]
        public int enemyLine;           // ���� ����
        public int pointLine;           // ���� ����

        [Header("���� ����")]
        public int endEnemyLine;        // �̵��� ����
        public int endPointLine;        // �̵��� ����

        [Header("������Ʈ ����")]
        public int times;               // �����ݺ� Ƚ��
        public float plusPos;           // �����Ҷ����� ������ �󸶸�ŭ �̵�����
        public float delay;             // ���� ����
    }
}
