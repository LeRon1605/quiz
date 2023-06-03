using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            await ApplyMigrations(webHost.Services);

            await webHost.RunAsync();
        }

        private static async Task ApplyMigrations(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            await using RepositoryDbContext dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();

            await dbContext.Database.MigrateAsync();
            await SeedDbAsync(dbContext);
        }

        private static async Task SeedDbAsync(RepositoryDbContext dbContext)
        {
            var answers = new List<List<Answer>>()
            {
                new List<Answer>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "1997"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "2001"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "2009"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "2015"
                    }
                },
                new List<Answer>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Elsa"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Snow white"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Coco"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Toy story"
                    }
                },
                new List<Answer>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Starbuck"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "HighLand"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Central Park"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Central Perk"
                    }
                },
                new List<Answer>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "3"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "4"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "11"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "9"
                    }
                }
            };

            var questions = new List<Question>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "When was Netflix founded?",
                    Answers = answers[0],
                    TrueAnswerId = answers[0][0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "Name Disney’s first film?",
                    Answers = answers[1],
                    TrueAnswerId = answers[1][1].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "What is the name of the coffee shop in the sitcom Friends?",
                    Answers = answers[2],
                    TrueAnswerId = answers[2][3].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "How many time zones are there in Russia",
                    Answers = answers[3],
                    TrueAnswerId = answers[3][2].Id
                }
            };

            var users = new List<User>()
            {
                new()
                {
                    Id = QuizConstant.DEFAULT_USER_ID,
                    Name = "Le Quoc Ron"
                }
            };

            if (!(await dbContext.Questions.AnyAsync()))
            {
                await dbContext.Questions.AddRangeAsync(questions);
                await dbContext.Users.AddRangeAsync(users);

                await dbContext.SaveChangesAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
