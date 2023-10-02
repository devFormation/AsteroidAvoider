using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _forseMagnitude;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _speedRotation;


    private Vector3 _movementDirection;
    private Camera _mainCamera;
    private Rigidbody _rb;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_movementDirection == Vector3.zero) return;

        _rb.AddForce(_movementDirection * _forseMagnitude, ForceMode.Force);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxVelocity);
    }

    private void Update()
    {
        ProcessInput();

        KeepPlayerOnScreen();

        RotateToFaceVelocity();
    }

    private void RotateToFaceVelocity()
    {
        if (_rb.velocity == Vector3.zero) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.back);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speedRotation * Time.deltaTime);


    }
    
    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewPortPosition =_mainCamera.WorldToViewportPoint(transform.position);

        if (viewPortPosition.x > 1) newPosition.x = -newPosition.x + 0.1f;
        if (viewPortPosition.y > 1) newPosition.y = -newPosition.y + 0.1f;
        if (viewPortPosition.x < 0) newPosition.x = -newPosition.x - 0.1f;
        if (viewPortPosition.y < 0) newPosition.y = -newPosition.y - 0.1f;

        transform.position = newPosition;
    }


    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

            _movementDirection = transform.position - worldPosition;
            _movementDirection.z = 0f;
            _movementDirection.Normalize();
        }
        else
        {
            _movementDirection = Vector3.zero;
        }
    }
}
