using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectile : Skill
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = true;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = 0;

        if (other.gameObject.CompareTag("Wall"))
            numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (GameObject effect in EffectsOnCollision)
            {
                GameObject instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                
                if (!UseWorldSpacePosition) 
                    instance.transform.parent = transform;

                if (UseFirePointRotation) 
                { 
                    instance.transform.LookAt(transform.position); 
                }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) 
                { 
                    instance.transform.rotation = Quaternion.Euler(rotationOffset); 
                }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }
                Destroy(instance, DestroyTimeDelay);
            }
        }
        if (DestoyMainEffect == true)
        {
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }
}
