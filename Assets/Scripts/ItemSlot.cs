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

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        RefreshIcon();
        button = GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(UseItemInSlot);
        }
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
            button.onClick.RemoveListener(UseItemInSlot);
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
        if(button != null)
            button.onClick.RemoveListener(UseItemInSlot);
    }

    private void OnEnable()
    {
        if(button != null)
            button.onClick.AddListener(UseItemInSlot);
    }
}
