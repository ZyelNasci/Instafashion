using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWayPoint : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    public Transform [] points;

    [SerializeField]
    private Color lineColor = Color.green;
    [SerializeField]
    private Color pointColor = Color.red;


#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = pointColor;
            Gizmos.DrawWireSphere(points[i].position, radius);
            Gizmos.color = lineColor;
            if (i< points.Length - 1)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(points[i].position, points[0].position);
            }
        }
        
    }
#endif
}
