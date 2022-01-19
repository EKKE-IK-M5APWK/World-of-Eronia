using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldOfEronia.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float catchDistance = 5f;
        private void Update()
        {

            if (DistanceToPlayer() < catchDistance)
            {
                Debug.Log(gameObject.name + " will move towards the player. Position:" + gameObject.transform.position);
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}

