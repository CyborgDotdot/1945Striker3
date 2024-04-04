using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPower : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Player player;

    void Start()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        GetComponent<Rigidbody2D>().velocity = randomDirection * moveSpeed;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos;

        // 오브젝트가 카메라 경계에 닿았을 때 처리
        if (viewPos.x <= 0 || viewPos.x >= 1 || viewPos.y <= 0 || viewPos.y >= 1)
        {
            // 현재 속도 벡터의 방향과 노말 벡터를 이용하여 반사 벡터를 계산
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            Vector2 normal = GetReflectionNormal(viewPos);
            Vector2 reflection = Vector2.Reflect(velocity, normal).normalized;

            // 반사 벡터를 새로운 속도로 설정
            GetComponent<Rigidbody2D>().velocity = reflection * moveSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.PowerUp();
            Destroy(gameObject);
        }
    }

    // 벽과 충돌할 때의 반사 법선 벡터를 계산하는 함수
    Vector2 GetReflectionNormal(Vector3 viewPos)
    {
        float x = Mathf.Clamp01(viewPos.x);
        float y = Mathf.Clamp01(viewPos.y);

        // 뷰포트 경계에 따라 충돌한 면을 확인하여 법선 벡터를 반환
        if (x == 0) return new Vector2(1, 0); // 왼쪽
        if (x == 1) return new Vector2(-1, 0); // 오른쪽
        if (y == 0) return new Vector2(0, 1); // 아래쪽
        if (y == 1) return new Vector2(0, -1); // 위쪽

        return Vector2.zero;
    }
}