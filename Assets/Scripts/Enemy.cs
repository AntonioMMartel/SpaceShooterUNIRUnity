using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector3 linearVelocity = Vector3.left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(linearVelocity * speed * Time.deltaTime);
        if (transform.position.x < -2.9)
        {
            linearVelocity = Vector3.right;
        }
        if (transform.position.x > 3)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerShot"))
        {
            Destroy(gameObject);
        }
    }
}
