using System.Collections.Generic;
using System.ServiceModel;
using SnowQueen.TestTask.DataAccess.Dtos;

namespace SnowQueen.TestTask.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductsWebService" in both code and config file together.
    [ServiceContract]
    public interface IProductsWebService
    {
        [OperationContract]
        void AddProduct(ProductDto productDto);

        [OperationContract]
        void AddSeveralProducts(IEnumerable<ProductDto> productDtos);

        [OperationContract]
        IEnumerable<ProductDto> GetProducts();
    }
}
