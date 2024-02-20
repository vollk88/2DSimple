using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units
{
    [Serializable]
    public class Health
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int health;
        [SerializeField, Range(0, 1)] private float armor;
        
        [HideInInspector] public bool IsDead;
        private IHealthBar _healthBar;


        public void SetEnableValues(IHealthBar uiManager = null)
        {
            health = maxHealth;

            if (uiManager is null) return;
			
            _healthBar = uiManager;
            _healthBar.SetHealthBar(health, maxHealth);

        }
        
        
        /// <summary> Восстановление хп не больше maxHealth.</summary>
        /// <param name="bonusHealth"></param>
        public void SetHealth(int bonusHealth)
        {
            health += bonusHealth;
			
            if (health > maxHealth)
                health = maxHealth;
            _healthBar?.SetHealthBar(health, maxHealth);
        }

        public void TakeHit(DamageStats damage)
        {
            float startArmor = armor;
            armor -= armor * damage.armorPenetration;
            damage.hitDamage -= (int)(damage.hitDamage * armor);
            armor = startArmor;
            health -= damage.hitDamage;
            IsDead = health <= 0;
            _healthBar?.SetHealthBar(health, maxHealth);
        }

        public override string ToString()
        {
            return health + "/" + maxHealth;
        }
    }
}