using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{

    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStart;

    // Start is called before the first frame update
    void Start()
    {
        _gameStart = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetWaypointPosition(int Index)
    {
        return CurentPosition + Points[Index];
    }

    private void OnDrawGizmos()
    {

        if (!_gameStart && transform.hasChanged)
        {
            _currentPosition = transform.position;
        }

        for (int i=0; i<points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i]+ _currentPosition, radius: 0.5f);

            if (i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i]+ _currentPosition, points[i + 1]+ _currentPosition);
            }
        }
    }

}
