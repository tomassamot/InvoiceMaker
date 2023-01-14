namespace back_end.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ProductId { get; set; }
        public int AccountId { get; set; }

        public Invoice()
        { 
        }
        public Invoice(int id, DateTime dateTime, int productId, int accountId)
        {
            Id = id;
            DateTime = dateTime;
            ProductId = productId;
            AccountId = accountId;
        }
    }
}
