using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<GameObject> ProjectilePrefab;
    public List<GameObject> ProjectileObj;
    public static ProjectileManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject Projectile = Instantiate(GetPrefab(1), this.transform.position, Quaternion.identity);
            Projectile.transform.parent = this.gameObject.transform;
            Projectile.SetActive(false);
            ProjectileObj.Add(Projectile);
        }
    }

    public static ProjectileManager Getinstance()
    {
        return instance;
    }

    public GameObject GetPrefab(int number)
    {
        return ProjectilePrefab[number];
    }
}
