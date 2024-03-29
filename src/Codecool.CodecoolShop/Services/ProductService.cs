using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }
        
        public IEnumerable<Product> GetProductsForAllCategory()
        {
            return this.productDao.GetAll();
        }
        
        public IEnumerable<Product> GetProductsForMultipleCategories(List<int> categoryIdList)
        {
            List<Product> products = new List<Product>();
            foreach (var categoryId in categoryIdList)
            {
                ProductCategory category = productCategoryDao.Get(categoryId);
                foreach (var product in productDao.GetBy(category))
                {
                    products.Add(product);
                }
            }
            return products;
        }
        
    }
}
