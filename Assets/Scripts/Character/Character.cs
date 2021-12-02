using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Character GameObject
    [SerializeField] protected GameObject characterGO;
    // Character RigidBody
    protected Rigidbody characterRB;
    // Character Animator
    protected Animator chracterAnim;
    // Character Data
    [SerializeField] protected CharacterData characterData;

    // Status Bits
    protected bool isAttack;
    protected bool attackDone;
    protected bool isDefend;
    protected bool defendDone;
    protected bool isAction;
    protected bool actionDone;

    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
        chracterAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //// COMBAT SCRIPTS ////
    
    // Look at your opponentin the eyes. Don't be a chicken
    public virtual void LookAtOpponent(GameObject opponentGO)
    {
        Quaternion newRotation = Quaternion.LookRotation(opponentGO.transform.position - transform.position);
        transform.localRotation = Quaternion.Lerp(transform.rotation, newRotation, characterData.LookSpeed * Time.deltaTime);
    }

    // Attack, Roll a NAT 20
    public virtual int AttackOpponent()
    {
        int damage = Random.Range((characterData.BaseDamage - characterData.DamageLowerLimit), (characterData.BaseDamage + characterData.DamageUpperLimit + 1));
        float critical = Random.Range(0f, 1f);
        if (critical <= characterData.CriticalRate && critical != 0) damage *= 2;
        return damage;
    }

    // Defend, Don't let them hit you
    // receives damage value, extraevasion if any and extra defense if any
    // extra evasion, extra defense rate and extra defense only available trhough items or other game mechanics
    public virtual int DefendAttack(int damage, int extraEvasion, int extraDefenseRate, int extraDefense)
    {
        float evasion = Random.Range(0f, 1f);
        if (evasion <= (characterData.EvasionRate + extraEvasion) && evasion != 0) damage = 0;
        else
        {
            float defense = Random.Range(0f, 1f);
            if (defense <= (characterData.BlockRate + extraDefenseRate) && defense != 0) damage -= (characterData.BaseDamage + extraDefense);
            else return damage;
        }
        return damage;
    }
}
