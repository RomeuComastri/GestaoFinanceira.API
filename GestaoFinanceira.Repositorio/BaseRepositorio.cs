using GestaoFinanceira.Repositorio.Contexto;

public abstract class BaseRepositorio
{
    protected readonly GestaoFinanceiraContexto _contexto;

    protected BaseRepositorio(GestaoFinanceiraContexto contexto)
    {
        _contexto = contexto;
    }
}