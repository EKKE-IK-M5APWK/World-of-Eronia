using UnityEngine;
using WorldOfEronia.Movement;
using WorldOfEronia.Core;
namespace WorldOfEronia.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        private void Update()
        {
            if (target == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
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
    }
}
