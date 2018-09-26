using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SnowQueen.TestTask.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductsWebService
    {
        [OperationContract]
        void AddProduct(ProductDataContract productDto);

        [OperationContract]
        void AddSeveralProducts(IEnumerable<ProductDataContract> productDtos);

        [OperationContract]
        IEnumerable<ProductDataContract> GetProducts();
    }

    [DataContract]
    public class ProductDataContract
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int Amount { get; set; }
    }
}
