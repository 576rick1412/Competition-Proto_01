using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager GM;

    [Header("게임 설정")]
    public bool isGameStop;     // 게임의 모든 오브젝트 정지 
    public uint score;          // 게임 점수
    public float runTime;       // 게임 진행 시간
    public uint stageCount;     // 현재 스테이지 번호
    public uint destroyCount;   // 적 파괴 개수

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
