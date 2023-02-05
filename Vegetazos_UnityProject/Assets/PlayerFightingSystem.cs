using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFightingSystem : MonoBehaviour
{
    #region Variables
    public int lifesLeft;
    public float lifePoints;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackDuration;
    [SerializeField] protected float attackCooldown;

    public bool canAttack;
    public bool isLookingRight;
    public GameObject hitbox;
    #endregion

    #region Unity Functions
    void Start()
    {
        hitbox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitboxLogic hitbox = collision.GetComponent<HitboxLogic>();
        if (hitbox)
        {
            lifePoints += hitbox.GetDamage();
            Debug.Log("Current life: " + lifePoints.ToString());
        }
    }

    
    
    #endregion

    #region Public Methods
    public virtual void Attack()
    {
        Attacking();
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetAttackDuration()
    {
        return attackDuration;
    }
    
    #endregion

    #region Private Methods
    private void Attacking()
    {
        hitbox.SetActive(true);
        canAttack = false;
    }
    #endregion
}
