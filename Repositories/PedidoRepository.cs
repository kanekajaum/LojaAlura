using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository 
    {
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
            var pedido = dbSet.Where(x => x.Id == pedidoId)
                .SingleOrDefault();
            if(pedido == null)
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                _context.SaveChanges();
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
    }
}
