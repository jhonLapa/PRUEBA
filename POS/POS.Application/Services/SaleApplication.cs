using AutoMapper;
using POS.Application.Commons.Base;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Commons.Sale.Request;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class SaleApplication : ISaleApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaleApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async  Task<BaseResponse<bool>> RegisterSaleProcedure(sp_ventRequestDto RequestDto)
        {
            var response = new BaseResponse<bool>();


            var sale = _mapper.Map<sp_CrearVentaEntityRequest>(RequestDto);
            response.Data = await _unitOfWork.Sale.RegisterSaleProcedure(sale);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> UpdateSaleProcedure(int saleId, sp_ventRequestDto saleRequestDto)
        {
            var response = new BaseResponse<bool>();
            var SaleEdit = await SaleById(saleId);

            if (SaleEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var sale = _mapper.Map<sp_EditarVentaEntityRequest>(saleRequestDto);
            sale.SaleId = saleId;
            response.Data = await _unitOfWork.Sale.UpdateSaleProcedure(sale);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveSaleProcedure(int saleId, sp_EliminarSaleRequest saleRequestDto)
        {
            var response = new BaseResponse<bool>();
            var SaleEdit = await SaleById(saleId);

            if (SaleEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var sale = _mapper.Map<EliminarSaleEntityRequest>(saleRequestDto);
            sale.SaleId = saleId;
            response.Data = await _unitOfWork.Sale.RemoveSaleProcedure(sale);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveSaleDetailProcedure(int saleId, sp_EliminarSaleRequest saleRequestDto)
        {
            var response = new BaseResponse<bool>();
            var SaleEdit = await SaleById(saleId);

            if (SaleEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var sale = _mapper.Map<sp_EliminarSaleDetalleEntityRequest>(saleRequestDto);
            sale.SaleId = saleId;
            response.Data = await _unitOfWork.Sale.RemoveSaleDetailProcedure(sale);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async  Task<BaseResponse<IEnumerable<sp_ListaComprasUsersResponse>>> ReporteSaleProcedure(int userlId)
        {
            var response = new BaseResponse<IEnumerable<sp_ListaComprasUsersResponse>>();
            var sale = await _unitOfWork.Sale.ReporteSaleProcedure(userlId);

            if (sale is not null)
            {
                response.Data = _mapper.Map<IEnumerable<sp_ListaComprasUsersResponse>>(sale);
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            return response;
        }

        public async Task<BaseResponse<SaleResponseDto>> SaleById(int saleId)
        {
            var response = new BaseResponse<SaleResponseDto>();
            var sale = await _unitOfWork.Sale.GetByIdAsync(saleId);

            if (sale is not null)
            {
                response.Data = _mapper.Map<SaleResponseDto>(sale);
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<SaleDetailByIdResponseDto>>> SaleDetailById(int saleId)
        {
            var response = new BaseResponse<IEnumerable<SaleDetailByIdResponseDto>>();
            var saleDetail = await _unitOfWork.Sale.SaleDetailById(saleId);

            if (saleDetail is not null)
            {
                response.Data = _mapper.Map<IEnumerable<SaleDetailByIdResponseDto>>(saleDetail);
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            return response;
        }

    }
}
