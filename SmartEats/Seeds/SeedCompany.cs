using SmartEats.DataBase;
using SmartEats.Models.Companies;


namespace SmartEats.Seeds
{
    public class SeedCompany
    {
        public static void Initialize(IServiceProvider serviceProvider, ApplicationDBContext context)
        {
            //Verifica se já existem dados na tabela antes de adicionar
            if (!context.Companies.Any())
            {
                context.Companies.AddRange(
                    new Company { Name = "Firma 1" },
                    new Company { Name = "Firma 2" },
                    new Company { Name = "Firma 3" },
                    new Company { Name = "Firma 4" }
                );

                context.SaveChanges();
            }
        }
    }
}
