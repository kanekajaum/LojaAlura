using CasaDoCodigo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context)
        {
            _context = context;
        }

        public void InicializaDB()
        {
            _context.Database.EnsureCreated();

            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

            foreach (var livro in livros)
            {
                _context.Set<Produto>().Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));

            }
            _context.SaveChanges();
        }
    }

    class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
