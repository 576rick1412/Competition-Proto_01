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
    }
}
