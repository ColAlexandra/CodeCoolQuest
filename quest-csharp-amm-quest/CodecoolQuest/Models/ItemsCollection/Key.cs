namespace Codecool.Quest.Models.ItemsCollection
{
    public class Key : Items
    {
        public override string TileName => "key";

        public Key(Cell cell) : base(cell)
        {
        }
    }
}