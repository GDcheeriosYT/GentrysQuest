namespace GentrysQuest.Game.Entity
{
    public class MovementDetails(int direction, bool isDodge)
    {
        public int Direction = direction;
        public bool IsDodge = isDodge;
    }
}
