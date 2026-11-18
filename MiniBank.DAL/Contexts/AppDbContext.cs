using Microsoft.EntityFrameworkCore;
using MiniBank.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.DAL.Contexts;
public class AppDbContext:DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transactionn> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
}
