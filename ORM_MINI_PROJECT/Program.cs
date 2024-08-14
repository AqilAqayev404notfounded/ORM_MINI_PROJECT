using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ORM_MINI_PROJECT.Context;
using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Implementations;

namespace ORM_MINI_PROJECT
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Payment payment = new Payment();
            User user = new User();
            Order order = new Order();
            OrderDetail orderDetail = new OrderDetail();
            UserService userService = new UserService();
            ProductService productService = new ProductService();
            AppDbContext appContext = new AppDbContext();
            while (true)
            {

            firstMenu:
                Console.WriteLine("Welcome MyMarket");
                Console.WriteLine("Register");
                Console.WriteLine("Login");
                Console.WriteLine("Exit");
                string select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        try
                        {
                        register:
                            Console.WriteLine("Please enter fulname");
                            string fulname = Console.ReadLine();
                            Console.WriteLine("Please enter email");
                            string email = Console.ReadLine();
                            Console.WriteLine("Please enter Pasword");
                            string pasword = Console.ReadLine();
                            Console.WriteLine("Please enter Adress");
                            string adress = Console.ReadLine();
                            UserDto userDto = new()
                            {

                                Fulname = fulname,
                                Email = email,

                                Password = pasword,
                                Address = adress
                            };
                            await userService.CreateUserAsync(userDto);
                        }
                        catch (InvalidUserInformationException e)
                        {

                            Console.WriteLine(e.Message);
                        }

                        goto firstMenu;
                    case "2":
                        Console.WriteLine("Please enter email");
                        string LoginEmail = Console.ReadLine();
                        Console.WriteLine("Please enter Password");

                        string LoginPasword = Console.ReadLine();
                        userPostDto userPostDto = new()
                        {
                            Email = LoginEmail,
                            Password = LoginPasword
                        };
                        userService.Login(userPostDto);
                        Console.Clear();
                        var users = await appContext.Users.ToListAsync();
                        foreach (var item in users)
                        {
                            if (userPostDto.Email == item.Email)
                            {
                                Console.WriteLine($"Welcome, {item.Fulname}!");
                            }
                        }
                        Console.WriteLine("======================================");
                        goto ServicesMenu;
                    case "3":
                        return;

                    default:
                        Console.WriteLine("wrong select");
                        goto firstMenu;
                }

            ServicesMenu:
                while (true)
                {


                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("1.Product Service");
                    Console.WriteLine("2.Order Service");
                    Console.WriteLine("3.Payment Service");
                    Console.WriteLine("4.Show Services");
                    Console.WriteLine("0.LogOut Account");

                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1":
                        ProductMenu:
                            Console.WriteLine("-----------------------------------------------------------------");
                            Console.WriteLine("Welcome to the Product Service");
                            Console.WriteLine("1.Add Product");
                            Console.WriteLine("2.Update Product");
                            Console.WriteLine("3.Get all products");
                            Console.WriteLine("4.Get Product By Id");
                            Console.WriteLine("5.Delete Product");
                            Console.WriteLine("6.Search Products By Name");
                            Console.WriteLine("7.Show Product Service Menu");
                            Console.WriteLine("0.Exit Porduct Service");

                            while (true)
                            {

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        await Console.Out.WriteLineAsync("please enter product name");
                                        string productName = Console.ReadLine();
                                        await Console.Out.WriteLineAsync("please enter product price");
                                        decimal productPrice = decimal.Parse(Console.ReadLine());
                                        await Console.Out.WriteLineAsync("please enter product stock");
                                        int stock = int.Parse(Console.ReadLine());
                                        await Console.Out.WriteLineAsync("please enter product Description");
                                        string DescriptionProduct = Console.ReadLine();
                                        ProductDto ProductDto = new ProductDto()
                                        {
                                            Name = productName,
                                            Price = productPrice,
                                            Stock = stock,
                                            Description = DescriptionProduct

                                        };
                                       await  productService.CreateProductAsync(ProductDto);
                                        await Console.Out.WriteLineAsync("secsesfuly");

                                        goto ProductMenu;
                                    case "2":
                                        await Console.Out.WriteLineAsync();
                                        await Console.Out.WriteLineAsync("please enter product id");
                                        int productid = int.Parse(Console.ReadLine());

                                        await Console.Out.WriteLineAsync("please enter product name");
                                        string productNameUpdate = Console.ReadLine();
                                        await Console.Out.WriteLineAsync("please enter product price");
                                        decimal productPriceUpdate = decimal.Parse(Console.ReadLine());
                                        await Console.Out.WriteLineAsync("please enter product stock");
                                        int stockUpdate = int.Parse(Console.ReadLine());
                                        await Console.Out.WriteLineAsync("please enter product Description");
                                        string DescriptionProductUpdate = Console.ReadLine();
                                        var users = await appContext.Product.ToListAsync();
                                        foreach (var item in users)
                                        {
                                            if (productid != item.Id)
                                            {
                                                await Console.Out.WriteLineAsync("Id is wrong");
                                                goto ProductMenu;
                                            }
                                        }
                                        ProductDto ProductDtoUpdate = new ProductDto()
                                        {
                                            Id = productid,
                                            Name = productNameUpdate,
                                            Price = productPriceUpdate,
                                            Stock = stockUpdate,
                                            Description = DescriptionProductUpdate
                                            

                                        };
                                        await productService.UpdateProductAsync(ProductDtoUpdate);
                                        await Console.Out.WriteLineAsync("secsesfuly");

                                        goto ProductMenu;
                                    case "3":
                                        var usersGetAll = await appContext.Product.ToListAsync();
                                        foreach (var item in usersGetAll)
                                        {
                                            await Console.Out.WriteLineAsync($"id-{item.Id}, name-{item.Name},Price -{item.Price},Description -{item.Description} ,CreatedDate-{item.CreatedDate},UpdatedDate-{item.UpdatedDate} ");
                                        }
                                        goto ProductMenu;
                                    case "5":
                                        await Console.Out.WriteLineAsync("please enter product id");
                                        int deleteProductId = int.Parse(Console.ReadLine()); 
                                        
                                        var usersDelete = await appContext.Product.ToListAsync();
                                        foreach (var item in usersDelete)
                                        {
                                            if (deleteProductId != item.Id)
                                            {
                                                await Console.Out.WriteLineAsync("Id is wrong");
                                                goto ProductMenu;
                                            }
                                        }
                                        await productService.DeleteProductAsync(deleteProductId);
                                        await Console.Out.WriteLineAsync("okey");
                                        goto ProductMenu;

                                    case "4":
                                        await Console.Out.WriteLineAsync("please enter product id");
                                        int getaLLProductId = int.Parse(Console.ReadLine());
                                        var usersGetAllId = await appContext.Product.ToListAsync();
                                        foreach (var item in usersGetAllId)
                                        {
                                            if (getaLLProductId!=item.Id)
                                            {
                                                await Console.Out.WriteLineAsync("Id is wrong");
                                                goto ProductMenu;

                                            }
                                            await Console.Out.WriteLineAsync($"id-{item.Id}, name-{item.Name},Price -{item.Price},Description -{item.Description} ,CreatedDate-{item.CreatedDate},UpdatedDate-{item.UpdatedDate} ");
                                        }
                                        goto ProductMenu;
                                    case "6":
                                        await Console.Out.WriteLineAsync("please enter product name");
                                        string productFilter = Console.ReadLine();
                                        List<ProductDto> products  = await productService.SearchByName(productFilter);
                                        if (products.Any())
                                        {
                                            foreach (var product in products)
                                            {
                                                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No products found with the given name.");
                                        }
                                           goto ProductMenu;
















                                }


                            }

                            break;



                    }

                    break;



                }


            }



        }
    }
}
