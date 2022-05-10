using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace algobitzTest
{
    public class BallDropTarget : MonoBehaviour
    {
       // [HideInInspector] 
        public bool dropPosMoved = false;
        private Vector3 mousePosToWorld = Vector3.zero;
        private Camera mainCam;
        
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

        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100))
            {
                mousePosToWorld = new Vector3(hit.point.x,transform.position.y,hit.point.z);
            }
        }
        
    }
}
