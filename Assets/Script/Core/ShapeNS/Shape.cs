using Script.Core.BoardNS;
using Script.Influencer;
using UnityEngine;

namespace Script.Core.ShapeNS
{
    public abstract class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeType shapeType;
        [SerializeField] protected AcceptanceType acceptanceType;
        protected Board board;
        private Rigidbody rigidbody;
        protected bool isActiveted = false; 
        private int id;
        private BlackHole blackHole;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        
        public abstract void Collected();

        public void LeaveHole(BlackHole blackHole)
        {
            BlackHole.onBlackHole -= MoveToBlackHole;
            this.blackHole = null;
        }

        public void LeaveHole()
        {
            if (blackHole == null) return;
            BlackHole.onBlackHole -= MoveToBlackHole;
            blackHole = null;
        }

        public void EnterHole(BlackHole blackHole)
        {
            if (!isActiveted) return;
            this.blackHole = blackHole;
            BlackHole.onBlackHole += MoveToBlackHole;
        }

        public void EnterMagneticField(Transform holeTransform)
        {
            ActiveRigidBody();
            var diff = holeTransform.position - transform.position;
            GetComponent<Rigidbody>().AddForce(- new Vector3(0f,1f,0f) * 50f);
            GetComponent<Rigidbody>().AddForce(diff * 30f);
        }

        public void MoveToBlackHole(Transform holeTransform)
        {
            var diff = holeTransform.position - transform.position;
            rigidbody.AddForce(- new Vector3(0f,1f,0f) * 200f);
            rigidbody.AddForce(diff * 1f);
        }

        public void ActiveShape(Generator.Model.Shape shape, int id, Vector3 midPosition)
        {
            LeaveHole();
            this.id = id;
            var currentTransform = transform;
            currentTransform.position = shape.firstPosition + midPosition;
            currentTransform.eulerAngles = shape.firstEulerAngles;
            currentTransform.localScale = shape.firstScale;
            shapeType = shape.shapeType;
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            isActiveted = false;
        }
        
        public void ActiveShape(int id, Vector3 position, Vector3 eulerAngles, ShapeType shapeType)
        {
            LeaveHole();
            this.id = id;
            var currentTransform = transform;
            currentTransform.position = position;
            currentTransform.eulerAngles = eulerAngles;
            this.shapeType = shapeType;
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            isActiveted = false;
        }

        private void ActiveRigidBody()
        {
            isActiveted = true;
            rigidbody.isKinematic = false;
        }
        
        public ShapeType ShapeType
        {
            get => shapeType;
            set => shapeType = value;
        }

        public AcceptanceType AcceptanceType
        {
            get => acceptanceType;
            set => acceptanceType = value;
        }

        public Board Board
        {
            get => board;
            set => board = value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && this.id.Equals(((Shape)obj).id);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
