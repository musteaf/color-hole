using Core.ShapeNS;
using UnityEngine;

namespace Influencer
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] public Transform holeTransform;

        public delegate void OnBlackHole(Transform holeTransform);

        public static event OnBlackHole onBlackHole;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Shape>().EnterHole(this);
        }

        private void OnTriggerExit(Collider other)
        {
            other.GetComponent<Shape>().LeaveHole(this);
        }

        private void FixedUpdate()
        {
            if (onBlackHole != null)
            {
                onBlackHole(holeTransform);
            }
        }
    }
}
