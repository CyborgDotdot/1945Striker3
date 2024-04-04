using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    GameObject target; //플레이어
    public float speed = 3.0f;
    public int damage;
    private Player player;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        player = FindObjectOfType<Player>();

        //플레이어를 태그로 찾기
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            //A - B -> A를 바라보는 벡터
            dir = target.transform.position - transform.position;
            //방향 벡터만 구하기:단위벡터 1의 크기로 만든다
            dirNo = dir.normalized;
        }

        //유니티로 구현된 함수
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
