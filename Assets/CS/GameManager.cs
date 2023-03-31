using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager GM;

    [Header("���� ����")]
    public bool isGameStop;         // ������ ��� ������Ʈ ���� 
    public uint score;              // ���� ����
    public float runTime;           // ���� ���� �ð�
    public uint stageCount;         // ���� �������� ��ȣ
    public uint destroyCount;       // �� �ı� ����

    [Header("������ ����")]
    public GameObject[] items;      // ������ ���
    public int attackUpgradeCount;  // ���� ���� ��ȭ����
    public IEnumerator infinityCo;  // ���� �ڷ�ƾ
    public bool isIfinity;          // ���� üũ
    public float infinityTime;      // ���� �ð�
    public int plusHPValue;         // ü��ȸ����
    public int plusOilValue;        // ���� ȸ����
    public uint bombCount;          // ��ź ���� ����
    public uint plueSrore;          // ȹ�� �� �ִ� ����

    // ���� ����
    string filePath;
    public MainDB data;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/MainDB.txt";
        Debug.Log(filePath);

        var obj = FindObjectsOfType<GameManager>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);

            GM = this;

            JsonLoad();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void GameDataReset()
    {
        isGameStop = false;
        score = 0;
        runTime = 0f;
        stageCount = 0;
        destroyCount = 0;

        attackUpgradeCount = 0;
        infinityCo = Infinity();
        isIfinity = false;
        bombCount = 0;
    }

    public string CommaText(uint value)
    {
        if(value == 0)
        {
            return "0";
        }
        
        return string.Format("{0:#,###}",value);
    }
    public string TenZeroCommaText(uint value)
    {
        return string.Format("{0:0000000000}", value);
    }

    public void RankArrange(string name)
    {
        Rank temp;
        temp.setName = name;
        temp.setScore = score;

        for (int i = 0; i < data.ranks.Length; i++)
        {
            if (temp.setScore > data.ranks[i].setScore)
            {
                Rank p = data.ranks[i];
                data.ranks[i] = temp;
                temp = p;
            }
        }
        JsonSave();
    }

    public void ItemSpawn(Vector3 pos)
    {
        int x = Random.Range(0, items.Length);
        Instantiate(items[x], pos, Quaternion.identity);
    }

    public void InfinityPlayer()
    {
        StartCoroutine(infinityCo);
        infinityCo = Infinity();
        StartCoroutine(infinityCo);
    }

    public IEnumerator Infinity()
    {
        isIfinity = true;
        yield return new WaitForSeconds(infinityTime);

        isIfinity = false;
        yield return null;
    }

    public void JsonSave()
    {
        var save = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, save);
    }

    public void JsonLoad()
    {
        if(!File.Exists(filePath))
        {
            ResetDB();
        }

        var load = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<MainDB>(load);
    }

    public void ResetDB()
    {
        data = new MainDB();

        for (int i = 0; i < data.ranks.Length; i++)
        {
            //data.ranks[i].setName = "Null";
            data.ranks[i].setName = "---";
            data.ranks[i].setScore = 0;
        }

        JsonSave();
    }

    [System.Serializable]
    public class MainDB
    {
        public Rank[] ranks = new Rank[10];
    }

    [System.Serializable]
    public struct Rank
    {
        public string setName;
        public uint setScore;
    }
}
