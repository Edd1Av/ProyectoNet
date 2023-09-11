using Domain.Domain;

namespace Aplication.ClienteAplication
{
    public interface IClienteManagmentServices
    {
        public abstract Response CreateUsuario(Cliente cliente);
        public abstract Response DeleteUsuario(int id);
        public abstract Response GetClientes();
    }
}
