using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartEats.Models.Companies;
using SmartEats.Models.Users;
using System;

namespace SmartEats.DataBase
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TypeUser>()
        //        .HasMany(conversa => conversa.Mensagens)
        //        .WithOne(mensagem => mensagem.Conversa)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<Mensagem>()
        //        .HasOne(mensagem => mensagem.Conversa)
        //        .WithMany(conversa => conversa.Mensagens);

        //    modelBuilder.Entity<Participante>()
        //    .HasKey(participante => new { participante.Id_Usuario, participante.Id_Conversa });

        //}
        //public DbSet<Conversa> Conversas { get; set; }
        //public DbSet<Mensagem> Mensagens { get; set; }
        //public DbSet<Participante> Participantes { get; set; }
        public DbSet<Company> Companies {  get; set; }
        //public DbSet<TypeUser> TypesUser { get; set; }
    }
}
