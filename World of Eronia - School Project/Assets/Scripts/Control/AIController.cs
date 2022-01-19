using System.Collections;
using System.Collections.Generic;
using WorldOfEronia.Combat;
using UnityEngine;

namespace WorldOfEronia.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float catchDistance = 5f;
        Fighter AI;
        GameObject player;
        private void Start()
        {
            AI = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
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
    }
}

