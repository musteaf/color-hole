using System.Collections;
using Script.Animation;
using Script.Core.BoardNS;
using UnityEngine;

namespace Script.Core
{
    public class CameraController : MonoBehaviour
    {
        private Camera camera;
        private CameraAnimation cameraAnimation;

        private void Awake()
        {
            camera = Camera.main;
            cameraAnimation = GetComponent<CameraAnimation>();
            cameraAnimation.SetContinuingAction(MoveSlowly);
            cameraAnimation.SetStopAction(MoveToTheTargetPosition);
        }
        
        private void MoveToNextBoard(Vector3 startPosition, Vector3 endPosition)
        {
            var lastPos = AdjustCameraAngle(endPosition);
            cameraAnimation.StartAnimation(AdjustCameraAngle(startPosition),
                lastPos, lastPos);
        }

        private void MoveToTheTargetPosition(Vector3 position)
        {
            transform.localPosition = position;
        }
        
        public void SetCameraPosition(Vector3 position)
        {
            transform.localPosition = AdjustCameraAngle(position);
        }
        
        private void MoveSlowly(Vector3 start, Vector3 end, float disc)
        {
            transform.localPosition = Vector3.Lerp(start, end, disc);
        }
        
        private Vector3 AdjustCameraAngle(Vector3 refPosition)
        {
            var currentBoard = BoardManager.instance.GetCurrentBoard();
            var meshRenderer = currentBoard.MeshRenderer;
            var bounds = meshRenderer.bounds;
            var objectSizes = bounds.max - bounds.min;
            var w = Mathf.Max(objectSizes.x);
            w *= 4.68f / 5f;
            var angleX = 90 - camera.transform.eulerAngles.x;
            var a = camera.fieldOfView * 0.5f;
            var h = w / camera.aspect;
            var distance1 = h * 0.5f / Mathf.Tan(a * Mathf.Deg2Rad);
            var distance2 = h * 0.5f / Mathf.Tan((90 - angleX) * Mathf.Deg2Rad);
            var zDistance = h * 0.5f / Mathf.Sin((90 - angleX) * Mathf.Deg2Rad);
            var vecMid = refPosition;
            vecMid.z += zDistance;
            return vecMid - ((distance1 + distance2) * camera.transform.forward);
        }

       public IEnumerator MoveToNextBoardEnumerator(Vector3 startPosition, Vector3 endPosition)
        {
            yield return new WaitForSeconds(0.5f);
            MoveToNextBoard(startPosition, endPosition);
        }
    }
}
