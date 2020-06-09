using Script.Core.ShapeNS;
using UnityEngine;

namespace Script.Influencer
{
    public class MagneticField : MonoBehaviour
    {
        [SerializeField] public Transform holeTransform;

        public Transform HoleTransform
        {
            get => holeTransform;
            set => holeTransform = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Shape>().EnterMagneticField(holeTransform);
        }
    }
}