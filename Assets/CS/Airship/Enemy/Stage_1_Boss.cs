using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_1_Boss : Enemy
{
    // 공격 종류
    public FormShot         formShot;
    public MultiShot360     multiShot360;
    public LengthDiamond    lengthDiamond;
    public TargetRound      targetRound;
    public RotLine          rotLine;

    [Header("보스 체력바")]
    public Image bossHPBar;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        StartCoroutine(StartPattern());
    }

    protected override void Update()
    {
        // 전체
        if (GameManager.GM.isGameStop)
        {
            StopAllCoroutines();
            return;
        }

        Vector3 velo = Vector3.zero;

        transform.position =
            Vector3.SmoothDamp(transform.position, endPos, ref velo, 0.05f);

        // 보스 전용
        bossHPBar.fillAmount = _HP / setHP;
    }

    IEnumerator StartPattern()
    {
        yield return new WaitForSeconds(startDelay);

        for (; ; )
        {
            int x = Random.Range(0, 5);

            GameObject pattern;

            switch (x)
            {
                case 0:
                    pattern = Instantiate(patternObject, transform.position, Quaternion.identity);
                    pattern.GetComponent<Pattern>().Start_FormShot(
                        formShot.times,
                        formShot.count,
                        formShot.bulletType,
                        formShot.speed,
                        formShot.damage,
                        formShot.angle
                        );
                    break;

                case 1:
                    pattern = Instantiate(patternObject, transform.position, Quaternion.identity);
                    pattern.GetComponent<Pattern>().Start_MultiShot360(
                        multiShot360.times,
                        multiShot360.count,
                        multiShot360.bulletType,
                        multiShot360.delay,
                        multiShot360.speed,
                        multiShot360.damage
                        );
                    break;

                case 2:
                    pattern = Instantiate(patternObject, transform.position, Quaternion.identity);
                    pattern.GetComponent<Pattern>().Start_LengthDiamond(
                        lengthDiamond.times,
                        lengthDiamond.count,
                        lengthDiamond.bulletType,
                        lengthDiamond.delay,
                        lengthDiamond.speed,
                        lengthDiamond.damage
                        );
                    break;

                case 3:
                    pattern = Instantiate(patternObject, transform.position, Quaternion.identity);
                    pattern.GetComponent<Pattern>().Start_TargetRound(
                        targetRound.times,
                        targetRound.count,
                        targetRound.bulletType,
                        targetRound.delay,
                        targetRound.speed,
                        targetRound.damage,
                        targetRound.rad
                        );
                    break;

                case 4:
                    pattern = Instantiate(patternObject, transform.position, Quaternion.identity);
                    pattern.GetComponent<Pattern>().Start_RotLine(
                        rotLine.times,
                        rotLine.count,
                        rotLine.bulletType,
                        rotLine.delay,
                        rotLine.speed,
                        rotLine.damage,
                        rotLine.rotDir
                        );
                    break;
            }
            Debug.Log(x);
            yield return new WaitForSeconds(delay);
        }
    }

    protected override void hpDie()
    {
        base.hpDie();

        foreach(var i in FindObjectsOfType<Pattern>())
        {
            foreach(var j in i.GetComponent<Pattern>().bulletList)
            {
                j.isSelfDes = true;
            }
            i.StopAllCoroutines();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = 3000;
        }
    }
}
