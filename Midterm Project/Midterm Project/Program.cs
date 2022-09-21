using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Let's create objets during compile time to easily explore their methods during run time
            //Creating Books
            new Book("Finite Automata Theory", "Cecil Jose A. Delfinado", "978-971-98-0173-3", "C & E Publishing, Inc.", 354.27F, 18.26F, true);
            new Book("The 360 Degree Leader", "John C. Maxwell", "978-1-4002-0359-8", "Thomas Nelson, Inc.", 451.27F, 25.326F, true);
            new Book("Electricity 2: Devices, Circuits, and Materials, 8th Edition", "Thomas S. Kubala", "978-981-4246-10-1", "Cengage Learning", 150.65F, 13.26F, true);
            Book new_book;

            //Creating Accounts
            new Account("John William Embate", "20-8013-354", true);
            new Account("Juan Dela Cruz", "20-8015-453", true);
            Account new_account;

            //Creating Librarians
            new Librarian("Maria Fe Cruz", "18-8231-678", "maria_cruz21", true);
            new Librarian("Alissa Mae Reyes", "17-3413-782", "Reyes_alissa72", true);
            Librarian new_librarian;

            //!!! Please change the general date_time value of all accounts here !!!///
            Account.Update_Recent_DateTime(new DateTime(2022, 9, 18));


            //For loop Starts here:
            bool continue_loop = true;
            int choice;
            do
            {
                Console.WriteLine("> What would you like to access?");
                Console.WriteLine("> [1] Book");
                Console.WriteLine("> [2] Account");
                Console.WriteLine("> [3] Librarian");
                Console.WriteLine("> [4] Exit");
                Console.Write("> Selection: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("--------------------------------------------------");
                if(choice == 1)
                {
                    BookAccess();
                }
                else if (choice == 2)
                {
                    AccountAccess();
                }
                else if (choice == 3)
                {
                    LibrarianAccess();
                }
                else if (choice == 4)
                {
                    continue_loop = false;
                }
                else
                {
                    continue_loop = true;
                    Console.WriteLine("> WARNING: Wrong Input. Please try again.");
                }
            } while (continue_loop);




            //Functions
            void BookAccess()
            {
                bool continue_loop_BA = true;
                int choice_BA;
                do
                {
                    Console.WriteLine(">>> Book <<<");
                    Console.WriteLine("> What would you like to do?");
                    Console.WriteLine("> [1] Create/add books in the library");
                    Console.WriteLine("> [2] Print all registered books in the library");
                    Console.WriteLine("> [3] Exit");
                    Console.Write("> Selection: ");
                    choice_BA = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    if (choice_BA == 1)
                    {
                        CreateBooks();
                    }
                    else if (choice_BA == 2)
                    {
                        PrintBooks();
                    }
                    else if (choice_BA == 3)
                    {
                        continue_loop_BA = false;
                    }
                    else
                    {
                        continue_loop_BA = true;
                        Console.WriteLine("> WARNING: Wrong Input. Please try again.");
                    }
                } while (continue_loop_BA);


                void CreateBooks()
                {
                    string title, author, ISBN, publication;
                    float fine_lost, fine_overdue_per_day;
                    int append = 0;
                    bool error_input = true;
                    do
                    {
                        Console.WriteLine(">>> Create Books <<<");
                        Console.Write("> Enter Book Title: ");
                        title = Console.ReadLine();
                        Console.Write("> Enter Book Author: ");
                        author = Console.ReadLine();
                        Console.Write("> Enter Book ISBN: ");
                        ISBN = Console.ReadLine();
                        Console.Write("> Enter Book publication: ");
                        publication = Console.ReadLine();
                        Console.Write("> Enter Book Lost Fine: ");
                        error_input = !float.TryParse(Console.ReadLine(), out fine_lost); //negation applied, if parsing unsuccessful, try again or exit
                        if (error_input) { if (try_again_error()) { continue; } else { break; } }
                        Console.Write("> Enter Book Overdue Fine Per Day: ");
                        error_input = !float.TryParse(Console.ReadLine(), out fine_overdue_per_day);
                        if (error_input) { if (try_again_error()) { continue; } else { break; } }
                        Console.WriteLine("> Do you want to push/append this book to the database?");
                        Console.WriteLine("> [1] Push/append");
                        Console.WriteLine("> [0] Cancel/exit");
                        Console.Write("> Selection: ");
                        error_input = !int.TryParse(Console.ReadLine(), out append);
                        if (error_input) { if (try_again_error()) { continue; } else { break; } }
                        error_input = !(append == 0 || append == 1); //if append is neither 1 or 0

                        if (error_input)
                        {
                            if (try_again_error())
                            { continue; }
                            else
                            { break; }
                        }
                        else
                        {
                            new_book = new Book(title, author, ISBN, publication, fine_lost, fine_overdue_per_day, append == 1);
                        }

                        //function to try again
                        bool try_again_error()
                        {
                            int try_again = 1;
                            Console.WriteLine("> WARNING: You have entered one/more wrong input. Would you like to try again or exit?");
                            Console.WriteLine("> [1] Try Again");
                            Console.WriteLine("> [0] Exit");
                            Console.Write("> Selection: ");
                            try_again = int.Parse(Console.ReadLine());
                            return try_again == 1; //if pressed 1, error input will be true and continue loop
                        }
                    } while (error_input);
                    Console.WriteLine("--------------------------------------------------");
                }

                void PrintBooks()
                {
                    Console.WriteLine(">>> Print Books <<<");
                    Console.WriteLine("> Books available in the library:\n");
                    Book.Print_books(Book.GetBooks);
                    Console.WriteLine("--------------------------------------------------");
                }
            }

            void AccountAccess()
            {
                bool continue_loop_AA = true;
                int choice_AA;
                do
                {
                    Console.WriteLine(">>> Account <<<");
                    Console.WriteLine("> What would you like to do?");
                    Console.WriteLine("> [1] Register new account in the database");
                    Console.WriteLine("> [2] Print all accounts from the database");
                    Console.WriteLine("> [3] Verify if an account exists in the database");
                    Console.WriteLine("> [4] Sign in to an account to inquire/borrow/reserve/return/calculate fine/declare lost books");
                    Console.WriteLine("> [5] Exit");
                    Console.Write("> Selection: ");
                    choice_AA = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    if (choice_AA == 1)
                    {
                        RegisterNewAccount();
                    }
                    else if (choice_AA == 2)
                    {
                        PrintAccounts();
                    }
                    else if (choice_AA == 3)
                    {
                        VerifyAccount();
                    }
                    else if (choice_AA == 4)
                    {
                        SignInAccount();
                    }
                    else if (choice_AA == 5)
                    {
                        continue_loop_AA = false;
                    }
                    else
                    {
                        continue_loop_AA = true;
                        Console.WriteLine("> WARNING: Wrong Input. Please try again.");
                    }
                } while (continue_loop_AA);

                //Functions
                void RegisterNewAccount()
                {
                    string name, id;
                    int append = 0, verbose = 0; //default to false
                    bool error_input = true;
                    do
                    {
                        Console.WriteLine(">>> Register New Account <<<");
                        Console.Write("> Enter Account Name: ");
                        name = Console.ReadLine();
                        Console.Write("> Enter Account ID: ");
                        id = Console.ReadLine();
                        Console.WriteLine("> Do you want to push/append this account to the database?");
                        Console.WriteLine("> [1] Push/append");
                        Console.WriteLine("> [0] Cancel/exit");
                        Console.Write("> Selection: ");
                        error_input = !int.TryParse(Console.ReadLine(), out append);
                        if (error_input) { if (try_again_error()) { continue; } else { break; } }
                        error_input = !(append == 0 || append == 1); //if append is neither 1 or 0

                        if (error_input)
                        {
                            if (try_again_error())
                            { continue; }
                            else
                            { break; }
                        }
                        else
                        {
                            Console.WriteLine("> Do you want to turn on verbosity to check if their are errors on registering new account to database?");
                            Console.WriteLine("> [1] Verbose On");
                            Console.WriteLine("> [0] Verbose Off");
                            Console.Write("> Selection: ");
                            error_input = !int.TryParse(Console.ReadLine(), out verbose);
                            if (error_input) { if (try_again_error()) { continue; } else { break; } }
                            error_input = !(verbose == 0 || verbose == 1); //if verbose is neither 1 or 0

                            if (error_input)
                            {
                                if (try_again_error())
                                { continue; }
                                else
                                { break; }
                            }
                            else
                            {
                                new_account = new Account(name, id, append == 1, verbose == 1);
                            }
                        }

                        //function to try again
                        bool try_again_error()
                        {
                            int try_again = 1;
                            Console.WriteLine("> WARNING: You have entered one/more wrong input. Would you like to try again or exit?");
                            Console.WriteLine("> [1] Try Again");
                            Console.WriteLine("> [0] Exit");
                            Console.Write("> Selection: ");
                            try_again = int.Parse(Console.ReadLine());
                            return try_again == 1; //if pressed 1, error input will be true and continue loop
                        }
                    } while (error_input);
                    Console.WriteLine("--------------------------------------------------");
                }

                void PrintAccounts()
                {
                    Console.WriteLine(">>> Print Accounts <<<");
                    Console.WriteLine("> Accounts registered in the database:\n");
                    int index = 1; //counter
                    foreach(var account in Account.GetAccounts)
                    {
                        Console.WriteLine($"Account [{index}] Information: ");
                        Console.WriteLine($"Name: {account.GetName}");
                        Console.WriteLine($"ID: {account.GetID}");
                        Console.WriteLine();
                        index++;
                    }
                    Console.WriteLine("--------------------------------------------------");
                }

                void VerifyAccount()
                {
                    int verification_method;
                    Console.WriteLine(">>> Verfiy Account <<<");
                    Console.WriteLine("> How would you like to verify an account?");
                    Console.WriteLine("> [1] Name only");
                    Console.WriteLine("> [2] ID only ");
                    Console.WriteLine("> [3] Both Name and ID");
                    Console.WriteLine("> [4] Exit");
                    Console.Write("> Selection: ");
                    verification_method = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    if (verification_method == 1 || verification_method == 2)
                    {
                        Console.Write($"> Please enter {((verification_method == 1) ? "name" : "ID")} of the account to search: ");
                        string name_or_id = Console.ReadLine();
                        bool verification = Account.VerifyAccount(name_or_id);
                        Console.WriteLine($"> Account with {((verification_method == 1) ? "name" : "ID")} ({name_or_id}) {((verification)?"exist":"doesn't exist")} in the database.");
                    }
                    else if (verification_method == 3)
                    {
                        Console.Write("> Please enter account name: ");
                        string name = Console.ReadLine();
                        Console.Write("> Please enter account ID: ");
                        string ID = Console.ReadLine();
                        bool verification = Account.VerifyAccount(name, ID);
                        Console.WriteLine($"Account with name ({name}) and ID ({ID}) {((verification) ? "exist":"doesn't exist")} in the database.");
                    }
                    if(verification_method != 4)
                    {
                        Console.WriteLine("--------------------------------------------------");
                    }
                }

                void SignInAccount()
                {
                    Account my_account;
                    string name, id, sign_in_msg;
                    bool verification = false, error_sign_in = true;
                    do
                    {
                        Console.WriteLine(">>> Sign In Account <<<");
                        Console.Write("> Please Enter Account Name: ");
                        name = Console.ReadLine();
                        Console.Write("> Please Enter Account ID: ");
                        id = Console.ReadLine();
                        verification = Account.VerifyAccount(name, id); //Verify first if account exist before getting it
                        sign_in_msg = (verification) ? "Signed In Successfully !!!" : "Signing In Unsuccessful";
                        if (verification)
                        {
                            Console.WriteLine($"> {sign_in_msg}");
                            my_account = Account.GetAccount(name, id); //to get and access the methods of my_account object
                            error_sign_in = false; //to exit while loop
                            Console.WriteLine("--------------------------------------------------");

                            //Account Object Method Calling: STARTS HERE!!!//
                            bool keep_sign_in = true;
                            do
                            {
                                int account_choice;
                                Console.WriteLine(">>> Signed In Account <<<");
                                Console.WriteLine($"> ACCOUNT NAME: {my_account.GetName}");
                                Console.WriteLine($"> ACCOUNT ID: {my_account.GetID}");
                                Console.WriteLine($"> PRESENT DATE (ALL ACCOUNTS): {Account.GetDateTimePresent:MM/dd/yyyy}");
                                Console.WriteLine("> What would you like to do?");
                                Console.WriteLine("> [1] Borrow a book");
                                Console.WriteLine("> [2] Print all borrowed books");
                                Console.WriteLine("> [3] Reserve Books");
                                Console.WriteLine("> [4] Print all reserved books");
                                Console.WriteLine("> [5] Return a book");
                                Console.WriteLine("> [6] Print all returned books");
                                Console.WriteLine("> [7] Declare a lost book");
                                Console.WriteLine("> [8] Print all lost books");
                                Console.WriteLine("> [9] Calculate account total fine");
                                Console.WriteLine("> [10] Update/change present date (All Accounts)");
                                Console.WriteLine("> [11] Sign Out/Exit");
                                Console.Write("> Selection: ");
                                account_choice = int.Parse(Console.ReadLine());
                                Console.WriteLine("--------------------------------------------------");

                                if (account_choice == 1 || account_choice == 3)
                                {
                                    my_account.Inquire_or_Request_Book();
                                }
                                else if (account_choice == 2)
                                {
                                    Console.WriteLine("> Account's Borrowed Books: ");
                                    Book.Print_books(my_account.GetBorrowedBooks);
                                    Console.WriteLine($"> Account's No. of Borrowed Books: {my_account.NoOfBorrowedBooks}");
                                    Console.WriteLine("--------------------------------------------------");
                                }
                                else if (account_choice == 4)
                                {
                                    Console.WriteLine("> Account's Reserved Books: ");
                                    Book.Print_books(my_account.GetReservedBooks);
                                    Console.WriteLine($"> Account's No. of Reserved Books: {my_account.NoOfReservedBooks}");
                                    Console.WriteLine("--------------------------------------------------");
                                }
                                else if (account_choice == 5)
                                {
                                    //need to check if account has borrowed books to return
                                    // and get index of book to return.
                                    Book book_to_return;

                                    if(my_account.NoOfBorrowedBooks > 0)
                                    {
                                        int book_index = 0;
                                        Console.WriteLine("> Borrowed Books that this account can return: ");
                                        Book.Print_books(my_account.GetBorrowedBooks);
                                        Console.Write("\n> Please input the number/index of the book that this account would like to return: ");
                                        book_index = int.Parse(Console.ReadLine());
                                        book_to_return = my_account.GetBorrowedBooks[book_index-1];
                                        Console.WriteLine("> Book To Return");
                                        Book.Print_book(book_to_return);
                                        my_account.ReturnBook(book_to_return);
                                        Console.WriteLine("--------------------------------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("> No available books to return because this account doesn't have any borrowed books to return.");
                                        Console.WriteLine("--------------------------------------------------");
                                    }

                                }
                                else if (account_choice == 6)
                                {
                                    Console.WriteLine("> Account's Returned Books: ");
                                    Book.Print_books(my_account.GetReturnedBooks);
                                    Console.WriteLine($"> Account's No. of Returned Books: {my_account.NoOfReturnedBooks}");
                                    Console.WriteLine("--------------------------------------------------");
                                }
                                else if (account_choice == 7)
                                {
                                    //need to check if account has borrowed books to declare lost
                                    // and get index of book to declare lost.
                                    Book book_to_declare_lost;

                                    if (my_account.NoOfBorrowedBooks > 0)
                                    {
                                        int book_index = 0;
                                        Console.WriteLine("> Borrowed Books that this account can declare lost: ");
                                        Book.Print_books(my_account.GetBorrowedBooks);
                                        Console.Write("\n> Please input the number/index of he book that this account would like to declare lost: ");
                                        book_index = int.Parse(Console.ReadLine());
                                        book_to_declare_lost = my_account.GetBorrowedBooks[book_index - 1];
                                        Console.WriteLine("> Book To Declare Lost");
                                        Book.Print_book(book_to_declare_lost);
                                        my_account.DeclareLostBook(book_to_declare_lost);
                                        Console.WriteLine("--------------------------------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("> No available books to declare lost because this account doesn't have any borrowed books to declare lost.");
                                        Console.WriteLine("--------------------------------------------------");
                                    }
                                }
                                else if (account_choice == 8)
                                {
                                    Console.WriteLine("> Account's Lost Books: ");
                                    Book.Print_books(my_account.GetLostBooks);
                                    Console.WriteLine($"> Account's No. of Lost Books: {my_account.NoOfLostBooks}");
                                    Console.WriteLine("--------------------------------------------------");
                                }
                                else if (account_choice == 9)
                                {
                                    int calculation_choice;
                                    Console.WriteLine("> Account's Total Fine : ");
                                    Console.WriteLine("> To calculate the total fine of this account a DateTime object must be passed/inputted.");
                                    Console.WriteLine("> How would you like to calculate the total fine");
                                    Console.WriteLine("> [1] All accounts' the same present DateTime value");
                                    Console.WriteLine("> [2] User DateTime input (Theoretical Date)");
                                    Console.Write("> Selection: ");
                                    calculation_choice = int.Parse(Console.ReadLine());
                                    Console.WriteLine("--------------------------------------------------");

                                    if (calculation_choice == 1)
                                    {
                                        Console.WriteLine($"> {my_account.GetName}'s Total Fine: {my_account.FineAmount}");
                                        Console.WriteLine("--------------------------------------------------");
                                    }
                                    else
                                    {
                                        Console.Write("> Please enter the date that you would like to pay all of this account's total fine in this date format (mm/dd/yyyy): ");
                                        DateTime due_date = DateTime.ParseExact(Console.ReadLine(),"M/d/yyyy", CultureInfo.InvariantCulture);
                                        my_account.CalculateFine(due_date, true); //true to print console
                                        Console.WriteLine("--------------------------------------------------");
                                    }
                                }
                                else if (account_choice == 10)
                                {
                                    Console.WriteLine("> Update Present Date: ");
                                    Console.Write("> Please enter the updated present date in this date format (mm/dd/yyyy): ");
                                    DateTime present_date = DateTime.ParseExact(Console.ReadLine(), "M/d/yyyy", CultureInfo.InvariantCulture);
                                    Console.WriteLine("--------------------------------------------------");
                                    Account.Update_Recent_DateTime(present_date);
                                }
                                else if (account_choice == 11)
                                {
                                    keep_sign_in = false;
                                }
                                else
                                {
                                    keep_sign_in = true;
                                    Console.WriteLine("> WARNING: Wrong Input. Please try again.");
                                    Console.WriteLine("--------------------------------------------------");
                                }
                            } while (keep_sign_in);

                            //Account Object Method Calling: ENDS HERE!!!//
                        }
                        else
                        {
                            int continue_choice;
                            Console.WriteLine("> The Account doesn't exist in the database");
                            Console.WriteLine($"> {sign_in_msg}");
                            Console.WriteLine("> Would you like to try again?");
                            Console.WriteLine("> [1] Yes");
                            Console.WriteLine("> [0] No/Exit");
                            Console.Write("> Selection: ");
                            continue_choice = int.Parse(Console.ReadLine());
                            error_sign_in = continue_choice == 1; //continue loop if Yes was selected else exit loop
                            Console.WriteLine("--------------------------------------------------");
                        }
                    } while (error_sign_in);
                }
            }

            void LibrarianAccess()
            {
                bool continue_loop_LA = true;
                int choice_LA;
                do
                {
                    Console.WriteLine(">>> Librarian <<<");
                    Console.WriteLine("> What would you like to do?");
                    Console.WriteLine("> [1] Register new librarian in the database");
                    Console.WriteLine("> [2] Print all librarians from the database");
                    Console.WriteLine("> [3] Verify if a librarian exists in the database");
                    Console.WriteLine("> [4] Sign in a librarian account to search a book and show its due date and/or reservation status");
                    Console.WriteLine("> [5] Exit");
                    Console.Write("> Selection: ");
                    choice_LA = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    if (choice_LA == 1)
                    {
                        RegisterNewLibrarian();
                    }
                    else if (choice_LA == 2)
                    {
                        PrintLibrarians();
                    }
                    else if (choice_LA == 3)
                    {
                        VerifyLibrarian();
                    }
                    else if (choice_LA == 4)
                    {
                        SignInLibrarian();
                    }
                    else if (choice_LA == 5)
                    {
                        continue_loop_LA = false;
                    }
                    else
                    {
                        continue_loop_LA = true;
                        Console.WriteLine("> WARNING: Wrong Input. Please try again.");
                    }
                } while (continue_loop_LA);

                void RegisterNewLibrarian()
                {
                    string name, id, password;
                    int append = 0, verbose = 0; //default to false
                    bool error_input = true;
                    do
                    {
                        Console.WriteLine(">>> Register New Librarian <<<");
                        Console.Write("> Enter Librarian Name: ");
                        name = Console.ReadLine();
                        Console.Write("> Enter Librarian ID: ");
                        id = Console.ReadLine();
                        Console.Write("> Enter Librarian Password: ");
                        password = Console.ReadLine();
                        Console.WriteLine("> Do you want to push/append this librarian account to the database?");
                        Console.WriteLine("> [1] Push/append");
                        Console.WriteLine("> [0] Cancel/exit");
                        Console.Write("> Selection: ");
                        error_input = !int.TryParse(Console.ReadLine(), out append);
                        if (error_input) { if (try_again_error()) { continue; } else { break; } }
                        error_input = !(append == 0 || append == 1); //if append is neither 1 or 0

                        if (error_input)
                        {
                            if (try_again_error())
                            { continue; }
                            else
                            { break; }
                        }
                        else
                        {
                            Console.WriteLine("> Do you want to turn on verbosity to check if their are errors on registering new account to database?");
                            Console.WriteLine("> [1] Verbose On");
                            Console.WriteLine("> [0] Verbose Off");
                            Console.Write("> Selection: ");
                            error_input = !int.TryParse(Console.ReadLine(), out verbose);
                            if (error_input) { if (try_again_error()) { continue; } else { break; } }
                            error_input = !(verbose == 0 || verbose == 1); //if verbose is neither 1 or 0

                            if (error_input)
                            {
                                if (try_again_error())
                                { continue; }
                                else
                                { break; }
                            }
                            else
                            {
                                new_librarian = new Librarian(name, id, password, append == 1, verbose == 1);
                            }
                        }

                        //function to try again
                        bool try_again_error()
                        {
                            int try_again = 1;
                            Console.WriteLine("> WARNING: You have entered one/more wrong input. Would you like to try again or exit?");
                            Console.WriteLine("> [1] Try Again");
                            Console.WriteLine("> [0] Exit");
                            Console.Write("> Selection: ");
                            try_again = int.Parse(Console.ReadLine());
                            return try_again == 1; //if pressed 1, error input will be true and continue loop
                        }
                    } while (error_input);
                    Console.WriteLine("--------------------------------------------------");
                }

                void PrintLibrarians()
                {
                    Console.WriteLine(">>> Print Librarians <<<");
                    Console.WriteLine("> Librarians registered in the database:\n");
                    int index = 1; //counter
                    foreach (var librarian in Librarian.GetLibrarians)
                    {
                        Console.WriteLine($"Librarian [{index}] Information: ");
                        Console.WriteLine($"Name: {librarian.GetName}");
                        Console.WriteLine($"ID: {librarian.GetID}");
                        Console.WriteLine();
                        index++;
                    }
                    Console.WriteLine("--------------------------------------------------");
                }

                void VerifyLibrarian()
                {
                    int verification_method;
                    Console.WriteLine(">>> Verfiy Librarian <<<");
                    Console.WriteLine("> How would you like to verify a librarian account?");
                    Console.WriteLine("> [1] Name only");
                    Console.WriteLine("> [2] ID only ");
                    Console.WriteLine("> [3] Both Name and ID");
                    Console.WriteLine("> [4] Exit");
                    Console.Write("> Selection: ");
                    verification_method = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------------------------------");
                    if (verification_method == 1 || verification_method == 2)
                    {
                        Console.Write($"> Please enter {((verification_method == 1) ? "name" : "ID")} of the librarian account to search: ");
                        string name_or_id = Console.ReadLine();
                        bool verification = Librarian.VerifyLibrarian(name_or_id);
                        Console.WriteLine($"> Librarian account with {((verification_method == 1) ? "name" : "ID")} ({name_or_id}) {((verification) ? "exist" : "doesn't exist")} in the database.");
                    }
                    else if (verification_method == 3)
                    {
                        Console.Write("> Please enter librarian name: ");
                        string name = Console.ReadLine();
                        Console.Write("> Please enter librarian ID: ");
                        string ID = Console.ReadLine();
                        bool verification = Librarian.VerifyLibrarian(name, ID);
                        Console.WriteLine($"Librarian account with name ({name}) and ID ({ID}) {((verification) ? "exist" : "doesn't exist")} in the database.");
                    }
                    if (verification_method != 4)
                    {
                        Console.WriteLine("--------------------------------------------------");
                    }
                }

                void SignInLibrarian()
                {
                    Librarian my_librarian;
                    string id, password, sign_in_msg;
                    bool verification = false, error_sign_in = true;
                    do
                    {
                        Console.WriteLine(">>> Sign In Librarian Account <<<");
                        Console.Write("> Please Enter Librarian ID: ");
                        id = Console.ReadLine();
                        Console.Write("> Please Enter Librarian Password: ");
                        password = Console.ReadLine();
                        verification = Librarian.VerifyLibrarian_ID_Pass(id, password); //Verify first if account exist before getting it
                        sign_in_msg = (verification) ? "Signed In Successfully !!!" : "Signing In Unsuccessful";
                        if (verification)
                        {
                            Console.WriteLine($"> {sign_in_msg}");
                            my_librarian = Librarian.GetLibrarian(id, password); //to get and access the methods of my_account object
                            error_sign_in = false; //to exit while loop
                            Console.WriteLine("--------------------------------------------------");

                            //Account Object Method Calling: STARTS HERE!!!//
                            bool keep_sign_in = true;
                            do
                            {
                                int librarian_choice;
                                Console.WriteLine(">>> Signed In Account <<<");
                                Console.WriteLine($"> LIBRARIAN NAME: {my_librarian.GetName}");
                                Console.WriteLine($"> LIBRARIAN ID: {my_librarian.GetID}");
                                Console.WriteLine("> What would you like to do?");
                                Console.WriteLine("> [1] Search a book");
                                Console.WriteLine("> [2] Sign Out/Exit");
                                Console.Write("> Selection: ");
                                librarian_choice = int.Parse(Console.ReadLine());
                                Console.WriteLine("--------------------------------------------------");

                                if (librarian_choice == 1)
                                {
                                    Console.Write("> Enter Book Title: ");
                                    string title = Console.ReadLine();
                                    Console.Write("> Enter Book Author: ");
                                    string author = Console.ReadLine();
                                    Book book_queried = my_librarian.Search(title, author, true);
                                    if(book_queried != null)
                                    {
                                        Console.WriteLine("> What would you like to know about this book?");
                                        Console.WriteLine("> [1] Print due date and account who borrowed it");
                                        Console.WriteLine("> [2] Print reservation status and account who reserved it");
                                        Console.WriteLine("> [3] Print both");
                                        Console.WriteLine("> [4] Exit");
                                        Console.Write("> Selection: ");
                                        int info_choice = int.Parse(Console.ReadLine());
                                        Console.WriteLine("--------------------------------------------------");
                                        if(info_choice == 1 || info_choice == 3)
                                        {
                                            DateTime? due_date = book_queried.ShowDueDate(true);
                                            if (due_date != null)
                                            {
                                                string name = Account.GetAccounts.FirstOrDefault(x => x.GetBorrowedBooks.Contains(book_queried)).GetName;
                                                Console.WriteLine($"Borrowed By: {name}");
                                            }
                                        }
                                        if(info_choice == 2 || info_choice == 3)
                                        {
                                            if (book_queried.Reservation_status(true)) //will return true if book queried is reserved
                                            {
                                                string name = Account.GetAccounts.FirstOrDefault(x => x.GetReservedBooks.Contains(book_queried)).GetName;
                                                Console.WriteLine($"Reserved By: {name}");
                                            }
                                        }
                                        if (info_choice == 1 || info_choice == 2 || info_choice == 3)
                                        {
                                            Console.WriteLine("--------------------------------------------------");
                                        }
                                    }

                                }
                                else if (librarian_choice == 2)
                                {
                                    keep_sign_in = false;
                                }
                                else
                                {
                                    keep_sign_in = true;
                                    Console.WriteLine("> WARNING: Wrong Input. Please try again.");
                                    Console.WriteLine("--------------------------------------------------");
                                }
                            } while (keep_sign_in);
                            //Account Object Method Calling: ENDS HERE!!!//
                        }
                        else
                        {
                            int continue_choice;
                            Console.WriteLine("> The Librarian account doesn't exist in the database");
                            Console.WriteLine($"> {sign_in_msg}");
                            Console.WriteLine("> Would you like to try again?");
                            Console.WriteLine("> [1] Yes");
                            Console.WriteLine("> [0] No/Exit");
                            Console.Write("> Selection: ");
                            continue_choice = int.Parse(Console.ReadLine());
                            error_sign_in = continue_choice == 1; //continue loop if Yes was selected else exit loop
                            Console.WriteLine("--------------------------------------------------");
                        }
                    } while (error_sign_in);
                }
            }
        }
    }
}
