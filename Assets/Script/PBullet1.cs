using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet1 : MonoBehaviour
{
    public float speed = 4.0f;

    void Update()
    {
        //�̻��� ���ʹ������� �����̱�
        //���� ���� * ���ǵ� * Ÿ��
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //ȭ������� ���� ���
    private void OnBecameInvisible()
    {
        //�ڱ� �ڽ� �����
        Destroy(gameObject);
    }
}
