using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<GameObject> ProjectilePrefab;
    public List<GameObject> ProjectilePool;
    public List<GameObject> UsedPool;  // 리스트말고 큐로?
    public static ProjectileManager instance;

    void Awake()
    {
        instance = this;
        InitPooling(20);
    }

    public static ProjectileManager Getinstance()
    {
        return instance;
    }

    public void InitPooling(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject Projectile = Instantiate(GetPrefab(1), this.transform.position, Quaternion.identity);
            Projectile.transform.parent = this.gameObject.transform;
            Projectile.SetActive(false);
            ProjectilePool.Add(Projectile);
        }
    }

    public GameObject UsePoolObj(int number)
    {
        UsedPool.Add(ProjectilePool[number]);
        ProjectilePool.Remove(ProjectilePool[number]);
        return UsedPool[UsedPool.Count - 1];
    }

    public void ReturnPoolObj(int number)
    {
        ProjectilePool.Add(UsedPool[number]);
        UsedPool.Remove(UsedPool[number]);
    }

    public GameObject GetPrefab(int number)
    {
        return ProjectilePrefab[number];
    }
}
