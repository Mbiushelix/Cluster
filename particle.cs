using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    public Color color;
    public Collider2D colliderObject;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D gameObject_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
        float velocity = Random.Range(0,main.init_vel);
        float mass = Random.Range(1f, 5f);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        gameObject.transform.localScale = new Vector2(mass / 2.5f, mass / 2.5f);
        gameObject_rigidbody.velocity = main.GenerateRandomVector(velocity);
        gameObject_rigidbody.mass = mass;
        color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0);
        sprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 70 || this.transform.position.x < -70)
        {
            Destroy(this);
        }

        if (this.transform.position.y > 30 || this.transform.position.y < -30)
        {
            Destroy(this);
        }
    }
}
