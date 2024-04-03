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
        //한번 호출
        Invoke("CreateBullet", Delay);
    }

    void CreateBullet()
    {
        GameObject bullet1 = Instantiate(bullet, ms1.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bullet, ms2.position, Quaternion.identity);

        bullet1.transform.parent = ms1;
        bullet2.transform.parent = ms2;

        //재귀호출
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        //아래쪽방향으로 움직이기
        //아래 방향 * 스피드 * 타임
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
