using Moq;
using System.Linq.Expressions;
using project_core.Models;
using project_core.Interfaces;

namespace project_core_mstest.Repositories;

public class ClienteRepositoryMoq : IRepository<Cliente>
{
    private readonly List<Cliente> clientes;
    private readonly Mock<IRepository<Cliente>> mockClientes;

    public ClienteRepositoryMoq()
    {
        clientes = new();
        mockClientes = new();
    }

    public Cliente Create(Cliente entity)
    {
        mockClientes.Setup(x => x.Create(It.IsAny<Cliente>())).Returns((Cliente cliente) =>
        {
            cliente.Id = clientes.Count + 1;
            clientes.Add(cliente);
            
            return cliente;
        });

        return mockClientes.Object.Create(entity);
    }

    public Cliente Update(Cliente entity)
    {
        mockClientes.Setup(x => x.Update(It.IsAny<Cliente>())).Returns((Cliente cliente) =>
        {
            var returnCliente = clientes.FirstOrDefault(x => x.Id == cliente.Id);
            
            if (returnCliente == null) throw new ArgumentException("Cliente não encontrado!");

            returnCliente.Nome = cliente.Nome;
            returnCliente.Email = cliente.Email;

            return returnCliente;
        });        

        return mockClientes.Object.Update(entity);
    }

    public void Delete(Cliente entity)
    {
        mockClientes.Setup(x => x.Delete(It.IsAny<Cliente>())).Callback((Cliente cliente) =>
        {
            clientes.Remove(cliente);
        });

        mockClientes.Object.Delete(entity);
    }

    public Cliente Get(params object[] key)
    {
        mockClientes.Setup(x => x.Get(It.IsAny<object[]>())).Returns((object[] k) =>
        {
            var returnCliente = clientes.FirstOrDefault(x => x.Id == (int)(k[0]));
            
            return returnCliente ?? new();
        });

        return mockClientes.Object.Get(key);
    }

    public IQueryable<Cliente> GetAll()
    {
        mockClientes.Setup(x => x.GetAll()).Returns(clientes.AsQueryable());

        return mockClientes.Object.GetAll();
    }

    public IQueryable<Cliente> GetAll(Expression<Func<Cliente, bool>> predicate)
    {
        mockClientes.Setup(x => x.GetAll(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns((Expression<Func<Cliente, bool>> p) =>
        {
            return clientes.AsQueryable().Where(p);
        });

        return mockClientes.Object.GetAll(predicate);
    }
}
