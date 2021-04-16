using ShopBridge.Models;
using ShopBridge.Services.Infrastructure;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ShopBridge.Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _context;
        public ProductRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public async Task<HttpStatusCode> AddProduct(Product product)
        {

            try
            {

                if (!string.IsNullOrEmpty(product.ProductName))
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> DeleteProduct(int Id)
        {
            try
            {
                Product product = await _context.Products.FindAsync(Id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<Product> GetProductID(int Id)
        {
            var product = (from item in _context.Products where item.ProductID == Id select item).SingleOrDefaultAsync();
            return await product;
        }

        public async Task<HttpStatusCode> UpdateProduct(Product product)
        {
            try
            {
                var productFromDb = _context.Products.Where(x => x.ProductID == product.ProductID);
                if (productFromDb.Count() > 0)
                {
                    if (productFromDb != null)
                    {
                        _context.Entry(product).State = EntityState.Detached;
                    }
                    _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}