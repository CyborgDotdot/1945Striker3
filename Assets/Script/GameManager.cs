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
            originalColor = playerSpriteRenderer.color; // 게임 시작 시 원래 색상 저장
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
            UpdateHealthUI(); // 플레이어가 데미지를 받을 때 Health UI 업데이트
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
        GameObject playerObject = GameObject.Find("Player"); // Player 오브젝트의 이름이 "Player"라고 가정합니다.
        if (playerObject != null)
        {
            GameObject effect = Instantiate(playerEffect, playerObject.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            // Player 오브젝트를 비활성화합니다.
            playerObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. Player 오브젝트의 이름이 정확한지 확인해주세요.");
        }
        Debug.Log("Game Over!");

        foreach (GameObject healthImage in healthImages)
        {
            healthImage.SetActive(false);
        }
        // 여기에 게임 오버 화면을 띄우거나, 씬을 전환하는 등의 추가적인 로직을 구현할 수 있습니다.
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
        // 여기에 UI 업데이트 로직 추가 가능
        Debug.Log("Healed. Current Health: " + currentHealth);
    }

    // Health UI를 업데이트하는 메서드
    void UpdateHealthUI()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            // 현재 Health보다 인덱스가 낮거나 같은 이미지는 활성화, 그렇지 않으면 비활성화
            healthImages[i].SetActive(i < currentHealth);
        }
    }

    public void PowerUpEffect(Vector3 position)
    {

        GameObject effect = Instantiate(powerUpEffect, position, Quaternion.identity);
        Destroy(effect, 1); // 1초 후에 파괴
    }

}