using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isUpgradeAttack;    // ���� ���׷��̵�
    public bool isInfinity;         // �����ð� ü�� ����   
    public bool isPlusHP;           // ü�� ȸ��
    public bool isPlusOil;          // ���� ȸ��
    public bool isBombBullet;       // �Ѿ� ����
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
                    // �Ʒ� 1�� �� �༭ 2��� ȹ��
                    GameManager.GM.score += GameManager.GM.plueSrore;
                }
            }           // ���� ���׷��̵�
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
            }             // ü�� ȸ��
            else if (isPlusOil)
            {
                var player = collision.gameObject.GetComponent<Player>();
                player.oil += GameManager.GM.plusOilValue;

                if (player.oil > player.setOil)
                {
                    player.oil = player.setOil;
                }
            }            // ���� ����
            else if (isBombBullet)
            {
                GameManager.GM.bombCount++;
            }         // ���� ����

            GameManager.GM.score += GameManager.GM.plueSrore;
            Destroy(gameObject);
        }
    }
}
