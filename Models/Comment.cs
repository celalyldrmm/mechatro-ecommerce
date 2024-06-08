namespace mechatro_ecommerce.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string Review { get; set; }
    }
}
