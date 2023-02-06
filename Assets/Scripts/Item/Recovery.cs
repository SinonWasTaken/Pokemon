using UnityEngine;

public class Recovery : ItemData
{
    public enum Recovery_Type
    {
        Status,
        Health,
        Revive,
        MaxRevive,
        FullStatus,
        Ether
    }

    public enum Status_Recovery
    {
        Poison,
        Paralyze,
        Sleep,
        Burn
    }

    [SerializeField] private Recovery_Type type;
    [SerializeField] private Status_Recovery status;

    [SerializeField] private float value;

    public Recovery(string itemName, string itemDescription, Item_Type itemType, int itemCost, int flingDamage, Recovery_Type type, Status_Recovery status, float value) : base(itemName, itemDescription, itemType, itemCost, flingDamage)
    {
        this.type = type;
        this.status = status;
        this.value = value;
    }

    public Recovery_Type Type => type;

    public Status_Recovery Status => status;

    public float Value => value;
}
