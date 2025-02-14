using Task1.DataModel;

namespace Task1.Services.Invoices
{
    public interface IInvoiceServeces
    {
        Task<bool>AddInvioce(Invoice invoice); 
        Task<Invoice> GetInvioce(int id);
        Task<List<Invoice>> GetAllInvoices();
    }
}
