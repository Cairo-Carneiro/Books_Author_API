using API_NEW.Models;
using Microsoft.EntityFrameworkCore;

namespace API_NEW.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //Meio de campo que recebe entre o server e o bancosa
        {

        }

        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

    }
}
