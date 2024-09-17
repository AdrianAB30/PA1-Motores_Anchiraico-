using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float closeEnoughDistance = 0.1f;

    [Header("Components")]
    private Rigidbody rbd;
    private int currentPatrolIndex = 0;

    private void Awake()
    {
        rbd = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        MoveToNextPatrolPoint();
    }
    private void FixedUpdate()
    {
        Patrol();
    }
    private void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 targetPosition = targetPoint.position;

        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 moveVelocity = direction * speed;

        rbd.velocity = moveVelocity;

        if (Vector3.Distance(transform.position, targetPosition) < closeEnoughDistance)
        {
            MoveToNextPatrolPoint();
        }
    }
    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}