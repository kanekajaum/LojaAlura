using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository 
    {
        void addItem(string codigo);
        Pedido GetPedido();
    }

    public class PedidoRepository : IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        protected readonly ApplicationContext _context;
        protected readonly DbSet<Pedido> dbSet;

        public PedidoRepository(ApplicationContext _context,
            IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this._context = _context;
            dbSet = _context.Set<Pedido>();
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = dbSet
                .Include(x => x.Itens)
                .ThenInclude(i => i.Produto)
                .Where(x => x.Id == pedidoId)
                .SingleOrDefault();
            if(pedido == null)
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                _context.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }

        private int? GetPedidoId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("pedidoID");   
        }


        private void SetPedidoId(int pedidoID)
        {
            contextAccessor.HttpContext.Session.SetInt32("pedidoID", pedidoID);
        }

        public void addItem(string codigo)
        {
            var produto = _context.Set<Produto>()
                .Where(x => x.Codigo == codigo)
                .SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = _context.Set<ItemPedido>()
                                .Where(i => i.Produto.Codigo == codigo
                                    && i.Pedido.Id == pedido.Id)
                                .SingleOrDefault();
            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _context.Set<ItemPedido>()
                    .Add(itemPedido);

                _context.SaveChanges();
            }
        }
    }
}
