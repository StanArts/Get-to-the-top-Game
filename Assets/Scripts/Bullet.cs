using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletEffect;

    private Rigidbody2D bulletRigidbody;
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bulletRigidbody.velocity = new Vector2(bulletSpeed * transform.localScale.x, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_1")
        {
            FindObjectOfType<Game_Manager>().HurtP1();
            return;
        }
        
        if (collision.tag == "Player_2")
        {
            FindObjectOfType<Game_Manager>().HurtP2();
            return;
        }

        Instantiate(bulletEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
