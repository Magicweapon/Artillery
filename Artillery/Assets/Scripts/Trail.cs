using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [Header("Configurar en el editor")]
    public float minDistanceBetweenPoints;

    private LineRenderer line;
    private GameObject _lineTarget;
    private List<Vector3> points;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }
    private void FixedUpdate()
    {
        if (_lineTarget == null)
        {
            if (FollowCamera.target != null)
            {
                if (FollowCamera.target.tag == "CannonBall")
                {
                    lineTarget = FollowCamera.target;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        AddPoint();
    }
    public void AddPoint()
    {
        Vector3 point = _lineTarget.transform.position;
        if (points.Count > 0 && (point - LastPoint).magnitude < minDistanceBetweenPoints)
        {
            return;
        }
        points.Add(point);
        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, LastPoint);
        line.enabled = true;
    }

    public void SetNullTarget()
    {
        lineTarget = null;
    }
    public GameObject lineTarget
    {
        get { return _lineTarget; }
        set
        {
            _lineTarget = value;
            if (_lineTarget != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public Vector3 LastPoint
    {
        get
        {
            if (points == null)
            {
                return Vector3.zero;
            }
            return points[points.Count - 1];
        }
    }
}
