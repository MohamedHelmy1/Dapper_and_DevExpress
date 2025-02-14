using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Task1.DataModel;
using Task1.Services.Product;

namespace Task1.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductServeses productServeses;

        public ProductController(IProductServeses productServeses)
        {
            this.productServeses = productServeses;
        }
        public async Task<ActionResult> Index()
        {
           var products =await productServeses.GetProducts();
            return View(products);
            
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Products model)
        {
            if (ModelState.IsValid)
            {
               var data =await productServeses.AddProduct(model);
                if (data == 0)
                {
                    ViewBag.Message = "Product not added";
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Edite(int id)
        {
            var product = await productServeses.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult> Edite(Products model)
        {
            if (ModelState.IsValid)
            {
                var data = await productServeses.UpdateProduct(model);
                if (data == 0)
                {
                    ViewBag.Message = "Product not updated";
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await productServeses.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Products model)
        {
            var data = await productServeses.DeleteProduct(model.Id);
            if (data == 0)
            {
                ViewBag.Message = "Product not deleted";
                return View();
            }
            return RedirectToAction("Index");
        }





    }
}
