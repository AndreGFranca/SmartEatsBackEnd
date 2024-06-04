using Microsoft.AspNetCore.Identity;
using SmartEats.DataBase;
using SmartEats.Models.Companies;
using SmartEats.Models.Users;

namespace SmartEats.Seeds
{
    public class SeedUsers
    {
        public static void Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, ApplicationDBContext context)
        {
            //Verifica se já existem dados na tabela antes de adicionar

            if (!context.Users.Any())
            {
                userManager.CreateAsync(
                    new User
                    {
                        Name = "André's Company",
                        CPF = "123456789",
                        UserName = "andre@gmail.com",
                        Id_Company = 1,
                        TypeUser = Enums.Users.TypeUser.Empresa,
                        Ativo = true,
                    }, "@1234Andre").Wait();
                userManager.CreateAsync(
                new User
                {
                    Name = "Rita's Company",
                    CPF = "123456780",
                    UserName = "rita@gmail.com",
                    Id_Company = 2,
                    TypeUser = Enums.Users.TypeUser.Empresa,
                    Ativo = true,
                }, "@1234Rita").Wait();

                userManager.CreateAsync(
                    new User
                    {
                        Name = "Diego's Company",
                        CPF = "123456781",
                        UserName = "diego@gmail.com",
                        Id_Company = 2,
                        TypeUser = Enums.Users.TypeUser.Empresa,
                        Ativo = true,
                    }, "@1234Diego").Wait();

                userManager.CreateAsync(
                    new User
                    {
                        Name = "Vini's Company",
                        CPF = "123456782",
                        UserName = "vini@gmail.com",
                        Id_Company = 2,
                        TypeUser = Enums.Users.TypeUser.Empresa,
                        Ativo = true,
                    }, "@1234Vini").Wait();
            }
        }
    }
}
