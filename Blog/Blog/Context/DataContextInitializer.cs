using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CodeFirstMembershipSharp
{
    public class DataContextInitializer: DropCreateDatabaseIfModelChanges<DataContext>
    {
        // Put some informations in DB
        protected override void Seed(DataContext context)
        {
            //MembershipCreateStatus Status;
            //Membership.CreateUser("Admin", "123456", "admin@sup.com", null, null, true, out Status);
            Roles.CreateRole("Admin");
            Roles.AddUserToRole("Admin", "Admin");

            var myUser = new User
            {
                UserId = Guid.NewGuid(),
                LastName = "Admin",
                FirstName = "Admin",
                Email = "admin@blog.com",
                Password = "123456",
                Username = "admin",
                IsApproved = true
            };            

            context.Users.Add(myUser);

            // SAVE
            context.SaveChanges();

            // Add User to Role Admin
            Roles.AddUserToRole(myUser.Username, "Admin");

            var myPersonal = new Personal
            {
                Firstname = "Jon",
                Lastname = "Contoso",
                Title = "Project Leader & Software Developer",
                Description = "Hi! My name is Jon Contoso. I'm a Project Leader and also a good software developer."
            };

            context.Personals.Add(myPersonal);

            // SAVE
            context.SaveChanges();


            var listPosts = new List<Post>
                                {
                                    new Post
                                        {
                                            Title = "HTML5",
                                            Text = "HTML5 is a markup language for structuring and presenting content for the World Wide Web and a core technology of the Internet. "
                                            + "It is the fifth revision of the HTML standard (created in 1990 and standardized as HTML4 as of 1997)[2] and, as of December 2012, is a W3C Candidate Recommendation.[3]"
                                            + "Its core aims have been to improve the language with support for the latest multimedia while keeping it easily readable by humans and consistently understood by computers and devices (web browsers, parsers, etc.)."
                                            + "HTML5 is intended to subsume not only HTML 4, but XHTML 1 and DOM Level 2 HTML as well.[2]Following its immediate predecessors HTML 4.01 and XHTML 1.1, "
                                            + "HTML5 is a response to the observation that the HTML and XHTML in common use on the World Wide Web are a mixture of features introduced by various specifications, "
                                            + "along with those introduced by software products such as web browsers, those established by common practice, and the many syntax errors in existing web documents.[4]"
                                            + "It is also an attempt to define a single markup language that can be written in either HTML or XHTML syntax. It includes detailed processing models to encourage more interoperable implementations; "
                                            + "it extends, improves and rationalises the markup available for documents, and introduces markup and application programming interfaces (APIs) for complex web applications.[5] For the same reasons, "
                                            + "HTML5 is also a potential candidate for cross-platform mobile applications. Many features of HTML5 have been built with the consideration of being able to run on low-powered devices such as smartphones and tablets. "
                                            + "In December 2011 research firm Strategy Analytics forecast sales of HTML5 compatible phones will top 1 billion in 2013.[6]",
                                            Posted = DateTime.Now
                                        },
                                    new Post
                                        {
                                            Title = "JavaScript",
                                            Text = "JavaScript (JS) is an open source client-side scripting language commonly implemented as part of a web browser in order to create enhanced user interfaces and dynamic websites."
                                            + "JavaScript is prototype-based scripting language that is dynamic, weakly typed and has first-class functions. It uses syntax influenced by the language C. "
                                            + "JavaScript copies many names and naming conventions from Java, but the two languages are otherwise unrelated and have very different semantics. "
                                            + "The key design principles within JavaScript are taken from the Self and Scheme programming languages.[5] It is a multi-paradigm language, "
                                            + "supporting object-oriented,[6] imperative, and functional[1][7] programming styles."
                                            + "JavaScript's use in applications outside web pages — for example in PDF documents, site-specific browsers, and desktop widgets—is also significant. "
                                            + "Newer and faster JavaScript VMs and frameworks built upon them (notably Node.js) have also increased the popularity of JavaScript for server-side web applications."
                                            + "JavaScript was formalized in the ECMAScript language standard and is primarily used in the form of client-side JavaScript (as part of a web browser). "
                                            + "This enables programmatic access to computational objects within a host environment.",
                                            Posted = new DateTime(2012, 5, 15)
                                        },
                                    new Post
                                        {
                                            Title = "Node.js",
                                            Text = "Node.js is a packaged compilation of Google's V8 JavaScript engine, the libUV platform abstraction layer, and a core library, which is itself primarily written in JavaScript."
                                            + "Node.js was created by Ryan Dahl starting in 2009, and its growth is sponsored by Joyent, his former employer.[3][4]"
                                            + "Dahl's original goal was to create the ability to make web sites with push capabilities as seen in web applications like Gmail. "
                                            + "After trying solutions in several other programming languages he chose JavaScript because of the lack of an existing I/O API. "
                                            + "This allowed him to define a convention of non-blocking, event-driven I/O.[5]"
                                            + "Similar environments written in other programming languages include Twisted for Python, Perl Object Environment for Perl, libevent for C, Vert.x for Java and EventMachine for Ruby. "
                                            + "Unlike most JavaScript programs, it is not executed in a web browser, but is instead a server-side JavaScript application. Node.js implements some CommonJS specifications.[6] It provides a REPL environment for interactive testing.",
                                            Posted = new DateTime(2012, 5, 8)
                                        }
                                };

            foreach (var post in listPosts)
                context.Posts.Add(post);

            // SAVE
            context.SaveChanges();

            foreach (var post in listPosts)
            {
                var listComments = new List<Comment>
                                    {
                                        new Comment
                                            {
                                                Name = "John Smith",
                                                Text = "Great post!",
                                                Posted = new DateTime(2009, 1, 18),
                                                Post = post
                                            },
                                        new Comment
                                            {
                                                Name = "Gill Smith",
                                                Text = "Fabulous",
                                                Posted = DateTime.Now,
                                                Post = post
                                            },
                                        new Comment
                                            {
                                                Name = "James Smith",
                                                Text = "I don't agree with you...",
                                                Posted = new DateTime(2010, 1, 18),
                                                Post = post
                                            }
                                    };
                foreach (var comment in listComments)
                {
                    context.Comments.Add(comment);
                }
            }

            // SAVE
            context.SaveChanges();

            base.Seed(context);
        }
    }
}