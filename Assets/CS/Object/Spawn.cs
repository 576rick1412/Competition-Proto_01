using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public struct Points
    {
        public string name;             // ����ü �̸� (�ν����Ϳ�)
        public Transform[] spawnPoints; // ���� ����Ʈ
    }

    public Points[] points;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
