using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet1 : MonoBehaviour
{
    public float speed = 4.0f;

    void Update()
    {
        //미사일 위쪽방향으로 움직이기
        //위의 방향 * 스피드 * 타임
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //화면밖으로 나갈 경우
    private void OnBecameInvisible()
    {
        //자기 자신 지우기
        Destroy(gameObject);
    }
}
