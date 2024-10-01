using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWaypointPosition(i), 0.4f);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
            }
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i)
        {
            return ((i + 1) == transform.childCount) ? 0 : i + 1;
        }
    }

}
