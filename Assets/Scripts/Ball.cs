using System;
using UnityEngine;
using UnityEngine.UI;

namespace algobitzTest
{
    public class Ball : MonoBehaviour
    {
        [Header("Ball Movement")]
        [SerializeField] private Transform ballParent;
        [SerializeField] private float moveSpeed;
        [SerializeField] private BallDropTarget ballDropTarget;
        [SerializeField] private JoystickController joyController;
        
        [Header("Ball Dribble")]
        [SerializeField] private Rigidbody ballRb;
        private float initialBounceSpeed;
        private float bounceSpeed;
        [SerializeField] private float maxDribbleHeight;
        
        [Header("UI References")] 
        [SerializeField] Slider bounceSpeedSlider;

        private string[] speedLevelsTexts = new[] {"SLOW", "MEDIUM", "FAST"};
        private int currentSpeedIndex = 0;
        
        private void Start()
        {
            initialBounceSpeed = 15;
            ResetBounceSpeedFromSlider();
            ChangeBounceSpeed();
        }
        private void FixedUpdate()
        {
            MoveToDropPoint();
            LimitBounceHeight();
        }

        private void Update()
        {
            if(Application.isMobilePlatform == false)
                KeyboardControls();
            
            MoveWithJoystick();

            if (Input.GetKeyDown(KeyCode.H))
            {
                BallMotionState();
            }
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                ChangeBounceSpeed();
            }
        }
        
        #region Move
        
        //Moving parent independently of the actual ball object (child) if the ball is bouncing.
        public void MoveBall(Vector3 direction)
        {
            if(Mathf.Abs(ballRb.velocity.y) > 0.1f)
                ballParent.Translate(direction * Time.deltaTime);
            Physics.SyncTransforms();
        }
        
        private void KeyboardControls()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Bounce();
            }

            var InputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            MoveBall(InputVector.normalized * moveSpeed);
        }

        private void MoveWithJoystick()
        {
            if(joyController.inputPos != Vector2.zero)
                MoveBall(new Vector3(joyController.inputPos.x,0,-joyController.inputPos.y) * moveSpeed);
        }

        public void MoveToDropPoint()
        {
            if (ballDropTarget.dropPosMoved && !ballRb.isKinematic)
            {
                var dropPos = ballDropTarget.transform.position;
                var targetVector = new Vector3(dropPos.x, transform.position.y, dropPos.z);
                ballParent.position = Vector3.Lerp(ballParent.position,targetVector, Time.deltaTime);
    
                if (Math.Abs(ballDropTarget.transform.position.x - ballParent.position.x) < 0.01f &&
                    Math.Abs(ballDropTarget.transform.position.z - ballParent.position.z) < 0.01f)
                {
                    ballParent.position = targetVector;
                    ballDropTarget.dropPosMoved = false;
                    ballDropTarget.transform.parent = this.transform;
                }
            }
        }
                
        #endregion
        
        #region Bounce

        public void Bounce()
        {
            ballRb.velocity = Vector3.down * bounceSpeed;
        }
        
        public void ResetBounceSpeedFromSlider()
        {
            bounceSpeed = bounceSpeedSlider.value;
        }

        private void LimitBounceHeight()
        {
            if (maxDribbleHeight < ballRb.transform.position.y)
            { 
                ballRb.velocity -= (ballRb.transform.position - ballParent.transform.position) * 1.4f;
            }
        }

        public void BallMotionState()
        {
            ballRb.isKinematic = !ballRb.isKinematic;
            GameManager.Instance.canMoveDropPosition = ballRb.isKinematic;
            GameManager.Instance.UpdateHoldStatusUI(ballRb.isKinematic);
        }

        public void ChangeBounceSpeed()
        {
            if (currentSpeedIndex == speedLevelsTexts.Length-1)
            {
                currentSpeedIndex = 0;
            }
            else
            {
                currentSpeedIndex += 1;
            }
            
            GameManager.Instance.UpdateBounceSpeedUI(speedLevelsTexts[currentSpeedIndex]);
            bounceSpeed = initialBounceSpeed * (currentSpeedIndex + 1);
        }

        #endregion

    }
}
