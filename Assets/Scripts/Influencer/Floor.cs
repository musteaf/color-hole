using Core.ShapeNS;
using UnityEngine;

namespace Influencer
{
    public class Floor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Shape>())
            {
                other.GetComponent<Shape>().Collected();
            }
        }
    }
}