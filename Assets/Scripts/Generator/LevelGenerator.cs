using System.Collections.Generic;
using System.Linq;
using Core;
using Generator.Model;
using Core.ShapeNS;
using UnityEngine;
using Shape = Core.ShapeNS.Shape;

namespace Generator
{ 
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject parent1;
        [SerializeField] private GameObject parent2;
        [SerializeField] private int levelNumber;
        [SerializeField] private bool produce;

        void Update()
        {
            if (produce)
            {
                produce = false;
                var board1 = ProduceShapeList(parent1);
                var board2 = ProduceShapeList(parent2);
                var level = new Level(levelNumber, board1, board2);
                ResourceSaveAndLoad.SerializeLevel(level, Global.path);
            }
        }
        
        private List<Model.Shape> ProduceShapeList(GameObject parent)
        {
            var shapes = new List<Model.Shape>();
            AddToShapes<AcceptableShape>(parent, shapes);
            AddToShapes<UnAcceptableShape>(parent, shapes);
            return shapes;
        }

        private void AddToShapes<T>(GameObject parent, List<Model.Shape> shapes) where T:Shape
        {
            shapes.AddRange(parent.GetComponentsInChildren<T>().Select(shape => new Model.Shape(shape)));
        }
        
        
        
        
    }
}
