﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SnowQueen.TestTask.DataComparer.ProductsWebService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductDataContract", Namespace="http://schemas.datacontract.org/2004/07/SnowQueen.TestTask.WCFService")]
    [System.SerializableAttribute()]
    public partial class ProductDataContract : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductsWebService.IProductsWebService")]
    public interface IProductsWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsWebService/AddProduct", ReplyAction="http://tempuri.org/IProductsWebService/AddProductResponse")]
        void AddProduct(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract productDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsWebService/AddProduct", ReplyAction="http://tempuri.org/IProductsWebService/AddProductResponse")]
        System.Threading.Tasks.Task AddProductAsync(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract productDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsWebService/AddSeveralProducts", ReplyAction="http://tempuri.org/IProductsWebService/AddSeveralProductsResponse")]
        void AddSeveralProducts(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[] productDtos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsWebService/AddSeveralProducts", ReplyAction="http://tempuri.org/IProductsWebService/AddSeveralProductsResponse")]
        System.Threading.Tasks.Task AddSeveralProductsAsync(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[] productDtos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsWebService/GetProducts", ReplyAction="http://tempuri.org/IProductsWebService/GetProductsResponse")]
        SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[] GetProducts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsWebService/GetProducts", ReplyAction="http://tempuri.org/IProductsWebService/GetProductsResponse")]
        System.Threading.Tasks.Task<SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[]> GetProductsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductsWebServiceChannel : SnowQueen.TestTask.DataComparer.ProductsWebService.IProductsWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductsWebServiceClient : System.ServiceModel.ClientBase<SnowQueen.TestTask.DataComparer.ProductsWebService.IProductsWebService>, SnowQueen.TestTask.DataComparer.ProductsWebService.IProductsWebService {
        
        public ProductsWebServiceClient() {
        }
        
        public ProductsWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductsWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductsWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductsWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddProduct(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract productDto) {
            base.Channel.AddProduct(productDto);
        }
        
        public System.Threading.Tasks.Task AddProductAsync(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract productDto) {
            return base.Channel.AddProductAsync(productDto);
        }
        
        public void AddSeveralProducts(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[] productDtos) {
            base.Channel.AddSeveralProducts(productDtos);
        }
        
        public System.Threading.Tasks.Task AddSeveralProductsAsync(SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[] productDtos) {
            return base.Channel.AddSeveralProductsAsync(productDtos);
        }
        
        public SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[] GetProducts() {
            return base.Channel.GetProducts();
        }
        
        public System.Threading.Tasks.Task<SnowQueen.TestTask.DataComparer.ProductsWebService.ProductDataContract[]> GetProductsAsync() {
            return base.Channel.GetProductsAsync();
        }
    }
}
