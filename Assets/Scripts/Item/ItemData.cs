using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public enum Item_Type
    {
        Heal,
        Pokeball,
        Key
    }
    
    [SerializeField] private string item_name;
    [SerializeField] private string item_description;
    
    [SerializeField] private Item_Type type;

    [SerializeField] private int item_cost;
    [SerializeField] private int item_max_stack_count;
    [SerializeField] private int item_fling_damage;

    public ItemData(string itemName, string itemDescription, Item_Type type, int cost, int itemFlingDamage)
    {
        item_name = itemName;
        item_description = itemDescription;
        this.type = type;
        item_cost = cost;
        item_fling_damage = itemFlingDamage;
    }

    public string ItemName => item_name;
    public string ItemDescription => item_description;
    
    public Item_Type Type => type;

    public int ItemCost => item_cost;
    public int ItemMaxStackCount => item_max_stack_count;
    public int ItemFlingDamage => item_fling_damage;
}