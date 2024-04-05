using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 3f;
    Vector2 vector2 = Vector2.down;
    public int damage = 2;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Move(Vector2 vector)
    {
        vector2 = vector;
    }

    void Update()
    {
        transform.Translate(vector2 * Speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}