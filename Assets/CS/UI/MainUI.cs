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

    [Header("��ź ����")]
    public TextMeshProUGUI bombTMP;

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

        // ��ź ����
        bombTMP.text = GameManager.GM.CommaText(GameManager.GM.bombCount);

        // ���� ����
        atkTMP.text = GameManager.GM.CommaText((uint)GameManager.GM.attackUpgradeCount + 1);
    }
}
