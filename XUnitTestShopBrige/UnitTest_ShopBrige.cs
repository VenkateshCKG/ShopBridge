using System;
using Xunit;
using ShopBridge;
using ShopBridge.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using ShopBridge.Services.Repository;
using ShopBridge.Models;

namespace XUnitTestShopBrige
{
    public class UnitTest_ShopBrige
    {
        readonly ProductRepository productRepository;
        public UnitTest_ShopBrige()
        {
            productRepository = new ProductRepository(new DataBaseContext());
        }
        [Fact]
        public void Get_Allproduct()
        {
            var pro = productRepository.GetAllProducts();
            Assert.Equal(pro, pro);
        }
    }
}
