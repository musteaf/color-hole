namespace Core.ShapeNS
{
    public class AcceptableShape : Shape
    {
        public override void Collected()
        {
            if (!isActiveted) return;
            LeaveHole();
            board.Collected(this);
        }
    }
}
