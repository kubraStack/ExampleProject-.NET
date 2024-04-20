namespace ConsoleUI
{
    //Inheritance yapmamızın sebebi DRY = Do not repeat yourself(kendini tekrar etme)
    //Bir class birden fazla class'dan inheritance alamaz.Bunu kullanmak için inheritance kullanılır.
    public class Customer:User
    {
        public string TaxNo { get; set; }
    }
}
