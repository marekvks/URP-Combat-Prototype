using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothTime = 0.01f;

    public static float DirectionMagnitude;

    private float _velocity;

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xInput, 0f, yInput).normalized;

        DirectionMagnitude = direction.magnitude;

        if (direction.magnitude >= 0.1f && !playerCombat.IsAttacking)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _velocity, smoothTime);
            Quaternion finalRotation = Quaternion.Euler(0f, smoothAngle, 0f);
            transform.rotation = finalRotation;

            Vector3 moveDirection = finalRotation * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}