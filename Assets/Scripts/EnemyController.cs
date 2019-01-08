using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 0.03f;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        float theta = Random.value * Mathf.PI * 2;
        direction = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + (Vector3)(direction * speed));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Food")
        {
            Physics2D.IgnoreCollision(collision.collider, collider2D);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            float rotation = collision.gameObject.transform.rotation.eulerAngles.z;
            Vector2 normal = Vector2.down.Rotate(rotation);

            direction = direction - 2 * (Vector2.Dot(direction, normal)) * normal;
        }
    }
}
