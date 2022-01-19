using System.Collections;
using System.Collections.Generic;
using WorldOfEronia.Combat;
using WorldOfEronia.Core;
using UnityEngine;

namespace WorldOfEronia.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float catchDistance = 5f;
        Fighter AI;
        Health health;
        GameObject player;
        private void Start()
        {
            AI = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
            if (health.IsDead()) { return; }
            if (InAttackRangeOfPlayer() && AI.CanAttackTarget(player))
            {
                // Debug.Log(gameObject.name + " will move towards the player. Position:" + gameObject.transform.position);
                GetComponent<Fighter>().Attack(player);
            }
            else
            {
                AI.Cancel();
            }
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
        }
    }
}

