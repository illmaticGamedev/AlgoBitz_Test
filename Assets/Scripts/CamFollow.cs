using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace algobitzTest
{
    public class CamFollow : MonoBehaviour
    {
        [SerializeField] private float followSpeed;
        [SerializeField] private Transform target;

        [SerializeField] private Vector3 followOffset;
      
        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + followOffset, followSpeed * Time.deltaTime);
        }
    }
}
