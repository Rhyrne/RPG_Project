using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage = 10f;

        Health targetObject;
        float timeSinceLastAttack;

        private void Update()
        {
            
            timeSinceLastAttack += Time.deltaTime;

            if (targetObject == null)
            {
                return;
            }

            if(targetObject.IsDead() == true)
            {
                GetComponent<Animator>().ResetTrigger("Attack");
                Cancel();
                return;
            }

            if (IsInRange() == false)
            {
                GetComponent<Mover>().MoveTo(targetObject.transform.position);
            }
            else
            {
                AttackMethod();
                GetComponent<Mover>().Cancel();
            }
        }

        private void AttackMethod()
        {
            transform.LookAt(targetObject.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health healthToTest = GetComponent<Health>();
            return healthToTest != null && !healthToTest.IsDead();
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        void Hit()
        {
            if (targetObject == null)
            {
                return;
            }

            targetObject.TakeDamage(weaponDamage);
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < weaponRange;
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetObject = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            targetObject = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
    }
}
