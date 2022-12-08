using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private int _magnitudeHash = Animator.StringToHash("Magnitude");
    private int _speedHash = Animator.StringToHash("Speed");


    public void UpdateLocomotion(float magnitude, float speed)
    {
        _animator.SetFloat(_magnitudeHash, magnitude);
        _animator.SetFloat(_speedHash, speed);
    }

    public void UpdateAttack(int comboTriggerHash)
    {
        _animator.SetTrigger(comboTriggerHash);
    }
}
