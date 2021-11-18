using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_Item_Info_Text;

    public void SetItemInfo(string text)
    {
        m_Item_Info_Text.text = text;
    }
}
