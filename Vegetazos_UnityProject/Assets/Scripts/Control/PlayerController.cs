using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xolito.Utilities;
using Xolito.Core;
using UnityEngine.InputSystem;

namespace Xolito.Control
{
    public class PlayerController : MonoBehaviour
    {
        //BORRAR SI ROMPE ALGO
        #region AUDIO 
        [SerializeField] public AudioSource source;
        #endregion

        #region variables
        public Animator animatorXolos;
        [SerializeField] PlayerSettings pSettings = null;
        Movement.Mover mover;
        public AudioClip jump, dash;
        #endregion

        #region Unity methods
        private void Awake()
        {
            animatorXolos = GetComponent<Animator>();
            mover = GetComponent<Movement.Mover>();
            

        }

        private void Start()
        {

        }


        #endregion

        #region Public methods
        public void Move(InputAction.CallbackContext context)
        {
            var direc = context.ReadValue<Vector2>().x;
            
            if (context.performed)
            {
                if (mover.InteractWith_Movement(direc))
                {
                    
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
                mover.InteractWith_Movement(0);
            }
        }

        private void ChangeSpriteOrientation(float direction)
        {
            if (direction < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        

        public void Dash(InputAction.CallbackContext context)
        {
            if (mover.InteractWithDash())
            {
                animatorXolos.SetBool("isDashing", true);
                //source.PlayOneShot(pSettings.Get_Audio(BasicActions.Dash));
            }
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
              if (mover.InteractWith_Jump())
              {
                animatorXolos.SetTrigger("jump");

                if(source && !source.isPlaying)
                source?.PlayOneShot(pSettings.Get_Audio(BasicActions.Jump));
              }
            }

        }
        #endregion
    }
}