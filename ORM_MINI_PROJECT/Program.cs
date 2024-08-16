using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ORM_MINI_PROJECT.Context;
using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Implementations;
using System.Text.RegularExpressions;
using ORM_MINI_PROJECT.Services.Interfances;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using ORM_MINI_PROJECT.Repositories.Implementations;

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
            OrderService orderService = new OrderService();
            OrderDetailService orderDetailService = new OrderDetailService();
            PaymentService paymentService = new PaymentService();
            User loggedUser = new User();
            while (true)
            {

            firstMenu:
                Console.WriteLine("Welcome MyMarket");
                Console.WriteLine("Register");
                Console.WriteLine("Login");
                Console.WriteLine("Excel export");
                Console.WriteLine("Exit");
                string select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("Please enter full name:");
                            string fullName = Console.ReadLine().Trim();

                            Console.WriteLine("Please enter email:");
                            string email = Console.ReadLine().Trim().ToLower();

                            if (!email.Contains("@"))
                            {
                                email += "@gmail.com";
                            }

                            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                            if (!Regex.IsMatch(email, emailPattern))
                            {
                                throw new InvalidUserInformationException("Invalid email format.");
                            }

                            Console.WriteLine("Please enter Password:");
                            string password = Console.ReadLine().Trim();

                            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
                            if (!Regex.IsMatch(password, passwordPattern))
                            {
                                throw new InvalidUserInformationException("Password must be at least 8 characters long and include one uppercase letter, one lowercase letter, and one digit.");
                            }

                            Console.WriteLine("Please enter Address:");
                            string address = Console.ReadLine().Trim();

                            UserDto userDto = new()
                            {
                                Fulname = fullName,
                                Email = email,
                                Password = password,
                                Address = address
                            };

                            await userService.CreateUserAsync(userDto);
                            Console.WriteLine("User registered successfully.");
                        }
                        catch (InvalidUserInformationException e)
                        {
                            Console.WriteLine($"Error: {e.Message}");
                            goto firstMenu;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"An unexpected error occurred: {e.Message}");
                        }

                        goto firstMenu;

                    case "2":

                        Console.WriteLine("Please enter email");
                        string LoginEmail = Console.ReadLine().Trim().ToLower();


                        try
                        {
                            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                            if (!Regex.IsMatch(LoginEmail, emailPattern))
                            {
                                Console.WriteLine("Invalid email format.");
                                goto firstMenu;
                            }

                            Console.WriteLine("Please enter Password");
                            string LoginPassword = Console.ReadLine().Trim();

                            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
                            if (!Regex.IsMatch(LoginPassword, passwordPattern))
                            {
                                Console.WriteLine("Invalid password format. Password must be at least 8 characters long, including letters and numbers.");
                                goto firstMenu;
                            }

                            userPostDto userPostDto = new()
                            {
                                Email = LoginEmail,
                                Password = LoginPassword
                            };

                            var userExists = appContext.Users.Any(u => u.Email == LoginEmail && u.Password == LoginPassword);
                            if (!userExists)
                            {
                                Console.WriteLine("Email or password is incorrect.");
                                goto firstMenu;
                            }

                            userService.Login(userPostDto);
                            loggedUser.Id = userService.Login(userPostDto).Id;
                            Console.Clear();

                            var users = await appContext.Users.ToListAsync();
                            foreach (var item in users)
                            {
                                if (userPostDto.Email == item.Email)
                                {
                                    Console.WriteLine($"Welcome, {item.Fulname}!");
                                }
                            }
                        }
                        catch (UserAuthenticationException ex)
                        {
                            Console.WriteLine(ex.Message);
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
                            Console.WriteLine("0.Exit Porduct Service");

                            while (true)
                            {

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Please enter product name:");
                                            string productName = Console.ReadLine().Trim();
                                            if (string.IsNullOrWhiteSpace(productName))
                                            {
                                                await Console.Out.WriteLineAsync(" name is null.");
                                                goto ProductMenu;

                                            }



                                            await Console.Out.WriteLineAsync("Please enter product price:");
                                            if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice) || productPrice <= 0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid price.");
                                                goto ProductMenu;
                                            }

                                            await Console.Out.WriteLineAsync("Please enter product stock:");
                                            if (!int.TryParse(Console.ReadLine(), out int stock) || stock <= 0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid stock.");
                                                goto ProductMenu;
                                            }

                                            await Console.Out.WriteLineAsync("Please enter product description:");
                                            string DescriptionProduct = Console.ReadLine();

                                            ProductDto productDto = new ProductDto()
                                            {
                                                Name = productName,
                                                Price = productPrice,
                                                Stock = stock,
                                                Description = DescriptionProduct
                                            };

                                            await productService.CreateProductAsync(productDto);
                                            await Console.Out.WriteLineAsync("Successfully created the product.");
                                        }
                                        catch (InvalidProductException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto ProductMenu;

                                    case "2":

                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Please enter product id:");
                                            if (!int.TryParse(Console.ReadLine(), out int productid))
                                            {
                                                await Console.Out.WriteLineAsync("Invalid product id.");
                                                goto ProductMenu;
                                            }

                                            await Console.Out.WriteLineAsync("Please enter product name:");
                                            string productNameUpdate = Console.ReadLine();

                                            await Console.Out.WriteLineAsync("Please enter product price:");
                                            if (!decimal.TryParse(Console.ReadLine(), out decimal productPriceUpdate) || productPriceUpdate <= 0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid price.");
                                                goto ProductMenu;
                                            }

                                            await Console.Out.WriteLineAsync("Please enter product stock:");
                                            if (!int.TryParse(Console.ReadLine(), out int stockUpdate) || stockUpdate <= 0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid stock.");
                                                goto ProductMenu;
                                            }

                                            await Console.Out.WriteLineAsync("Please enter product description:");
                                            string DescriptionProductUpdate = Console.ReadLine();

                                            var productsUptude = await appContext.Product.ToListAsync();
                                            var productFound = false;

                                            foreach (var item in productsUptude)
                                            {
                                                if (productid == item.Id)
                                                {
                                                    ProductDto productDtoUpdate = new ProductDto()
                                                    {
                                                        Id = productid,
                                                        Name = productNameUpdate,
                                                        Price = productPriceUpdate,
                                                        Stock = stockUpdate,
                                                        Description = DescriptionProductUpdate
                                                    };

                                                    await productService.UpdateProductAsync(productDtoUpdate);
                                                    await Console.Out.WriteLineAsync("Successfully updated.");
                                                    productFound = true;
                                                    break;
                                                }
                                            }

                                            if (!productFound)
                                            {
                                                await Console.Out.WriteLineAsync("Id is wrong.");
                                            }
                                        }
                                        catch (NotFoundException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto ProductMenu;

                                    case "3":
                                        try
                                        {
                                            var usersGetAll = await appContext.Product.ToListAsync();

                                            if (usersGetAll.Count == 0)
                                            {
                                                await Console.Out.WriteLineAsync("No products found.");
                                            }
                                            else
                                            {
                                                foreach (var item in usersGetAll)
                                                {
                                                    await Console.Out.WriteLineAsync(
                                                        $"ID: {item.Id}, Name: {item.Name}, Price: {item.Price}, Description: {item.Description}, Created Date: {item.CreatedDate}, Updated Date: {item.UpdatedDate}");
                                                }
                                            }
                                        }
                                        catch (NotFoundException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto ProductMenu;

                                    case "5":

                                        try
                                        {
                                            var usersGetAll = await appContext.Product.ToListAsync();

                                            if (usersGetAll.Count == 0)
                                            {
                                                await Console.Out.WriteLineAsync("No products found.");
                                            }
                                            else
                                            {
                                                foreach (var item in usersGetAll)
                                                {
                                                    await Console.Out.WriteLineAsync(
                                                        $"ID: {item.Id}, Name: {item.Name}, Price: {item.Price}, Description: {item.Description}, Created Date: {item.CreatedDate}, Updated Date: {item.UpdatedDate}");
                                                }
                                            }
                                        }
                                        catch (NotFoundException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }
                                        await Console.Out.WriteLineAsync("Please enter product ID to delete:");

                                        int deleteProductId;
                                        if (!int.TryParse(Console.ReadLine(), out deleteProductId) || deleteProductId <= 0)
                                        {
                                            await Console.Out.WriteLineAsync("Invalid ID format. Please enter a valid number.");
                                            goto ProductMenu;
                                        }

                                        try
                                        {
                                            var product = await appContext.Product.FirstOrDefaultAsync(p => p.Id == deleteProductId);

                                            if (product == null)
                                            {
                                                await Console.Out.WriteLineAsync("Product not found with the given ID.");
                                            }
                                            else
                                            {
                                                await productService.DeleteProductAsync(deleteProductId);
                                                await Console.Out.WriteLineAsync("Product successfully deleted.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto ProductMenu;


                                    case "4":
                                        try
                                        {
                                            var usersGetAll = await appContext.Product.ToListAsync();

                                            if (usersGetAll.Count == 0)
                                            {
                                                await Console.Out.WriteLineAsync("No products found.");
                                            }
                                            else
                                            {
                                                foreach (var item in usersGetAll)
                                                {
                                                    await Console.Out.WriteLineAsync(
                                                        $"ID: {item.Id}, Name: {item.Name}, Price: {item.Price}, Description: {item.Description}, Created Date: {item.CreatedDate}, Updated Date: {item.UpdatedDate}");
                                                }
                                            }
                                        }
                                        catch (NotFoundException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }
                                        await Console.Out.WriteLineAsync("Please enter product ID:");

                                        int getAllProductId;
                                        if (!int.TryParse(Console.ReadLine(), out getAllProductId))
                                        {
                                            await Console.Out.WriteLineAsync("Invalid ID format. Please enter a valid number.");
                                            goto ProductMenu;
                                        }

                                        try
                                        {
                                            var product = await appContext.Product.FirstOrDefaultAsync(p => p.Id == getAllProductId);

                                            if (product == null)
                                            {
                                                await Console.Out.WriteLineAsync("Product not found with the given ID.");
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}, Created Date: {product.CreatedDate}, Updated Date: {product.UpdatedDate}");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto ProductMenu;

                                    case "6":
                                        await Console.Out.WriteLineAsync("Please enter product name:");
                                        string productFilter = Console.ReadLine();

                                        try
                                        {
                                            List<ProductDto> products = await productService.SearchByName(productFilter);

                                            if (products.Any())
                                            {
                                                foreach (var item in products)
                                                {
                                                    await Console.Out.WriteLineAsync($"ID: {item.Id}, Name: {item.Name}, Price: {item.Price}, Description: {item.Description}");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("No products found with the given name.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto ProductMenu;

                                    case "0":
                                        goto firstMenu;
                                    default:
                                        await Console.Out.WriteLineAsync("wrong select");
                                        goto ProductMenu;


                                }


                            }

                        case "2":
                        OrderMenu:
                            Console.WriteLine("-----------------------------------------------------------------");
                            Console.WriteLine("Welcome to the Order Service");
                            Console.WriteLine("1.Create Order");
                            Console.WriteLine("2.Cancel Order");
                            Console.WriteLine("3.Get Orders");
                            Console.WriteLine("4.Add OrderDetail");
                            Console.WriteLine("0.Exit Order Service");
                            string selectOrder = Console.ReadLine();

                            while (true)
                            {
                                switch (selectOrder)
                                {

                                    case "1":
                                        try
                                        {
                                            var usersGetAll = await appContext.Users.ToListAsync();
                                            if (usersGetAll.Any())
                                            {
                                                foreach (var item in usersGetAll)
                                                {
                                                    await Console.Out.WriteLineAsync($"ID: {item.Id}, Name: {item.Fulname}");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("No users found.");
                                            }

                                            await Console.Out.WriteLineAsync("Enter user ID:");
                                            string userIdInput = Console.ReadLine();

                                            if (int.TryParse(userIdInput, out int userId) || userId <= 0)
                                            {
                                                var newOrder = new OrderDto
                                                {
                                                    UserId = userId,
                                                    Status = Enums.OrderStatus.pending,
                                                };

                                                await orderService.CreateOrderAsync(newOrder, loggedUser.Id);
                                                await Console.Out.WriteLineAsync("Order successfully created.");
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("Invalid user ID. Please enter a valid number.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto OrderMenu;

                                    case "2":
                                        try
                                        {
                                            var usersorderAll = await orderService.GetAllOrdersAsync(loggedUser.Id);
                                            if (usersorderAll.Any())
                                            {
                                                foreach (var item in usersorderAll)
                                                {
                                                    await Console.Out.WriteLineAsync($"ID: {item.Id}, UserId {item.UserId} TotalAmount {item.TotalAmount}  Status {item.Status.ToString()}");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("No users found.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Enter order ID:");
                                            string orderIdInput = Console.ReadLine();

                                            if (int.TryParse(orderIdInput, out int orderId) || orderId <= 0)
                                            {
                                                var orderToCancel = await orderService.GetOrderByIdAsync(orderId,loggedUser.Id);

                                                if (orderToCancel != null)
                                                {
                                                    orderToCancel.Status = Enums.OrderStatus.Cancelled;
                                                    await orderService.UpdateOrderAsync(orderToCancel,loggedUser.Id);
                                                    await Console.Out.WriteLineAsync("Order successfully cancelled.");
                                                }
                                                else
                                                {
                                                    await Console.Out.WriteLineAsync("Order not found.");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("Invalid order ID. Please enter a valid number.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto OrderMenu;

                                    
                                       
                                    case "3":
                                        try
                                        {
                                            var usersorderAll = await orderService.GetAllOrdersAsync(loggedUser.Id);
                                            if (usersorderAll.Any())
                                            {
                                                foreach (var item in usersorderAll)
                                                {
                                                    await Console.Out.WriteLineAsync($"ID: {item.Id}, UserId {item.UserId} TotalAmount {item.TotalAmount}  Status {item.Status.ToString()}");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("No users found.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto OrderMenu;

                                    case "4":
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Enter order ID:");
                                            string addDetailOrderIdInput = Console.ReadLine();

                                            if (int.TryParse(addDetailOrderIdInput, out int addDetailOrderId) || addDetailOrderId <= 0)
                                            {
                                                var orders = await orderService.GetOrderByIdAsync(addDetailOrderId,loggedUser.Id);

                                                if (orders != null)
                                                {
                                                    await Console.Out.WriteLineAsync("Enter product ID:");
                                                    string addDetailProductIdInput = Console.ReadLine();

                                                    if (int.TryParse(addDetailProductIdInput, out int addDetailProductId) || addDetailProductId <= 0)
                                                    {
                                                        await Console.Out.WriteLineAsync("Enter quantity:");
                                                        string quantityInput = Console.ReadLine();

                                                        if (int.TryParse(quantityInput, out int quantity) && quantity > 0)
                                                        {
                                                            var newOrderDetail = new OrderDetailDto
                                                            {
                                                                OrderID = addDetailOrderId,
                                                                ProductID = addDetailProductId,
                                                                Quantity = quantity
                                                            };

                                                            await orderDetailService.CreateOrderDetailAsync(newOrderDetail);
                                                            await Console.Out.WriteLineAsync("Order detail successfully added.");
                                                        }
                                                        else
                                                        {
                                                            await Console.Out.WriteLineAsync("Invalid quantity. Please enter a positive number.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        await Console.Out.WriteLineAsync("Invalid product ID. Please enter a valid number.");
                                                    }
                                                }
                                                else
                                                {
                                                    await Console.Out.WriteLineAsync("Order not found or you do not have permission to add details to this order.");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("Invalid order ID. Please enter a valid number.");
                                            }
                                        }
                                        catch (InvalidOrderException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }
                                        catch (NotFoundException ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }
                                        goto OrderMenu;

                                    case "0":
                                        goto ServicesMenu;
                                    default:
                                        Console.WriteLine("Invalid selection, please try again.");



                                        goto OrderMenu;

                                }
                            }
                        case "3":
                        paymentMenu:
                            while (true)
                            {
                                Console.WriteLine("-------------------------------------------------------------");
                                Console.WriteLine("Payment Service Menu");
                                Console.WriteLine("1. Create Payment");
                                Console.WriteLine("2. Get All Payments");
                                Console.WriteLine("3. Get Payment By Id");
                                Console.WriteLine("0. Back to Main Menu");

                                string commandPeyment = Console.ReadLine();

                                switch (commandPeyment)
                                {
                                    case "1":
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Enter order ID:");
                                            string orderIdInput = Console.ReadLine();

                                            if (!int.TryParse(orderIdInput, out int orderId) || orderId <= 0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid order ID. Please enter a valid number.");
                                                goto paymentMenu;
                                            }

                                            var orderpeyment = await orderService.GetOrderByIdAsync(orderId, loggedUser.Id);
                                            if (orderpeyment == null)
                                            {
                                                await Console.Out.WriteLineAsync("Order not found.");
                                                goto paymentMenu;
                                            }

                                            await Console.Out.WriteLineAsync("Enter payment amount:");
                                            string amountInput = Console.ReadLine();

                                            if (!decimal.TryParse(amountInput, out decimal amount) || amount <= 0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid payment amount. Please enter a positive number.");
                                                goto paymentMenu;
                                            }

                                            var newPayment = new PaymentDto
                                            {
                                                OrderId = orderId,
                                                Amount = amount,
                                            };

                                            await paymentService.CreatePaymentAsync(newPayment);
                                            await Console.Out.WriteLineAsync("Payment successfully created.");

                                            orderpeyment.Status = Enums.OrderStatus.Completed;
                                            await orderService.UpdateOrderAsync(orderpeyment, loggedUser.Id);
                                            await Console.Out.WriteLineAsync("Order status updated to Completed.");
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
                                        }


                                        goto paymentMenu;
                                    case "2":
                                        try
                                        {
                                            var payments = await paymentService.GetAllPaymentsAsync();

                                            if (payments.Any())
                                            {
                                                foreach (var item in payments)
                                                {
                                                    await Console.Out.WriteLineAsync($"ID: {item.Id}, Order ID: {item.OrderId}, Amount: {item.Amount}");
                                                }
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("No payments found.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync($"An error occurred: {ex.Message}");
                                        }

                                        goto paymentMenu;

                                    case "3":
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Enter payment id:");
                                            string input = Console.ReadLine();

                                            if (!int.TryParse(input, out int paymentId)|| paymentId<=0)
                                            {
                                                await Console.Out.WriteLineAsync("Invalid ID format. Please enter a valid integer.");
                                                goto paymentMenu;
                                            }

                                            var paymentById = await paymentService.GetPaymentByIdAsync(paymentId);

                                            if (paymentById != null)
                                            {
                                                await Console.Out.WriteLineAsync($"ID: {paymentById.Id}, Order ID: {paymentById.OrderId}, Amount: {paymentById.Amount}");
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("No payment found with the provided ID.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                        goto paymentMenu;

                                    case "0":
                                        goto ServicesMenu;
                                    default:
                                        Console.WriteLine("Invalid selection, please try again.");
                                        goto paymentMenu;

                                }
                            }
                    }

                }
            }
        }
    }
}
