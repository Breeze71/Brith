

using UnityEngine;

namespace V
{
    public interface IDamagable
    {
        public int HealthAmount {get; set; }

        public HealthSystem HealthSystem {get; set;}
        public HealthBarUI HealthBarUI{get; set;}

        public void TakeDamage(int damageAmount);
        public void Die();
    }
}
