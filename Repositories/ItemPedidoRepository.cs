namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
    }

    public class ItemPedidoRepository : IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext context, IProdutoRepository produtoRepository)
        {
        }
    }
}
