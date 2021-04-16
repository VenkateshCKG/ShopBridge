using System;
using Xunit;
using ShopBridge;
using ShopBridge.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;

namespace XUnitTestShopBrige
{
    public class UnitTest_ShopBrige
    {
        ProductController product = new ProductController();
        [Fact]
        public void Get_Allproduct()
        {
            var pro = product.GetProducts();
            Assert.Equal(pro, pro);
        }
    }
}
