using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float acceleration = 300f;

    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;

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

        shoot.action.started += OnShoot;
        shoot.action.performed += OnShoot;
        shoot.action.canceled += OnShoot;
    }

    // Update is called once per frame
    void Update()
    {
        const float rawMoveThresholdForBraking = 0.05f;
        if(rawMove.magnitude < rawMoveThresholdForBraking) currentVelocity *= 0.1f *Time.deltaTime;
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

        shoot.action.started -= OnShoot;
        shoot.action.performed -= OnShoot;
        shoot.action.canceled -= OnShoot;

    }

    private void OnShoot(InputAction.CallbackContext context)
    {
       Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }


}
