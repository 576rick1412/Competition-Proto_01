using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isUpgradeAttack;    // 공격 업그레이드
    public bool isInfinity;         // 일정시간 체력 무한   
    public bool isPlusHP;           // 체력 회복
    public bool isPlusOil;          // 연료 회복
    public bool isBombBullet;       // 총알 제거
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (isUpgradeAttack)
            {
                if(GameManager.GM.attackUpgradeCount < 3)
                {
                    GameManager.GM.attackUpgradeCount++;
                }
                else
                {
                    // 아래 1번 더 줘서 2배로 획득
                    GameManager.GM.score += GameManager.GM.plueSrore;
                }
            }           // 공격 업그레이드
            else if (isInfinity)
            {

            }
            else if (isPlusHP)
            {
                var player = collision.gameObject.GetComponent<Player>();
                player.HP += GameManager.GM.plusHPValue;

                if(player.HP > player.setHP)
                {
                    player.HP = player.setHP;
                }
            }             // 체력 회복
            else if (isPlusOil)
            {
                var player = collision.gameObject.GetComponent<Player>();
                player.oil += GameManager.GM.plusOilValue;

                if (player.oil > player.setOil)
                {
                    player.oil = player.setOil;
                }
            }            // 연료 충전
            else if (isBombBullet)
            {
                GameManager.GM.bombCount++;
            }         // 폭알 자폭

            GameManager.GM.score += GameManager.GM.plueSrore;
            Destroy(gameObject);
        }
    }
}
