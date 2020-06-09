using System.Linq;
using UnityEngine;

namespace Core.BoardNS
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Material[] materials;
        public void ChangeColor()
        {
            materials.ToList().ForEach(material => material.mainTextureOffset += new Vector2(0 , 10f/8f));
        }
    }
}