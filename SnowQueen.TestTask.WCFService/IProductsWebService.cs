using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using SnowQueen.TestTask.DataAccess.Dtos;

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


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ProductDataContract
    {
        string name;
        decimal price;
        int amount;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
