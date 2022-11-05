using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;


namespace POS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleApplication _saleApplication;

        public SaleController(ISaleApplication saleApplication)
        {
            _saleApplication = saleApplication;
        }

        [HttpGet("{saleId:int}")]
        public async Task<IActionResult> SaleById(int saleId)
        {
            var response = await _saleApplication.SaleById(saleId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("SaleDetailById/{saleId:int}")]
        public async Task<IActionResult> SaleDetailById(int saleId)
        {
            var response = await _saleApplication.SaleDetailById(saleId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("RegisterProcedure")]
        public async Task<IActionResult> RegisterSaleProcedure([FromBody] sp_ventRequestDto requestDto)
        {
            if (requestDto is null)
                return BadRequest();
            var response = await _saleApplication.RegisterSaleProcedure(requestDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);

        }

        [HttpPut("UpdateProcedure/{saleId:int}")]
        public async Task<IActionResult> UpdateSaleProcedure(int saleId, [FromBody] sp_ventRequestDto requestDto)
        {
            if (requestDto is null)
                return BadRequest();

            var response = await _saleApplication.UpdateSaleProcedure(saleId, requestDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ReporteSaleProcedure/{userlId:int}")]
        public async Task<IActionResult> ReporteSaleProcedure(int userlId)
        {
            var response = await _saleApplication.ReporteSaleProcedure(userlId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Remove/{saleId:int}")]
        public async Task<IActionResult> RemoveSaleProcedure(int saleId, [FromBody] sp_EliminarSaleRequest requestDto)
        {
            if (requestDto is null)
                return BadRequest();

            var response = await _saleApplication.RemoveSaleProcedure(saleId, requestDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("RemoveSaleDetailProcedure/{saleId:int}")]
        public async Task<IActionResult> RemoveSaleDetailProcedure(int saleId, [FromBody] sp_EliminarSaleRequest requestDto)
        {
            if (requestDto is null)
                return BadRequest();

            var response = await _saleApplication.RemoveSaleDetailProcedure(saleId, requestDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
       


    }
}
