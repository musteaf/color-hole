using System.Collections;
using Script.Animation;
using UnityEngine;

namespace Script.Core
{
    public class HoleMovement : MonoBehaviour
    {
        private HoleAnimation holeAnimation;
        public void Awake()
        {
            holeAnimation = gameObject.GetComponent<HoleAnimation>();
            holeAnimation.SetContinuingAction(MoveSlowly);
            holeAnimation.SetStopAction(MoveToTheTargetPosition);
        }

        private void MoveToMiddlePoint()
        {
            holeAnimation.Speed = 2f;
            var currentPosition =  transform.position;
            var middlePoint = new Vector3(0, currentPosition.y, currentPosition.z);
            holeAnimation.StartAnimation(currentPosition,middlePoint,middlePoint);
        }
        
        public void MoveToTheNextBoard(Vector3 endPosition)
        {
            MoveToMiddlePoint();
            StartCoroutine(MoveToTheNextBoardEnumerator(endPosition));
        }

        private IEnumerator MoveToTheNextBoardEnumerator(Vector3 endPosition)
        {
            yield return new WaitForSeconds(0.5f);
            holeAnimation.Speed = 0.4f;
            holeAnimation.StartAnimation(transform.position, endPosition, endPosition);
        }

        public void MoveToTheTargetPosition(Vector3 position)
        {
            transform.localPosition = position;
        }
        
        private void MoveSlowly(Vector3 start, Vector3 end, float disc)
        {
            transform.localPosition = Vector3.Lerp(start, end, disc);
        }
    }
}