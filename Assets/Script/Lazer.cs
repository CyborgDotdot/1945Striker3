using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject effect;
    Transform pos;
    int Attack = 10;

    void Start()
    {
        pos = GameObject.Find("Player").GetComponent<Player>().pos;
    }

    void Update()
    {
        transform.position = pos.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(Attack++);
            GameObject go = Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(go, 1);
        }

        if(collision.tag == "Boss")
        {
            GameObject go = Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(go, 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(Attack++);
            GameObject go = Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(go, 1);
        }

        if (collision.tag == "Boss")
        {
            GameObject go = Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(go, 1);
        }
    }
}
