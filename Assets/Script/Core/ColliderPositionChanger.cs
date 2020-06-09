using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Core
{
    public class ColliderPositionChanger : MonoBehaviour
    {
        private MeshCollider meshCollider;
        private Mesh mesh;
        [SerializeField] private Transform holetransform;
        private Vector3 lastHoleTransformPosition;
        private bool[] closeVertices;

        private void Start()
        {
            meshCollider = GetComponent<MeshCollider>();
            mesh = meshCollider.sharedMesh;
            mesh = Instantiate(mesh);
            meshCollider.sharedMesh = mesh;
            FindCloseVertices(mesh.vertices);
            lastHoleTransformPosition = holetransform.position;
        }

        private void FixedUpdate()
        {
            var currentHolePosition = holetransform.position;
            if (lastHoleTransformPosition == currentHolePosition) return;
            var diff = currentHolePosition - lastHoleTransformPosition;
            lastHoleTransformPosition = currentHolePosition;
            var vertices = new List<Vector3>(mesh.vertices);
            for (var i = 0; i < vertices.Count; i++)
            {
                if (closeVertices[i])
                    vertices[i] += diff;
            }
            mesh.vertices = vertices.ToArray();
            mesh.RecalculateBounds();
            meshCollider.sharedMesh = mesh;
        }

        private void FindCloseVertices(Vector3[] verticesArr)
        {
            closeVertices = new bool[verticesArr.Length];
            var vertices = new List<Vector3>(verticesArr);
            var total = vertices.Sum(v => Vector3.Distance(v, Vector3.zero));
            var mean = total/ vertices.Count;
            for (var i = 0; i < vertices.Count; i++)
            {
                var distance = Vector3.Distance(vertices[i], Vector3.zero);
                if (distance > mean)
                {
                    this.closeVertices[i] = false;
                }else
                {
                    this.closeVertices[i] = true;
                }
            }
        }
    }
}
