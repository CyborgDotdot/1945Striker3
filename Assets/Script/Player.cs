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
    public GameObject[] bullets; // �迭�� bullet ����
    private int currentBulletIndex = 0; // ���� ��� ���� bullet�� �ε���
    private Monster monster;

    public GameObject effect;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

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
            //������ ��ġ ���� ����
            Instantiate(bullets[currentBulletIndex], pos.position, Quaternion.identity);
        }

        transform.Translate(moveX, moveY, 0);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//�ٽÿ�����ǥ�� ��ȯ
        transform.position = worldPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            monster.TakeDamage(3);
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
    public void PowerUp()
    {
        currentBulletIndex++;

        if (currentBulletIndex >= bullets.Length)
        {
            currentBulletIndex = bullets.Length - 1;
        }
    }
}