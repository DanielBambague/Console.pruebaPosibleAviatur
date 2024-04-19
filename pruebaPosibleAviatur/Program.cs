using Newtonsoft.Json;
using posiblePruebaAviatur.Dominio.Service;
using posiblePruebaAviatur.Infraestructura.Entidades;
using System.Runtime.InteropServices;


//CAPA DE PRESENTACIÓN
namespace pruebaPosibleAviatur
{
	internal class Program
	{
		
		static void Main(string[] args)
		{
            ClienteEntity cl = new ClienteEntity();
            ClienteService _clienteService = new ClienteService();
            int op1;
            bool band = false;
            do
            {
                Console.WriteLine("***************ingrese una opcion***********************\n" +
                              "1. Crear Cliente\n" +
                              "2. Consultar Clientes\n" +
                              "3. Consultar Cliente Por ID\n" +
                              "4. Actualizar Cliente\n" +
                              "5. Eliminar Cliente\n" +
                              "6. consultar por genero\n" +
                              "7. Salir");
                string op = Console.ReadLine();
                int.TryParse(op, out op1);


                switch (op1)
                {
                    case 1:
                        _clienteService.Crear();
                        break;
                        band = true;
                    case 2:
                        List<ClienteEntity> lista=_clienteService.obtenerClientes();
                        string list = JsonConvert.SerializeObject(lista, Formatting.Indented);
                        Console.WriteLine(list);
                        break;
                        band = true;
                    case 3:
                        ClienteEntity cliente = _clienteService.ObtenerCliente();
                        string cliente1 = JsonConvert.SerializeObject(cliente, Formatting.Indented);
                        Console.WriteLine(cliente1);
                        break;
                        band = true;
                    case 4:
                        _clienteService.Actualizar();
                        break;
                        band = true;
                    case 5:
                        Console.WriteLine("Ingrese el id para eliminar");
                        int id=int.Parse(Console.ReadLine());
                        _clienteService.Eliminar(id);
                        break;
                        band = true;
                    case 6:
                        List<ClienteEntity> clienteGen = _clienteService.ObtenerClienteXGenero();
                        string cliente2 = JsonConvert.SerializeObject(clienteGen, Formatting.Indented);
                        Console.WriteLine(cliente2);
                        break;
                    case 7:
                        band = true;
                        continue;
                        break;


                    default:
                        Console.WriteLine("opcion no válida");
                        band = false;
                        break;

                }
            } while (!band);



            




        }
	}
}