using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Boss : MonoBehaviour
{
    #region field
    int flag = 1;
    public float speed = 0.5f;

    public Transform ms1;
    public Transform ms2;
    public Transform ms3;
    public Transform ms4;
    public Transform ms5;

    public GameObject bullet;

    public GameObject item;

    public int monsterHP = 100;
    private Player player;

    public GameObject effect;
    #endregion

    void Start()
    {
        StartCoroutine(BossMissle());//�ڷ�ƾ ����
        StartCoroutine(UniqueMissle());//�ڷ�ƾ ����
        StartCoroutine(CircleFire());//�ڷ�ƾ ����


        player = FindObjectOfType<Player>();
    }

    IEnumerator BossMissle()
    {
        while(true)
        {
            Instantiate(bullet, ms1.position, Quaternion.identity);
            Instantiate(bullet, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator UniqueMissle()
    {
        // �߻� ���� ����
        float interval = 0.1f; // �߻� ������ 0.05�ʷ� ����
                                // �߻� ���� ����
        int count = 10; // �� 20���� �߻�ü�� ����
                        // �ݿ��� �̷�� �߻� ���� ����
        float halfCircle = 180f;

        while (true)
        {
            // ���ʿ��� ���������� �ݿ� ���·� �߻�
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bullet, ms3.position, Quaternion.identity);
                // �ݿ� ���·� �߻��ϱ� ���� ���� ���
                float angle = (halfCircle / (count - 1)) * i; // �߻� ������ ����Ͽ� �� �߻�ü�� ����
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = -Mathf.Sin(angle * Mathf.Deg2Rad);
                // �߻�ü �̵� ���� ����
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                // ª�� ������ �ΰ� ���� �߻�ü ����
                yield return new WaitForSeconds(interval);
            }

            // �����ʿ��� �������� �ݿ� ���·� �߻�
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bullet, ms4.position, Quaternion.identity);
                // �ݿ� ���·� �߻��ϱ� ���� ���� ���(�����ʿ��� �������� �߻��ϱ� ���� ���� ����)
                float angle = 180 - (halfCircle / (count - 1)) * i; // �߻� ������ ����Ͽ� �� �߻�ü�� ����
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = -Mathf.Sin(angle * Mathf.Deg2Rad);
                // �߻�ü �̵� ���� ����
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                // ª�� ������ �ΰ� ���� �߻�ü ����
                yield return new WaitForSeconds(interval);
            }

            // 3�� �������� ��ƾ �ݺ�
            yield return new WaitForSeconds(3f - count * interval * 2); // �߻� �ð��� ����Ͽ� ��� �ð� ����
        }
    }


    //�� �������� �̻��� �߻�
    IEnumerator CircleFire()
    {
        //�����ֱ�
        float attackRate = 3;
        //�߻�ü ��������
        int count = 20;
        //�߻�ü ������ ����
        float internalAngle = 360 / count;
        //���ߵǴ� ����(�׻� ���� ��ġ�� �߻���� �ʵ��� ����)
        float WeightAngle = 0f;

        //�� ���·� ����ϴ� �߻�ü ����
        while (true)
        {
            for(int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bullet,ms5.position, Quaternion.identity);
                //�߻�ü �̵� ����(����)
                float angle = WeightAngle + internalAngle * i;
                //�߻�ü �̵� ����(����)
                //Cos(����)���� ������ ���� ǥ���� ���� pi / 180�� ����
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //Sin(����)���� ������ ���� ǥ���� ���� PI / 100�� ����
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //�߻�ü �̵� ����
                clone.GetComponent<BossBullet>().Move(new Vector2(x,y));
            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            WeightAngle += 1;
            //3�ʸ��� �̻��� �߻�
            yield return new WaitForSeconds(attackRate);
        }
    }

    void Update()
    {
        // Main Camera�κ��� ����Ʈ�� �ϴ� ���ʰ� ��� ������ �ڳ��� ���� ��ǥ�� ���մϴ�.
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // ������Ʈ�� ���� ��ġ�� �����ɴϴ�.
        Vector3 pos = transform.position;

        // ������Ʈ�� ȭ�� ������ ������ �ʵ��� x�� y ��ǥ�� �����մϴ�.
        pos.x = Mathf.Clamp(pos.x, minScreenBounds.x, maxScreenBounds.x);
        pos.y = Mathf.Clamp(pos.y, minScreenBounds.y, maxScreenBounds.y);

        // ���ѵ� ��ġ�� ������Ʈ�� �̵���ŵ�ϴ�.
        transform.position = pos;

        // ������Ʈ�� ȭ�� �����ڸ��� �����ϸ� ������ �ٲߴϴ�.
        if (transform.position.x >= maxScreenBounds.x || transform.position.x <= minScreenBounds.x)
        {
            flag *= -1;
        }

        if (transform.position.y >= 5f || transform.position.y <= 3f)
        {
            flag *= -1;
        }

        // ������Ʈ�� �̵���ŵ�ϴ�.
        transform.Translate(flag * speed * Time.deltaTime, flag * speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(3);
        }
    }

    public void TakeDamage(int damage)
    {
        if (this == null) return;
        monsterHP -= damage;

        if (monsterHP <= 0)
        {
            GameObject Effect = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(Effect, 1);

            ItemDrop();

            Destroy(gameObject);
        }
    }

    public void ItemDrop()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}