using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Xolito.Movement;



public class PlayerFightingSystem : MonoBehaviour
{
    #region Variables
    public int lifesLeft;
    public float lifePoints;
    private float currentLifePoints;
    public float force;
    public float doomThreshold;
    private float elapsedTime;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackDuration;

    private Transform initialPosition;

    private bool wasHit;
    public bool canAttack;
    public bool isLookingRight;
    public GameObject rightHitbox;
    public GameObject leftHitbox;
    public IHittable enemy = null;

    #endregion

    #region Unity Functions
    void Start()
    {
        rightHitbox.SetActive(false);
        leftHitbox.SetActive(false);
        wasHit = false;
        currentLifePoints = lifePoints;
    }
    #endregion

    private void Update()
    {
        if (lifePoints != currentLifePoints)
        {
            if(enemy != null)
                gameObject.GetComponent<IHittable>().InteractWithDash(Vector2.right * (isLookingRight ? 1 : -1), lifePoints * force, 0.9f, lifePoints * force);

            currentLifePoints = lifePoints;
        }
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
            PlayerFightingSystem enemyHit = collision.transform.parent.GetComponent<PlayerFightingSystem>();
            if (enemyHit != null)
            {
                enemy = gameObject.GetComponent<IHittable>();
                lifePoints += enemyHit.GetDamage();
                Vector3 destiny = new Vector3(transform.position.x + (lifePoints * force), transform.position.y, transform.position.z);
                //enemy.InteractWithDash(Vector2.right * (isLookingRight ? 1 : -1), lifePoints * force, 0.9f, force);
                Debug.Log(gameObject.name + " received damage");
            }
            else
            {
                Debug.Log("No hitbox");
            }
        }

        if (collision.CompareTag("Deadzone"))
        {
            PlayerDied();
            Debug.Log("Dead!");
        }
 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy = null;
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

    public Transform GetInitialPosition()
    {
        return initialPosition;
    }

    public void SetInitialPosition(Transform desiredPosition)
    {
        initialPosition = desiredPosition;
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
        if (lifesLeft <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        this.lifesLeft -= 1;
        lifePoints = 0;
        gameObject.transform.position = initialPosition.position;
    }
    #endregion
}
