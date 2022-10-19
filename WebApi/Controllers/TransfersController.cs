using Business.Abstract;
using Core.Utilities.Requests;
using Entities.Dtos.Transfer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransfersController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] TransferGetByIdRequestDto requestDto)
        {
            var result = await _transferService.GetByIdAsync(requestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetFilePath")]
        public async Task<IActionResult> GetFilePathAsync([FromQuery] TransferGetFileRequestDto requestDto)
        {
            var result = await _transferService.GetFilePathAsync(requestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetListAsync([FromQuery] DataRequest<TransferGetListRequestDto> requestDto)
        {
            var result = await _transferService.GetAsync(requestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] TransferAddDto addDto)
        {
            var result = await _transferService.AddAsync(addDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TransferUpdateDto updateDto)
        {
            var result = await _transferService.UpdateAsync(updateDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] TransferDeleteDto deleteDto)
        {
            var result = await _transferService.DeleteAsync(deleteDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
