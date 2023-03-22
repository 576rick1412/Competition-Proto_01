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
    }
}
