public abstract class Product
{
    public int Id {get; set;}
    public string Name {get; set;}
    public double Price {get; set;}
}

// Generic Shopping Cart

public class ShoppingCart<T> where T : Product
{
    private Dictionary<T,int> _cartItems = new Dictionary<T,int>();

    // Add To Cart
    public void AddToCart(T Product, int quantity)
    {
        // adds or updates product and quantity

        if(_cartItems.ContainsKey(Product))
        {
            _cartItems[Product] += quantity;
        }

        else
        {
            _cartItems.Add(Product,quantity);
        }

    }

    public double CalculateTotal(Func<T,double,double> discountCalculator = null)
    {
        double total = 0;
        foreach (var item in _cartItems)
        {
            double price = item.Key.Price * item.Value;
            if (discountCalculator != null)
                price = discountCalculator(item.Key, price);
            total += price;
        }
        return total;
    }

    // Get Top N expensive items using LINQ

    public List<T> GetTopExpensiveItems(int n)
    {
         return _cartItems
                .OrderByDescending(x => x.Key.Price)   // Sort by price descending
                .Take(n)                                // Take top N
                .Select(x => x.Key)                     // Select product
                .ToList();
    }
}

class Electronics : Product { }
class Clothing : Product { }


class Program
{
    public static void Main(string[] args)
    {
        ShoppingCart<Electronics> cart = new ShoppingCart<Electronics>();

        cart.AddToCart(new Electronics { Id = 1, Name = "Laptop", Price = 999.99 }, 1);
        cart.AddToCart(new Electronics { Id = 2, Name = "Mouse", Price = 29.99 }, 2);

        // Apply 10% discount for items over $100
        double total = cart.CalculateTotal((product, price) => price > 100 ? price * 0.9 : price);
        
        Console.WriteLine($"Total: ${total:F2}");

        var topItems = cart.GetTopExpensiveItems(1);
        
        Console.WriteLine(topItems[0].Name); // Should output: Laptop          
    }
}