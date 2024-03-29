using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowCamera : MonoBehaviour
{
    public static GameObject target;
    public UnityEvent TargetNulled;

    [Header("Configurar en el editor")]
    public float smoothing;
    public Vector2 XYLimit;

    [Header("Configuraciones dinámicas")]
    public float camZ;
    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 goal;

        if (target == null)
        {
            goal = new Vector3(-30.0f, -7.0f, 0.0f);
        }
        else
        {
            goal = target.transform.position;
            if (target.tag == "Cannonball")
            {
                bool sleeping = target.GetComponent<Rigidbody>().IsSleeping();
                if (sleeping)
                {
                    target = null;
                    TargetNulled.Invoke();
                    //goal = new Vector3(-30.0f, -10.0f, 0.0f);
                    GameManager.Blocked = false;
                    return;
                }
            }
        }

        goal.x = Mathf.Max(XYLimit.x, goal.x);
        goal.y = Mathf.Max(XYLimit.y, goal.y);
        goal = Vector3.Lerp(transform.position, goal, smoothing);
        goal.z = camZ;
        
        transform.position = goal;
        Camera.main.orthographicSize = goal.y + 20;
    }
}
