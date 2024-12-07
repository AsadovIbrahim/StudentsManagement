using Microsoft.AspNetCore.Identity;
using StudentManagement.Models.Concretes;

namespace StudentManagement.Services
{
    public static class RegisterService
    {
        public static async void AddRoleServices(this IServiceProvider collection)
        {
            using var container = collection.CreateScope();
            var usermanager = container.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRole = await roleManager.RoleExistsAsync("Admin");

            if (!adminRole)
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            var adminUser = await usermanager.FindByNameAsync("admin");

            if (adminUser is null)
            {
                var result = await usermanager.CreateAsync(new User
                {
                    UserName = "admin",
                    FirstName = "Ibrahim",
                    LastName = "Asadov",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                }, "ibrahiM123!");

                if (result.Succeeded)
                {
                    var user = await usermanager.FindByNameAsync("admin");
                    await usermanager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
        
    
}

