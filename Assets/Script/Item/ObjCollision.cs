using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCollision : MonoBehaviour
{
    Collider[] m_Colliders;

    public float ItemColliderRange = 1.0f;

    // Update is called once per frame
    void Update()
    {
        WallCollisionCheck();
    }

    public void WallCollisionCheck()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Wall");
        m_Colliders = Physics.OverlapSphere(transform.position,
             ItemColliderRange, nLayer);

        for (int i = 0; i < m_Colliders.Length; i++)
        {
            if (m_Colliders[i])
            {
                Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
                rigid.constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ItemColliderRange);
    }

}
