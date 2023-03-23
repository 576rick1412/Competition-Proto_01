using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image hpBar;

    Enemy enemy;
    void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<Enemy>();
    }

    void Update()
    {
        hpBar.fillAmount = enemy._HP / enemy.setHP;
    }
}
