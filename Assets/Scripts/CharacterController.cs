﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    #region Inspector Fields
    #region Movement
    [SerializeField]                        float MovementSpeed;
    [SerializeField, Range(0, 1)]           float CrouchMovementModifier;
    #endregion

    #region Camera Controls
    [SerializeField]                        float MouseXSensitivity;
    [SerializeField]                        float MouseYSensitivity;
    #endregion
    #endregion


    Camera                  MainCamera;
    Vector2                 MovementDirection;
    Vector2                 AimDirection;
    float                   CameraPitch;
    bool                    isCrouching;


    // Start is called before the first frame update
    void Start()
    {
        MainCamera = gameObject.GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Update orientation first
        PerformAiming(AimDirection);

        // Then move
        PerformMovement(MovementDirection);
        PerformCrouching();
    }

    void PerformMovement(Vector2 direction)
    {
        if (direction.sqrMagnitude <= 0.01)
            return;

        // Walk in the same direction as I'm looking
        var movement = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        // Apply the movement tranformation
        gameObject.transform.position += movement * MovementSpeed * Time.deltaTime;
    }

    void PerformAiming(Vector2 rotation)
    {
        if (rotation.sqrMagnitude <= 0.01)
            return;

        // Rotate the player model, for the horizontal axis, as the camera is parented to this
        gameObject.transform.Rotate(Vector3.up, rotation.x * MouseXSensitivity * Time.deltaTime);
        // Only pitch the camera when we look up or down --
        //TODO: Look into head-only rotation to get the head to look up or down with the camera (Not that it's necessary with FPP)
        CameraPitch = Mathf.Clamp(CameraPitch - rotation.y * MouseYSensitivity * Time.deltaTime, -89, 89);

        MainCamera.transform.localEulerAngles = new Vector3(CameraPitch, 0, 0);
    }

    void PerformCrouching()
    {
        if (isCrouching)
        {
            
        }
        else
        {
            
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        AimDirection = context.ReadValue<Vector2>();
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValue<float>() > 0.015;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        
    }
}
