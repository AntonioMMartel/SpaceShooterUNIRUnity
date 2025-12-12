using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    Vector3 linearVelocity = Vector3.down;
    [SerializeField] float explosionDuration = 1f;

    [SerializeField] Animator animator;

    [SerializeField] GameObject scoreManager;
    
    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }
    private void HandleMove()
    {
        transform.Translate(linearVelocity * speed * Time.deltaTime);
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
    private bool isDying = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDying) return;
        if (other.CompareTag("PlayerShot") || other.CompareTag("Player"))
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            animator.SetTrigger("Explosion");
            Destroy(gameObject, explosionDuration);
            FindObjectOfType<ScoreManager>().AddScore(100);
        }
    }
}
