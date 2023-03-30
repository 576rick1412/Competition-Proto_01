using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageWaveUI : MonoBehaviour
{
    public TextMeshProUGUI stageTitleTMP;

    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI destroyTMP;
    public TextMeshProUGUI HPTMP;

    void Start()
    {
        stageTitleTMP.text = "< " + GameManager.GM.stageCount + " Stage >";

        scoreTMP.text = GameManager.GM.TenZeroCommaText(GameManager.GM.score);
        destroyTMP.text = GameManager.GM.TenZeroCommaText(GameManager.GM.destroyCount);
        HPTMP.text = GameManager.GM.TenZeroCommaText
            ((uint)GameObject.Find("Player").GetComponent<Airship>()._HP);
    }

    void Update()
    {
        
    }
}
