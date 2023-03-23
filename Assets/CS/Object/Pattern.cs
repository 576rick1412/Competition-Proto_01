using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [Header("�⺻ ����")]
    public GameObject[] bullets;    // ������ �Ѿ� ����
    /*
    public float bulletSpeed;       // �Ѿ� �ӵ�
    public int bulletDamege;        // �Ѿ� ������

    [Header("��� ����")]
    public string patternType;      // ������ ���� ����

    public int makeTimes;           // ���� ���� Ƚ��
    public int makeBulletCount;     // 1ȸ�� ������ �Ѿ� ����

    public float makeDelay;         // ���� ���� ���� ������
    public int setBullet;           // ���Ͽ��� ����� �Ѿ� ��ȣ ����
    public int rotDir;              // �Ѿ� ȸ�� ����
    public int angle;               // ����
    */
    public float desTime;           // ���� �ð�

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
