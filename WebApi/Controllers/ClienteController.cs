using Aplication.ClienteAplication;
using Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteManagmentServices _clienteService;

        public ClienteController(IClienteManagmentServices clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("clientes")]
        public IActionResult GetClientes()
        {
            Response<List<Cliente>> clientes = _clienteService.GetClientes();
            return Ok(clientes);
        }

        [HttpGet]
        [Route("cliente")]
        public IActionResult GetCliente(int id)
        {
            Response<Cliente?> cliente = _clienteService.GetCliente(id);
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult PostCliente(ClienteApiModel clienteApiModel)
        {
            Cliente cliente = new Cliente()
            {
                Nombre = clienteApiModel.Nombre.Trim(),
                Apellido = clienteApiModel.Apellido.Trim(),
                Edad = clienteApiModel.Edad,
                CorreoElectronico = clienteApiModel.CorreoElectronico.Trim().ToLower(),
            };
            Response<int?> resp = _clienteService.CreateCliente(cliente);
            return Ok(resp);
        }

        [HttpPut]
        public IActionResult UpdateCliente(ClienteApiModel clienteApiModel)
        {
            Cliente cliente = new Cliente()
            {
                Id = clienteApiModel.Id,
                Nombre = clienteApiModel.Nombre.Trim(),
                Apellido = clienteApiModel.Apellido.Trim(),
                Edad = clienteApiModel.Edad,
                CorreoElectronico = clienteApiModel.CorreoElectronico.Trim().ToLower(),
            };
            Response<bool> resp = _clienteService.UpdateCliente(cliente);
            return Ok(resp);
        }

        [HttpDelete]
        public IActionResult DeleteUsuario(int id)
        {
            Response<bool> resp = _clienteService.DeleteCliente(id);
            return Ok(resp);
        }
    }
}
