using CpmPedidos.Interface;

namespace CpmPedidos.Repository
{
    public class BaseRepository
    {
        protected const int TamanhoPagina = 5;

        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
