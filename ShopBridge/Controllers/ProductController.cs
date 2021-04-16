using ShopBridge.Models;
using ShopBridge.Services.Infrastructure;
using ShopBridge.Services.Repository;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopBridge.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ProductController : ApiController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        DataBaseContext db = new DataBaseContext();

        static readonly IProductRepository productRepository = new ProductRepository(new DataBaseContext());
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await productRepository.GetAllProducts();
        }
        [HttpGet]

        public async Task<Product> GetProduct(int id)
        {
            return await productRepository.GetProductID(id);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> AddProduct(Product product)
        {
            var errors = new List<string>();
            try
            {
                HttpStatusCode httpStatusCode = await productRepository.AddProduct(product);

                if (HttpStatusCode.OK == httpStatusCode)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "Product Add Successfully");
                }
                else if (HttpStatusCode.BadRequest == httpStatusCode)
                {
                    errors.Add(product.ProductName + " : is Mandatory");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                }
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateProduct(Product product)
        {
            try
            {
                HttpStatusCode httpStatusCode = await productRepository.UpdateProduct(product);

                if (HttpStatusCode.OK == httpStatusCode)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Product Updated Successfully");
                }
                else if (HttpStatusCode.BadRequest == httpStatusCode)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "ProductID Mandatory");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            try
            {
                HttpStatusCode httpStatusCode = await productRepository.DeleteProduct(id);

                if (HttpStatusCode.OK == httpStatusCode)
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK, "Product Deleted Successfully");
                }
                else if (HttpStatusCode.BadRequest == httpStatusCode)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Product Not Found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
