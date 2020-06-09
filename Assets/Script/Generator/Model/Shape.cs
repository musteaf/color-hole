using System;
using Script.Core.ShapeNS;
using UnityEngine;

namespace Script.Generator.Model
{
    [Serializable]
    public class Shape
    {
        public Vector3 firstPosition;
        public Vector3 firstEulerAngles;
        public ShapeType shapeType;
        public AcceptanceType acceptanceType;
        public Vector3 firstScale;
        
        public Shape(Script.Core.ShapeNS.Shape shape)
        {
            var shapeTransform = shape.transform;
            firstPosition = shapeTransform.position;
            firstEulerAngles = shapeTransform.eulerAngles;
            shapeType = shape.ShapeType;
            firstScale = shapeTransform.localScale;
            acceptanceType = shape.AcceptanceType;
        }
    }
}