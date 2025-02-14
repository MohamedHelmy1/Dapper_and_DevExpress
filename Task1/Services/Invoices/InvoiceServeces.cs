using Dapper;
using System.Data;
using Task1.DataModel;

namespace Task1.Services.Invoices
{
    public class InvoiceServeces : IInvoiceServeces
    {
        private readonly IDbConnection db;

        public InvoiceServeces(IDbConnection db)
        {
            this.db = db;
        }

        public async Task<bool> AddInvioce(Invoice invoice)
        {
            db.Open(); //لازم افتح connection
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var insertInvoiceSql = @"
                            INSERT INTO Invoice (Code, RegisterationDate, CustomerName)
                            VALUES (@Code, @RegisterationDate, @CustomerName);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                    var invoiceId = await db.ExecuteScalarAsync<int>(insertInvoiceSql, new
                    {
                        invoice.Code,
                        invoice.RegisterationDate,
                        invoice.CustomerName
                    }, transaction);

                    foreach (var product in invoice.Products)
                    {
                        var productId = product.Id;

                        var insertInvoiceProductSql = @"
                                INSERT INTO InvoiceProduct (InvoiceId, ProductId,Price)
                                VALUES (@InvoiceId, @ProductId,@Price);";

                        await db.ExecuteAsync(insertInvoiceProductSql, new
                        {
                            InvoiceId = invoiceId,
                            ProductId = productId,
                            Price= product.Price
                        }, transaction);
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var sql = @"
                    SELECT 
                        i.Id, i.Code, i.RegisterationDate, i.CustomerName,
                        p.Id, p.Name, p.Price
                    FROM Invoice i
                    LEFT JOIN InvoiceProduct ip ON i.Id = ip.InvoiceId
                    LEFT JOIN Products p ON ip.ProductId = p.Id";

            var invoiceDictionary = new Dictionary<int, Invoice>();

            var result = await db.QueryAsync<Invoice, Products, Invoice>(
                sql,
                (invoice, product) =>
                {
                    if (!invoiceDictionary.TryGetValue(invoice.Id, out var currentInvoice))
                    {
                        currentInvoice = invoice;
                        currentInvoice.Products = new List<Products>();
                        invoiceDictionary.Add(currentInvoice.Id, currentInvoice);
                    }
                    currentInvoice.Products.Add(product);
                    return currentInvoice;
                },
                splitOn: "Id"
            );

            return invoiceDictionary.Values.ToList();
        }

        public Task<Invoice> GetInvioce(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
