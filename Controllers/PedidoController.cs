using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        protected readonly ApplicationContext _context;
        protected readonly DbSet<Pedido> dbSet;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(ApplicationContext _context,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            this._context = _context;
            dbSet = _context.Set<Pedido>();
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public IActionResult Carrossel()
        {
            return View(produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.addItem(codigo);
            }
            Pedido pedido = pedidoRepository.GetPedido();
            return View(pedido.Itens);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Resumo()
        {
            Pedido pedido = pedidoRepository.GetPedido();
            return View(pedido);
        }

        [HttpPost]
        public void UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            itemPedidoRepository.UpdateQuantidade(itemPedido);
        }
    }
}