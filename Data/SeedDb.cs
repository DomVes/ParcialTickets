using ParcialTickets.Data;
using ParcialTickets.Data.Entities;

namespace Parcial2.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckEntrancesAsync();
        }

        public async Task CheckEntrancesAsync()
        {
            if (!_context.Entradas.Any())
            {
                _context.Entradas.Add(new Entrance { Description = "Norte" });
                _context.Entradas.Add(new Entrance { Description = "Sur" });
                _context.Entradas.Add(new Entrance { Description = "Occidete" });
                _context.Entradas.Add(new Entrance { Description = "Oriente" });
                await _context.SaveChangesAsync();

            }
        }

    }
}
