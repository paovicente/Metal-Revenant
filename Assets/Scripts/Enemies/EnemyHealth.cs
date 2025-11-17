using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int CurrentHealth;

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        Debug.Log("Enemy has been hitted! life left" + CurrentHealth);

        if (CurrentHealth <=0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy defeated");
        gameObject.SetActive(false);
    }

}
