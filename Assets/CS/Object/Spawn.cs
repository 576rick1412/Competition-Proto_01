using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public struct Points
    {
        public string name;             // 구조체 이름 (인스펙터용)
        public Transform[] spawnPoints; // 스폰 포인트
    }

    public Points[] points;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
