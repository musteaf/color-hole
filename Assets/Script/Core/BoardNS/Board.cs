using System.Collections.Generic;
using System.Linq;
using Script.Core.ShapeNS;
using UnityEngine;

namespace Script.Core.BoardNS
{
    public delegate void BroadCastLevel(float level);
    public class Board : MonoBehaviour
    {
        // To make it more dynamic I used so many references here.
        // may we can calculate positions according to bounds but
        // If the new object does not match the calculations, that would harder to change calculations.
        private int acceptableShapeCount;
        private int collectedAcceptableShapeCount;
        private HashSet<Shape> shapes;
        [SerializeField] private Transform cameraReference;
        [SerializeField] private Transform holeReference;
        [SerializeField] private Transform fillerReference;
        [SerializeField] private BoardType boardType;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private GameObject[] limits;
        public void CollectRemains()
        {
            foreach (var shape in shapes)
            {
                shape.LeaveHole();
                ShapePool.instance.AddToPool(shape);
            }
            shapes.Clear();
        }

        public void Collected(Shape shape)
        {
            ShapePool.instance.AddToPool(shape);
            shapes.Remove(shape);
            collectedAcceptableShapeCount++;
            BroadCastLevel(Id + (collectedAcceptableShapeCount / (float) acceptableShapeCount));
            if (collectedAcceptableShapeCount == acceptableShapeCount)
            {
                BoardManager.instance.BoardCollected(this);
            }
        }

        public int Id { get; private set; }

        public BroadCastLevel BroadCastLevel { get; set; }

        public Vector3 GetCameraPosition()
        {
            return cameraReference.transform.position;
        }
        
        public Vector3 GetHolePosition()
        {
            return holeReference.transform.position;
        }
        
        public Vector3 GetFillerPosition()
        {
            return fillerReference.transform.position;
        }
        
        public Vector3 GetMidPosition()
        {
            return BoardMath.FindMiddlePoint(limits.ToList());
        }
        
        public Limits GetLimits()
        {
            return BoardMath.CalculateLimits(limits.ToList());
        }

        public BoardType BoardType
        {
            get => boardType;
            set => boardType = value;
        }
        
        public MeshRenderer MeshRenderer
        {
            get => meshRenderer;
            set => meshRenderer = value;
        }

        public void Initialize(int acceptableShapeCount, HashSet<Shape> shapeSet, int id)
        {
            this.acceptableShapeCount = acceptableShapeCount;
            shapes = shapeSet;
            this.Id = id;
            collectedAcceptableShapeCount = 0;
        }
    }
}
