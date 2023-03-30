using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Airship
{
    [Header("제거 시 주는 포인트")]
    public uint destroyPoint;

    [Header("패턴 기본 설정")]
    public GameObject patternObject;    // 패턴 발사할 오브젝트
    public float startDelay;            // 공격 시작 전 딜레이
    public float delay;                 // 공격 딜레이

    [HideInInspector]
    public Vector3 endPos;

    GameObject hpCanvas;
    float tempHP;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        hpCanvas = transform.Find("HPCanvas").gameObject;
        hpCanvas.SetActive(false);

        tempHP = _HP;
    }

    protected override void Update()
    {
        if (GameManager.GM.isGameStop)
        {
            StopAllCoroutines();
            return;
        }
        
        CanvasUI();

        Vector3 velo = Vector3.zero;

        transform.position =
            Vector3.SmoothDamp(transform.position, endPos, ref velo, 0.05f);
    }

    void CanvasUI()
    {
        if (tempHP != _HP)
        {
            hpCanvas.SetActive(true);
        }

        tempHP = _HP;
    }

    protected override void hpDie()
    {
        base.hpDie();

        GameManager.GM.destroyCount++;
        GameManager.GM.score += destroyPoint;
    }

    [Serializable]
    public struct Shot360
    {
        public int count;
        public int bulletType;
        public float speed;
        public int damage;
    }

    [Serializable]
    public struct MultiShot360
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
    }

    [Serializable]
    public struct LengthDiamond
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
    }

    [Serializable]
    public struct WidthDiamond
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
    }

    [Serializable]
    public struct MultiDiamond
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
    }

    [Serializable]
    public struct DrawLine
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
        public int rotDir;
    }

    [Serializable]
    public struct RotLine
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
        public int rotDir;
    }

    [Serializable]
    public struct TargetLine
    {
        public int times;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
    }

    [Serializable]
    public struct TargetRound
    {
        public int times;
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
        public float rad;
    }

    [Serializable]
    public struct StopTargetRound
    {
        public int count;
        public int bulletType;
        public float delay;
        public float speed;
        public int damage;
        public float rad;
    }

    [Serializable]
    public struct FormShot
    {
        public int times;
        public int count;
        public int bulletType;
        public float speed;
        public int damage;
        public float angle;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = 30;
            hpDie();
        }
    }
}
