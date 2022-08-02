using UnityEngine;

namespace Invector.vCharacterController
{
    public class PlayerInputController_RB : MonoBehaviour
    {
        #region Variables
        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [HideInInspector]
        public PlayerController_RB cc;
        [HideInInspector]
        public Camera cameraMain;
        #endregion

        protected virtual void Start()
        {
            InitilizeController();
        }

        protected virtual void FixedUpdate()
        {            
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs
        protected virtual void InitilizeController()
        {
            cc = GetComponent<PlayerController_RB>();
            if (cc != null) { cc.Init(); }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main)
                {
                    Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                }
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput)) { cc.Strafe(); }
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput)) { cc.Sprint(true); }
            else if (Input.GetKeyUp(sprintInput)) { cc.Sprint(false); }
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions()) { cc.Jump(); }
        }
        #endregion       
    }
}
