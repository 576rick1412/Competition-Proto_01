using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float speed;

    [Header("���� / �� ��ǥ")]
    public float startPosX;
    public float endPosX;
    public float n;             
    // �̵� ������, �ΰ����̶� Ÿ��Ʋ�̶� �������� �޶� �����������

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
