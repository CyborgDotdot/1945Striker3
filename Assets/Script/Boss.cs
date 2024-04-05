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
        StartCoroutine(BossMissle());//코루틴 실행
        StartCoroutine(UniqueMissle());//코루틴 실행
        StartCoroutine(CircleFire());//코루틴 실행


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
        // 발사 간격 설정
        float interval = 0.1f; // 발사 간격을 0.05초로 설정
                                // 발사 갯수 설정
        int count = 10; // 총 20개의 발사체를 생성
                        // 반원을 이루는 발사 각도 설정
        float halfCircle = 180f;

        while (true)
        {
            // 왼쪽에서 오른쪽으로 반원 형태로 발사
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bullet, ms3.position, Quaternion.identity);
                // 반원 형태로 발사하기 위해 각도 계산
                float angle = (halfCircle / (count - 1)) * i; // 발사 각도를 계산하여 각 발사체에 적용
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = -Mathf.Sin(angle * Mathf.Deg2Rad);
                // 발사체 이동 방향 설정
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                // 짧은 간격을 두고 다음 발사체 생성
                yield return new WaitForSeconds(interval);
            }

            // 오른쪽에서 왼쪽으로 반원 형태로 발사
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bullet, ms4.position, Quaternion.identity);
                // 반원 형태로 발사하기 위해 각도 계산(오른쪽에서 왼쪽으로 발사하기 위해 각도 조정)
                float angle = 180 - (halfCircle / (count - 1)) * i; // 발사 각도를 계산하여 각 발사체에 적용
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = -Mathf.Sin(angle * Mathf.Deg2Rad);
                // 발사체 이동 방향 설정
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                // 짧은 간격을 두고 다음 발사체 생성
                yield return new WaitForSeconds(interval);
            }

            // 3초 간격으로 루틴 반복
            yield return new WaitForSeconds(3f - count * interval * 2); // 발사 시간을 고려하여 대기 시간 조정
        }
    }


    //원 방향으로 미사일 발사
    IEnumerator CircleFire()
    {
        //공격주기
        float attackRate = 3;
        //발사체 생성갯수
        int count = 20;
        //발사체 사이의 각도
        float internalAngle = 360 / count;
        //가중되는 각도(항상 같은 위치로 발사되지 않도록 설정)
        float WeightAngle = 0f;

        //원 형태로 방사하는 발사체 생성
        while (true)
        {
            for(int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bullet,ms5.position, Quaternion.identity);
                //발사체 이동 방향(각도)
                float angle = WeightAngle + internalAngle * i;
                //발사체 이동 방향(벡터)
                //Cos(각도)라디안 단위의 각도 표현을 위해 pi / 180을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //Sin(각도)라디안 단위의 각도 표현을 위해 PI / 100을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //발사체 이동 방향
                clone.GetComponent<BossBullet>().Move(new Vector2(x,y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            WeightAngle += 1;
            //3초마다 미사일 발사
            yield return new WaitForSeconds(attackRate);
        }
    }

    void Update()
    {
        // Main Camera로부터 뷰포트의 하단 왼쪽과 상단 오른쪽 코너의 월드 좌표를 구합니다.
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // 오브젝트의 현재 위치를 가져옵니다.
        Vector3 pos = transform.position;

        // 오브젝트가 화면 밖으로 나가지 않도록 x와 y 좌표를 제한합니다.
        pos.x = Mathf.Clamp(pos.x, minScreenBounds.x, maxScreenBounds.x);
        pos.y = Mathf.Clamp(pos.y, minScreenBounds.y, maxScreenBounds.y);

        // 제한된 위치로 오브젝트를 이동시킵니다.
        transform.position = pos;

        // 오브젝트가 화면 가장자리에 도달하면 방향을 바꿉니다.
        if (transform.position.x >= maxScreenBounds.x || transform.position.x <= minScreenBounds.x)
        {
            flag *= -1;
        }

        if (transform.position.y >= 5f || transform.position.y <= 3f)
        {
            flag *= -1;
        }

        // 오브젝트를 이동시킵니다.
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