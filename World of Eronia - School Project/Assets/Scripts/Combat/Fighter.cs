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
        float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if(target.IsDead()) return;
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
            if(timeSinceLastAttack > timeBetweenAttacks) {
                GetComponent<Animator>().SetTrigger("attackTrigger");
                timeSinceLastAttack = 0;
               
            }
            
        }

        // Animation Event
        void Hit() 
        {   
            target.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.GetComponent<Health>();;
        }
        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttackTrigger");
            target = null;

        }

        
    }
}
