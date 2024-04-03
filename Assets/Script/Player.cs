using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    Animator ani;
    public Transform pos = null;
    public GameObject bullet;

    void Start()
    {
        ani = GetComponent<Animator>();

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
            //프리팹 위치 방향 생성
            GameObject pBullet = Instantiate(bullet,pos.position, Quaternion.identity);
            pBullet.transform.parent = pos;
        }

        transform.Translate(moveX, moveY, 0);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos;
    }
}
