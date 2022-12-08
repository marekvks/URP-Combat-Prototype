using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _playerLayer;

    private Transform _player;

    private void Update()
    {
        _player = GetPlayer().transform;
    }

    private void HandleFOVDetection()
    {
    }

    private GameObject GetPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _playerLayer);
        GameObject player = null;
        if (colliders != null)
            player = colliders[0].gameObject;

        if (player == null) return null;

        if (Vector3.Angle(transform.position))

        return player;
    }
}
