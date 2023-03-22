using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager GM;

    [Header("게임 설정")]
    public bool isGameStop; // 게임의 모든 오브젝트 정지 
    public uint score;      // 게임 점수

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
    }

    public string CommaText(uint value)
    {
        if(value == 0)
        {
            return "0";
        }

        return string.Format("{0:#,###}",value);
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






        JsonSave();
    }

    [System.Serializable]
    public class MainDB
    {

    }
}
