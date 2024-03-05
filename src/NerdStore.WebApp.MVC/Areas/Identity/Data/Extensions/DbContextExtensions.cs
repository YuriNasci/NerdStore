using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.WebApp.MVC.Areas.Identity.Data;
using NerdStore.WebApp.MVC.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace NerdStore.WebApp.MVC.Areas.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = configuration.GetConnectionString(ContextConstants.DB_CONNECTION_NAME) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                  x =>
                  {
                      x.MigrationsHistoryTable("__EFMigrationsHistory");
                      x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name);
                  });
            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;

		}

        public static void SeedIdentityData(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    ArgumentNullException.ThrowIfNull(context, nameof(context));

                    context.Database.Migrate();

                    if (!context.Users.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                var users = new List<IdentityUser>();

                                var userAdmin = new IdentityUser()
                                {
                                    Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                                    UserName = "admin@gmail.com",
                                    NormalizedUserName = "ADMIN@GMAIL.COM",
                                    Email = "admin@gmail.com",
                                    NormalizedEmail = "ADMIN@GMAIL.COM",
                                    LockoutEnabled = false,
                                    PhoneNumber = "21970851350",
                                    EmailConfirmed = true
                                };
                                var user = new IdentityUser()
                                {
                                    Id = "17c6c007-ad05-455f-903e-edbfff4b856b",
                                    UserName = "usuario@gmail.com",
                                    NormalizedUserName = "USUARIO@GMAIL.COM",
                                    Email = "usuario@gmail.com",
                                    NormalizedEmail = "USUARIO@GMAIL.COM",
                                    LockoutEnabled = false,
                                    PhoneNumber = "21970696089",
                                    EmailConfirmed = true
                                };

                                PasswordHasher<IdentityUser> hasher1 = new PasswordHasher<IdentityUser>();
                                userAdmin.PasswordHash = hasher1.HashPassword(userAdmin, "Admin@123");

                                PasswordHasher<IdentityUser> hasher2 = new PasswordHasher<IdentityUser>();
                                user.PasswordHash = hasher2.HashPassword(user, "Teste@123");

                                users.Add(userAdmin);
                                users.Add(user);

                                context.Users.AddRange(users);
                                context.SaveChanges();

                                if (!context.UserClaims.Any())
                                {

                                    var claims = new List<IdentityUserClaim<string>>()
                                    {

                                        new IdentityUserClaim<string>()
                                        {                                            
                                            UserId = userAdmin.Id,
                                            ClaimType = "Produto",
                                            ClaimValue = "Adicionar"
                                        },
                                        new IdentityUserClaim<string>()
                                        {                                           
                                            UserId = userAdmin.Id,
                                            ClaimType = "Produto",
                                            ClaimValue = "Editar"
                                        },
                                        new IdentityUserClaim<string>()
                                        {
                                            UserId = userAdmin.Id,
                                            ClaimType = "Produto",
                                            ClaimValue = "Excluir"
                                        },
                                        new IdentityUserClaim<string>()
                                        {                                            
                                            UserId = userAdmin.Id,
                                            ClaimType = "Produto",
                                            ClaimValue = "Leitura"
                                        },
                                         new IdentityUserClaim<string>()
                                        {                                            
                                            UserId = userAdmin.Id,
                                            ClaimType = "Produto",
                                            ClaimValue = "AcessoPermitido"
                                        },
                                        new IdentityUserClaim<string>()
                                        {                                            
                                            UserId = user.Id,
                                            ClaimType = "Produto",
                                            ClaimValue = "Leitura"
                                        }

                                    };

                                    context.UserClaims.AddRange(claims);
                                    context.SaveChanges();

                                    transaction.Commit();
                                }
                            }
                            catch
                            {
                                transaction.Rollback();
                                //throw;
                            }
                        }
                    }

                }
            }
        }

        public static void SeedIdentityDataFromScripts(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    ArgumentNullException.ThrowIfNull(context, nameof(context));
                    if (context.Users.Any()) return;
                    context.Database.Migrate();

                    var assembly = typeof(DbContextExtensions).Assembly;
                    var files = assembly.GetManifestResourceNames();

                    var executedSeedings = context.SeedingEntries.ToArray();
                    var filePrefix = $"{assembly.GetName().Name}.Seedings.";
                    foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                                              .Select(f => new
                                              {
                                                  PhysicalFile = f,
                                                  LogicalFile = f.Replace(filePrefix, String.Empty)
                                              })
                                              .OrderBy(f => f.LogicalFile))
                    {
                        if (executedSeedings.Any(e => e.Name == file.LogicalFile))
                            continue;

                        string command = string.Empty;
                        using (Stream stream = assembly.GetManifestResourceStream(file.PhysicalFile))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                command = reader.ReadToEnd();
                            }
                        }

                        if (String.IsNullOrWhiteSpace(command))
                            continue;

                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Database.ExecuteSqlRaw(command);
                                context.SeedingEntries.Add(new Entities.SeedingEntry() { Name = file.LogicalFile });
                                context.SaveChanges();
                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }

                    }
                }
            }
        }

    }
}
