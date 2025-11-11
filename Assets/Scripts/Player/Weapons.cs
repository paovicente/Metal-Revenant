using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    private float offset = 1f;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private InputActionReference shootAction;

    [Header("Bullet settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 1f;
    public static int bulletDamage = 10;

    private void Start()
    {
        if (shootAction != null)
        {
            shootAction.action.Enable();
            shootAction.action.started += HandleShootInput;
        }
        else
        {
            Debug.LogError("Shoot Action no está asignado en PlayerShoot");
        }
    }

    private void OnDestroy()
    {
        if (shootAction != null)
        {
            shootAction.action.started -= HandleShootInput;
        }
    }

    private void HandleShootInput(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();

        bullet.transform.position = new Vector3(
            firePoint.parent.position.x,
            firePoint.parent.position.y
        );
        bullet.transform.rotation = firePoint.parent.rotation;

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.linearVelocity = Vector2.left * bulletSpeed;

        Vector2 shootDirection = playerRenderer.flipX ? Vector2.left : Vector2.right;
        bulletRigidbody.linearVelocity = shootDirection * bulletSpeed;
    }
}


    



