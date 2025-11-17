using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance; 

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private Transform parentPosition;

    private List<GameObject> bullets;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        bullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab,parentPosition);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject bulletInstance = Instantiate(bulletPrefab, parentPosition);
        bulletInstance.SetActive(true);
        bullets.Add(bulletInstance);
        return bulletInstance;
    }
    public void ReturnBullet(GameObject bulletToReturn) 
    {
        bulletToReturn.SetActive(false);
    }

}
