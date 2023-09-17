using Aplication.FacturaAplication;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaManagmentServices _facturaService;

        public FacturaController(IFacturaManagmentServices facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public IActionResult GetFacturas()
        {
            Response<List<Factura>> facturas = _facturaService.GetFacturas();
            return Ok(facturas);

        }

        [HttpGet]
        public IActionResult GetFactura(int id)
        {
            Response<Factura?> factura = _facturaService.GetFactura(id);
            return Ok(factura);

        }

        [HttpPost]
        public IActionResult PostFactura(FacturaApiModel facturaM)
        {
            Factura facturaEntity = new()
            {
                IdCliente = facturaM.IdCliente,
                Folio = facturaM.Folio,
                Saldo = facturaM.Saldo,
                FechaFacturacion = facturaM.FechaFacturacion,
                FechaCreacion = DateTime.Now
            };

            Response<int?> resp = _facturaService.CreateFactura(facturaEntity);
            return Ok(resp);
        }

        [HttpPut]
        public IActionResult UpdateFactura(FacturaApiModel facturaM)
        {
            Factura facturaEntity = new()
            {
                Id = facturaM.Id,
                IdCliente = facturaM.IdCliente,
                Folio = facturaM.Folio,
                Saldo = facturaM.Saldo,
                FechaFacturacion = facturaM.FechaFacturacion,
            };

            Response<bool> resp = _facturaService.UpdateFactura(facturaEntity);
            return Ok(resp);
        }


        [HttpDelete]
        public IActionResult DeleteFactura(int id)
        {
            Response<bool> resp = _facturaService.DeleteFactura(id);
            return Ok(resp);
        }
    }
}
