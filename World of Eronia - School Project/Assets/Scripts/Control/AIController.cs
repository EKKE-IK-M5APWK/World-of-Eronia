using System.Collections;
using System.Collections.Generic;
using WorldOfEronia.Combat;
using WorldOfEronia.Core;
using WorldOfEronia.Movement;

using UnityEngine;
using System;

namespace WorldOfEronia.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float catchDistance = 5f;
        [SerializeField] float holdPositionTime = 5f;
        [SerializeField] PatrolController patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float waitTime = 2f;
        Fighter AI;
        Move move;
        Health health;
        GameObject player;
        Vector3 basePosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        int currentWayPointIndex = 0;

        private void Start()
        {
            AI = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            move = GetComponent<Move>();
            basePosition = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) { return; }
            if (InAttackRangeOfPlayer() && AI.CanAttackTarget(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < holdPositionTime)
            {
                HoldPositionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = basePosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypont();
            }
            if (timeSinceArrivedAtWaypoint > waitTime) move.StartMoveAction(nextPosition);

        }

        private Vector3 GetCurrentWaypont()
        {
            return patrolPath.GetWaypoint(currentWayPointIndex);
        }

        private void CycleWaypoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWaypoint()
        {
            float distance = Vector3.Distance(transform.position, GetCurrentWaypont());
            return distance < wayPointTolerance;
        }

        private void HoldPositionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            AI.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) < catchDistance;
        }

        /// <summary>
        /// Callback to draw gizmos only if the object is selected.
        /// </summary>
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, catchDistance);
        }
    }
}

