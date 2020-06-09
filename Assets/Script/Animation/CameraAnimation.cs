using UnityEngine;

namespace Script.Animation
{
    public class CameraAnimation : Animation<Vector3, Vector3>
    {
        protected override void Run()
        {
            var disc = (Time.time - startTime) * 0.4f;
            if (disc <= 1f)
            {
                continuingAction(pose1, pose2, disc);
            }
            else
            {
                stopAction(lastPose);
                started = false;
            }
        }
    }
}