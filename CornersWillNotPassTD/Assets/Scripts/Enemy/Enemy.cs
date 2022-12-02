using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static Action <Enemy> OnEndReached;

    [SerializeField] private float moveSpeed = 3f;


    public float MoveSpeed { get; set; }
    public WayPoint WayPoint { get; set; }


    public Vector3 CurrentWaypointPosition => WayPoint.GetWaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;
    private Vector3 _lustPointPosition;

    private EnemyHealth _enemyHealth;
    [SerializeField]private GameObject _enemyVisual;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;
        _lustPointPosition = transform.position;
        _enemyVisual = transform.Find("Visual").gameObject;
    }

    private void Update()
    {
        Move();
        Rotate();


        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentWaypointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate()
    {
        if (CurrentWaypointPosition.x > _lustPointPosition.x)
        {
            _enemyVisual.transform.localScale = new Vector3(1f,1,1);
        }
        else
        {
            _enemyVisual.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentWaypointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lustPointPosition = transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = WayPoint.Points.Length - 1;
        if(_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        // The Same thing

        // if (OnEndReached != null)
        // {
        //     OnEndReached.Invoke();
        // }
        _enemyHealth.ResetHealth();
        ObgectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
      //  _enemyHealth.ResetHealth();
     //   ResumeMovement();
    }

}
