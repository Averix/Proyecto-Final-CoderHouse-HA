using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Default name and menu diretion 
[CreateAssetMenu(fileName = "New CharacterData", menuName = "Data/Character Data")]

public class CharacterData : ScriptableObject
{
    // Name, HP, and Stats (0-1)
    [SerializeField]
    private string characterName;
    [SerializeField]
    private int hp;
    [SerializeField]
    private int baseDamage;
    [SerializeField]
    private int damageUpperLimit;
    [SerializeField]
    private int damageLowerLimit;
    [SerializeField]
    private int baseDefense;
    [SerializeField]
    [Range(0f, 1f)]
    private float criticalRate;
    [SerializeField]
    [Range(0f, 1f)]
    private float evasionRate;
    [SerializeField]
    [Range(0f, 1f)]
    private float blockRate;

    // Other parameters
    [SerializeField]
    private float lookSpeed;

    //GETTER
    public string CharacterName
    {
        get
        {
            return characterName;
        }
    }

    public int HP
    {
        get
        {
            return hp;
        }
    }

    public int BaseDamage
    {
        get
        {
            return baseDamage;
        }
    }
    public int DamageUpperLimit
    {
        get
        {
            return damageUpperLimit;
        }
    }

    public int DamageLowerLimit
    {
        get
        {
            return damageLowerLimit;
        }
    }

    public int BaseDefense
    {
        get
        {
            return baseDefense;
        }
    }

    public float CriticalRate
    {
        get
        {
            return criticalRate;
        }
    }

    public float EvasionRate
    {
        get
        {
            return evasionRate;
        }
    }

    public float BlockRate
    {
        get
        {
            return blockRate;
        }
    }

    public float LookSpeed
    {
        get
        {
            return lookSpeed;
        }
    }
}
