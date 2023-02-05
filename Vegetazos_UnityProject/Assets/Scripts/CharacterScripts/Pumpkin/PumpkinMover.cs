using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Xolito.Control;
using Xolito.Core;
using Xolito.Utilities;
using static Xolito.Utilities.Utilities;

namespace Xolito.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PumpkinMover : MonoBehaviour
    {
        #region variables
        [Header("References")]
        [SerializeField] PlayerSettings pSettings = null;

        [Space]
        [Header("Status")]
        [SerializeField] protected bool inDash = false;
        [SerializeField] protected bool isGrounded = false;
        [SerializeField] protected bool canJump = true;
        [SerializeField] protected bool canDash = false;
        [SerializeField] protected bool isBesidePlatform = false;
        [SerializeField] protected bool isTouchingTheWall = false;
        [SerializeField] protected bool isWallRight = false;
        //   [SerializeField] private float angleOfContact = 0;
        [SerializeField] protected bool haveGravity = false;
        [SerializeField] protected bool inCoyote = false;


        protected Rigidbody2D rgb2d;
        protected BoxCollider2D boxCollider;
        protected CoolDownAction cdJump;
        protected CoolDownAction cdDash;
        protected CoolDownAction cdCoyote;

        [SerializeField] protected Vector2 currentDirection = default;
        protected (bool isTouchingWall, bool isAtRight, float? distance) wallDetection;
        protected (float? distance, float currentVelocity) groundData = default;
        protected BoxCollider2D PlatformToLand = default;
        protected float dashSpeed = 0;
        protected Vector2 dashTarget = Vector2.zero;
        private bool shouldFall = false;
        protected bool onAir = false;
        protected (float? distance, float finalDistance, int framesCount, int currentFrames) currentDistances = default;
        public GameObject laser;

        LimitArea ground = default;
        LimitArea wall;
        struct LimitArea
        {
            public float? distance;
            public float velocity;
        }

        protected virtual bool IsGrounded
        {
            get => isGrounded;

            set
            {
                if (!value && isGrounded)
                    onAir = true;
                else if (!isGrounded && value)
                    onAir = false;

                isGrounded = value;
            }
        }
        #endregion

        #region Unity methods
        private void Awake()
        {
            rgb2d = this.gameObject.GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();

        }

        private void Start()
        {
            canJump = true;
            canDash = true;

            cdJump = new CoolDownAction(pSettings.JumpCoolDown, Set_CanJump);
            cdDash = new CoolDownAction(pSettings.DashCoolDown);
            cdCoyote = new CoolDownAction(pSettings.CoyoteTime, Fall);

            haveGravity = rgb2d.gravityScale == pSettings.Gravity;
        }

        private void Update()
        {
            inCoyote = !cdCoyote.CanUse;
            Check_Ground();

            Check_Wall();

            Move();

            //if (currentDistances.distance.HasValue && currentDistances.currentFrames >= currentDistances.framesCount)
            //{
            //    Move_Gravity(false);
            //    currentDistances = default;
            //}
            //else if (currentDistances.distance.HasValue && currentDistances.currentFrames < currentDistances.framesCount)
            //    currentDistances.currentFrames++;
        }

        protected void Move()
        {
            if (inDash || currentDirection.x == 0) return;

            if (!wallDetection.isTouchingWall || ((currentDirection.x >= 0 && !wallDetection.isAtRight) || currentDirection.x < 0 && wallDetection.isAtRight))
            {
                transform.Translate(currentDirection.x * pSettings.Speed * Time.deltaTime, 0, 0);
                currentDirection.x = currentDirection.x > 0 ? 1 : -1;
            }
            else
            {
                print("stop");
            }
        }
        #endregion

        #region Public Methods

        //--Move--//
        public virtual bool InteractWith_Movement(float direction)
        {

            currentDirection.x = direction;

            return true;
        }

        //--Jump--//
        public virtual bool InteractWith_Jump()
        {
            if (!IsGrounded || inDash || !cdJump.CanUse) return false;

            (float? distance, GameObject item) = Get_DistanceToMove(Vector2.up, pSettings.JumpSize);

            if (/*!distance.HasValue*/ true)
            {
                Enable_Gravity();
                Jump();
                print("Jump");
                StartCoroutine(cdJump.CoolDown());
            }

            return false;
        }

        //--Dash--//
        public virtual bool InteractWithDash()
        {
            if (inDash || !canDash || !cdDash.CanUse) return false;

            (float? distance, _) = Get_DistanceToMove(currentDirection * pSettings.DashDistance, .9f);

            if (!distance.HasValue || distance >= pSettings.DashDistance)
            {
                distance = pSettings.DashDistance;
            }
            else distance += boxCollider.bounds.extents.x;

            StartCoroutine(DashTo(currentDirection.normalized * distance.Value, pSettings.DashTime));
            StartCoroutine(cdDash.CoolDown());

            return true;
        }

        //--SpecialAtttack--//
        public virtual bool InteractWithSpecialAttack()
        {

            laser.SetActive(true);

            return true;
        }



        public (float? distance, GameObject hit) Get_DistanceToMove(Vector2 Destiny, float size)
        {
            RaycastHit2D[] hit2D;
            //Vector3 startPosition = Get_VectorWithOffset(Destiny, offset) + new Vector3(Destiny.x, Destiny.y) * boxCollider.bounds.extents.x;
            //Vector3 startPosition = transform.position + new Vector3(Destiny.x, Destiny.y) * boxCollider.bounds.extents.x;
            Vector3 startPosition = transform.position;
            Vector3 finalPosition = new Vector3(Destiny.x, Destiny.y, 0);
            Vector2 newSize = boxCollider.size * new Vector2
            {
                x = Destiny switch
                {
                    { x: float nx } when (nx == -1 || nx == 1) => boxCollider.size.x * size,
                    _ => .1f
                },
                y = Destiny switch
                {
                    { y: float ny } when (ny == -1 || ny == 1) => boxCollider.size.y * size,
                    _ => .1f
                }
            };
            float angle = Get_Angle(Destiny.normalized);

            hit2D = Physics2D.BoxCastAll(startPosition, newSize, angle, finalPosition, Destiny.magnitude);

            (float? nearestDistance, GameObject item) result = default;

            if (hit2D.Length != 0)
            {
                foreach (RaycastHit2D hit in hit2D)
                {
                    if (hit.transform.gameObject == gameObject) continue;

                    if (Vector2.Angle(Destiny * -1, hit.normal) < pSettings.MaxSlope)
                    {
                        if (Get_Distance(hit) is var nd && nd.HasValue && (!result.nearestDistance.HasValue || nd < result.nearestDistance))
                        {
                            result.nearestDistance = nd;
                            result.item = hit.transform.gameObject;
                        }
                    }
                    #region Debug
                    //if (gameObject.name.Contains("lanco"))
                    //    Debug.DrawLine(hit.collider.bounds.min, hit.point + hit.normal * 2, Color.red);
                    //if (Vector2.Angle(Destiny * -1, hit.normal) < 80)
                    //{
                    //    if (founded)
                    //        continue;
                    //    else
                    //        return Get_Distance(hit);
                    //} 
                    #endregion
                }
            }

            return result;

            float? Get_Distance(RaycastHit2D hit)
            {
                //if (Destiny.normalized.y == -1 && hit.collider.bounds.max.y <= boxCollider.bounds.min.y && gameObject.name.Contains("lanc"))
                //{
                //    print(hit.transform.name);
                //    Debug.DrawLine(startPosition, finalPosition, Color.blue);
                //    //Debug.DrawLine(new Vector3(startPosition.x + newSize.x, startPosition.y + newSize.y, 0), finalPosition, Color.red);
                //    //Debug.DrawLine(hit.collider.bounds.max, boxCollider.bounds.min);
                //    //Debug.DrawLine(boxCollider.bounds.min, Vector3.up * 2, Color.blue);
                //}
                //print(Destiny.normalized.y);
                return Destiny.normalized switch
                {
                    { x: -1 } when (hit.collider.bounds.max.x <= boxCollider.bounds.min.x) =>
                        boxCollider.bounds.min.x - hit.collider.bounds.max.x,

                    { x: 1 } when (hit.collider.bounds.min.x >= boxCollider.bounds.max.x) =>
                        hit.collider.bounds.min.x - boxCollider.bounds.max.x,

                    { y: -1 } when (hit.collider.bounds.max.y <= boxCollider.bounds.min.y) =>
                        boxCollider.bounds.min.y - hit.collider.bounds.max.y,

                    { y: 1 } when (hit.collider.bounds.min.y >= boxCollider.bounds.max.y) =>
                        hit.collider.bounds.min.y - boxCollider.bounds.max.y,

                    _ => null,
                };
            }
        }



        #endregion

        #region private methods

        private void Fall(bool shouldFall)
        {
            //this.shouldFall = shouldFall;

            if (!PlatformToLand)
                Enable_Gravity(shouldFall);
        }

        protected void Check_Ground()
        {
            (float? distance, GameObject item) = Get_DistanceToMove(Vector2.down * pSettings.GroundDistance, pSettings.GroundSize);

            if (distance.HasValue)
            {

                if (PlatformToLand && PlatformToLand.gameObject != item && !onAir && rgb2d.velocity.y == 0)
                {
                    if (cdCoyote.CanUse)
                    {
                        //PlatformToLand = item.GetComponent<BoxCollider2D>();
                        Enable_Gravity();
                        PlatformToLand = null;

                        StartCoroutine(cdCoyote.CoolDown());
                        return;
                    }
                    //StartCoroutine(cdCoyote.CoolDown());
                }

                //
                //else if (!groundToLand && distance < pSettings.DistanceToEnable && item.tag != "Platform")
                //{
                //    //if (onAir) return;

                //    //currentDistances.finalDistance = distance.Value;
                //    //currentDistances.
                //    return;
                //}

                //Revisa si estas sobre la misma plataforma
                else if (PlatformToLand && PlatformToLand.gameObject == item)
                {
                    //groundToLand.isTrigger = false;
                    //print(groundToLand.gameObject.name + " Active ");
                    //return;
                    //Move_Gravity(false);
                    //return;
                    //if (!IsGrounded)
                    //    Enable_Gravity(false);
                }

                //Registra nueva plataforma
                else if (PlatformToLand && PlatformToLand.gameObject != item && item.tag == "Platform")
                {
                    PlatformToLand = item.GetComponent<BoxCollider2D>();
                    //groundToLand.isTrigger = false;
                    //Check_CurrentDistances(true);
                    Enable_Gravity();
                }

                //Revisa si ya estas tocando el piso
                if (distance < pSettings.DistanceToEnable && rgb2d.velocity.y <= 0)
                {
                    IsGrounded = true;
                    Enable_Gravity(false);
                }
                else IsGrounded = false;

                PlatformToLand = item.GetComponent<BoxCollider2D>();
            }
            else if (PlatformToLand && cdCoyote.CanUse && rgb2d.velocity.y == 0)
            {
                Enable_Gravity();
                PlatformToLand = null;

                StartCoroutine(cdCoyote.CoolDown());
            }
            //else if (!onAir && rgb2d.velocity.y == 0)
            //{
            //    //groundToLand.isTrigger = true;
            //    PlatformToLand = null;
            //    IsGrounded = true;
            //    //StartCoroutine(cdCoyote.CoolDown());
            //}
        }

        private void Check_Wall()
        {
            (float? distance, GameObject item) = Get_DistanceToMove(currentDirection * pSettings.WallDistance, pSettings.WallSize);
            if (distance.HasValue)
            {
                if (item.tag == "Platform")
                {
                    wallDetection.isTouchingWall = false;
                    return;
                }

                wallDetection.isTouchingWall = currentDirection switch
                {
                    { x: 1 } => wallDetection.isAtRight = true,
                    { x: -1 } => !(wallDetection.isAtRight = false),
                    _ => wallDetection.isAtRight = false
                };
                print(item.name);
            }
            else
                wallDetection.isTouchingWall = false;

            isTouchingTheWall = wallDetection.isTouchingWall;
            isWallRight = wallDetection.isAtRight;
            wallDetection.distance = distance;
        }

        private float Check_Dash()
        {
            (float? distance, _) = Get_DistanceToMove(currentDirection * pSettings.DashDistance, .9f);

            if (distance.HasValue && distance > pSettings.DashDistance)
                return distance.Value;
            else
                return pSettings.DashDistance * currentDirection.x;
        }

        protected void Jump() => rgb2d.velocity = new Vector2(rgb2d.velocity.x, pSettings.JumpForce);

        private void Clear_XVelocity() => rgb2d.velocity = new Vector2(0, rgb2d.velocity.y);

        private void Clear_YVelocity() => rgb2d.velocity = new Vector2(rgb2d.velocity.x, 0);

        private void FreezeVerticalPosition(bool shouldFreeze = true) =>
            rgb2d.constraints = shouldFreeze switch
            {
                true => RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation,
                _ => RigidbodyConstraints2D.FreezeRotation
            };

        private void Set_CanJump(bool canJump) => this.canJump = canJump;

        private void Enable_Gravity(bool shouldEnable = true)
        {
            rgb2d.gravityScale = shouldEnable ? pSettings.Gravity : 0;
            haveGravity = shouldEnable;
            if (!shouldEnable) Clear_YVelocity();
        }

        private void Check_CurrentDistances(bool shouldCheck = true)
        {
            if (!shouldCheck)
            {
                currentDistances = default;
                return;
            }

            if (!currentDistances.distance.HasValue) return;

            var finalVel = rgb2d.velocity.y;

            for (int i = 0; i < 4; i++)
            {
                var hi = finalVel - pSettings.Gravity * Time.deltaTime * i;
                finalVel = hi;

                if (finalVel < pSettings.DistanceToEnable)
                {
                    currentDistances.framesCount = i;
                    currentDistances.finalDistance = hi;
                    break;
                }
            }
        }
        #endregion

        #region Coroutines
        IEnumerator DashTo(Vector2 final, float dashTime)
        {
            inDash = true;
            FreezeVerticalPosition();
            Clear_XVelocity();
            Vector3 target = transform.position + new Vector3
            {
                x = final.x,
                y = final.y,
                z = 0
            };

            print("in Dash");
            //AUDIO
            //source.PlayOneShot(manager1.dash);
            float distance = Vector2.Distance(transform.position, target);

            while (distance > 0)
            {
                transform.position = Vector2.Lerp(transform.position, target, pSettings.DashSpeed * Time.deltaTime);
                distance -= pSettings.DashSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            inDash = false;

            this.GetComponent<Animator>().SetBool("isDashing", false);
            Clear_XVelocity();
            FreezeVerticalPosition(false);
            yield break;
        }

        IEnumerator EnableCollition(Collider2D collider)
        {
            yield return new WaitForSeconds(2);

            Physics2D.IgnoreCollision(boxCollider, collider, false);
        }
        #endregion
    }
}