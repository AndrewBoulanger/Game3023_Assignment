using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public Item itemInSlot = null;
    public int itemCount = 0;

    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    [SerializeField]
    private Image itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        RefreshIcon();
        itemIcon.GetComponent<Button>().onClick.AddListener(UseItemInSlot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItemInSlot()
    {
        if (itemInSlot != null)
        {
            itemInSlot.Use();
            if(itemInSlot.isConsumable)
            {
                itemCount--;
            }
            RefreshIcon();
        }
    }

    void RefreshIcon()
    {
        if(itemCount < 1)
        {
            itemInSlot = null;
            itemCountText.text = "";
             itemIcon.GetComponent<Button>().onClick.RemoveListener(UseItemInSlot);
        }

        if(itemInSlot != null)
        { 
            itemIcon.sprite = itemInSlot.icon;
            itemCountText.text = itemCount.ToString();
            itemIcon.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        
    }
}
