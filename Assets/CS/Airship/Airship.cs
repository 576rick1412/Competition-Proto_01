using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airship : MonoBehaviour
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
            if(HP <= 0)
            {
                hpDie();
            }
            else
            {
                Hit();
            }
        }
    }
    [Header("HP")]
    public float setHP;
    public float HP;

    [Header("캐릭터 속도")]
    public float speed;

    [Header("파괴 버블")]
    public GameObject bomb;      

    protected Rigidbody2D rigid;
    protected Animator anim;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        HP = setHP;
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Move(Vector3 vec, float speed)
    {
        transform.position += vec.normalized * speed * Time.deltaTime;
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }
    public virtual void hpDie()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
