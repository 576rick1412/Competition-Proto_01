using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [Header("기본 설정")]
    public GameObject[] bullets;    // 생성할 총알 종류
    public float bulletSpeed;       // 총알 속도
    public int bulletDamege;        // 총알 데미지

    [Header("고급 설정")]
    public string patternType;      // 생성할 패턴 종류

    public int makeTimes;           // 패턴 생성 횟수
    public int makeBulletCount;     // 1회당 생성할 총알 개수

    public float makeDelay;         // 개별 패턴 생성 딜레이
    public int setBullet;           // 패턴에서 사용할 총알 번호 지정
    public int rotDir;              // 총알 회전 방향
    void Start()
    {
        switch(patternType)
        {
            case "Shot360":
                StartCoroutine(Shot360());
                break;

            case "MultiShot360":
                StartCoroutine(MultiShot360());
                break;

            case "LengthDiamond":
                StartCoroutine(LengthDiamond());
                break;

            case "WidthDiamond":
                StartCoroutine(WidthDiamond());
                break;

            case "MultiDiamond":
                StartCoroutine(MultiDiamond());
                break;

            case "DrawLine":
                StartCoroutine(DrawLine());
                break;

            case "RotLine":
                StartCoroutine(RotLine());
                break;

            case "TargetLine":
                StartCoroutine(TargetLine());
                break;

            case "TargetRound":
                StartCoroutine(TargetRound());
                break;

            case "StopTargetRound":
                StartCoroutine(StopTargetRound());
                break;

            case "FormShot":
                StartCoroutine(FormShot());
                break;
        }
    }
    
    void Update()
    {
        
    }

    public void PatternSetting(float speed = 1, int damege = 0, int rd = 1)
    {
        bulletSpeed = speed;
        bulletDamege = damege;
        rotDir = rd;
    }

    public void PatternSetting(string type = null, int times = 1, int count = 16, int setBullet = 0,float delay = 0.5f)
    {
        patternType = type;
        makeTimes = times;
        makeBulletCount = count;
        this.setBullet = setBullet;
        makeDelay = delay;
    }

    IEnumerator Shot360()
    {
        for (int i = 0; i < makeBulletCount; i++)
        {
            var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().BulletSetting(true, dmg: bulletDamege);

            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                bulletSpeed * Mathf.Cos(Mathf.PI * 2 * i / makeBulletCount),
                bulletSpeed * Mathf.Sin(Mathf.PI * 2 * i / makeBulletCount)
                ), ForceMode2D.Impulse);

            bullet.transform.Rotate(0f, 0f, 360 * i / makeBulletCount - 90);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator MultiShot360()
    {
        float speedPlus = 0;
        for (int i = 0; i < makeTimes; i++)
        {
            for (int j = 0; j < makeBulletCount; j++)
            {
                var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: bulletDamege);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (bulletSpeed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / makeBulletCount),
                    (bulletSpeed + speedPlus) * Mathf.Sin(Mathf.PI * 2 * j / makeBulletCount)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / makeBulletCount - 90);
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator LengthDiamond()
    {
        float speedPlus = 0;
        for (int i = 0; i < makeTimes; i++)
        {
            for (int j = 0; j < makeBulletCount; j++)
            {
                var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: bulletDamege);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (bulletSpeed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / makeBulletCount),
                    (bulletSpeed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / makeBulletCount)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / makeBulletCount);
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator WidthDiamond()
    {
        float speedPlus = 0;
        for (int i = 0; i < makeTimes; i++)
        {
            for (int j = 0; j < makeBulletCount; j++)
            {
                var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: bulletDamege);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (bulletSpeed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / makeBulletCount),
                    (bulletSpeed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / makeBulletCount)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / makeBulletCount);
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator MultiDiamond()
    {
        float speedPlus = 0;
        for (int i = 0; i < makeTimes; i++)
        {
            for (int j = 0; j < makeBulletCount; j++)
            {
                var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: bulletDamege);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (bulletSpeed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / makeBulletCount),
                    (bulletSpeed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / makeBulletCount)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / makeBulletCount - 90);

                //=========================================================================================

                bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: bulletDamege);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (bulletSpeed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / makeBulletCount),
                    (bulletSpeed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / makeBulletCount)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / makeBulletCount);
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator DrawLine()
    {
        for (int i = 0; i < makeTimes; i++)
        {
            for (int j = 0; j < 360; j += 10)
            {
                for (int k = 0, rot = 0; k < makeBulletCount; k++, rot += 360 / makeBulletCount)
                {
                    var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.Euler(0f, 0f, (j + rot) * rotDir));
                    bullet.GetComponent<Bullet>().BulletSetting(spd: bulletSpeed, dmg: bulletDamege);
                }
                yield return new WaitForSeconds(makeDelay);
            }
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator RotLine()
    {
        float plus = 0f;
        for (int i = 0; i < makeTimes * 2; i++)
        {
            for (int j = 0; j < makeBulletCount * 10; j += 10)
            {
                for (int k = 0, rot = 0; k < makeBulletCount; k++, rot += 360 / makeBulletCount)
                {
                    var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.Euler(0f, 0f, (j + rot) * rotDir));
                    bullet.GetComponent<Bullet>().BulletSetting(spd: bulletSpeed + plus, dmg: bulletDamege);
                }
                yield return new WaitForSeconds(makeDelay);
            }

            rotDir *= -1;
            plus += makeDelay * 3;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator TargetLine()
    {
        Transform player = GameObject.Find("Player").transform;

        Vector2 dir = (player.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < makeTimes; i++)
        {
            var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.Euler(0, 0, z - 90));
            bullet.GetComponent<Bullet>().BulletSetting(spd: bulletSpeed, dmg: bulletDamege);

            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator TargetRound()
    {
        Transform player = GameObject.Find("Player").transform;

        float rad = 1f;
        for (int i = 0; i < makeTimes; i++)
        {
            for (int j = 0; j < 360; j += 360 / makeBulletCount)
            {
                Vector3 pos = transform.position + 
                    new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * rad, Mathf.Sin(j * Mathf.Deg2Rad) * rad, 0);

                Vector2 dir = (player.position - pos).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                var bullet =  Instantiate(bullets[setBullet], pos, Quaternion.Euler(0, 0, z - 90));
                bullet.GetComponent<Bullet>().BulletSetting(spd: bulletSpeed, dmg: bulletDamege);
            }
            rad += 2f;

            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator StopTargetRound()
    {
        List<Bullet> tempBullets = new List<Bullet>();

        Transform player = GameObject.Find("Player").transform;

        float rad = 1f;
        for (int j = 0; j < 360; j += 360 / makeBulletCount)
        {
            Vector3 pos = transform.position +
                new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * rad, Mathf.Sin(j * Mathf.Deg2Rad) * rad, 0);

            Vector2 dir = (player.position - pos).normalized;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var bullet = Instantiate(bullets[setBullet], pos, Quaternion.Euler(0, 0, z - 90));
            bullet.GetComponent<Bullet>().BulletSetting(isForce: true, spd: bulletSpeed, dmg: bulletDamege);
            tempBullets.Add(bullet.GetComponent<Bullet>());
        }

        foreach (var i in tempBullets)
        {
            i.isForceMove = false;
            yield return new WaitForSeconds(makeDelay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator FormShot()
    {
        float plus = 0f;
        float rad = 40f;

        Transform player = GameObject.Find("Player").transform;
        Vector2 dir = (player.position - transform.position).normalized;
        float dirZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < makeTimes; i++)
        {
            float amount = rad / (makeBulletCount - 1);
            float z = rad / -2f + (int)dirZ;

            for (int j = 0; j < makeBulletCount; j++)
            {
                var bullet = Instantiate(bullets[setBullet], transform.position, Quaternion.Euler(0, 0, z - 90));
                bullet.GetComponent<Bullet>().BulletSetting(spd: bulletSpeed + plus, dmg: bulletDamege);
                z += amount;
            }

            plus += 0.5f;
            makeBulletCount++;
        }

        Destroy(gameObject);
        yield return null;
    }
}
