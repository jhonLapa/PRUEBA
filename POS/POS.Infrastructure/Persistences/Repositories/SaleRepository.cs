using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Sale.Request;
using POS.Infrastructure.Commons.Sale.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly POSContext _context;

        public SaleRepository(POSContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> RegisterSaleProcedure(sp_CrearVentaEntityRequest sale)
        {
            await _context.AddAsync(sale);
            var recordsAffected = await _context.Database.ExecuteSqlInterpolatedAsync
            ($"Exec sp_CrearVentaDetalle @UserId = {sale.UserId},@Client = {sale.Client},@Total = {sale.Total},@ProductId = {sale.product}");

            return recordsAffected > 0;
        }

        public async Task<bool> UpdateSaleProcedure(sp_EditarVentaEntityRequest sale)
        {
            await _context.AddAsync(sale);
            var recordsAffected = await _context.Database.ExecuteSqlInterpolatedAsync
            ($"Exec sp_ActualizarVentaDetalle @SaleId = {sale.SaleId} ,@UserId = {sale.UserId},@Client = {sale.Client},@Total = {sale.Total},@ProductId = {sale.product}");

            return recordsAffected > 0;
        }

        public async Task<bool> RemoveSaleDetailProcedure(sp_EliminarSaleDetalleEntityRequest sale)
        {
            _context.Remove(sale);
            var recordsAffected = await _context.Database.ExecuteSqlInterpolatedAsync
             ($@"Exec sp_EliminarVentaDetalleTotal @saleId = {sale.SaleId} ,@ProductId  = {sale.Product}  ");
            return recordsAffected > 0;
        }

        public async Task<bool> RemoveSaleProcedure(EliminarSaleEntityRequest sale)
        {
            _context.Update(sale);
            var recordsAffected = await _context.Database.ExecuteSqlInterpolatedAsync
             ($@"Exec sp_EliminarVentaDetalle @saleId = {sale.SaleId} ,@ProductId  = {sale.Product}  ");
            return recordsAffected > 0;
        }

        public async Task<IEnumerable<sp_ListaComprasUsersEntityResponse>> ReporteSaleProcedure(int userId)
        {
            var sale = await _context.Sp_ListaComprasUsersEntityResponses
                 .FromSqlInterpolated($"Exec sp_ListaComprasUsers  @UserId ={userId} ").ToListAsync();
            return sale;
        }

        public async Task<IEnumerable<SaleDetail>> SaleDetailById(int saleId)
        {
            var saleDetail = await _context.SaleDetails
               .Include(g => g.Sale)
               .Include(po => po.Product)
               .Include(s => s.Product.Category)
               .Where(u => u.SaleId.Equals(saleId))
               .AsNoTracking().ToArrayAsync();
            return saleDetail;
        }

      
    }
}
