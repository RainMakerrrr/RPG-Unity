using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 6f;
    [SerializeField] private float _speedSmoothTime = 0.1f;
    
    private CharacterController _characterController;
    private Animator _animator;
    private Transform _mainCamera = null;


    private float _gravity = 4f;
    private float _speedSmoothVelocity;
    private float _currentSpeed;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
       
        _mainCamera = Camera.main.transform;

    }


    private void Update()
    {
       
        
        Move();
        
    }

    private void Move()
    {
        Vector2 movementInput = new Vector2(
                 Input.GetAxisRaw("Horizontal"),
                 Input.GetAxisRaw("Vertical")
             ).normalized;

        Vector3 forward = _mainCamera.forward;
        Vector3 right = _mainCamera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
        Vector3 gravityVector = Vector3.zero;

        if(!_characterController.isGrounded)
        {
            gravityVector.y -= _gravity;
        }

        if(desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
        }
        float targetSpeed = _movementSpeed * movementInput.magnitude;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _speedSmoothVelocity, _speedSmoothTime);

        _characterController.Move(gravityVector * Time.deltaTime);
        _characterController.Move(desiredMoveDirection * _currentSpeed * Time.deltaTime);
        _animator.SetFloat("speed", 3f * movementInput.magnitude, _speedSmoothTime, Time.deltaTime);
    }
}
