using Data.Data;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ClienteAplication
{
    public class ClienteManagmentServices : IClienteManagmentServices
    {
        private readonly DataDBContext _context;

        public ClienteManagmentServices(DataDBContext context)
        {
            _context = context;
        }

        public Response<int?> CreateCliente(Cliente clienteEntity)
        {
            if (_context.Clientes.Any(x => x.CorreoElectronico.ToLower() == clienteEntity.CorreoElectronico.ToLower()))
            {
                return new Response<int?> { Success = true, Message = "Ya existe un cliente registrado con ese correo electrónico", Content = null };
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Clientes.Add(clienteEntity);

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response<int?> { Success = false, Message = $"{ex.Message}", Content=null};
                }
            }

            return new Response<int?> { Success = true, Message = "Usuario registrado", Content=clienteEntity.Id};
        }

        public Response<bool> DeleteCliente(int id)
        {
            Cliente usuarioEntity = _context.Clientes.Find(id);
            if (usuarioEntity == null)
            {
                return new Response<bool> { Success = true, Message = "El usuario no existe", Content = false };
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Clientes.Remove(usuarioEntity);

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response<bool> { Success = false, Message = $"{ex.Message}", Content = false };
                }
            }
            return new Response<bool> { Success = true, Message = "Cliente Eliminado", Content=true };
        }

        public Response<List<Cliente>> GetClientes()
        {
            List<Cliente> clientes = _context.Clientes.Include(x => x.Facturas).ToList();
            if (clientes==null || clientes.Count() == 0)
            {
                return new Response<List<Cliente>> { Success = true, Message = $"No hay clientes registrados", Content = null};
            }
            else
            {
                return new Response<List<Cliente>> { Success = true, Message = null, Content = clientes };
            }
        }

        public Response<Cliente?> GetCliente(int id)
        {
            Cliente? cliente = _context.Clientes.Where(x => x.Id == id).Include(x=>x.Facturas).FirstOrDefault();
            if (cliente == null)
            {
                return new Response<Cliente?> { Success = true, Message = $"El cliente no existe", Content = null };
            }
            else
            {
                return new Response<Cliente?> { Success = true, Message = null, Content = cliente };
            }
        }

        public Response<bool> UpdateCliente(Cliente cliente)
        {
            if (cliente.Id == null || cliente.Id < 0)
            {
                return new Response<bool> { Success = true, Message = "El Id del cliente no es valido", Content = false };
            }
            if (_context.Clientes.Any(x => x.CorreoElectronico.ToLower() == cliente.CorreoElectronico.ToLower() && x.Id!=cliente.Id))
            {
                return new Response<bool> { Success = true, Message = "Ya existe un cliente registrado con ese correo electrónico", Content = false };
            }
            if (_context.Clientes.Any(x => x.Id == cliente.Id) == false)
            {
                return new Response<bool> { Success = true, Message = "El cliente no existe", Content = false };
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //Cliente updateC = _context.Clientes.Find(cliente.Id);
                    //if (updateC == null) {
                    //    return new Response<bool> { Success = true, Message = "El cliente no existe", Content = false };
                    //}
                    //updateC.Nombre = cliente.Nombre;
                    //updateC.Apellido = cliente.Apellido;
                    //updateC.Edad = cliente.Edad;
                    //updateC.CorreoElectronico = cliente.CorreoElectronico;
                    _context.Clientes.Update(cliente);
                    _context.SaveChanges();
                    transaction.Commit();
                    return new Response<bool> { Success = true, Message = null, Content = true };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response<bool> { Success = false, Message = $"{ex.Message}", Content = false };
                }
            }
        }
    }

}
