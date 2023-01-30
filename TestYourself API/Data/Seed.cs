using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestYourself_API.Models;

namespace TestYourself_API.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();


                context.Tests.AddRange(new List<Test>()
                    {
                        new Test()
                        {
                            Title = "MathTest",
                            questionsAmount = 2,
                            Image = "https://news.harvard.edu/wp-content/uploads/2022/11/iStock-mathproblems-1200x800.jpg",
                            Description = "Really funny test",
                            TestCategory = Enum.TestCategory.Math
                         },
                        new Test()
                        {
                            Title = "LiteratureTest",
                            questionsAmount = 2,
                            Image = "https://static.toiimg.com/imagenext/toiblogs/photo/readersblog/wp-content/uploads/2021/02/litreture-TOI.jpg",
                            Description = "Really funny test",
                            TestCategory = Enum.TestCategory.Literature
                        },
                        new Test()
                        {
                            Title = "EnglishTest",
                            questionsAmount = 2,
                            Image = "https://www.ed2go.com/common/images/1/16510.jpg",
                            Description = "Really funny test",
                            TestCategory = Enum.TestCategory.English
                        }
                    });
                context.SaveChanges();

                //Questions

                var math = context.Tests.First(t => t.TestCategory == Enum.TestCategory.Math).Id;
                var literature = context.Tests.First(t => t.TestCategory == Enum.TestCategory.Literature).Id;
                var english = context.Tests.First(t => t.TestCategory == Enum.TestCategory.English).Id;
                var questions = new List<Question>()
                    {
                        new Question()
                        {
                          Contain = "2+2",
                          FirstAnswer = "3",
                          SecondAnswer = "4",
                          CorrectAnswer = "SecondAnswer",
                          testId = math

                        },
                        new Question()
                        {
                           Contain = "3+3",
                          FirstAnswer = "3",
                          SecondAnswer = "6",
                          CorrectAnswer = "SecondAnswer",
                          testId = math
                        },
                      new Question()
                        {
                          Contain = "5+5",
                          FirstAnswer = "10",
                          SecondAnswer = "4",
                          CorrectAnswer = "FirstAnswer",
                          testId = math

                        },
                      new Question()
                        {
                          Contain = "10+10",
                          FirstAnswer = "20",
                          SecondAnswer = "30",
                          CorrectAnswer = "FirstAnswer",
                          testId = math
                        },
                       new Question()
                        {
                          Contain = "2+2+2",
                          FirstAnswer = "6",
                          SecondAnswer = "4",
                          CorrectAnswer = "FirstAnswer",
                          testId = math
                        },
                        new Question()
                        {
                          Contain = "Heard melodies are sweet, but those unheard\r\nAre sweeter; therefore, ye soft pipes, play on;\r\nNot to the sensual " +
                          "ear, but, more endear'd,\r\nPipe to the spirit ditties of no " +
                          "tone:\r\nFair youth, beneath the trees, thou canst not leave\r\nThy song, " +
                          "nor ever can those trees be bare;\r\nBold Lover, never, never canst thou kiss,\r\nThough winning near the go" +
                          "al - yet, do not grieve;\r\nShe cannot fade, though thou hast not thy bliss,\r\nFor ever wilt thou love, and she be fair!\r\n\r\nThis poem" +
                          " was written in which of the following eras?",
                          FirstAnswer = "Romantic",
                          SecondAnswer = "Postmodern",
                          CorrectAnswer = "FirstAnswer",
                          testId = literature
                        },
                         new Question()
                        {
                          Contain = "The theme of this stanza can best be described as",
                          FirstAnswer = "Music energizes the heart.",
                          SecondAnswer = "Life is enhanced by the imagination.",
                          CorrectAnswer = "SecondAnswer",
                          testId = literature
                        },
                          new Question()
                        {
                          Contain = "What historical period does this passage arise out of?",
                          FirstAnswer = "The Great Depression",
                          SecondAnswer = "The Civil War",
                          CorrectAnswer = "FirstAnswer",
                          testId = literature
                        },
                           new Question()
                        {
                          Contain = "The passage is based on the ideas of which of the following",
                          FirstAnswer = "Emerson",
                          SecondAnswer = "Marx",
                          CorrectAnswer = "SecondAnswer",
                          testId = literature
                        },
                            new Question()
                        {
                          Contain = "The versification of the poem would best be classified as",
                          FirstAnswer = "free verse",
                          SecondAnswer = "blank verse",
                          CorrectAnswer = "FirstAnswer",
                          testId = literature
                        },
                              new Question()
                        {
                          Contain = "Would you mind ______ the window?",
                          FirstAnswer = "to close",
                          SecondAnswer = "closing",
                          CorrectAnswer = "SecondAnswer",
                          testId = english
                        },
                                new Question()
                        {
                          Contain = "The best way to learn a language is ______ a little every day.",
                          FirstAnswer = "by speaking",
                          SecondAnswer = "to speaking",
                          CorrectAnswer = "FirstAnswer",
                          testId = english
                        },
                                  new Question()
                        {
                          Contain = "I'm fed up ______ this excersice",
                          FirstAnswer = "with doing",
                          SecondAnswer = "for doing",
                          CorrectAnswer = "FirstAnswer",
                          testId = english
                        },
                                    new Question()
                        {
                          Contain = "I come ______ England",
                          FirstAnswer = "to",
                          SecondAnswer = "from",
                          CorrectAnswer = "SecondAnswer",
                          testId = english
                        },
                                      new Question()
                        {
                          Contain = "You ______ the cleaning. I would have done it tonight",
                          FirstAnswer = "couldn't have done",
                          SecondAnswer = "needn't have done",
                          CorrectAnswer = "SecondAnswer",
                          testId = english
                        },
                                        new Question()
                        {
                          Contain = "Have you visited London?\" \"______.\"",
                          FirstAnswer = "Not yet",
                          SecondAnswer = "Already",
                          CorrectAnswer = "FirstAnswer",
                          testId = english
                        }

                    };

                context.Questions.AddRange(questions);
                context.SaveChanges();
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
