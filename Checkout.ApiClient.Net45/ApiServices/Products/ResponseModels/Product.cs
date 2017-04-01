using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Products.ResponseModels
{
    public class Product : SharedModels.Product
    {
        public string Id { get; set; }
        public bool LiveMode { get; set; }
        public string Created { get; set; }
    }
}
