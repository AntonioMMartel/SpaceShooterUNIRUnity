using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float lifetime = 10f;

    [SerializeField] Animator animator;

    private Boolean hasImpacted = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasImpacted) transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasImpacted = true;
            animator.SetTrigger("Impact");
            Destroy(gameObject, 0.5f);
        }

    }
}
