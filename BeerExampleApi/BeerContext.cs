using BeerExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerExampleApi
{
    public class BeerContext : DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options) : base(options)
        {
        }
        
        public DbSet<Beer> Beers { get; set; }
    }
}