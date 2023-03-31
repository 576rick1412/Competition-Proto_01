using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isTargetToPlayer;   // �Ѿ��� �÷��̾ Ÿ���������� ����
    string target;                  // Ÿ�� ����

    public bool isForceMove;        // �ֵ������� �����̴��� ����, ���� ��� �Ѿ� ������ �������� ����
    public bool isMove;             // �Ѿ� �Ͻ�����
    public float bulletSpeed;       // �Ѿ� �ӵ�
    public int damage;              // �Ѿ� ������

    [Header("����")]
    public bool isSelfDes;          // �Ѿ� ����
    public bool isOneDes;           // �Ѿ� ���� �ѹ��� �ϵ���
    public GameObject bomb;         // �ı� ����

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
        // ����
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
