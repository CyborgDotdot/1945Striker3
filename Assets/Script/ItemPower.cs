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
        viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//�ٽÿ�����ǥ�� ��ȯ
        transform.position = worldPos;

        // ������Ʈ�� ī�޶� ��迡 ����� �� ó��
        if (viewPos.x <= 0 || viewPos.x >= 1 || viewPos.y <= 0 || viewPos.y >= 1)
        {
            // ���� �ӵ� ������ ����� �븻 ���͸� �̿��Ͽ� �ݻ� ���͸� ���
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            Vector2 normal = GetReflectionNormal(viewPos);
            Vector2 reflection = Vector2.Reflect(velocity, normal).normalized;

            // �ݻ� ���͸� ���ο� �ӵ��� ����
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

    // ���� �浹�� ���� �ݻ� ���� ���͸� ����ϴ� �Լ�
    Vector2 GetReflectionNormal(Vector3 viewPos)
    {
        float x = Mathf.Clamp01(viewPos.x);
        float y = Mathf.Clamp01(viewPos.y);

        // ����Ʈ ��迡 ���� �浹�� ���� Ȯ���Ͽ� ���� ���͸� ��ȯ
        if (x == 0) return new Vector2(1, 0); // ����
        if (x == 1) return new Vector2(-1, 0); // ������
        if (y == 0) return new Vector2(0, 1); // �Ʒ���
        if (y == 1) return new Vector2(0, -1); // ����

        return Vector2.zero;
    }
}