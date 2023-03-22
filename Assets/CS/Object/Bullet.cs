using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isForceMove;
    public bool isMove;
    public float bulletSpeed;
    public int damage;

    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(isForceMove)
        {
            if(GameManager.GM.isGameStop)
            {
                transform.position = Vector3.zero;
            }

            return;
        }

        if (GameManager.GM.isGameStop == false)
        {
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
        }
    }

    public void BulletSetting(bool isForce = false, float spd = 0, int dmg = 0)
    {
        isForceMove = isForce;
        bulletSpeed = spd;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            float ran = Random.Range(0f, 0.5f);
            Destroy(gameObject, ran);
        }
    }
}
