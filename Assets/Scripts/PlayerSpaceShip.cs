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


    Vector2 rawMove;
    Vector2 currentVelocity = Vector2.zero;

    [SerializeField] GameObject projectilePrefab;
    private void OnEnable()
    {
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
    }

    private void HandleSprite()
    {
        if (rawMove.x < 0) // Jugador se mueve a la izquierda
        {
            spriteRenderer.sprite = spriteLeft;
        }
        if (rawMove.x > 0)
        {
            spriteRenderer.sprite = spriteRight;
        }
        if(rawMove.x == 0) 
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
    private Coroutine shootingCoroutine = null;
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
          
        }
    }


}
