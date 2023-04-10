using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [Header("ü��")]
    public Image hpBar;

    [Header("����")]
    public Image oilBar;

    [Header("����")]
    public TextMeshProUGUI scoreTMP;

    [Header("��ź ��ų")]
    public TextMeshProUGUI bombTMP;
    public GameObject bombGreen;
    public GameObject bombBlack;
    public Image bombCoolBar;

    [Header("ȸ�� ��ų")]
    public TextMeshProUGUI healTMP;
    public GameObject healGreen;
    public GameObject healBlack;
    public Image healCoolBar;

    [Header("���� ����")]
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
        // ü��
        hpBar.fillAmount = player._HP / player.setHP;

        // ����
        oilBar.fillAmount = player._Oil / player.setOil;

        // ����
        scoreTMP.text = GameManager.GM.CommaText(GameManager.GM.score);

        // ���� ����
        atkTMP.text = GameManager.GM.CommaText((uint)GameManager.GM.attackUpgradeCount + 1);

        // ��ź ��ų
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

        // �� ��ų
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
