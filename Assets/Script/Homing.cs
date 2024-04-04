using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    GameObject target; //�÷��̾�
    public float speed = 3.0f;
    public int damage;
    private Player player;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        player = FindObjectOfType<Player>();

        //�÷��̾ �±׷� ã��
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            //A - B -> A�� �ٶ󺸴� ����
            dir = target.transform.position - transform.position;
            //���� ���͸� ���ϱ�:�������� 1�� ũ��� �����
            dirNo = dir.normalized;
        }

        //����Ƽ�� ������ �Լ�
        //Vector3.MoveTowards()
    }

    void Update()
    {
        transform.Translate(dirNo * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
