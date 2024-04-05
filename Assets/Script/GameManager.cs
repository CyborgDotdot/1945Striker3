using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxHealth = 10;
    private int currentHealth;
    public GameObject[] healthImages;

    public GameObject playerEffect;

    public SpriteRenderer playerSpriteRenderer;
    private Color originalColor;

    public GameObject powerUpEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        if (playerSpriteRenderer != null)
        {
            originalColor = playerSpriteRenderer.color; // ���� ���� �� ���� ���� ����
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            UpdateHealthUI(); // �÷��̾ �������� ���� �� Health UI ������Ʈ
        }

        Debug.Log("Current Health: " + currentHealth);

        if (playerSpriteRenderer != null)
        {
            StartCoroutine(FlashCoroutine());
        }
    }
    IEnumerator FlashCoroutine()
    {
        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = originalColor;
    }

    public void GameOver()
    {
        GameObject playerObject = GameObject.Find("Player"); // Player ������Ʈ�� �̸��� "Player"��� �����մϴ�.
        if (playerObject != null)
        {
            GameObject effect = Instantiate(playerEffect, playerObject.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            // Player ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            playerObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. Player ������Ʈ�� �̸��� ��Ȯ���� Ȯ�����ּ���.");
        }
        Debug.Log("Game Over!");

        foreach (GameObject healthImage in healthImages)
        {
            healthImage.SetActive(false);
        }
        // ���⿡ ���� ���� ȭ���� ���ų�, ���� ��ȯ�ϴ� ���� �߰����� ������ ������ �� �ֽ��ϴ�.
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
        // ���⿡ UI ������Ʈ ���� �߰� ����
        Debug.Log("Healed. Current Health: " + currentHealth);
    }

    // Health UI�� ������Ʈ�ϴ� �޼���
    void UpdateHealthUI()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            // ���� Health���� �ε����� ���ų� ���� �̹����� Ȱ��ȭ, �׷��� ������ ��Ȱ��ȭ
            healthImages[i].SetActive(i < currentHealth);
        }
    }

    public void PowerUpEffect(Vector3 position)
    {

        GameObject effect = Instantiate(powerUpEffect, position, Quaternion.identity);
        Destroy(effect, 1); // 1�� �Ŀ� �ı�
    }

}