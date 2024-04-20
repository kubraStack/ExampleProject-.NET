using ConsoleUI;

Console.WriteLine("Hello World!");

int number = 10;

//OOP => Gerçek hayattaki nesneyi bilgisayara tanıtmak.

//Kalıptan üretilen bir örnek (intance üretmek)
Product product = new Product(1,"Kazak"); //Constructor, ctor -> Yapıcı blok
product.Name = "Kazak";
product.Id = 1;

//C# 8 ile gelen bir syntax var oda newleme işleminde nesne adını yazmamak
Product product2 = new();

Product product1 = new Product();
product1.Name = "Laptop";
product1.Id = 2;

Customer customer = new Customer();
customer.FirstName = "Ali";
customer.LastName = "Sönmez";
customer.Email = "ali@gmail.com";
customer.TaxNo = "1245638";

//Min. User'ın gereksinimlerini karşılayabilecek bir obje.
User user = new User();
User user1 = new Admin(); //Polymorphism => Inherit edilen class'ı kullanabilme.
User user2 = new Customer();
User user3 = new Shipper();



//Yazılım işçi sınıflara da nesne olarak bakar.Manager,DataAccessLayer(DaL)
//OOP Concepts -> Access Modifiers,Constructor, Inheritance(Kalıtım), Polymorphism(Çok Biçimlilik-Yerine Geçilebilirlik)
//Abstraction
