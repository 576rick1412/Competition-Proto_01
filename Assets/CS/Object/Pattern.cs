using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [Header("기본 설정")]
    public GameObject[] bullets;    // 생성할 총알 종류
    /*
    public float bulletSpeed;       // 총알 속도
    public int bulletDamege;        // 총알 데미지

    [Header("고급 설정")]
    public string patternType;      // 생성할 패턴 종류

    public int makeTimes;           // 패턴 생성 횟수
    public int makeBulletCount;     // 1회당 생성할 총알 개수

    public float makeDelay;         // 개별 패턴 생성 딜레이
    public int setBullet;           // 패턴에서 사용할 총알 번호 지정
    public int rotDir;              // 총알 회전 방향
    public int angle;               // 각도
    */
    public float desTime;           // 자폭 시간

    List<Bullet> bulletList = new List<Bullet>();

    void Start()
    {
        /*
        switch (patternType)
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
        */
    }

    public void Start_Shot360(                     int count, int bulletType,              float speed, int damage)
    {
        StartCoroutine(Shot360(count, bulletType, speed, damage));
    }
    public void Start_MultiShot360(     int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(MultiShot360(times, count, bulletType, delay, speed, damage));
    }
    public void Start_LengthDiamond(    int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(LengthDiamond(times, count, bulletType, delay, speed, damage));
    }
    public void Start_WidthDiamond(     int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(WidthDiamond(times, count, bulletType, delay, speed, damage));
    }
    public void Start_MultiDiamond(     int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(MultiDiamond(times, count, bulletType, delay, speed, damage));
    }
    public void Start_DrawLine(         int times, int count, int bulletType, float delay, float speed, int damage, int rotDir)
    {
        StartCoroutine(DrawLine(times, count, bulletType, delay, speed, damage, rotDir));
    }
    public void Start_RotLine(          int times, int count, int bulletType, float delay, float speed, int damage, int rotDir)
    {
        StartCoroutine(RotLine(times, count, bulletType, delay, speed, damage, rotDir));
    }
    public void Start_TargetLine(       int times,            int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(TargetLine(times, bulletType, delay, speed, damage));
    }
    public void Start_TargetRound(      int times, int count, int bulletType, float delay, float speed, int damage, float rad)
    {
        StartCoroutine(TargetRound(times, count, bulletType, delay, speed, damage, rad));
    }
    public void Start_StopTargetRound(             int count, int bulletType, float delay, float speed, int damage, float rad)
    {
        StartCoroutine(StopTargetRound(count, bulletType, delay, speed, damage, rad));
    }
    public void Start_FormShot(         int times, int count, int bulletType,              float speed, int damage, float angle)
    {
        StartCoroutine(FormShot(times, count, bulletType, speed, damage, angle));
    }

    void Update()
    {
        
    }

    IEnumerator Shot360(int count, int bulletType,float speed,int damage)
    {
        for (int i = 0; i < count; i++)
        {
            var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                speed * Mathf.Cos(Mathf.PI * 2 * i / count),
                speed * Mathf.Sin(Mathf.PI * 2 * i / count)
                ), ForceMode2D.Impulse);

            bullet.transform.Rotate(0f, 0f, 360 * i / count - 90);

            bulletList.Add(bullet.GetComponent<Bullet>());
        }

        yield return new WaitForSeconds(desTime);

        foreach(var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator MultiShot360(int times, int count, int bulletType,float delay, float speed, int damage)
    {
        float speedPlus = 0;
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < count; j++)
            {
                var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (speed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / count),
                    (speed + speedPlus) * Mathf.Sin(Mathf.PI * 2 * j / count)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / count - 90);

                bulletList.Add(bullet.GetComponent<Bullet>());
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator LengthDiamond(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        float speedPlus = 0;
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < count; j++)
            {
                var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (speed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / count),
                    (speed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / count)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / count);

                bulletList.Add(bullet.GetComponent<Bullet>());
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator WidthDiamond(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        float speedPlus = 0;
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < count; j++)
            {
                var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (speed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / count),
                    (speed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / count)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / count);

                bulletList.Add(bullet.GetComponent<Bullet>());
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator MultiDiamond(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        float speedPlus = 0;
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < count; j++)
            {
                var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (speed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / count),
                    (speed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / count)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / count - 90);

                bulletList.Add(bullet.GetComponent<Bullet>());

                //=========================================================================================

                bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (speed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / count),
                    (speed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / count)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / count);

                bulletList.Add(bullet.GetComponent<Bullet>());
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator DrawLine(int times, int count, int bulletType, float delay, float speed, int damage, int rotDir)
    {
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < 360; j += 10)
            {
                for (int k = 0, rot = 0; k < count; k++, rot += 360 / count)
                {
                    var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.Euler(0f, 0f, (j + rot) * rotDir));
                    bullet.GetComponent<Bullet>().BulletSetting(spd: speed, dmg: damage);

                    bulletList.Add(bullet.GetComponent<Bullet>());
                }
                yield return new WaitForSeconds(delay);
            }
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator RotLine(int times, int count, int bulletType, float delay, float speed, int damage, int rotDir)
    {
        float plus = 0f;
        for (int i = 0; i < times * 2; i++)
        {
            for (int j = 0; j < count * 10; j += 10)
            {
                for (int k = 0, rot = 0; k < count; k++, rot += 360 / count)
                {
                    var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.Euler(0f, 0f, (j + rot) * rotDir));
                    bullet.GetComponent<Bullet>().BulletSetting(spd: speed + plus, dmg: damage);

                    bulletList.Add(bullet.GetComponent<Bullet>());
                }
                yield return new WaitForSeconds(delay);
            }

            rotDir *= -1;
            plus += delay * 3;
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator TargetLine(int times, int bulletType, float delay, float speed, int damage)
    {
        Transform player = GameObject.Find("Player").transform;

        Vector2 dir = (player.position - transform.position).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < times; i++)
        {
            var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.Euler(0, 0, z - 90));
            bullet.GetComponent<Bullet>().BulletSetting(spd: speed, dmg: damage);

            bulletList.Add(bullet.GetComponent<Bullet>());

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator TargetRound(int times, int count, int bulletType, float delay, float speed, int damage, float rad)
    {
        Transform player = GameObject.Find("Player").transform;

        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < 360; j += 360 / count)
            {
                Vector3 pos = transform.position + 
                    new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * rad, Mathf.Sin(j * Mathf.Deg2Rad) * rad, 0);

                Vector2 dir = (player.position - pos).normalized;
                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                var bullet =  Instantiate(bullets[bulletType], pos, Quaternion.Euler(0, 0, z - 90));
                bullet.GetComponent<Bullet>().BulletSetting(spd: speed, dmg: damage);

                bulletList.Add(bullet.GetComponent<Bullet>());
            }
            rad += 2f;

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator StopTargetRound(int count, int bulletType, float delay, float speed, int damage, float rad)
    {
        Transform player = GameObject.Find("Player").transform;

        for (int j = 0; j < 360; j += 360 / count)
        {
            Vector3 pos = transform.position +
                new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * rad, Mathf.Sin(j * Mathf.Deg2Rad) * rad, 0);

            Vector2 dir = (player.position - pos).normalized;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var bullet = Instantiate(bullets[bulletType], pos, Quaternion.Euler(0, 0, z - 90));
            bullet.GetComponent<Bullet>().BulletSetting(isForce: true, spd: speed, dmg: damage);
            bulletList.Add(bullet.GetComponent<Bullet>());
        }

        foreach (var i in bulletList)
        {
            i.isForceMove = false;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator FormShot(int times, int count, int bulletType,  float speed, int damage, float angle)
    {
        float plus = 0f;

        Transform player = GameObject.Find("Player").transform;
        Vector2 dir = (player.position - transform.position).normalized;
        float dirZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < times; i++)
        {
            float amount = angle / (count - 1);
            float z = angle / -2f + (int)dirZ;

            for (int j = 0; j < count; j++)
            {
                var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.Euler(0, 0, z - 90));
                bullet.GetComponent<Bullet>().BulletSetting(spd: speed + plus, dmg: damage);
                z += amount;

                bulletList.Add(bullet.GetComponent<Bullet>());
            }

            plus += 0.2f;
            count++;
        }

        yield return new WaitForSeconds(desTime);

        foreach (var i in bulletList)
        {
            i.isSelfDes = true;
        }

        Destroy(gameObject);
        yield return null;
    }
}
