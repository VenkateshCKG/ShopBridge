using ShopBridge.Models;

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
    public class ProductController : ApiController
    {
        DataBaseContext db = new DataBaseContext();
        [HttpGet]

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await db.Products.ToListAsync();
        }
        [HttpGet]

        public async Task<Product> GetProduct(int id)
        {
            var product = (from item in db.Products where item.ProductID == id select item).SingleOrDefaultAsync();
            return await product;
        }
        [HttpPost]
        public async Task<HttpResponseMessage> AddProduct(Product product)
        {
            var errors = new List<string>();
            try
            {

                if (!string.IsNullOrEmpty(product.ProductName))
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.Created, "Product Add Successfully");
                }
                else
                {
                    errors.Add(product.ProductName + " : is Mandatory");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
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
                var productFromDb = db.Products.Where(x => x.ProductID == product.ProductID);
                if (productFromDb.Count() > 0)
                {
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK, "Product Updated Successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "ProductID Mandatory");
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
                Product product = await db.Products.FindAsync(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK, "Product Deleted Successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Product Not Found");
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
