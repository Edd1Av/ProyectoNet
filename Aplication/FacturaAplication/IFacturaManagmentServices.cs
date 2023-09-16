using Domain.Domain;

namespace Aplication.FacturaAplication
{
    public interface IFacturaManagmentServices
    {
        public abstract Response<int?> CreateFactura(Factura product);
        public abstract Response<bool> DeleteFactura(int id);
        public abstract Response<List<Factura>> GetFacturas();
        public abstract Response<Factura?> GetFactura(int id);
        public abstract Response<bool> UpdateFactura(Factura factura);
    }
}
