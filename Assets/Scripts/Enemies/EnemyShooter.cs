using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    private float nextFireTime = 0f;

    [Header("Detection")]
    [SerializeField] private float detectionRangeX = 5f;  
    [SerializeField] private float detectionRangeY = 8f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float deltaX = Mathf.Abs(player.position.x - transform.position.x);
        float deltaY = player.position.y - transform.position.y;

        if (deltaX <= detectionRangeX && deltaY <= detectionRangeY) //&& deltaY >= 0f)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = firePoint.position;

        Vector2 direction = (player.position - firePoint.position).normalized;
        bullet.GetComponent<Bullet>().Shoot(direction);
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.red;

        Vector3 center = transform.position + new Vector3(0, detectionRangeY / 3f, 0);

        Vector3 size = new Vector3(detectionRangeX * 2f, detectionRangeY*3f, 0f);

        Gizmos.DrawWireCube(center, size);
    }
}
