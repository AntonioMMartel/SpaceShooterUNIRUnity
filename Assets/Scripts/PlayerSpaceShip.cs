using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float acceleration = 300f;

    [SerializeField] float fireRate = 0.5f;
    Boolean isShooting = false;

    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;

    [SerializeField] Sprite spriteLeft;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteRight;
    [SerializeField] Sprite spriteDefault;

    [SerializeField] GameObject[] lifes;
    private int remainingLifes = 3;
    [SerializeField] float invincibilityFrame = 1.0f;
    private float invincibilityTimer = 0f; // Temporizador de cuánto lleva el i-frame
    private bool isInvicible = false;

    Vector2 rawMove;
    Vector2 currentVelocity = Vector2.zero;

    [SerializeField] GameObject projectilePrefab;
    private void OnEnable()
    {
        invincibilityTimer = invincibilityFrame;

        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += StartShooting;
        shoot.action.canceled += StopShooting;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleShoot();
        HandleSprite();
        HandleIFrame();
    }

    private void HandleIFrame()
    {
        if (isInvicible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0f)
            {
                isInvicible = false;
                invincibilityTimer = invincibilityFrame;
            }
        }
    }

    private void HandleSprite()
    {
        if (rawMove.x < 0) // Jugador se mueve a la izquierda
        {
            spriteRenderer.sprite = spriteLeft;
        }
        if (rawMove.x > 0) // Derecha
        {
            spriteRenderer.sprite = spriteRight;
        }
        if(rawMove.x == 0) // Normal
        {
            spriteRenderer.sprite = spriteDefault;
        }
    }
    private float timeFromLastShot;
    private void HandleShoot()
    {
        if (isShooting)
        {
            timeFromLastShot += Time.deltaTime;

            if (timeFromLastShot > fireRate)
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                timeFromLastShot = 0;
            }

        }

    }

    private void HandleMove()
    {
        const float rawMoveThresholdForBraking = 0.05f;
        if (rawMove.magnitude < rawMoveThresholdForBraking) currentVelocity *= 0.1f * Time.deltaTime;
        // Vector de dirección hacia donde el jugador se mueve en función a la velocidad
        currentVelocity += rawMove * acceleration * Time.deltaTime;

        // Magnitud del vector = velocidad que está experimentando jugador ahora mismo
        float linearVelocity = currentVelocity.magnitude;

        // Recortamos magnitud
        linearVelocity = Mathf.Clamp(linearVelocity, 0, maxSpeed);

        // Pillamos dirección del jugador y le añadimos la velocidad que queremos
        currentVelocity = currentVelocity.normalized * linearVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);
    }

    private void OnDisable()
    {
        move.action.Disable();
        shoot.action.Disable();

        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        shoot.action.started -= StartShooting;
        shoot.action.canceled -= StopShooting;
    }
    private void StartShooting(InputAction.CallbackContext context)
    {
        isShooting = true;    
    }
    private void StopShooting(InputAction.CallbackContext context)
    {
        isShooting = false;
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyShot"))
        {
            if (isInvicible) return;

            if (remainingLifes > 0)
            {
                Destroy(lifes[remainingLifes - 1]);
                remainingLifes--;
                isInvicible = true;
                StartCoroutine(PlayerSpritePulse());
            }
            else
            {
                Destroy(gameObject);
                FindObjectOfType<GameManager>().GameIsOver();
            }
        }
    }

    IEnumerator PlayerSpritePulse()
    {
        while (isInvicible)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = true;
    }

}
