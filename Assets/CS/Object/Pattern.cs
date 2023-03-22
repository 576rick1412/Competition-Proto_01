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

    public void Start_Shot360(int count, int bulletType, float speed, int damage)
    {
        StartCoroutine(Shot360(count, bulletType, speed, damage));
    }
    public void Start_MultiShot360(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(MultiShot360(times, count, bulletType, delay, speed, damage));
    }
    public void Start_LengthDiamond(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(LengthDiamond(times, count, bulletType, delay, speed, damage));
    }
    public void Start_WidthDiamond(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(WidthDiamond(times, count, bulletType, delay, speed, damage));
    }
    public void Start_MultiDiamond(int times, int count, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(MultiDiamond(times, count, bulletType, delay, speed, damage));
    }
    public void Start_DrawLine(int times, int count, int bulletType, float delay, int rotDir, float speed, int damage)
    {
        StartCoroutine(DrawLine(times, count, bulletType, delay, rotDir, speed, damage));
    }
    public void Start_RotLine(int times, int count, int bulletType, float delay, int rotDir, float speed, int damage)
    {
        StartCoroutine(RotLine(times, count, bulletType, delay, rotDir, speed, damage));
    }
    public void Start_TargetLine(int times, int bulletType, float delay, float speed, int damage)
    {
        StartCoroutine(TargetLine(times, bulletType, delay, speed, damage));
    }
    public void Start_TargetRound(int times, int count, int bulletType, float delay, float rad, float speed, int damage)
    {
        StartCoroutine(TargetRound(times, count, bulletType, delay, rad, speed, damage));
    }
    public void Start_StopTargetRound(int count, int bulletType, float delay, float rad, float speed, int damage)
    {
        StartCoroutine(StopTargetRound(count, bulletType, delay, rad, speed, damage));
    }
    public void Start_FormShot(int times, int count, int bulletType, float angle, float speed, int damage)
    {
        StartCoroutine(FormShot(times, count, bulletType, angle, speed, damage));
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
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
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
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
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
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
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

                //=========================================================================================

                bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().BulletSetting(true, dmg: damage);

                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (speed + speedPlus) * Mathf.Tan(Mathf.PI * 2 * j / count),
                    (speed + speedPlus) * Mathf.Cos(Mathf.PI * 2 * j / count)
                    ), ForceMode2D.Impulse);

                bullet.transform.Rotate(0f, 0f, 360 * j / count);
            }

            speedPlus += 0.5f;
            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator DrawLine(int times, int count, int bulletType, float delay,int rotDir, float speed, int damage)
    {
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < 360; j += 10)
            {
                for (int k = 0, rot = 0; k < count; k++, rot += 360 / count)
                {
                    var bullet = Instantiate(bullets[bulletType], transform.position, Quaternion.Euler(0f, 0f, (j + rot) * rotDir));
                    bullet.GetComponent<Bullet>().BulletSetting(spd: speed, dmg: damage);
                }
                yield return new WaitForSeconds(delay);
            }
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator RotLine(int times, int count, int bulletType, float delay, int rotDir, float speed, int damage)
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
                }
                yield return new WaitForSeconds(delay);
            }

            rotDir *= -1;
            plus += delay * 3;
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

            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator TargetRound(int times, int count, int bulletType, float delay, float rad, float speed, int damage)
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
            }
            rad += 2f;

            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator StopTargetRound(int count, int bulletType, float delay, float rad, float speed, int damage)
    {
        List<Bullet> tempBullets = new List<Bullet>();

        Transform player = GameObject.Find("Player").transform;

        for (int j = 0; j < 360; j += 360 / count)
        {
            Vector3 pos = transform.position +
                new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * rad, Mathf.Sin(j * Mathf.Deg2Rad) * rad, 0);

            Vector2 dir = (player.position - pos).normalized;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var bullet = Instantiate(bullets[bulletType], pos, Quaternion.Euler(0, 0, z - 90));
            bullet.GetComponent<Bullet>().BulletSetting(isForce: true, spd: speed, dmg: damage);
            tempBullets.Add(bullet.GetComponent<Bullet>());
        }

        foreach (var i in tempBullets)
        {
            i.isForceMove = false;
            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator FormShot(int times, int count, int bulletType, float angle, float speed, int damage)
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
            }

            plus += 0.5f;
            count++;
        }

        Destroy(gameObject);
        yield return null;
    }
}
