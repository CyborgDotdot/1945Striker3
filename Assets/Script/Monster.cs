using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 3.0f;
    public float Delay = 0.1f;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;

    void Start()
    {
        //�ѹ� ȣ��
        Invoke("CreateBullet", Delay);
    }

    void CreateBullet()
    {
        GameObject bullet1 = Instantiate(bullet, ms1.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bullet, ms2.position, Quaternion.identity);

        bullet1.transform.parent = ms1;
        bullet2.transform.parent = ms2;

        //���ȣ��
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        //�Ʒ��ʹ������� �����̱�
        //�Ʒ� ���� * ���ǵ� * Ÿ��
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
