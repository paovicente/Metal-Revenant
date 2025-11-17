using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Image healthBarFill;

    [Header("Damage Settings")]
    public float damageCooldown = 2f; // 2 second of invulnerability
    private float lastDamageTime = -10f; // so player can take damage at start

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    //public function to receive damage
    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return; // still in cooldown, ignore damage

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        lastDamageTime = Time.time; // reset cooldown
        
        Debug.Log("Player took damage: " + damage + ". Current health: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
            Die();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = fillAmount;

            if (fillAmount > 0.6f)
                healthBarFill.color = Color.green;   
            else if (fillAmount > 0.3f)
                healthBarFill.color = Color.yellow; 
            else
                healthBarFill.color = Color.red;     
        }

    }

    private void Die()
    {
        Debug.Log("Player died.");

        PlayerPrefs.SetString("GameResult", "Game Over");

        LevelManager.instance.LoadScene("ResultScene");

        Destroy(gameObject);
    }

}
