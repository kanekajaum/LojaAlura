using CasaDoCodigo.Models;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        void UpdateQuantidade(ItemPedido itemPedido);
    }

    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly ApplicationContext _context;

        public ItemPedidoRepository(ApplicationContext context, IProdutoRepository produtoRepository)
        {
            _context = context;
        }

        public void UpdateQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoDB = _context.Set<ItemPedido>()
                .Where(x => x.Id == itemPedido.Id).SingleOrDefault();

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);
                _context.SaveChanges();
            }
        }
    }
}
