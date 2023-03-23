using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B : Enemy
{
    public FormShot formShot;

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
            pattern.GetComponent<Pattern>().Start_FormShot(
                formShot.times,
                formShot.count,
                formShot.bulletType,
                formShot.speed,
                formShot.damage,
                formShot.angle);
            
            yield return new WaitForSeconds(delay);
        }
    }
}
