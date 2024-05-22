using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.Models;
using Tazkara.Repository;

namespace Tazkara
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IContactUs,ContactUsRepository>();
            builder.Services.AddScoped<IMatch,MatchRepository>();
            builder.Services.AddScoped<IStadium,StadiumRepository>();
            builder.Services.AddScoped<ITeam,TeamRepository>();
            builder.Services.AddScoped<ILeague,LeagueRepository>();
            builder.Services.AddScoped<ITicket,TicketRepository>();
            builder.Services.AddScoped<IReservationCart, ReservationCartRepository>();
            builder.Services.AddScoped<IApplicationUser, ApplicationUserRepository>();

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(20);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
