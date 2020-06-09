using Core.BoardNS;

namespace Core.ShapeNS
{ 
    public class UnAcceptableShape : Shape
    {
        public override void Collected()
        {
            if (!isActiveted) return;
            LeaveHole();
            BoardManager.instance.RefreshLevel();
        }
    }
}
