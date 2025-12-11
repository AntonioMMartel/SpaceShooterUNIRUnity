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

    [SerializeField] GameObject barrel1;
    [SerializeField] GameObject barrel2;

    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Shooting");
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
            Instantiate(projectilePrefab, barrel1.transform.position, Quaternion.identity);
            Instantiate(projectilePrefab, barrel2.transform.position, Quaternion.identity);
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
