using System;
using System.Collections.Generic;

// TASK 5: NAMESPACE

namespace LibrarySystem
{
    // TASK 7: ENUMS
    public enum UserRole
    {
        Admin,
        Librarian,
        Member
    }

    public enum ItemStatus
    {
        Available,
        Borrowed,
        Reserved,
        Lost
    }

    // TASK 1: ABSTRACT CLASS
    public abstract class LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int ItemID { get; set; }

        public abstract void DisplayItemDetails();
        public abstract double CalculateLateFee(int daysLate);
    }

    // TASK 2: INTERFACES
    public interface IReservable
    {
        void ReserveItem();
    }

    public interface INotifiable
    {
        void SendNotification(string message);
    }

    // NESTED NAMESPACE: ITEMS
    namespace Items{
        // BOOK CLASS
        public class Book : LibraryItem, IReservable, INotifiable
        {
            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: Book");
                Console.WriteLine($"Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"Item ID: {ItemID}");
            }

            public override double CalculateLateFee(int daysLate)
            {
                return daysLate * 1.0;
            }

            // TASK 4: EXPLICIT INTERFACE IMPLEMENTATION
            void IReservable.ReserveItem()
            {
                Console.WriteLine("Book reserved successfully.");
            }

            void INotifiable.SendNotification(string message)
            {
                Console.WriteLine($"Notification: {message}");
            }
        }

        // MAGAZINE CLASS
        public class Magazine : LibraryItem
        {
            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: Magazine");
                Console.WriteLine($"Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"Item ID: {ItemID}");
            }

            public override double CalculateLateFee(int daysLate)
            {
                return daysLate * 0.5;
            }
        }

        // BONUS: EBOOK CLASS
        public class eBook : LibraryItem
        {
            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: eBook");
                Console.WriteLine($"Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"Item ID: {ItemID}");
            }

            public override double CalculateLateFee(int daysLate)
            {
                return 0; // No late fee for digital items
            }

            public void Download()
            {
                Console.WriteLine("eBook downloaded successfully.");
            }
        }
    }

    // NESTED NAMESPACE: USERS
    namespace Users
    {
        public class Member
        {
            public string Name { get; set; }
            public UserRole Role { get; set; }
        }
    

    // TASK 6: PARTIAL + STATIC
    public partial class LibraryAnalytics
    {
        public static int TotalBorrowedItems { get; set; }
    }

    public partial class LibraryAnalytics
    {
        public static void DisplayAnalytics()
        {
            Console.WriteLine($"Total Items Borrowed: {TotalBorrowedItems}");
        }
    }

    // MAIN PROGRAM
    class Program
    {
        static void Main(string[] args)
        {
            // Namespace alias
            using ItemsAlias = LibrarySystem.Items;

            // TASK 1 PROOF
            ItemsAlias.Book book = new ItemsAlias.Book
            {
                Title = "C# Fundamentals",
                Author = "John Doe",
                ItemID = 101
            };

            ItemsAlias.Magazine magazine = new ItemsAlias.Magazine
            {
                Title = "Tech Today",
                Author = "Jane Doe",
                ItemID = 201
            };

            book.DisplayItemDetails();
            Console.WriteLine($"Late Fee for 3 days: {book.CalculateLateFee(3)}\n");

            magazine.DisplayItemDetails();
            Console.WriteLine($"Late Fee for 3 days: {magazine.CalculateLateFee(3)}\n");

            // TASK 2 & 4 PROOF
            IReservable reservableBook = book;
            INotifiable notifiableBook = book;

            reservableBook.ReserveItem();
            notifiableBook.SendNotification("Please return the book on time.");

            Console.WriteLine();

            // Explanation:
            // Explicit implementation restricts access so methods can only be called through interface references.

            // TASK 3: POLYMORPHISM
            List<LibraryItem> items = new List<LibraryItem>();
            items.Add(book);
            items.Add(magazine);

            foreach (LibraryItem item in items)
            {
                item.DisplayItemDetails();
                Console.WriteLine();
            }

            Console.WriteLine("Method selection happens at runtime based on object type.\n");

            // TASK 6 PROOF
            LibraryAnalytics.TotalBorrowedItems = 5;
            LibraryAnalytics.DisplayAnalytics();
            Console.WriteLine("Static members are used because the data is shared across the system.\n");

            // TASK 7 PROOF
            Users.Member member = new Users.Member
            {
                Name = "Aryan",
                Role = UserRole.Member
            };

            ItemStatus status = ItemStatus.Borrowed;

            Console.WriteLine($"User Role: {member.Role}");
            Console.WriteLine($"Item Status: {status}");
            Console.WriteLine("Enums avoid invalid values and make code more readable.\n");

            // BONUS TASK
            if (member.Role == UserRole.Admin)
            {
                Console.WriteLine("Admin Alert: System maintenance scheduled.");
            }
            else
            {
                Console.WriteLine("Member Notification: Your borrowed item is due tomorrow.");
            }

            ItemsAlias.eBook ebook = new ItemsAlias.eBook
            {
                Title = "Learn C# Digitally",
                Author = "Online Author",
                ItemID = 301
            };

            ebook.Download();
        }
    }
}
}
