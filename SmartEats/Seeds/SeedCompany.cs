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
                    new Company { Name = "Vini's Company", CNPJ = "123456782" },
                    new Company { Name = "Vitor's Company", CNPJ = "123456783" },
                    new Company { Name = "Peterson's Company", CNPJ = "123456784" }
                );

                context.SaveChangesAsync().Wait();
            }
        }
    }
}
