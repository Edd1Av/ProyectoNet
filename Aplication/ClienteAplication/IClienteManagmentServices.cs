using Domain.Domain;

namespace Aplication.ClienteAplication
{
    public interface IClienteManagmentServices
    {
        public abstract Response<int?> CreateCliente(Cliente cliente);
        public abstract Response<bool> DeleteCliente(int id);
        public abstract Response<List<Cliente>> GetClientes();
        public abstract Response<Cliente?> GetCliente(int id);
        public abstract Response<bool> UpdateCliente(Cliente cliente);  
    }
}
