namespace Core.ShapeNS
{
    public class FillerShape : Shape
    {
        public override void Collected()
        {
            if (!isActiveted) return;
            LeaveHole();
            ShapePool.instance.AddToPool(this);
        }
    }
}