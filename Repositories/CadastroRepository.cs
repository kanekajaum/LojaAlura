using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroRepository
    {
    }

    public class CadastroRepository : ICadastroRepository
    {
        public CadastroRepository(ApplicationContext context, IProdutoRepository produtoRepository)
        {
        }
    }
}
