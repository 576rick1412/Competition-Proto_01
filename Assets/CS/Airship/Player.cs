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

        var bullet = Instantiate(bullets[0], transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().BulletSetting(spd:bulletSpeed, dmg:damage);

        yield return new WaitForSeconds(attackDelay);
        isAttack = true;

        yield return null;
    }

    public override void hpDie()
    {
        
    }
    public void oilDie()
    {

    }
}
