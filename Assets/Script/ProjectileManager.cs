using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public List<GameObject> ProjectilePrefab;
    public static ProjectileManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
