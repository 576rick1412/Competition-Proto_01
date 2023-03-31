using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("이동 제한")]
    public float leftEnd;
    public float rightEnd;
    public float topEnd;
    public float bottomEnd;

    [Header("게임오버 UI")]
    public GameObject gameOverUI;

    [Header("블러드씬")]
    public Image bloodScene;
    IEnumerator bloodCo;
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
        bloodCo = BloodSceneCo();
    }

    protected override void Update()
    {
        //base.Update();
        if(!GameManager.GM.isGameStop)
        {
            _Oil = Time.deltaTime;
            GameManager.GM.runTime += Time.deltaTime;
        }
        
        InputControl();
    }

    public void InputControl()
    {
        if (GameManager.GM.isGameStop)
        {
            return;
        }

        InputMove();

        if(Input.GetKey(KeyCode.Space))
        {
            if(isAttack)
            {
                StartCoroutine(Fire());
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GameManager.GM.bombCount > 0)
            {
                // 적이 쏜 총알 전부 제거
                foreach (var i in FindObjectsOfType<Bullet>())
                {
                    var BS = i.GetComponent<Bullet>();

                    if (BS.isTargetToPlayer)
                    {
                        i.GetComponent<Bullet>().damage = 0;
                        i.GetComponent<Bullet>().isSelfDes = true;
                    }
                }

                GameManager.GM.bombCount--;
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

        switch (GameManager.GM.attackUpgradeCount)
        {
            case 0:
                FireInstan();
                break;

            case 1:
                FireInstan(x:  0.1f);
                FireInstan(x: -0.1f);
                break;

            case 2:
                FireInstan(x:  0.1f);
                FireInstan(x: -0.1f);

                FireInstan(x:  0.1f, rot:  0.1f);
                FireInstan(x: -0.1f, rot: -0.1f);
                break;

            case 3:
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
        bullet.GetComponent<Bullet>().BulletSetting(false, true, bulletSpeed, damage);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(rot, 1) * bulletSpeed, ForceMode2D.Impulse);
    }
    protected override void Hit()
    {
        base.Hit();

        StopCoroutine(bloodCo);

        bloodCo = BloodSceneCo();
        StartCoroutine(bloodCo);
    }
    protected override void hpDie()
    {
        if (GameManager.GM.isGameStop)
        {
            return;
        }   // 한번만 실행하도록 예외처리

        GameManager.GM.isGameStop = true;

        var temp = Instantiate(gameOverUI, transform.position, Quaternion.identity);
        temp.GetComponent<GameOverUI>().isNoHP = true;
    }
    public void oilDie()
    {
        if(GameManager.GM.isGameStop)
        {
            return;
        }   // 한번만 실행하도록 예외처리

        GameManager.GM.isGameStop = true;

        var temp = Instantiate(gameOverUI, transform.position, Quaternion.identity);
        temp.GetComponent<GameOverUI>().isNoHP = false;
    }

    IEnumerator BloodSceneCo()
    {
        byte a = 100;
        bloodScene.color = new Color32(255, 0, 0, a);

        int times = 40;
        for (int i = 0; i < times; i++)
        {
            
            bloodScene.color = new Color32(255, 0, 0, a);
            a -= (byte)(100 / times);

            yield return null;
        }

        bloodScene.color = new Color32(255, 0, 0, 0);
        yield return null;
    }
}
