using System;
using System.Collections.Generic;
using Bangazon;
using Bangazon.Models;
using Bangazon.Managers;
using Xunit;

namespace Bangazon.Tests
{
    public class ProductManagerShould: IDisposable
    {
        private readonly ProductManager _pm;
        private readonly DatabaseInterface _dab;
        private readonly CustomerManager _cm;
        public ProductManagerShould()
        {
            _dab = new DatabaseInterface("BANGAZONCLI_TEST_DB");
            _pm = new ProductManager(_dab);
            _cm = new CustomerManager(_dab);

            _dab.CheckCustomerTable();
            _dab.CheckProductTable();
        }
        [Fact]
        public void AddCustomerProductShould()
        {
            // Create a customer
            // int id = _dab.Insert($"INSERT into customer values (null, 'jack', 'turnbladt', '434 paper street', 'Assville', 'MN', '90210', '424-546-9822')");

            // _cm.AddCustomer(new Customer()
            //     {
            //         id = 1,
            //         firstName = "jack",
            //         lastName = "turnbladt",
            //         address = "434 paper street",
            //         city = "Assville",
            //         state = "MN",
            //         zipCode = "90210",
            //         phoneNumber = "424-546-9822"
            //     }
            // );

            Product newProduct = new Product()
                {
                    
                    Name = "nameit",
                    CustomerId = CustomerManager.activeCustomer,
                    Price = 12.99,
                    Quantity = 300,
                    Description = "description"
                };
            
            _pm.AddCustomerProduct(newProduct);

            List<Product> result = _pm.GetProductList();

            Assert.Contains(newProduct, result);
        }

        [Fact]
        public void GetProductListShould()
        {
            List<Product> products = _pm.GetProductList();

            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void SelectProductShould()
        {
            int productId = _pm.SelectProduct(0);
            Assert.IsType<int>(productId);
            Assert.True(productId > 0);
        }

        public void Dispose()
        {
            _dab.Delete("DELETE from product");
            _dab.Delete("DELETE from customer");
        }
    }
}