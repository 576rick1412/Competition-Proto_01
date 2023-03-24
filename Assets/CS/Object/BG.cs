using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [Header("이동 속도")]
    public float speed;

    [Header("시작 / 끝 좌표")]
    public float startPosX;
    public float endPosX;
    public float n;             
    // 이동 보정값, 인게임이랑 타이틀이랑 스케일이 달라서 보정해줘야함

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.GM.isGameStop)
        {
            return;
        }

        transform.position += new Vector3(0f, -1, 0f) * speed * 5 * Time.deltaTime;

        if (transform.position.y <= endPosX * n)
        {
            transform.position += new Vector3(0f, startPosX * n, 0f);
        }
    }
}
