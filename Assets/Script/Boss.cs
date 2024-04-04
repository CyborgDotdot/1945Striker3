using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Boss : MonoBehaviour
{
    #region field
    public float speed = 0.1f;
    public float Delay1 = 0.1f;
    public float Delay2 = 0.5f;
    public float Delay3 = 1.0f;

    public Transform ms1;
    public Transform ms2;
    public Transform ms3;
    public Transform ms4;
    public Transform ms5;

    public GameObject bossBullet;
    public GameObject bullet;
    public GameObject homing;

    public GameObject item;

    public int monsterHP = 3;
    private Player player;

    public GameObject effect;
    #endregion

    void Start()
    {
        //�ѹ� ȣ��
        Invoke("CreateBullet", Delay1);
        Invoke("CreateHoming", Delay2);
        Invoke("CreateCircle", Delay3);

        player = FindObjectOfType<Player>();
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //���ȣ��
        Invoke("CreateBullet", Delay1);
    }
    void CreateHoming()
    {
        Instantiate(homing, ms3.position, Quaternion.identity);
        Instantiate(homing, ms4.position, Quaternion.identity);

        //���ȣ��
        Invoke("CreateHoming", Delay2);
    }
    void CreateCircle()
    {
        int bulletCount = 24; // �Ѿ��� ����
        float angleStep = 360f / bulletCount; // �Ѿ� ���� ���� ����

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 direction = rotation * Vector3.right;
            Vector3 spawnPosition = transform.position + direction * 0.1f; // ������

            Instantiate(bossBullet, spawnPosition, rotation);
        }
        Invoke("CreateCircle", Delay3);
    }

    void Update()
    {
        //�Ʒ��ʹ������� �����̱�
        //�Ʒ� ���� * ���ǵ� * Ÿ��
        transform.Translate(Vector2.down * speed * Time.deltaTime);
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