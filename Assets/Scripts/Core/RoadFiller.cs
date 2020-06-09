using System.Collections.Generic;
using System.Linq;
using Core.ShapeNS;
using UnityEngine;

namespace Core
{
    public class RoadFiller : MonoBehaviour
    {
        private List<GameObject> oldList;
        public void Fill(Vector3 startPosition, Vector3 endPosition)
        {
            if(oldList == null)
                oldList = new List<GameObject>();
            RemoveOldObjects();
            var zDiff = endPosition.z - startPosition.z;
            for (var i = 0; i < zDiff; i++)
            {
                for (var j = -1; j < 2; j++)
                {
                    var shapeGameObject = ShapePool.instance.GetShapeGameObject(ShapeType.Square, AcceptanceType.None);
                    shapeGameObject.GetComponent<Shape>().ActiveShape((int)(i*zDiff+(j+1)),
                        startPosition + new Vector3(j,0, i), Vector3.zero, ShapeType.Square );
                    oldList.Add(shapeGameObject);
                }
            }
        }

        private void RemoveOldObjects()
        {
            foreach (var go in oldList.Where(go => gameObject.activeSelf))
            {
                go.GetComponent<Shape>().LeaveHole();
                ShapePool.instance.AddToPool(go.GetComponent<Shape>());
            }
        }
    }
}