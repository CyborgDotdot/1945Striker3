using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Monster : MonoBehaviour
{
    #region field
    public float speed = 3.0f;
    public float Delay = 0.1f;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject item;

    private int monsterHP = 3;
    private Player player;

    public GameObject effect;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    #endregion

    void Start()
    {
        //한번 호출
        Invoke("CreateBullet", Delay);
        player = FindObjectOfType<Player>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //재귀호출
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        //아래쪽방향으로 움직이기
        //아래 방향 * 스피드 * 타임
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
        monsterHP -= damage;
        StartCoroutine(FlashRed());
        if (monsterHP <= 0)
        {
            GameObject Effect = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(Effect, 1);

            Instantiate(item, ms1.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}