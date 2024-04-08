using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    Animator ani;
    public Transform pos;
    public GameObject[] bullets; // 배열로 bullet 저장
    private int currentBulletIndex = 0; // 현재 사용 중인 bullet의 인덱스

    public GameObject lazer;
    public float gValue = 0;
    public Image Gage;

    void Start()
    {
        ani = GetComponent<Animator>();
        pos = transform;
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
            Instantiate(bullets[currentBulletIndex], pos.position, Quaternion.identity);
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            gValue += Time.deltaTime;
            Gage.fillAmount = gValue;

            if(gValue >= 1)
            {
                GameObject go = Instantiate(lazer, pos.position, Quaternion.identity);
                Destroy(go, 1);
                gValue = 0;
            }
        }

        else
        {
            gValue -= Time.deltaTime;
            if(gValue <= 0)
            {
                gValue = 0;
            }
            Gage.fillAmount = gValue;
        }

        transform.Translate(moveX, moveY, 0);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos;
    }

    public void TakeDamage(int damage)
    {
        GameManager.instance.TakeDamage(damage);
    }

    public void PowerUp()
    {
        currentBulletIndex++;

        if (currentBulletIndex < bullets.Length)
        {
            GameManager.instance.PowerUpEffect(transform.position);
        }

        if (currentBulletIndex >= bullets.Length)
        {
            currentBulletIndex = bullets.Length - 1;
        }
    }
}