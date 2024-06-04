using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartEats.Models.Companies;
using SmartEats.Models.Confirms;
using SmartEats.Models.Justifies;
using SmartEats.Models.Menus;
using SmartEats.Models.Users;
using System;

namespace SmartEats.DataBase
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Menu>().Property(e => e.Data)
                .HasConversion(new DateOnlyConverter());

            modelBuilder.Entity<Menu>()
                .HasKey(m => new { m.Data, m.IdEmpresa });

            modelBuilder.Entity<Company>()
                        .HasIndex(e => e.CNPJ)
                        .IsUnique();

            modelBuilder.Entity<PlateDay>().Property(e => e.CardapioDate)
                .HasConversion(new DateOnlyConverter());

            modelBuilder.Entity<PlateDay>()
                .HasOne(pd => pd.Cardapio)
                .WithMany(m => m.PlatesDay)
                .HasForeignKey(pd => new { pd.CardapioDate, pd.CompanyId });

            //modelBuilder.Entity<TypeUser>()
            //    .HasMany(conversa => conversa.Mensagens)
            //    .WithOne(mensagem => mensagem.Conversa)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Mensagem>()
            //    .HasOne(mensagem => mensagem.Conversa)
            //    .WithMany(conversa => conversa.Mensagens);

            //modelBuilder.Entity<Participante>()
            //.HasKey(participante => new { participante.Id_Usuario, participante.Id_Conversa });

        }
        //public DbSet<Conversa> Conversas { get; set; }
        //public DbSet<Mensagem> Mensagens { get; set; }
        //public DbSet<Participante> Participantes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Confirm> Confirms { get; set; }
        public DbSet<Justify> Justifies { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<PlateDay> PlatesDay { get; set; }

        //public DbSet<TypeUser> TypesUser { get; set; }
    }
}
