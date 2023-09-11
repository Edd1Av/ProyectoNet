using Domain.Domain;

namespace Aplication.FacturaAplication
{
    public interface IFacturaManagmentServices
    {
        public abstract Response CreateFactura(Factura product);
        public abstract Response DeleteFactura(int id);
        public abstract Response GetFacturas();
    }
}
