using Data.Data;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aplication.FacturaAplication
{
    public class FacturaManagmentServices : IFacturaManagmentServices
    {
        private readonly DataDBContext _context;

        public FacturaManagmentServices(DataDBContext context)
        {
            _context = context;
        }

        public Response<int?> CreateFactura(Factura facturaEntity)
        {
            if (_context.Facturas.Any(x => x.Folio.ToUpper() == facturaEntity.Folio.ToUpper()))
            {
                return new Response<int?> { Success = true, Message = "Ya existe una factura con ese folio", Content = null };
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Facturas.Add(facturaEntity);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response<int?> { Success = false, Message = ex.Message, Content = null };
                }
            }
                
            return new Response<int?> { Success = true,  Message= "Factura registrada", Content=facturaEntity.Id};
        }

        public Response<bool> DeleteFactura(int id)
        {
            Factura facturaEntity = _context.Facturas.Find(id);
            if (facturaEntity != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Facturas.Remove(facturaEntity);
                        _context.SaveChanges();
                        transaction.Commit();
                        return new Response<bool> { Success = true, Message = "Factura eliminada", Content = true };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new Response<bool> { Success = false, Message = ex.Message, Content = false };
                    }
                }

            }
            else
            {
                return new Response<bool> { Success = true, Message = "La factura no existe", Content=false };
            }
        }

        public Response<List<Factura>> GetFacturas()
        {
            List<Factura> facturas = _context.Facturas.Include(x => x.Cliente).ToList();
            if (facturas == null || facturas.Count() == 0)
            {
                return new Response<List<Factura>> { Success = true, Message = $"No hay facturas registradas", Content = null };
            }
            else
            {
                return new Response<List<Factura>> { Success = true, Message = null, Content = facturas };
            }

        }

        public Response<Factura?> GetFactura(int id)
        {
            Factura? factura = _context.Facturas.Where(x=>x.Id==id).Include(x => x.Cliente).FirstOrDefault();
            if (factura == null)
            {
                return new Response<Factura?> { Success = true, Message = $"La factura no existe", Content = null };
            }
            else
            {
                return new Response<Factura?> { Success = true, Message = null, Content = factura };
            }
        }

        public Response<bool> UpdateFactura(Factura factura)
        {
            if(factura.Id==null || factura.Id < 0)
            {
                return new Response<bool> { Success = true, Message = "El Id de la factura no es valido", Content = false };
            }
            if (_context.Facturas.Any(x=>x.Id==factura.Id) == false)
            {
                return new Response<bool> { Success = true, Message = "La factura no existe", Content = false };
            }
            if (_context.Facturas.Any(x => x.Folio.ToUpper() == factura.Folio.ToUpper()))
            {
                return new Response<bool> { Success = true, Message = "Ya existe una factura con ese folio", Content = false };
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Factura UpdateFactura = _context.Facturas.Find(factura.Id);
                    UpdateFactura.FechaFacturacion = factura.FechaFacturacion;
                    UpdateFactura.Saldo = factura.Saldo;
                    UpdateFactura.Folio = factura.Folio;
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
