using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Airship
{
    public float _Oil
    {
        get
        {
            return oil;
        }
        set
        {
            oil -= value;
            if (oil <= 0)
            {
                oilDie();
            }
        }
    }
    [Header("oil")]
    public float setOil;
    public float oil;

    [Header("공격 설정")]
    public GameObject[] bullets;    // 총알
    bool isAttack;                  // 공격 가능 여부, 참일 때 공격 가능
    public float attackDelay;       // 공격 간격
    public int damage;              // 공격 데미지
    public int bulletSpeed;         // 총알 속도
    public int bulletUpgrade;       // 총알 강화값

    [Header("이동 제한")]
    public float leftEnd;
    public float rightEnd;
    public float topEnd;
    public float bottomEnd;
    protected override void Awake()
    {
        base.Awake();
        GameManager.GM.GameDataReset();

        oil = setOil;

        isAttack = true;
    }

    protected override void Start()
    {
        //base.Start();
    }

    protected override void Update()
    {
        //base.Update();
        _Oil = Time.deltaTime;

        InputControl();
    }

    public void InputControl()
    {
        InputMove();

        if(Input.GetKey(KeyCode.Space))
        {
            if(isAttack)
            {
                StartCoroutine(Fire());
            }
        }
    }

    void BorderCheck(ref Vector3 move)
    {
        if (transform.position.x + (move.x * speed * Time.deltaTime) <= leftEnd ||
            transform.position.x + (move.x * speed * Time.deltaTime) >= rightEnd)
        {
            move.x = 0f;
        }

        if (transform.position.y + (move.y * speed * Time.deltaTime) >= topEnd ||
            transform.position.y + (move.y * speed * Time.deltaTime) <= bottomEnd)
        {
            move.y = 0f;
        }
    }

    void InputMove()
    {
        Vector3 move = new Vector3(0, 0, 0);
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        BorderCheck(ref move);
        Move(move, speed);

        anim.SetInteger("move", (int)move.x);
    }
    IEnumerator Fire()
    {
        isAttack = false;

        switch (bulletUpgrade)
        {
            case 1:
                FireInstan();
                break;

            case 2:
                FireInstan(x:  0.1f);
                FireInstan(x: -0.1f);
                break;

            case 3:
                FireInstan(x:  0.1f);
                FireInstan(x: -0.1f);

                FireInstan(x:  0.1f, rot:  0.1f);
                FireInstan(x: -0.1f, rot: -0.1f);
                break;

            case 4:
                FireInstan(x:  0.1f);
                FireInstan(x: -0.1f);
                FireInstan(x:  0.3f);
                FireInstan(x: -0.3f);

                FireInstan(x:  0.1f, rot:  0.1f);
                FireInstan(x: -0.1f, rot: -0.1f);
                break;
        }


        yield return new WaitForSeconds(attackDelay);
        isAttack = true;

        yield return null;
    }

    public void FireInstan(int setBullet = 0, float x = 0, float rot = 0)
    {
        var bullet = Instantiate(bullets[setBullet], transform.position + new Vector3(x, 0f, 0f), transform.rotation);
        bullet.GetComponent<Bullet>().BulletSetting(isForce: true, spd: bulletSpeed, dmg: damage);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(rot, 1) * bulletSpeed, ForceMode2D.Impulse);
    }

    public override void hpDie()
    {
        
    }
    public void oilDie()
    {

    }
}
