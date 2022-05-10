using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace algobitzTest
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;

        public void MoveInDirection(Vector3 direction)
        {
            transform.Translate(direction * Time.deltaTime);
        }
    }
}
