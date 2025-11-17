using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private InputActionReference shootAction;

    [Header("Shooting Settings")]
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime;

    private void OnEnable()
    {
        shootAction.action.performed += OnShootPerformed;
        shootAction.action.Enable();
    }

    private void OnDisable()
    {
        shootAction.action.performed -= OnShootPerformed;
        shootAction.action.Disable();
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPoolPlayer.Instance.GetBullet();

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
    }
}
