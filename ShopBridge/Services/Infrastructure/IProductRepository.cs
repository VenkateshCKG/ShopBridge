using ShopBridge.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Services.Infrastructure
{
    interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductID(int Id);
        Task<HttpStatusCode> DeleteProduct(int Id);
        Task<HttpStatusCode> UpdateProduct(Product item);
        Task<HttpStatusCode> AddProduct(Product item);
    }
}
