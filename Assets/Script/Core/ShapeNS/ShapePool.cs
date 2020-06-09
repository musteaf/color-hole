using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core.ShapeNS
{
    public class ShapePool : MonoBehaviour
    {
        public static ShapePool instance;
        public GameObject[] shapePrefabs;
        public Dictionary<string, List<GameObject>> shapes;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            shapes = new Dictionary<string, List<GameObject>>();
        }

        public void AddToPool(Shape shape)
        {
            shape.transform.position = Global.farPosition;
            var key = shape.ShapeType + shape.AcceptanceType.ToString();
            if (shapes.ContainsKey(key))
            {
                shapes[key].Add(shape.gameObject);
            }
            else
            {
                var newTypeList = new List<GameObject> {shape.gameObject};
                shapes.Add(key, newTypeList);
            }
            var rigidbody = shape.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            shape.gameObject.SetActive(false);
        }

        public GameObject GetShapeGameObject(ShapeType shapeType, AcceptanceType acceptanceType)
        {
            var key = shapeType + acceptanceType.ToString();
            if (shapes.ContainsKey(key))
            {
                List<GameObject> shapeList = shapes[key];
                if (shapeList.Count == 0)
                {
                    return InstantiateShape(shapeType, acceptanceType);
                }
                else
                {
                    GameObject willReturn = shapeList[shapeList.Count - 1];
                    shapeList.RemoveAt(shapeList.Count - 1);
                    willReturn.gameObject.SetActive(true);
                    return willReturn;
                }
            }
            else
            {
                return InstantiateShape(shapeType, acceptanceType);
            }
        }
        
        private GameObject InstantiateShape(ShapeType shapeType, AcceptanceType acceptanceType)
        {
            var acceptanceTypeCount = Enum.GetNames(typeof(AcceptanceType)).Length;
            var newGameObject = 
                Instantiate(shapePrefabs[((int) acceptanceType * acceptanceTypeCount )+ (int) shapeType],
                    Global.farPosition, Quaternion.identity);
            if (acceptanceType == AcceptanceType.Acceptable)
            {
                newGameObject.AddComponent<AcceptableShape>().AcceptanceType = AcceptanceType.Acceptable;
            }else if(acceptanceType == AcceptanceType.UnAcceptable)
            {
                newGameObject.AddComponent<UnAcceptableShape>().AcceptanceType = AcceptanceType.UnAcceptable;
            }
            else
            {
                newGameObject.AddComponent<FillerShape>().AcceptanceType = AcceptanceType.None;
            }
            return newGameObject;
        }
    }
}
