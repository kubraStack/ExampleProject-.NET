using MyApplication.Entities;
using MyApplication.Services;

Product product = new Product();
product.Name = "Monitör";
product.Id = 1;
product.UnitPrice = 5000;

BaseProductService productService = new ProductService();
productService.AddProductWithLoggging(product);


BaseProductService productServiceMysql = new ProductServiceMysql();
productServiceMysql.AddProductWithLoggging(product);