namespace TodayIHad.Domain.Entities
{
    public class FoodUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Seq { get; set; }
        public float Amount { get; set; }
        public float GramWeight { get; set; }

        public virtual int FoodId { get; set; }

    }
}
