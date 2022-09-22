using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance());
        }


        public IActionResult Index()
        {
            var products = ProductService.GetProductsForAllCategory();
            return View(products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Filtered(int id)
        {
            var products = ProductService.GetProductsForCategory(id);
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Cart()
        {
            var products = ProductService.GetProductsForAllCategory();
            return View(products.ToList());
        }

        [Route("/api/AddToCart")]
        [HttpPost]
        public JsonResult AddToCart([FromBody] int productId)
        {

            var products = ProductService.GetProductsForAllCategory();

            foreach (var product in products)
            {
                if (product.Id == productId)
                {
                    product.HowManyIsInCart += 1;
                }
            }

            return Json("");
        }

        [Route("/api/DeleteFromCart")]
        [HttpPost]
        public JsonResult DeleteFromCart([FromBody] int productId)
        {

            var products = ProductService.GetProductsForAllCategory();

            foreach (var product in products)
            {
                if (product.Id == productId)
                {
                    if (product.HowManyIsInCart > 0)
                    {
                        product.HowManyIsInCart -= 1;
                    }
                }
            }

            return Json("");
        }


        [Route("/api/GetNumberOfItemsInCart")]
        [HttpGet]
        public int GetNumberOfItemsInCart()
        {
            var number = 0;
            var products = ProductService.GetProductsForAllCategory();

            foreach (var product in products)
            {
                number += product.HowManyIsInCart;
            }


            return number;
        }


        [Route("/api/GetItemsPrice")]
        [HttpGet]
        public double GetItemsPrice()
        {
            var number = 0.0;
            var products = ProductService.GetProductsForAllCategory();

            foreach (var product in products)
            {
                number += product.HowManyIsInCart * decimal.ToDouble(product.DefaultPrice);
            }


            return number;
        }


    }
}

