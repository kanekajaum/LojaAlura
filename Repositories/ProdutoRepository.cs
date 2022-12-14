using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationContext _context;

        public ProdutoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IList<Produto> GetProdutos()
        {
            return _context.Set<Produto>().ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!_context.Set<Produto>().Where(x => x.Codigo == livro.Codigo).Any())
                {
                    _context.Set<Produto>().Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
                }

            }
            _context.SaveChanges();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
