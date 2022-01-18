using UnityEngine;
using WorldOfEronia.Movement;
using WorldOfEronia.Core;
namespace WorldOfEronia.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        Transform target;
        float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.position);
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

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.transform;
        }
        public void Cancel()
        {
            target = null;
        }

        
        void Hit() 
        {
            // Animation Event
        }
    }
}
