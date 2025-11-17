using UnityEngine;

public class Bullets : MonoBehaviour
{ 
  
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private int damage = 10;

    private float timer;

    private void OnEnable()
    {
        timer = lifeTime;
    }
    
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //aca despues hay que poner lo del daño al enemigo y cuando es q vuelve al pool de objetos la bala
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        BulletPool.Instance.ReturnBullet(gameObject);
    }
    
}


