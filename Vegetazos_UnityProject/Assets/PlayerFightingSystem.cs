using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFightingSystem : MonoBehaviour
{
    #region Variables
    public int lifesLeft;
    public float lifePoints;
    public float force;
    public float doomThreshold;
    private float elapsedTime;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackDuration;

    private bool wasHit;
    public bool canAttack;
    public bool isLookingRight;
    public GameObject rightHitbox;
    public GameObject leftHitbox;
    #endregion

    #region Unity Functions
    void Start()
    {
        rightHitbox.SetActive(false);
        leftHitbox.SetActive(false);
        wasHit = false;
    }
    #endregion

    private void Update()
    {

        if (wasHit)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 2)
            {
                gameObject.GetComponent<Rigidbody>().velocity *= -1;
                elapsedTime = 0;
                wasHit = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            Debug.Log("Hit!");
        }
        PlayerFightingSystem enemyHit = collision.GetComponent<PlayerFightingSystem>();
        if (enemyHit != null)
        {
            enemyHit.lifePoints += enemyHit.GetDamage();

            if(lifePoints > doomThreshold)
            {
                Vector3 direction = (gameObject.transform.position - collision.transform.position).normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * lifePoints, ForceMode2D.Impulse);
                wasHit = false;
            }
            Debug.Log("Hit confirmed");
        }
        else
        {
            Debug.Log("No hitbox");
        }
    }

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

    public int GetLifepoints()
    {
        return lifesLeft;
    }
    
    #endregion

    #region Private Methods
    private void Attacking()
    {
        ChooseHitbox();
        canAttack = false;
    }

    private void ChooseHitbox()
    {
        if (isLookingRight)
        {
            rightHitbox.SetActive(true);
            leftHitbox.SetActive(false);
        }
        if (!isLookingRight)
        {
            leftHitbox.SetActive(true);
            rightHitbox.SetActive(false);
        }
    }

    private void PlayerDied()
    {
        this.lifesLeft -= 1;
        lifePoints = 0;
    }
    #endregion
}
