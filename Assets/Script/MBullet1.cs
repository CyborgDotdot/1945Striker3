using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBullet1 : MonoBehaviour
{
    public float speed = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
