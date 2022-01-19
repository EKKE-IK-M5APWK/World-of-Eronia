using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldOfEronia.Control
{

    public class PatrolController : MonoBehaviour
    {
        const float gizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), gizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }
        }

        public int GetNextIndex(int i)
        {
            return (i + 1) == transform.childCount ? 0 : i + 1;
        }
        public Vector3 GetWaypoint(int i)
        {
            return transform.transform.GetChild(i).position;
        }
    }
}
