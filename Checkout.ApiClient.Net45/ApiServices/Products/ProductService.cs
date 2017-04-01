using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ApiServices.Products.RequestModels;
using Checkout.ApiServices.Products.ResponseModels;
using Checkout.ApiServices.SharedModels;
using Checkout.Utilities;

namespace Checkout.ApiServices.Products
{
    public class ProductService
    {
        public HttpResponse<ResponseModels.Product> CreateProduct(ProductCreate requestModel)
        {
            return new ApiHttpClient().PostRequest<ResponseModels.Product>(string.Format(ApiUrls.Product, string.Empty), AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> UpdateProduct(string productName, ProductUpdate requestModel)
        {
            var updateProductUri = string.Format(ApiUrls.Product, string.Empty);
            return new ApiHttpClient().PutRequest<OkResponse>(updateProductUri, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> DeleteProduct(string productName)
        {
            var deleteProductUri = string.Format(ApiUrls.Product, productName);
            return new ApiHttpClient().DeleteRequest<OkResponse>(deleteProductUri, AppSettings.SecretKey);
        }

        public HttpResponse<ResponseModels.Product> GetProduct(string productName)
        {
            var getProductUri = string.Format(ApiUrls.Product, productName);
            return new ApiHttpClient().GetRequest<ResponseModels.Product>(getProductUri, AppSettings.SecretKey);
        }

        public HttpResponse<List<ResponseModels.Product>> GetProductList(ProductGetList request)
        {
            var getProductsListUri = string.Format(ApiUrls.Product, string.Empty);

			return new ApiHttpClient().GetRequest<List<ResponseModels.Product>>(getProductsListUri, AppSettings.SecretKey);
        }
    }
}