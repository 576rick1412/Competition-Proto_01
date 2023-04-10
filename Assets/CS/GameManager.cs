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
    public uint plueSrore;          // ȹ�� �� �ִ� ����

    [Header("��ų ����")]
    public int bombCount;           // ��ź ����
    public float setBombCoolTime;   // ��ź ��Ÿ�� ����
    public float bombCoolTime;      // ���� ��ź ��Ÿ��

    public int healCount;           // �� ����
    public int healValue;           // �� ���
    public float setHealCoolTime;   // �� ��Ÿ�� ����
    public float healCoolTime;      // ���� �� ��Ÿ��

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
        // ��ź ��ų ����
        if (bombCoolTime >= setBombCoolTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (bombCount > 0)
                {
                    bombCoolTime = 0;

                    // ���� �� �Ѿ� ���� ����
                    foreach (var i in FindObjectsOfType<Bullet>())
                    {
                        var BS = i.GetComponent<Bullet>();

                        if (BS.isTargetToPlayer)
                        {
                            i.GetComponent<Bullet>().damage = 0;
                            i.GetComponent<Bullet>().isSelfDes = true;
                        }
                    }

                    bombCount--;
                }
            }
        }
        else
        {
            bombCoolTime += Time.deltaTime;
        }

        // �� ��ų ����
        if (healCoolTime >= setHealCoolTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (healCount > 0)
                {
                    healCoolTime = 0;

                    var player = GameObject.Find("Player").GetComponent<Player>();
                    player.HP += healValue;

                    if(player.HP > player.setHP)
                    {
                        player.HP = player.setHP;
                    }

                    healCount--;
                }
            }
        }
        else
        {
            healCoolTime += Time.deltaTime;
        }
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

        bombCount = 2;
        healCount = 2;

        // ���� �� 20�ʸ� ��ٷ��� ��ų ��� ����
        healCoolTime = setHealCoolTime - 20;
        bombCoolTime = setBombCoolTime - 20;
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
