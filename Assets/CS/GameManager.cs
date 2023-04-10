using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager GM;

    [Header("게임 설정")]
    public bool isGameStop;         // 게임의 모든 오브젝트 정지 
    public uint score;              // 게임 점수
    public float runTime;           // 게임 진행 시간
    public uint stageCount;         // 현재 스테이지 번호
    public uint destroyCount;       // 적 파괴 개수

    [Header("아이템 설정")]
    public GameObject[] items;      // 아이템 목록
    public int attackUpgradeCount;  // 현재 공격 강화상태
    public IEnumerator infinityCo;  // 무적 코루틴
    public bool isIfinity;          // 무적 체크
    public float infinityTime;      // 무적 시간
    public int plusHPValue;         // 체력회복량
    public int plusOilValue;        // 연료 회복량
    public uint plueSrore;          // 획득 시 주는 점수

    [Header("스킬 설정")]
    public int bombCount;           // 폭탄 개수
    public float setBombCoolTime;   // 폭탄 쿨타임 저장
    public float bombCoolTime;      // 현재 폭탄 쿨타임

    public int healCount;           // 힐 개수
    public int healValue;           // 힐 계수
    public float setHealCoolTime;   // 힐 쿨타임 저장
    public float healCoolTime;      // 현재 힐 쿨타임

    // 저장 관련
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
        // 폭탄 스킬 관리
        if (bombCoolTime >= setBombCoolTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (bombCount > 0)
                {
                    bombCoolTime = 0;

                    // 적이 쏜 총알 전부 제거
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

        // 힐 스킬 관리
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

        // 시작 시 20초를 기다려야 스킬 사용 가능
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
