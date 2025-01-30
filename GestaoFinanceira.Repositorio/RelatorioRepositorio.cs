using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestaoFinanceira.Dominio.Entidades;
using GestaoFinanceira.Repositorio.Contexto;
using GestaoFinanceira.Dominio.Enumeradores;
using GestaoFinanceira.Dominio.Models;
using Dapper;


namespace GestaoFinanceira.Repositorio
{
    public class RelatorioRepositorio : BaseRepositorio, IRelatorioRepositorio
    {
        public RelatorioRepositorio(GestaoFinanceiraContexto contexto) : base(contexto)
        {
        }

        public async Task<SaldoToTalTransacoes> SaldoTotalUsuario(int usuarioId, DateTime? dataInicio = null, DateTime? dataFim = null, int? categoriaId = null, string tipo = null)
        {
            var parameters = new
            {
                UsuarioID = usuarioId,
                DataInicio = dataInicio,
                DataFim = dataFim,
                CategoriaID = categoriaId,
                Tipo = tipo
            };

            // Certifique-se de que o método CriarConexao retorna uma SqlConnection
            using (var connection = _contexto.CriarConexao())
            {
                // Abrir a conexão de forma assíncrona
                await connection.OpenAsync();

                // Executar a stored procedure de forma assíncrona e obter o resultado
                var saldo = await connection.QuerySingleOrDefaultAsync<SaldoToTalTransacoes>(
                    "GetSaldoComFiltros",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return saldo;
            }
        }

        public async Task<IEnumerable<TransacoesComFiltro>> ObterTransacoesComFiltroAsync(int usuarioId, string tipo = null, int? categoriaId = null, DateTime? dataInicio = null, DateTime? dataFim = null, bool? status = null)
        {
            var parameters = new
            {
                UsuarioID = usuarioId,
                Tipo = tipo,
                CategoriaID = categoriaId,
                DataInicio = dataInicio,
                DataFim = dataFim,
                Status = status
            };

            // Usando o método CriarConexao para obter a conexão do banco
            using (var connection = _contexto.CriarConexao())
            {
                // Abrir a conexão de forma assíncrona
                await connection.OpenAsync();

                // Executar a stored procedure de forma assíncrona e obter os resultados
                var transacoes = await connection.QueryAsync<TransacoesComFiltro>(
                    "ObterListaTransacoes",  // Nome da sua stored procedure
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return transacoes;
            }
        }
    }

}
