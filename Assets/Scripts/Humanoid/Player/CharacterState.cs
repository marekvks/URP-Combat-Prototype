using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum State
    {
        Idle,
        Walk,
        Fight
    }

    public static State CurrentState;

    private void Start()
    {
        CurrentState = State.Idle;
    }

    private void Update()
    {
        if (PlayerController.DirectionMagnitude >= 0.1f && CurrentState != State.Fight)
            CurrentState = State.Walk;
        else if (PlayerController.DirectionMagnitude < 0.1f && CurrentState != State.Fight)
            CurrentState = State.Idle;

        HandleAnimations();
    }

    private void HandleAnimations()
    {
        animator.SetFloat("Magnitude", PlayerController.DirectionMagnitude);
    }
}
