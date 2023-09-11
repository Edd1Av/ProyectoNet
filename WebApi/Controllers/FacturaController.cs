using Aplication.FacturaAplication;
using Domain.Domain;
using Microsoft.AspNetCore.Authorization;
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
            List<Factura> facturas = _facturaService.GetFacturas();
            return Ok(facturas);

        }

        [HttpPost]
        public IActionResult PostFactura(FacturaApiModel factura)
        {

            ResponseApiModel resp = _facturaService.CreateFactura(factura);
            return Ok(resp);
        }


        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteFactura(FacturaApiModel factura)
        {
            ResponseApiModel resp = _facturaService.DeleteFactura(factura.Id);
            return Ok(resp);
        }
    }
}
