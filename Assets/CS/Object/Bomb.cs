using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float desTime;   // ���� �ð� ����
    void Start()
    {
        Destroy(gameObject, desTime);
        StartCoroutine(BombCo());
    }

    IEnumerator BombCo()
    {
        for (; ; )
        {
            transform.localScale *= 0.85f;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
