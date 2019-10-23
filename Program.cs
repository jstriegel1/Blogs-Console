using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            bool done = false;
            var db = new BloggingContext();
            logger.Info("Program started");
            try
            {
               

                do
                {
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1) Display All Blogs");
                    Console.WriteLine("2) Add a Blog");
                    Console.WriteLine("3) Create a Post");
                    Console.WriteLine("4) Exit");
                    Console.Write("==> ");
                    
                    string choice = Console.ReadLine();
                    Console.WriteLine("");

                    if (choice == "1")
                    {
                        // Display all Blogs from the database
                        var query = db.Blogs.OrderBy(b => b.BlogId);

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.BlogId + ") " + item.Name);
                        }
                        Console.WriteLine("");
                        done = false;
                    }
                    
                    else if (choice == "2")
                    {
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };


                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                    }
                }
                while (!done);


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}

