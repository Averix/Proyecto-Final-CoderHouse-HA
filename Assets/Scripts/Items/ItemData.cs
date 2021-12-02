using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Data/Item Data")]

public class ItemData : ScriptableObject
{
    // Item name, value, rarity and stats modifiers
    // rarity is based on the total stats modifier
    // raity will be used to balance and categorize items
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int purchaseValue;
    [SerializeField]
    private int selllValue;
    [SerializeField]
    private int itemRarity;
    [SerializeField]
    private int damageModifier;
    [SerializeField]
    private int defenseModifier;
    [SerializeField]
    [Range(0f, 1f)]
    private float criticalRateModifier;
    [SerializeField]
    [Range(0f, 1f)]
    private float evasionRateModifier;
    [SerializeField]
    [Range(0f, 1f)]
    private float blockRateModifier;

    //GETTER
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public int PurchaseValue
    {
        get
        {
            return purchaseValue;
        }
    }

    public int SellValue
    {
        get
        {
            return selllValue;
        }
    }

    public int ItemRarity
    {
        get
        {
            return itemRarity;
        }
    }

    public int DamageModifier
    {
        get
        {
            return damageModifier;
        }
    }

    public int BaseDefenseModifier
    {
        get
        {
            return defenseModifier;
        }
    }

    public float CriticalRateModifier
    {
        get
        {
            return criticalRateModifier;
        }
    }

    public float EvasionRateModifier
    {
        get
        {
            return evasionRateModifier;
        }
    }

    public float BlockRateModifier
    {
        get
        {
            return blockRateModifier;
        }
    }
}
