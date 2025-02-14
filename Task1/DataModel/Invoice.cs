namespace Task1.DataModel
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime RegisterationDate { get; set; }
        public string CustomerName { get; set; }

        public List<Products> Products { get; set; }
    
    }
}
