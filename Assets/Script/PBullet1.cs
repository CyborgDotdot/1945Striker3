using UnityEngine;

public class PBullet1 : MonoBehaviour
{
    public float speed = 4.0f;
    private Monster monster;

    void Start()
    {
        monster = FindObjectOfType<Monster>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            monster.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
