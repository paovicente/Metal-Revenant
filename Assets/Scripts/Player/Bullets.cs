using UnityEngine;

public class Bullets : MonoBehaviour
{ 
    [SerializeField] private float lifeTime = 1f;
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
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}


