using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float desTime;   // 삭제 시간 지정
    public float damage = 0;

    void Start()
    {
        StartCoroutine(BombCo());
    }

    IEnumerator BombCo()
    {
        int times = 40;

        for (int i = 0; i < times; i++)
        {
            transform.localScale *= 0.9f;
            yield return new WaitForSeconds(desTime / times);
        }

        float ran = Random.Range(0f, 0.5f);
        Destroy(gameObject, ran);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = damage / 4;
            Destroy(gameObject);
        }
    }
}
