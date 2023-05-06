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

    public GameObject lineTarget
    {
        get { return _lineTarget; }
        set
        {
            _lineTarget = value;
        }
    }
}
