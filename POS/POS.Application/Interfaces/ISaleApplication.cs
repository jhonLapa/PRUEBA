using POS.Application.Commons.Base;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Dtos.Sale.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface ISaleApplication
    {
        Task<BaseResponse<IEnumerable<SaleDetailByIdResponseDto>>> SaleDetailById(int saleId);
        Task<BaseResponse<SaleResponseDto>> SaleById(int saleId);
        Task<BaseResponse<bool>> RegisterSaleProcedure(sp_ventRequestDto RequestDto);
        Task<BaseResponse<bool>> UpdateSaleProcedure(int saleId, sp_ventRequestDto saleRequestDto);
        Task<BaseResponse<bool>> RemoveSaleProcedure(int saleId, sp_EliminarSaleRequest saleRequestDto);
        Task<BaseResponse<bool>> RemoveSaleDetailProcedure(int saleId, sp_EliminarSaleRequest saleRequestDto);
        Task<BaseResponse<IEnumerable<sp_ListaComprasUsersResponse>>> ReporteSaleProcedure(int userlId);

    }
}
