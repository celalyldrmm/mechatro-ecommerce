namespace mechatro_ecommerce.Models
{
    public interface IOrderRepository
    {
        public bool AddToMyCart(string id);
        public void DeleteFromMyCart(string id);
        public List<OrderRepository> SelectMyCart();
        public void EfaturaCreate();
        public string OrderCreate(string Email);
        public  void Send_Sms(string OrderGroupGUID);
        public  string XMLPOST(string url, string xmlData);
        public  void Send_Email(string OrderGroupGUID);
        public List<vw_MyOrders> SelectMyOrders(string Email);

    }
}
