using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxLogic : MonoBehaviour
{
    private float damage;
    private float elapsedTime;
    private bool isAttacking = false;
    [SerializeField] protected PlayerFightingSystem fightingSystem;
    private void OnEnable()
    {
        isAttacking = true;
    }

    private void Start()
    {
        damage = fightingSystem.GetDamage();
    }

    private void Update()
    {
        if (isAttacking)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > fightingSystem.GetAttackDuration())
            {
                fightingSystem.canAttack = true;
                elapsedTime = 0;
                this.gameObject.SetActive(false);
            }
        }   
    }

    public float GetDamage()
    {
        return damage;
    }
}
