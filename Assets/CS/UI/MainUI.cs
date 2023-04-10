using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [Header("체력")]
    public Image hpBar;

    [Header("연료")]
    public Image oilBar;

    [Header("점수")]
    public TextMeshProUGUI scoreTMP;

    [Header("폭탄 스킬")]
    public TextMeshProUGUI bombTMP;
    public GameObject bombGreen;
    public GameObject bombBlack;
    public Image bombCoolBar;

    [Header("회복 스킬")]
    public TextMeshProUGUI healTMP;
    public GameObject healGreen;
    public GameObject healBlack;
    public Image healCoolBar;

    [Header("공격 레벨")]
    public TextMeshProUGUI atkTMP;

    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        UI_Update();
    }

    void UI_Update()
    {
        // 체력
        hpBar.fillAmount = player._HP / player.setHP;

        // 연료
        oilBar.fillAmount = player._Oil / player.setOil;

        // 점수
        scoreTMP.text = GameManager.GM.CommaText(GameManager.GM.score);

        // 공격 레벨
        atkTMP.text = GameManager.GM.CommaText((uint)GameManager.GM.attackUpgradeCount + 1);

        // 폭탄 스킬
        bombTMP.text = "Bomb\ntimes: " + GameManager.GM.bombCount;

        if(GameManager.GM.bombCoolTime >= GameManager.GM.setBombCoolTime)
        {
            bombGreen.SetActive(true);
            bombBlack.SetActive(false);
        }
        else
        {
            bombGreen.SetActive(false);
            bombBlack.SetActive(true);
            bombCoolBar.fillAmount = GameManager.GM.bombCoolTime / GameManager.GM.setBombCoolTime;
        }

        // 힐 스킬
        healTMP.text = "Bomb\ntimes: " + GameManager.GM.healCount;

        if (GameManager.GM.healCoolTime >= GameManager.GM.setHealCoolTime)
        {
            healGreen.SetActive(true);
            healBlack.SetActive(false);
        }
        else
        {
            healGreen.SetActive(false);
            healBlack.SetActive(true);
            healCoolBar.fillAmount = GameManager.GM.healCoolTime / GameManager.GM.setHealCoolTime;
        }
    }
}
