namespace TodayIHad.Domain.Entities
{
    public class FollowersToFollowed
    {
        public int Id { get; set; }

        public virtual string FollowerId { get; set;}
        public virtual string FollowedId { get; set; }
    }
}
