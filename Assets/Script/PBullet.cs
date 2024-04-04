using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f;
    public int damage;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
            else
            {
                Boss boss = other.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
