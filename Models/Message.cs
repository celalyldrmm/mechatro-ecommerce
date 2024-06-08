namespace mechatro_ecommerce.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string Content { get; set; }
    }
}
