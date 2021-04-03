using ShopBridge.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class ProductController : ApiController
    {
        DataBaseContext db = new DataBaseContext();

        [HttpGet]

        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }
        [HttpGet]

        public Product GetProduct(int id)
        {
            return db.Products.Find(id);
        }
        [HttpPost]
        public HttpResponseMessage AddProduct(Product product)
        {
            var errors = new List<string>();
            try
            {

                if (!string.IsNullOrEmpty(product.ProductName))
                {
                    db.Products.Add(product);
                    db.SaveChanges();
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
        public HttpResponseMessage UpdateProduct(Product product)
        {
            try
            {
                var productFromDb = db.Products.Where(x => x.ProductID == product.ProductID);
                if (productFromDb.Count() > 0)
                {
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
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
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
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
