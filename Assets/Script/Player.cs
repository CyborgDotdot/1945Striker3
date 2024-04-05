using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    Animator ani;
    public static Transform pos;
    public GameObject[] bullets; // �迭�� bullet ����
    private int currentBulletIndex = 0; // ���� ��� ���� bullet�� �ε���

    bool isFiring = false;

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
            // ������ �ݺ� ����
            isFiring = true;
            StartCoroutine(FireBullets());
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //������ ���� �ߴ�
            isFiring = false;
            StopCoroutine(FireBullets());
        }

        transform.Translate(moveX, moveY, 0);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//�ٽÿ�����ǥ�� ��ȯ
        transform.position = worldPos;
    }

    public void TakeDamage(int damage)
    {
        GameManager.instance.TakeDamage(damage);
    }

    IEnumerator FireBullets()
    {
        while (isFiring) // isFiring �÷��װ� true�� ���ȿ��� �ݺ�
        {
            // ���� �ε����� �Ѿ� ������ ����
            Instantiate(bullets[currentBulletIndex], pos.position, Quaternion.identity);
            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(0.2f);
        }
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