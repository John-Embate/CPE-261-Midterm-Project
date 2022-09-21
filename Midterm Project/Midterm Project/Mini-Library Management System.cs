using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Midterm_Project
{
    class Book
    {
        //Fields
        protected string title;
        protected string author;
        protected string ISBN;
        protected string publication;
        protected DateTime? borrowed_date = null;
        protected DateTime? due_date = null;
        protected float fine_lost;
        protected float fine_overdue_per_day;
        protected bool is_reserved = false;
        protected bool is_lost = false;
        static protected List<Book> Books = new List<Book>();

        //Properties
        public string GetTitle
        {
            get { return title; }
        }
        public string GetAuthor
        {
            get { return author; }
        }
        public string GetISBN
        {
            get { return ISBN; }
        }
        public string Getpublication
        {
            get { return publication; }
        }
        public DateTime? Due_date
        {
            get { return due_date; }
        }
        public float Fine_lost
        {
            get { return fine_lost; }
        }
        public float Fine_overdue_per_day
        {
            get { return fine_overdue_per_day; }
        }
        public static List<Book> GetBooks
        {
            get { return Books; }
        }
        protected bool Is_borrowed
        {
            get { return borrowed_date != null; }
        }

        //Constructor   
        public Book(string title, string author, string ISBN, string publication, float fine_lost = 0, float fine_overdue_per_day = 0, bool append = false)
        {
            this.title = title;
            this.author = author;
            this.ISBN = ISBN;
            this.publication = publication;
            this.fine_lost = fine_lost;
            this.fine_overdue_per_day = fine_overdue_per_day;
            if (append)
            {
                Books.Add(this);
            }
        }

        //Methods

        public static Book GetBook(Book book)
        {
            return Books.FirstOrDefault(x => x == book);
        }

        public static Book GetBook(string title, string author)
        {
            return Books.FirstOrDefault(x => (x.title == title && x.author == author));
        }

        public DateTime? ShowDueDate(bool console_write = false)
        {
            if (console_write)
            {
                if (due_date != null)
                    Console.WriteLine($"This book's due date: {due_date:MM/dd/yyyy}.");
                else
                    Console.WriteLine("WARNING: This book has no due date because it was not borrowed.");
            }
            return due_date;
        }

        public bool Reservation_status(bool console_write = false)
        {
            if (console_write)
            {
               Console.WriteLine($"This book's reservation status: {((is_reserved)?"Reserved":"Not Reserved")}.");
            }
            return is_reserved;
        }

        public static void Print_books(List<Book> books_to_print)
        {
            int i = 1;
            foreach (Book book in books_to_print)
            {
                Console.WriteLine($"Book [{i}] Information: ");
                Console.WriteLine($"Title: {book.title}");
                Console.WriteLine($"Author: {book.author}");
                Console.WriteLine($"ISBN: {book.ISBN}");
                Console.WriteLine($"Publication: {book.publication}");
                Console.WriteLine($"Lost Fine: {book.fine_lost}");
                Console.WriteLine($"Overdue Per Day Fine: {book.fine_overdue_per_day}");

                Console.WriteLine();
                i++;
            }
        }

        public static void Print_book(Book book_to_print)
        {
                Console.WriteLine($"Book Information: ");
                Console.WriteLine($"Title: {book_to_print.title}");
                Console.WriteLine($"Author: {book_to_print.author}");
                Console.WriteLine($"ISBN: {book_to_print.ISBN}");
                Console.WriteLine($"Publication: {book_to_print.publication}");
                Console.WriteLine($"Lost Fine: {book_to_print.fine_lost}");
                Console.WriteLine($"Overdue Per Day Fine: {book_to_print.fine_overdue_per_day}");

                Console.WriteLine();
        }

        //return tuple
        private static (Book, string) BooksGet(List<Book> books_to_access, Account account)
        {

            Print_books(books_to_access);

            bool continue_while_loop, try_again = false, try_set_of_books = false;
            (Book book_to_pass, string status) = (null, null);
            if(books_to_access.Count == 0)
            {
                Console.WriteLine("This set of books is empty. Please try another set of books");
            }
            else if (books_to_access.Count > 0)
            {
                do
                {
                    continue_while_loop = false;
                    int book_choice;
                    Console.Write("Which book would you like to borrow/reserve. Please type the number: ");
                    book_choice = int.Parse(Console.ReadLine()) - 1;

                    if (books_to_access[book_choice].is_reserved)
                    {
                        if (account.GetReservedBooks.Contains(books_to_access[book_choice]) || !books_to_access[book_choice].Is_borrowed)
                        {
                            Console.WriteLine("This book is available and was already reserved by you thus, you can borrow it now. Thank you for waiting.");
                            Console.Write("Please enter the borrowed date for this book in this date format (mm/dd/yyyy): ");
                            DateTime borrowed_date = DateTime.ParseExact(Console.ReadLine(), "M/d/yyyy", CultureInfo.InvariantCulture);
                            Console.Write("Please enter the date you would like it to return for the due date in this date format (mm/dd/yyyy): ");
                            DateTime due_date = DateTime.ParseExact(Console.ReadLine(), "M/d/yyyy", CultureInfo.InvariantCulture);
                            books_to_access[book_choice].borrowed_date = borrowed_date;
                            books_to_access[book_choice].due_date = due_date;
                            books_to_access[book_choice].is_reserved = false;
                            account.GetReservedBooks.Remove(books_to_access[book_choice]);
                            (book_to_pass, status) = (books_to_access[book_choice], "borrow");
                            try_again = false;
                        }
                        else
                        {
                            int choice;
                            Console.WriteLine("Sorry this book is already reserved. Want to try another book or choose another set of books to choose?");
                            Console.WriteLine("[0] Try another book\n[1] Try Another Set of Books");
                            Console.WriteLine("Selection: ");
                            choice = int.Parse(Console.ReadLine());
                            if (choice == 0)
                            {
                                try_again = true;
                            }
                            else if (choice == 1)
                            {
                                try_set_of_books = true;
                            }
                            else
                            {
                                continue_while_loop = true;
                            }
                        }
                    }
                    else if (books_to_access[book_choice].Is_borrowed)
                    {
                        int choice;
                        Console.WriteLine("This book is already borrowed. Want to try another book or choose another set of books to choose or make a reservation?");
                        Console.WriteLine("[0] Try another book\n[1] Try Another Set of Books\n[2] Reserve");
                        Console.Write("Selection: ");
                        choice = int.Parse(Console.ReadLine());
                        if (choice == 0)
                        {
                            try_again = true;
                        }
                        else if (choice == 1)
                        {
                            try_set_of_books = true;
                        }
                        else if (choice == 2)
                        {
                            books_to_access[book_choice].is_reserved = true;
                            (book_to_pass, status) = (books_to_access[book_choice], "reserve");
                        }
                        else
                        {
                            continue_while_loop = true;
                        }
                    }
                    else if (books_to_access[book_choice].Is_borrowed == false)
                    {
                        int choice;
                        Console.WriteLine("This book is free to borrow/reserve. Want to borrow or reserve or choose another set of books to choose?");
                        Console.WriteLine("[0] Borrow\n[1] Reserve\n[2] Try Another Set of Books");
                        Console.Write("Selection: ");
                        choice = int.Parse(Console.ReadLine());
                        if (choice == 0)
                        {
                            //Input Date Borrowed and Due date
                            Console.Write("Please enter the borrowed date for this book in this date format (mm/dd/yyyy): ");
                            DateTime borrowed_date = DateTime.ParseExact(Console.ReadLine(), "M/d/yyyy", CultureInfo.InvariantCulture);
                            Console.Write("Please enter the date you would like it to return for the due date in this date format (mm/dd/yyyy): ");
                            DateTime due_date = DateTime.ParseExact(Console.ReadLine(), "M/d/yyyy", CultureInfo.InvariantCulture);
                            books_to_access[book_choice].borrowed_date = borrowed_date;
                            books_to_access[book_choice].due_date = due_date;
                            (book_to_pass, status) = (books_to_access[book_choice], "borrow");
                            try_again = false;
                        }
                        else if (choice == 1)
                        {
                            books_to_access[book_choice].is_reserved = true;
                            (book_to_pass, status) = (books_to_access[book_choice], "reserve");
                            try_again = false;
                        }
                        else if (choice == 2)
                        {
                            try_set_of_books = true;
                        }
                        else
                        {
                            continue_while_loop = true;
                        }
                    }
                    if (continue_while_loop || try_again)
                    {
                        Console.Write("Status: ");
                        Console.WriteLine($"{((try_again) ? "Try Again" : "WARNING!: Wrong input key, please try again.")}");
                        Console.WriteLine("--------------------------------------------------");
                    }

                } while (continue_while_loop || try_again);
            }

            if (try_set_of_books)
            {
                (book_to_pass, status) = (null,null);
            }
            Console.WriteLine("--------------------------------------------------");
            return (book_to_pass, status);
        }

        public static (Book, string) BookRequest(Account account)
        {
            bool try_again;
            int choice;
            (Book book_to_pass, string status) = (null, null); //tuple
            do
            {
                try_again = false;

                Console.WriteLine("Please choose what you want to do: ");
                Console.WriteLine("[1] Show All books");
                Console.WriteLine("[2] Show All unborrowed books");
                Console.WriteLine("[3] Show All borrowed unreserved books");
                Console.WriteLine("[4] Show All borrowed reserved books");
                Console.WriteLine("[5] EXIT");
                Console.Write("Selection: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("--------------------------------------------------");

                List<Book> queried_books;
                if (choice == 1)
                {
                    queried_books = Books;
                    (book_to_pass, status) = BooksGet(queried_books, account);
                }
                else if (choice == 2)
                {
                    queried_books = (from book_ in Books
                                     where book_.Is_borrowed == false && book_.is_lost == false
                                     select book_).ToList();
                    (book_to_pass, status) = BooksGet(queried_books, account);
                }
                else if (choice == 3)
                {
                    queried_books = (from book_ in Books
                                    where book_.Is_borrowed == true && book_.is_reserved == false && book_.is_lost == false
                                    select book_).ToList();
                    (book_to_pass, status) = BooksGet(queried_books, account);
                }
                else if (choice == 4)
                {
                    queried_books = (from book_ in Books
                                     where book_.Is_borrowed == true && book_.is_reserved == true && book_.is_lost == false
                                     select book_).ToList();
                    (book_to_pass, status) = BooksGet(queried_books, account);
                }
                else if (choice == 5)
                {
                    (book_to_pass, status) = (null, null);
                }
                else
                {
                    try_again = true;
                    Console.WriteLine("WARNING: Wrong Input. Please try again.");
                }
            } while (try_again || (choice != 5 && status == null)); //If user want another set of books and status is null, while loop will repeat

            return (book_to_pass, status);
        }

        public void BookReturn()
        {
            borrowed_date = null;
            due_date = null;
        }

        public void BookDeclaredLost()
        {
            is_lost = true;
        }
    }


    class User
    {
        //Fields
        protected string Name;
        protected string ID;
        protected static List<User> Users = new List<User>();

        //Properties
        public static List<User> GetUsers
        {
            get { return Users; }
        }

        public string GetID
        {
            get { return ID; }
        }

        public string GetName
        {
            get { return Name; }
        }

        //Methods
        public static bool Verify(string name, string id, bool verbose = false) //check if name and id exist in the List
        {
            bool found = Users.Any(x => (x.Name == name && x.ID == id));
            if (verbose)
            {
                Verification_message(found);
            }
            return found;
        }

        public static bool Verify(string name_or_id, bool verbose = false) //check if either name or id exist in the List
        {
            bool found = Users.Any(x => (x.Name == name_or_id || x.ID == name_or_id));
            if (verbose)
            {
                Verification_message(found);
            }
            return found;
        }

        public static bool Verify(User user, bool verbose = false)
        {
            bool found = Users.Contains(user);
            if (verbose)
            {
                Verification_message(found);
            }
            return found;
        }

        protected static void Verification_message(bool found, string object_type = "user")
        {
            Console.WriteLine($"VERBOSE STATUS: The {object_type} {((found)?"already":"doesn't")} exist in the database.{((found)?"":" If you think this is a mistake, please don't forget to append/push")}");
        }

        public static void Append(User user, bool verbose = false)  //Append user to Users list
        {
            if (!Verify(user.Name, user.ID, verbose)) // Append to Users list if name & id doesn't exist in Users list
            {
                Users.Add(user);
            }
            else
            {
                Console.WriteLine($"Can't append User: {user.Name} w/ ID: ({user.ID}) :: \"Already Exist\"");
            }
        }

        public static User GetUser(User user)
        {
            return Users.FirstOrDefault(x => x == user);
        }

        public static User GetUser(string name, string id)
        {
            return Users.FirstOrDefault(x => (x.Name == name && x.ID == id));
        }

        //Constructors
        public User(string name, string id, bool append = false, bool verbose = false)
        {
            this.Name = name;
            this.ID = id;
            if (append)
            {
                Append(this, verbose);
            }
        }

        public User(User user, bool append = false, bool verbose = false)
        {
            if (append)
            {
                Append(user, verbose);
            }
        }

        public User()
        {
            //empty Constructor
        }

    }

    class Account : User
    {
        //Fields
        protected List<Book> BorrowedBooks = new List<Book>();
        protected List<Book> ReservedBooks = new List<Book>();
        protected List<Book> ReturnedBooks = new List<Book>();
        protected List<Book> LostBooks = new List<Book>();
        protected static List<Account> Accounts = new List<Account>();
        protected static DateTime date_time_present; //date_time now/recent


        //Properties 
        public static List<Account> GetAccounts
        {
            get { return Accounts;  }
        }
        public List<Book> GetBorrowedBooks
        {
            get { return BorrowedBooks; }
        }
        public List<Book> GetReservedBooks
        {
            get { return ReservedBooks; }
        }
        public List<Book> GetReturnedBooks
        {
            get { return ReturnedBooks; }
        }
        public List<Book> GetLostBooks
        {
            get { return LostBooks; }
        }
        public static DateTime GetDateTimePresent
        {
            get { return date_time_present; }
        }
        public int NoOfBorrowedBooks
        {
            get {return BorrowedBooks.Count; }
        }
        public int NoOfReservedBooks
        {
            get { return ReservedBooks.Count; }
        }
        public int NoOfReturnedBooks
        {
            get { return ReturnedBooks.Count; }
        }
        public int NoOfLostBooks
        {
            get { return LostBooks.Count; }
        }
        public float FineAmount
        {
            get
            {
                return this.CalculateFine(date_time_present);
            }
        }

        //Constructor
        public Account(string name, string id, bool append = false, bool verbose = false) : base(name, id, append, verbose)
        {
            this.Name = name;
            this.ID = id;
            if (append)
            {
                Append_account(this, verbose);
            }
        }

        public Account(User user, bool append = false, bool verbose = false) : base(user, append, verbose)
        {
            //No need to set Name and Id for the object inside this constructor
            //becuase we are already calling the base constructor
            if (append)
            {
                Append_account(this, verbose);
            }
        }

        //Methods
        public static bool VerifyAccount(string name, string id, bool verbose = false)
        {
            bool found = Accounts.Any(x => (x.Name == name && x.ID == id));
            if (verbose)
            {
                User.Verification_message(found, "account");
            }
            return found;
        }

        public static bool VerifyAccount(string name_id, bool verbose = false)
        {
            bool found = Accounts.Any(x => (x.Name == name_id || x.ID == name_id));
            if (verbose)
            {
                User.Verification_message(found, "account");
            }
            return found;
        }

        public static bool VerifyAccount(Account account, bool verbose = false)
        {
            bool found = Accounts.Contains(account);
            if (verbose)
            {
                User.Verification_message(found, "account");
            }
            return found;
        }

        public static void Append_account(Account account, bool verbose = false)
        {
            if (!VerifyAccount(account.Name, account.ID, verbose)) // Append to Librarians list if (name & id) / librarian object doesn't exist in Librarians list
            {
                Accounts.Add(account);
                if (verbose)
                {
                    Console.WriteLine($"Successfully registering Account: {account.Name} w/ ID ({account.ID}) to the database");
                }
            }
            else
            {
                Console.WriteLine($"Can't append Account: {account.Name} w/ ID ({account.ID}) :: \"Already Exist\"");
            }
        }

        public static Account GetAccount(Account account)
        {
            return Accounts.FirstOrDefault(x => x == account);
        }

        public static Account GetAccount(string name, string id)
        {
            return Accounts.FirstOrDefault(x => (x.Name == name && x.ID == id));
        }

        public void Inquire_or_Request_Book()
        {
            (Book book_passed, string status) = Book.BookRequest(this);
            if (status == null)
            {
                Console.WriteLine("No book was passed for borrowing/reservation. User may have exited or no books were available");
                Console.WriteLine("--------------------------------------------------");
            }
            else if (status.Equals("borrow"))
            {
                BorrowBook(book_passed);
            }
            else if (status.Equals("reserve"))
            {
                ReserveBook(book_passed);
            }
        }

        public void ReturnBook(Book book)
        {
            ReturnedBooks.Add(book);
            book.BookReturn();
            BorrowedBooks.Remove(book);
        }

        private void ReserveBook(Book book)
        {
            this.ReservedBooks.Add(book);
        }

        private void BorrowBook(Book book)
        {
            this.BorrowedBooks.Add(book);
        }

        public void DeclareLostBook(Book book)
        {
            this.LostBooks.Add(book);
            book.BookDeclaredLost();
        }

        public static void Update_Recent_DateTime(DateTime update)
        {
            date_time_present = update;
        }

        public float CalculateFine(DateTime date_time_recent, bool console_write = false)   //this will calculate the fines based on the argument passed to date_time_recent parameter
        {                                                                                   // but this won't change the value of DateTime date_time_present
                                                                                            // to do it properly use the Update_Recent_DateTime method
            float tot_lost_fine = 0, tot_overdue_fine = 0, tot_fine_amount = 0;
            //Create LINQ query then cast from IEnumerabale<T> to List<T> then call ForEach method to pass lambda expression to continously
            //add values to tot_overdue_fine where due_date is greater than zero
            (from book_ in BorrowedBooks
             where ((TimeSpan)(date_time_recent - book_.Due_date)).Days > 0
             select book_).ToList().ForEach(x => tot_overdue_fine += ((TimeSpan)(date_time_recent - x.Due_date)).Days * x.Fine_overdue_per_day);

            //Use Foreach method of type List to pass lambda
            LostBooks.ForEach(x => tot_lost_fine += x.Fine_lost);

            tot_fine_amount = tot_lost_fine + tot_overdue_fine;
            if (console_write)
            {
                Console.WriteLine($"{this.Name}'s Total Fine based on date({date_time_recent:MM/dd/yyyy}): {tot_fine_amount:f4}");
            }
            return tot_fine_amount;
        }
    }

    class Librarian : User
    {
        //Field
        protected string password;
        protected static List<Librarian> Librarians = new List<Librarian>();

        //Properties
        public static List<Librarian> GetLibrarians
        {
            get { return Librarians; }
        }

        //Constructor
        public Librarian(string name, string id, string password, bool append = false, bool verbose = false)
        {
            this.Name = name;
            this.ID = id;
            this.password = password;
            if (append)
            {
                User.Append(this, verbose);
                Append_librarian(this, verbose);
            }
        }

        public Librarian(User user, string password, bool append = false, bool verbose = false)
        {
            this.Name = user.GetName;
            this.ID = user.GetID;
            this.password = password;
            if (append)
            {
                // We don't include !User.Verify(name,id) in our conditional statement because it won't matter 
                // The user object might be already present in the Users list but not in the Librarians list
                User.Append(this, verbose);
                Append_librarian(this, verbose);
            }
        }

        //Methods
        public static bool VerifyLibrarian(string name, string id, string password, bool verbose = false)
        {
            bool found = Librarians.Any(x => (x.Name == name && x.ID == id && x.password == password));
            if (verbose)
            {
                User.Verification_message(found, "librarian");
            }
            return found;
        }

        public static bool VerifyLibrarian(string name, string id, bool verbose = false)
        {
            bool found = Librarians.Any(x => (x.Name == name && x.ID == id));
            if (verbose)
            {
                User.Verification_message(found, "librarian");
            }
            return found;
        }

        public static bool VerifyLibrarian_ID_Pass(string id, string password, bool verbose = false)
        {
            bool found = Librarians.Any(x => (x.ID == id && x.password == password));
            if (verbose)
            {
                User.Verification_message(found, "librarian");
            }
            return found;
        }

        public static bool VerifyLibrarian(string name_id_password, bool verbose = false)
        {
            bool found = Librarians.Any(x => (x.Name == name_id_password || x.ID == name_id_password || x.password == name_id_password));
            if (verbose)
            {
                User.Verification_message(found, "librarian");
            }
            return found;
        }

        public static bool VerifyLibrarian(Librarian librarian, bool verbose = false)
        {
            bool found = Librarians.Contains(librarian);
            if (verbose)
            {
                User.Verification_message(found, "librarian");
            }
            return found;
        }

        public static void Append_librarian(Librarian librarian, bool verbose = false)
        {
            if (!VerifyLibrarian(librarian.Name, librarian.ID, verbose)) // Append to Librarians list if (name & id) / librarian object doesn't exist in Librarians list
            {
                Librarians.Add(librarian);
                if (verbose)
                {
                    Console.WriteLine($"Successfully registering Librarian: {librarian.Name} w/ ID ({librarian.ID}) to the database");
                }
            }
            else
            {
                Console.WriteLine($"Can't append Librarian: {librarian.Name} w/ ID ({librarian.ID}) :: \"Already Exist\"");
            }
        }

        public static Librarian GetLibrarian(Librarian librarian)
        {
           return Librarians.FirstOrDefault(x => x == librarian);
        }

        public static Librarian GetLibrarian(string name, string id, string password)
        {
           return Librarians.FirstOrDefault(x => (x.Name == name && x.ID == id && x.password == password));
        }

        public static Librarian GetLibrarian(string id, string password)
        {
            return Librarians.FirstOrDefault(x => (x.ID == id && x.password == password));
        }

        public Book Search(Book book, bool verbose = false)
        {
            bool search_result = Book.GetBooks.Contains(book);
            if (verbose)
            {
                Console.WriteLine($"The book {((search_result) ? "" : "doesn't")} exist in the library.{((search_result) ? "" : " If you think this is a mistake, please don't forget to append/push")}");
            }
            return Book.GetBook(book);
        }

        public Book Search(string title, string author, bool verbose = false)
        {
            bool search_result = Book.GetBooks.Any(x => (x.GetTitle == title && x.GetAuthor == author));
            if (verbose)
            {
                Console.WriteLine($"The book {((search_result) ? "" : "doesn't ")}exist in the library.{((search_result) ? "" : " If you think this is a mistake, please don't forget to append/push")}");
            }
            return Book.GetBook(title, author);
        }
    }
}
