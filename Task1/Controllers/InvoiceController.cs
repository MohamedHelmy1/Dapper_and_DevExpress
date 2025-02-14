using Microsoft.AspNetCore.Mvc;
using Task1.DataModel;
using Task1.Services.Invoices;
using Task1.Services.Product;

namespace Task1.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceServeces invoiceServeces;
        private readonly IProductServeses productService;

        public InvoiceController(IInvoiceServeces invoiceServeces, IProductServeses productService)
        {
            this.invoiceServeces = invoiceServeces;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await invoiceServeces.GetAllInvoices();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await productService.GetProducts();
            ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {

            if (ModelState.IsValid)
            {
                await invoiceServeces.AddInvioce(invoice);
                return RedirectToAction(nameof(Index));
            }
            var allProducts = await productService.GetProducts();
            ViewBag.Products = allProducts;
            return View(invoice);
        
        }
    }
}
