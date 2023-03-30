using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    public float _HP
    {
        get
        {
            return HP;
        }
        set
        {
            HP -= value;
            if (HP <= 0)
            {
                Die();
            }
        }
    }

    [Header("HP")]
    public float setHP;
    public float HP;

    public float speed;     // 운석 속도
    public int damage;      // 운석 데미지
    public GameObject bomb; // 운석 이펙트?
    bool isAlive;
    void Start()
    {
        HP = setHP;
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            var meteo = Instantiate(bomb, transform.position, Quaternion.identity);
            meteo.GetComponent<Bomb>().desTime = 2;
        }
    }

    public void MeteoSetting(float spd,int dmg)
    {
        speed = spd;
        damage = dmg;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>()._HP = damage;
            Destroy(gameObject);
        }
    }

    void Die()
    {
        var meteo = Instantiate(bomb, transform.position, Quaternion.identity);
        meteo.GetComponent<Bomb>().desTime = 1f;
        meteo.transform.localScale = new Vector3(6, 6, 6);
        meteo.GetComponent<SpriteRenderer>().sortingOrder = 2;

        isAlive = false;

        Destroy(gameObject);
    }
}
