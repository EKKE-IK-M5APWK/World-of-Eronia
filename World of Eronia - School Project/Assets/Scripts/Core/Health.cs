using UnityEngine;

namespace WorldOfEronia.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        private bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Death();
            }
            Debug.Log("Health:" + health);
        }
        private void Death()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("deathTrigger");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
