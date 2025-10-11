using GIP5_ScrumBoard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GIP5_ScrumBoard.Interfaces;
using GIP5_ScrumBoard.Services;
using System.Threading.Tasks;

namespace GIP5_ScrumBoard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMilestoneService, MilestoneService>(); // DI
            builder.Services.AddScoped<ITicketService, TicketService>(); // DI

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ScrumBoardContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ScrumBoardContext>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var serviceProvider = app.Services.CreateScope().ServiceProvider;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Project Manager", "Member" };
            foreach (var roleName in roleNames)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var allUsers = userManager.Users.ToList();

            foreach (var user in allUsers)
            {
                if (user.Email.EndsWith("manager@ucll.com", StringComparison.OrdinalIgnoreCase))
                {
                    if (!await userManager.IsInRoleAsync(user, "Project Manager"))
                    {
                        await userManager.AddToRoleAsync(user, "Project Manager");
                    }
                }
                else if (user.Email.EndsWith("member@ucll.com", StringComparison.OrdinalIgnoreCase))
                {
                    if (!await userManager.IsInRoleAsync(user, "Member"))
                    {
                        await userManager.AddToRoleAsync(user, "Member");
                    }
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Milestones}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
