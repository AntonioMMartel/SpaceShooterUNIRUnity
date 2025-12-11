using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector3 linearVelocity = Vector3.left;
    [SerializeField] float explosionDuration = 1f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 2f;

    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleShoot();
    }

    private float timeFromLastShot;
    private void HandleShoot()
    {
        timeFromLastShot += Time.deltaTime;

        if (timeFromLastShot > fireRate)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            timeFromLastShot = 0;
        }

    }
    private void HandleMove()
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
            animator.SetTrigger("Explosion");
            Destroy(gameObject, explosionDuration);
        }
    }
}
