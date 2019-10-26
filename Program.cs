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
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("All blogs in the database:");
                        int i = 1;
                        foreach (var item in query)
                        {
                            Console.WriteLine( i + ") " + item.Name);
                            i++;
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
                        done = false;
                    }

                    else if (choice == "3")
                    {
                        // Display all Blogs from the database for user to choose from
                        Console.WriteLine("Which blog would you like to create a post for?");
                        var query = db.Blogs.OrderBy(b => b.BlogId);
                        foreach (var item in query)
                        {
                            Console.WriteLine( item.BlogId + ") " + item.Name);
                        }
                        Console.Write("==> ");
                        int blogChoice = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("");

                        Console.Write("Enter title of post: ");
                        var title = Console.ReadLine();
                        Console.Write("Enter post content: ");
                        var content = Console.ReadLine();
                        var post = new Post { Title = title, Content = content, BlogId = blogChoice };

                        db.AddPost(post);
                        logger.Info("Post added - {title}", title);
                        done = false;

                    }
                    else if(choice == "4")
                    {
                        done = true;
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

