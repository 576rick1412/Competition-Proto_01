using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isTargetToPlayer;   // 총알이 플레이어를 타겟으로할지 여부
    string target;                  // 타겟 지정

    public bool isForceMove;        // 애드포스로 움직이는지 여부, 참일 경우 총알 스스로 움직이지 않음
    public bool isMove;             // 총알 일시정지
    public float bulletSpeed;       // 총알 속도
    public int damage;              // 총알 데미지

    [Header("자폭")]
    public bool isSelfDes;          // 총알 자폭
    public bool isOneDes;           // 총알 자폭 한번만 하도록
    public GameObject bomb;         // 파괴 버블

    void Awake()
    {
        isSelfDes = false;
        isOneDes = true;
    }
    void Start()
    {
        if (isTargetToPlayer)
        {
            target = "Player";
        }
        else
        {
            target = "Enemy";
        }
    }

    void Update()
    {
        // 자폭
        if(isSelfDes && isOneDes)
        {
            isOneDes = false;

            Invoke("InstanDes", Random.Range(0f, 0.5f));
        }

        if (isForceMove)
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

    void InstanDes()
    {
        var temp = Instantiate(bomb, transform.position, Quaternion.identity);
        temp.GetComponent<Bomb>().damage = damage * 2f;
        Destroy(gameObject);
    }

    public void BulletSetting(bool target = true, bool isForce = false, float spd = 0, int dmg = 0)
    {
        isTargetToPlayer = target;

        isForceMove = isForce;
        bulletSpeed = spd;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(target))
        {
            if(target == "Player" && GameManager.GM.isIfinity)
            {
                return;
            }

            collision.gameObject.GetComponent<Airship>()._HP = damage;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Meteo") && target == "Enemy")
        {
            collision.gameObject.GetComponent<Meteo>()._HP = damage;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            float ran = Random.Range(0f, 0.5f);
            Destroy(gameObject, ran);
        }
    }
}
