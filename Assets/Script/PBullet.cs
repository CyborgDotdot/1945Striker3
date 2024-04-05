using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f;
    public int damage;
    public GameObject effect;

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
            GameObject _effect = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(_effect, 1);
            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            GameObject _effect = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(_effect, 1);

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
