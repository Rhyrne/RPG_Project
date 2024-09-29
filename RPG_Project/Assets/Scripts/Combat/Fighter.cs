using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField] float weaponRange;
        Transform targetObject;

        private void Update()
        {
            if (targetObject == null)
            {
                return;
            }
            bool isInRange = IsInRange();
            if (isInRange == false)
            {
                GetComponent<Mover>().MoveTo(targetObject.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.position) < weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            targetObject = target.transform;
        }

        public void Cancel()
        {
            targetObject = null;
        }
    }
}
