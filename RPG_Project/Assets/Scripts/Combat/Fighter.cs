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
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;


        Health targetObject;
        float timeSinceLastAttack;

        private void Start()
        {
            SpawnWeapon(defaultWeapon);
        }

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
                GetComponent<Mover>().MoveTo(targetObject.transform.position, 1f);
            }
            else
            {
                AttackMethod();
                GetComponent<Mover>().Cancel();
            }
        }

        public void SpawnWeapon(Weapon weapon)
        {

            defaultWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            defaultWeapon.Spawn(rightHandTransform, leftHandTransform, animator);
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
            if(defaultWeapon.HasProjectile())
            {
                defaultWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, targetObject);
            }
            else
            {
                targetObject.TakeDamage(defaultWeapon.GetDamage());
            }

            
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < defaultWeapon.GetRange();
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
