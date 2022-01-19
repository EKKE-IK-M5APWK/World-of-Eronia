using UnityEngine;
using WorldOfEronia.Movement;
using WorldOfEronia.Core;
namespace WorldOfEronia.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                AttackTrigger();
                timeSinceLastAttack = 0;
            }
        }

        private void AttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("attackTrigger");
            GetComponent<Animator>().SetTrigger("attackTrigger");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.GetComponent<Health>(); ;
        }
        public bool CanAttackTarget(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Cancel()
        {
            StopAttackTrigger();
            target = null;

        }

        private void StopAttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("stopAttackTrigger");
            GetComponent<Animator>().SetTrigger("stopAttackTrigger");
        }

    }
}
