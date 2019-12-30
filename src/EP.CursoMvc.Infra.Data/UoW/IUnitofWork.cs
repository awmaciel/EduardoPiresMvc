namespace EP.CursoMvc.Infra.Data.UoW
{
    public interface IUnitofWork
    {
        bool Commit();
    }
}