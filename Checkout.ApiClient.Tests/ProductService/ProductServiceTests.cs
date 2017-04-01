using Checkout.ApiServices.Products.RequestModels;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ProductServiceTests : BaseServiceTests
    {
        [Test]
        public void CreateProduct()
        {
            var productCreateModel = TestHelper.GetProductCreateModel();
            var response = CheckoutClient.ProductService.CreateProduct(productCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("prod_");
            productCreateModel.ShouldBeEquivalentTo(response.Model, options => options.Excluding(x => x.Description));
        }

        [Test]
        public void DeleteProduct()
        {
            var productCreateModel = TestHelper.GetProductCreateModel();
            var product =
                CheckoutClient.ProductService.CreateProduct(productCreateModel).Model;

            var response = CheckoutClient.ProductService.DeleteProduct(productCreateModel.Name);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetProduct()
        {
            var productCreateModel = TestHelper.GetProductCreateModel();
            var product = CheckoutClient.ProductService.CreateProduct(productCreateModel).Model;

            var response = CheckoutClient.ProductService.GetProduct(product.Name);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be(product.Name);
            response.Model.Id.Should().StartWith("prod_");
            product.ShouldBeEquivalentTo(response.Model);
        }

        [Test]
        public void GetProductList()
        {
            var startTime = DateTime.UtcNow.AddHours(-1); // records for the past hour

            var product1 = CheckoutClient.ProductService.CreateProduct(TestHelper.GetProductCreateModel());
            var product2 = CheckoutClient.ProductService.CreateProduct(TestHelper.GetProductCreateModel());
            var product3 = CheckoutClient.ProductService.CreateProduct(TestHelper.GetProductCreateModel());
            var product4 = CheckoutClient.ProductService.CreateProduct(TestHelper.GetProductCreateModel());

            var prodGetListRequest = new ProductGetList
            {
                FromDate = startTime,
                ToDate = DateTime.UtcNow
            };

            //Get all Products created
            var response = CheckoutClient.ProductService.GetProductList(prodGetListRequest);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Count.Should().BeGreaterOrEqualTo(4);

            response.Model.Select(x => x.Name).Should().Contain(product4.Model.Name);
            response.Model.Select(x => x.Name).Should().Contain(product3.Model.Name);
            response.Model.Select(x => x.Name).Should().Contain(product2.Model.Name);
            response.Model.Select(x => x.Name).Should().Contain(product1.Model.Name);
        }

        [Test]
        public void UpdateProduct()
        {
            var productCreateModel = TestHelper.GetProductCreateModel();
            var product =
            CheckoutClient.ProductService.CreateProduct(productCreateModel).Model;

            var productUpdateModel = TestHelper.GetProductUpdateModel(product.Name);
            var response = CheckoutClient.ProductService.UpdateProduct(productUpdateModel.Name, productUpdateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }
    }
}
