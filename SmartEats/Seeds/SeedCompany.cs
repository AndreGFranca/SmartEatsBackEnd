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
                    new Company { Name = "André's Company",CNPJ ="123456789"  },
                    new Company { Name = "Rita's Company", CNPJ = "123456780" },
                    new Company { Name = "Diego's Company", CNPJ = "123456781" },
                    new Company { Name = "Vini's Company", CNPJ = "123456782" }
                );

                context.SaveChangesAsync().Wait();
            }
        }
    }
}
