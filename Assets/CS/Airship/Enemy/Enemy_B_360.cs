using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B_360 : Enemy
{
    public Shot360 shot360;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        StartCoroutine(StartPattern());
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator StartPattern()
    {
        yield return new WaitForSeconds(startDelay);

        for (; ; )
        {
            var pattern = Instantiate(patternObject, transform.position, Quaternion.identity);
            pattern.GetComponent<Pattern>().Start_Shot360(
                shot360.count,
                shot360.bulletType,
                shot360.speed,
                shot360.damage
                );

            yield return new WaitForSeconds(delay);
        }
    }
}
