using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour, IHealthBar
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI healthCountText;
        [SerializeField] private Image healtLightBar;
        
        private static WaitForSeconds waitFor10Milliseconds;
        
        
        public void SetHealthBar(int health, int maxHealth)
        {
            if (healthBar.fillAmount > (float)health / maxHealth && healtLightBar)
                StartCoroutine(LightHealthController());
            healthBar.fillAmount = (float)health / maxHealth;

            healthCountText.text = health + "/" + maxHealth;
        }
        

        private IEnumerator LightHealthController()
        {
            if (healtLightBar.IsActive()) yield break;
            
            healtLightBar.gameObject.SetActive(true);
            
            float i = healthBar.fillAmount;
            healtLightBar.fillAmount = i;
            yield return new WaitForSeconds(0.1f);
            while (i > healthBar.fillAmount)
            {
                // Debug.Log("lol " + i);
                yield return waitFor10Milliseconds;
                healtLightBar.fillAmount = i;
                i -= 0.005f;
            }
            healtLightBar.gameObject.SetActive(false);
        }
        
    }
}