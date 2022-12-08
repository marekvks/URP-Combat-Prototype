using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerAnimationHandler _playerAnimationHandler;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float speedTransition = 1f;
    [SerializeField] private float smoothTime = 0.1f;

    private float _dirMagnitude;
    private float _velocity;

    private float _targetSpeed;

    private void Awake()
    {
        currentSpeed = walkSpeed;
        _targetSpeed = currentSpeed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) RunTransition();
        else WalkTransition();

        currentSpeed = Mathf.Lerp(currentSpeed, _targetSpeed, speedTransition * Time.deltaTime);

        Movement();

        _playerAnimationHandler.UpdateLocomotion(_dirMagnitude, currentSpeed);
    }

    private void Movement()
    {
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xInput, 0f, yInput).normalized;
    
            _dirMagnitude = direction.magnitude;
    
            if (_dirMagnitude >= 0.1f && !playerCombat.IsAttacking)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _velocity, smoothTime);
                Quaternion finalRotation = Quaternion.Euler(0f, smoothAngle, 0f);
                transform.rotation = finalRotation;
    
                Vector3 moveDirection = finalRotation * Vector3.forward;
                controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
            }
    }

    public void RunTransition() => _targetSpeed = runSpeed;

    public void WalkTransition() => _targetSpeed = walkSpeed;
}