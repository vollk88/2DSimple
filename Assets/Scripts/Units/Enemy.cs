using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class Enemy : AUnit, IHealthBar
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI healthCountText;
        [SerializeField] private Image healtLightBar;
        public void SetHealthBar(int health, int maxHealth)
        {
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }
}