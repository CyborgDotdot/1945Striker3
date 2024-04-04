using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    private int currentHealth;

    Animator ani;
    public static Transform pos;
    public GameObject[] bullets; // 배열로 bullet 저장
    private int currentBulletIndex = 0; // 현재 사용 중인 bullet의 인덱스
    private Monster monster;

    public GameObject effect;
    public GameObject powerUpEffect;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    bool isFiring = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        pos = transform;
        currentHealth = maxHealth;
        monster = FindObjectOfType<Monster>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        //-1 0 1
        if (Input.GetAxis("Horizontal") <= -0.5f)
            ani.SetBool("left", true);
        else
            ani.SetBool("left", false);

        if (Input.GetAxis("Horizontal") >= 0.5f)
            ani.SetBool("right", true);
        else
            ani.SetBool("right", false);

        if (Input.GetAxis("Vertical") >= 0.5f)
            ani.SetBool("up", true);
        else
            ani.SetBool("up", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 프리팹 반복 생성
            isFiring = true;
            StartCoroutine(FireBullets());
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //프리팹 생성 중단
            isFiring = false;
            StopCoroutine(FireBullets());
        }

        transform.Translate(moveX, moveY, 0);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>(); // 충돌한 오브젝트로부터 Monster 컴포넌트를 가져옴
            if (monster != null)
            {
                monster.TakeDamage(3);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            GameObject Effect = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(Effect, 1);

            Destroy(gameObject);
        }
    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    IEnumerator FireBullets()
    {
        while (isFiring) // isFiring 플래그가 true인 동안에만 반복
        {
            // 현재 인덱스의 총알 프리팹 생성
            Instantiate(bullets[currentBulletIndex], pos.position, Quaternion.identity);
            // 지정된 간격만큼 대기
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void PowerUp()
    {
        currentBulletIndex++;

        if (currentBulletIndex < bullets.Length)
        {
            GameObject Effect = Instantiate(powerUpEffect, transform.position, Quaternion.identity);
            Effect.transform.parent = pos;
            Destroy(Effect, 1);
        }

        if (currentBulletIndex >= bullets.Length)
        {
            currentBulletIndex = bullets.Length - 1;
        }


    }
}