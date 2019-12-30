using EP.CursoMvc.Infra.Data.Context;

namespace EP.CursoMvc.Infra.Data.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private readonly CursoMvcContext _context;

        public UnitofWork(CursoMvcContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}