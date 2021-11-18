using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    GameObject m_Target;
    Collider[] m_GetItem;
    public float m_GetItemRange;

    // Start is called before the first frame update
    void Start()
    {
        if(m_Target == null)
            m_Target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ItemGet();
    }

    public void ItemGet()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Item");
        m_GetItem = Physics.OverlapSphere(transform.position,
             m_GetItemRange, nLayer);

        for (int i = 0; i < m_GetItem.Length; i++)
        {
            if (m_GetItem[i])
            {
                PlayerCharacter Character = m_Target.GetComponent<PlayerCharacter>();
                Inventory ItemInventory = Character.ReturnPlayerInventory();
                ItemInfo Iteminfo = m_GetItem[i].GetComponent<ItemInfo>();
                Status itemStatus = m_GetItem[i].GetComponent<Status>();
                Item item = Iteminfo.ReturnItem();
                ItemInventory.AcquireItem(item, itemStatus, 1);
                Destroy(m_GetItem[i].gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, m_GetItemRange);
    }
}
