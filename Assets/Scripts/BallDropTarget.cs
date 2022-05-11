using System;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace algobitzTest
{
    public class BallDropTarget : MonoBehaviour
    {
        // [HideInInspector] 
        public bool dropPosMoved = false;
        private Vector3 mousePosToWorld = Vector3.zero;
        private Camera mainCam;

        [SerializeField] private JoystickController joyController;

        private void Start()
        {
            mainCam = Camera.main;
        }

        private void OnMouseDrag()
        {
            if (GameManager.Instance.canMoveDropPosition)
            {
                transform.position = mousePosToWorld;
                dropPosMoved = true;
                transform.parent = null;
            }
        }

        private void Update()
        {
            if (GameManager.Instance.canMoveDropPosition)
            {
                if (joyController.inputPos != Vector2.zero)
                {
                    MoveDropPosition(transform.TransformDirection(
                        new Vector3(joyController.inputPos.x, 0, joyController.inputPos.y).normalized));
                }
                else
                {
                    var InputVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                    MoveDropPosition(InputVector);
                }
            }
        }

        void MoveDropPosition(Vector3 direction)
        {
            if(direction == Vector3.zero) return;
            
            transform.Translate(direction * (Time.deltaTime * 10));
            dropPosMoved = true;
            transform.parent = null;
        }

        //Raycasting to check ground position and move the red dot there.
        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                mousePosToWorld = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }
    }
}