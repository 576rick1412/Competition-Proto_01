using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float desTime;   // 삭제 시간 지정
    void Start()
    {
        float ran = Random.Range(0f, 0.5f);
        Destroy(gameObject, desTime + ran);
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
