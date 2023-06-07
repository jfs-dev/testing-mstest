using project_core.Models;
using project_core_mstest.Repositories;

namespace project_core_mstest;

[TestClass]
public class ClienteRepositoryMoqTests
{
    private static ClienteRepositoryMoq _repository = new();

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        _repository.Create(new Cliente { Nome = "Peter Parker", Email = "peter.parker@marvel.com" });
        _repository.Create(new Cliente { Nome = "Stan Lee", Email = "stan.lee@marvel.com" });
        _repository.Create(new Cliente { Nome = "Mary Jane", Email = "mary.jane@marvel.com" });
        _repository.Create(new Cliente { Nome = "Bruce Wayne", Email = "bruce.wayne@dc.com" });
        _repository.Create(new Cliente { Nome = "Diana", Email = "diana@dc.com" });
    }

    [TestMethod]
    [TestCategory("Create")]
    public void Dado_Cliente_Valido_Incluir_Retornar_Cliente_Id()
    {
        var tioBen = new Cliente { Nome = "Ben Parker", Email = "ben.parker@marvel.com" };
        var returnCliente = _repository.Create(tioBen);

        Assert.IsNotNull(returnCliente);
        Assert.AreEqual(6, returnCliente.Id);
    }

    [TestMethod]
    [TestCategory("Update")]
    public void Dado_Cliente_Valido_Atualizar_Retornar_Cliente_Atualizado()
    {
        var maryJane = _repository.Get(3);
        maryJane.Nome = "Mary Jane Watson";

        var returnCliente = _repository.Update(maryJane);

        Assert.IsNotNull(returnCliente);
        Assert.AreEqual(maryJane.Nome, returnCliente.Nome);
    }

    [TestMethod]
    [TestCategory("Update")]
    [ExpectedException(typeof(System.ArgumentException))]
    public void Dado_Cliente_Invalido_Retornar_Argument_Exception()
    {
        var lexLuthor = new Cliente { Id = 7, Nome = "Lex Luthor", Email = "lex.luthor@dc.com" };

        _repository.Update(lexLuthor);
    }

    [TestMethod]
    [TestCategory("Delete")]
    public void Dado_Cliente_Valido_Excluir()
    {
        var tioBen = _repository.Get(6);
        
        _repository.Delete(tioBen);

        var returnTioBen = _repository.Get(6);

        Assert.AreEqual(0, returnTioBen.Id);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void Dado_Id_Valido_Retornar_Cliente()
    {
        var stanLee = _repository.Get(2);

        Assert.IsNotNull(stanLee);
        Assert.AreEqual(2, stanLee.Id);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void Dado_Id_Invalido_Retornar_Vazio()
    {
        var lexLuthor = _repository.Get(7);

        Assert.AreEqual(0, lexLuthor.Id);
    }

    [TestMethod]
    [TestCategory("GetAll")]
    public void Retornar_Colecao_Clientes()
    {
        var clientes = _repository.GetAll();

        var peterParker = _repository.Get(1);
        var stanLee = _repository.Get(2);

        Assert.AreEqual(5, clientes.Count());
        Assert.IsTrue(clientes.Contains(peterParker));
        Assert.IsTrue(clientes.Contains(stanLee));
    }

    [TestMethod]
    [TestCategory("GetAllFilter")]
    public void Dado_Filtro_Retornar_Colecao_Clientes()
    {
        var clientes = _repository.GetAll(x => x.Email.Contains("@dc.com"));

        var bruceWayne = _repository.Get(4);
        var stanLee = _repository.Get(2);

        Assert.AreEqual(2, clientes.Count());
        Assert.IsTrue(clientes.Contains(bruceWayne));
        Assert.IsFalse(clientes.Contains(stanLee));
    }
}
