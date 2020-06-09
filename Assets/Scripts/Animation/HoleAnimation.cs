using UnityEngine;

namespace Animation
{
    public class HoleAnimation : Animation<Vector3, Vector3>
    {
        public float Speed { get; set; } = 1;
        
        protected override void Run()
        {
            var disc = (Time.time - startTime) * Speed;
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