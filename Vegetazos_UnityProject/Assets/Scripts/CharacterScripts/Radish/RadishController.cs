using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xolito.Utilities;
using Xolito.Core;
using UnityEngine.InputSystem;

namespace Xolito.Control
{
    public class RadishController : MonoBehaviour
    {
        //BORRAR SI ROMPE ALGO
        #region AUDIO 
        [SerializeField] public AudioSource source;
        
        #endregion

        #region variables
        public Animator animatorXolos;
        [SerializeField] PlayerSettings pSettings = null;
        protected Movement.RadishMovement mover;
        public PlayerFightingSystem fightingSystem;
        [SerializeField] List<Sprite> radishSprites = new List<Sprite>();
        public AudioClip jump, dash;
        
        #endregion

        #region Unity methods
        private void Awake()
        {
            animatorXolos = GetComponent<Animator>();
            mover = GetComponent<Movement.RadishMovement>();
            fightingSystem = GetComponent<PlayerFightingSystem>();

        }

        private void Start()
        {

        }


        #endregion

        #region Public methods
        public virtual void Move(InputAction.CallbackContext context)
        {
            
            var direc = context.ReadValue<Vector2>().x;

            if (context.performed)
            {
                if (this.mover.InteractWith_Movement(direc))
                {
                    Debug.Log("Entro");
                    gameObject.GetComponent<SpriteRenderer>().sprite = radishSprites[1];
                    if (direc != 0)
                    {
                        //Debug.Log("Direction " + direction);
                        //   animatorXolos.SetBool("isMoving", true);
                        ChangeSpriteOrientation(direc);
                    }

                    //animatorXolos.SetInteger(0, (int)direction);
                    //source.PlayOneShot(pSettings.Get_Audio(BasicActions.Walk));
                }
            }
            else if (context.canceled)
            {
                this.mover.InteractWith_Movement(0);

            }
        }

        public virtual void ChangeSpriteOrientation(float direction)
        {
            if (direction < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
                fightingSystem.isLookingRight = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
                fightingSystem.isLookingRight = false;
            }
        }



        public virtual void Dash(InputAction.CallbackContext context)
        {
            if (mover.InteractWithDash())
            {
                animatorXolos.SetBool("isDashing", true);
                //source.PlayOneShot(pSettings.Get_Audio(BasicActions.Dash));
            }
        }

        public virtual void Jump(InputAction.CallbackContext context)
        {

            if (context.performed)
            {
                
                this.gameObject.GetComponent<SpriteRenderer>().sprite = radishSprites[2];
                if (mover.InteractWith_Jump())
                {
                    
                    
                    

                    if (source && !source.isPlaying)
                        source?.PlayOneShot(pSettings.Get_Audio(BasicActions.Jump));
                }
            }

        }

        public virtual void Attack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (mover.InteractWithAttack())
                {
                    fightingSystem.Attack();
                }
            }
        }

        public virtual void Defense(InputAction.CallbackContext context)
        {

        }

        public virtual void SpecialAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = radishSprites[5];
                if (mover.InteractWithSpecialAttack())
                {
                    animatorXolos.SetTrigger("specialattack");
                }
            }
        }
        #endregion
    }
}