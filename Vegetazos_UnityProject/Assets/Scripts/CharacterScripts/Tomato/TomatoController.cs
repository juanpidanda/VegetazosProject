using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xolito.Utilities;
using Xolito.Core;
using UnityEngine.InputSystem;

namespace Xolito.Control
{
    public class TomatoController : MonoBehaviour
    {
        //BORRAR SI ROMPE ALGO
        #region AUDIO 
        [SerializeField] public AudioSource source;
        #endregion

        #region variables
        public Animator animatorXolos;
        [SerializeField] PlayerSettings pSettings = null;
        protected Movement.TomatoMover mover;

        public AudioClip jump, dash;

        [SerializeField] List<Sprite> characterSprites = new List<Sprite>();
        public GameObject spriteRenderer;
        public GameObject dashTrail;
        #endregion

        #region Unity methods
        private void Awake()
        {
            animatorXolos = GetComponent<Animator>();
            mover = GetComponent<Movement.TomatoMover>();


        }

        private void Start()
        {

        }


        #endregion

        #region Public methods
        public virtual void Move(InputAction.CallbackContext context)
        {
            dashTrail.SetActive(false);
            spriteRenderer.GetComponent<SpriteRenderer>().sprite = characterSprites[1];
            print("entrobase");
            var direc = context.ReadValue<Vector2>().x;

            if (context.performed)
            {
                if (this.mover.InteractWith_Movement(direc))
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
                this.mover.InteractWith_Movement(0);

            }
        }

        public virtual void ChangeSpriteOrientation(float direction)
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
            Debug.Log("entro1");
            if (context.performed)
            {
                if (mover.InteractWith_Jump())
                {
                    animatorXolos.SetTrigger("jump");

                    if (source && !source.isPlaying)
                        source?.PlayOneShot(pSettings.Get_Audio(BasicActions.Jump));
                }
            }

        }
        public virtual void SpecialAttack(InputAction.CallbackContext context)
        {
            Debug.Log("cSA");


            if (mover.InteractWithSpecialAttack())
            {
                animatorXolos.SetTrigger("specialattack");

            }

        }
        #endregion
    }
}

