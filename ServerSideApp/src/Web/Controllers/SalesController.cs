using Application.CQRS.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using ServerSideApp.Application.CQRS.Commans.Create;
using ServerSideApp.Application.CQRS.Commans.Delete;
using ServerSideApp.Application.CQRS.Commans.Update;
using ServerSideApp.Application.CQRS.Queries.Get;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Enums;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Domain.Entities;
using ServerSideApp.Web.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly IMediator _mediator;
        private readonly ISaleStatisticService _saleStatisticService;

        /// <summary>
        /// Create controller to manage sales API.
        /// </summary>
        /// <param name="logger">Logger service.</param>
        /// <param name="mediator">Mediator service.</param>
        /// <param name="saleStatisticService">Service to calculate sales statistic.</param>
        public SalesController(ILogger<SalesController> logger,
                                IMediator mediator,
                                ISaleStatisticService saleStatisticService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _saleStatisticService = saleStatisticService ?? throw new ArgumentNullException(nameof(saleStatisticService));
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IEnumerable<SaleDTO>> GetSales()
        {
            var sales = await _mediator.Send(new GetSalesQuery());

            _logger.LogInformation($"{sales.Count()} {SalesConstants.GET_SALES}");

            return sales;
        }

        // GET: api/sales/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleQuery = new GetSaleQuery { Id = id };
            var sale = await _mediator.Send(saleQuery);

            if (sale == null)
            {
                _logger.LogWarning($"{id} {SalesConstants.SALE_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.LogInformation($"{sale.Id} {SalesConstants.GET_FOUND_SALE}");
            return Ok(sale);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> RegisterNewSale([FromBody] SaleDTO sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createSaleCommand = new CreateSaleCommand { Model = sale };

            int id;
            try
            {
                id = await _mediator.Send(createSaleCommand);
            }
            catch
            {
                _logger.LogInformation($"{sale.Id} {SalesConstants.REGISTER_SALE_CONFLICT}");
                return Conflict();
            }

            sale.Id = id;

            _logger.LogInformation($"{sale.Id} {SalesConstants.REGISTER_SALE_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewSale), sale);
        }

        // PUT: api/sales/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale([FromBody] SaleDTO sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sale.Id <= 0)
            {
                return BadRequest(sale.Id);
            }

            var updateSaleCommand = new UpdateSaleCommand { Model = sale };

            try
            {
                await _mediator.Send(updateSaleCommand);
            }
            catch (Exception)
            {
                _logger.LogWarning($"{sale.Id} {SalesConstants.UPDATE_SALE_CONFLICT}");
                return Conflict();
            }

            _logger.LogInformation($"{sale.Id} {SalesConstants.UPDATE_SALE_SUCCESS}");
            return Ok(sale);
        }

        // DELETE: api/sales/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteSaleCommand = new DeleteSaleCommand { Id = id };

            try
            {
                await _mediator.Send(deleteSaleCommand);
            }
            catch
            {
                _logger.LogWarning($"{id} {SalesConstants.SALE_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.LogInformation($"{id} {SalesConstants.DELETE_SALE_SUCCESS}");
            return Ok(id);
        }

        // GET: api/sales/statistic?scale=0&startdate=2019-11-01&enddate=2020-02-01
        [HttpGet("statistic")]
        public async Task<IActionResult> GetSalesStatistic([FromQuery] TimeUnit? scale, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (scale == null || !startDate.HasValue || !endDate.HasValue)
            {
                return BadRequest("Missing one or more request parameters!");
            }

            if (startDate.Value >= endDate.Value)
            {
                return BadRequest("Parameter 'endDate' must be greater than 'startDate'!");
            }

            var sales = await _saleStatisticService.GetSalesStatistic(scale.Value, startDate.Value, endDate.Value);

            return Ok(sales);
        }
    }
}