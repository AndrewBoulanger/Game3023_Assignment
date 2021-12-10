using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ItemSystem/item")]
public class Item : ScriptableObject
{
    public new string name = "item";
    [TextArea]
    public string description;

    public Sprite icon;
    public bool isConsumable = true;

    public void Use()
    {
        Debug.Log("Used " + name);
    }

}
