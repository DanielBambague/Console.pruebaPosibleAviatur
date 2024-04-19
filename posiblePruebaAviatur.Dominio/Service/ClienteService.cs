


using Newtonsoft.Json;
using posiablePruebaAviatur.Comunes.Comunes;
using posiablePruebaAviatur.Comunes.Helpers;
using posiblePruebaAviatur.Infraestructura;
using posiblePruebaAviatur.Infraestructura.Entidades;
using System.Data;


namespace posiblePruebaAviatur.Dominio.Service
{
    public class ClienteService
    {

        public ClienteRepository _clienteRepository;
        public ClienteService()
        {
            _clienteRepository = new ClienteRepository();
        }

        public List<ClienteEntity> obtenerClientes()
        {
            List<ClienteEntity> listaClientes = new List<ClienteEntity>();

            //vamos a la capa de acceso a datos para traer la  informacion
            DataTable tabla = _clienteRepository.CrudCliente((int)OperacionesEnum
                .consultarTodo, new ClienteEntity());

            listaClientes = ParsearTableToJson.TableToList<ClienteEntity>(tabla);

            return listaClientes;
        }

        public void Crear()
        {
            int dia = 0;
            int mes = 0;
            int año = 0;
            string fecha = "";
            ClienteEntity cliente = new ClienteEntity();
            Console.WriteLine("Ingrese su nombre ");
            cliente.nombre = Console.ReadLine();
            Console.WriteLine("Ingrese su apellido ");
            cliente.apellido = Console.ReadLine();
            Console.WriteLine("Ingrese su direccion ");
            cliente.direccion = Console.ReadLine();
            do
            {
                Console.WriteLine("Ingrese su fecha de nacimiento dia/mes/año");
                fecha = Console.ReadLine();

            } while (fecha.Length < 10);
            cliente.fecha_nacimiento = fecha;
            Console.WriteLine("Con que genero se identifica?\n" +
                             "1. masculino\n" +
                             "2. femenino\n" +
                             "3. otro");
            string opc = Console.ReadLine();
            int opc1;
            int.TryParse(opc, out opc1);
            bool band = false;
            while (!band)
            {
                switch (opc1)
                {
                    case 1:
                        cliente.genero = "masculino";
                        band = true;
                        break;
                    case 2:
                        cliente.genero = "femenino";
                        break;
                        band = true;
                    case 3:
                        cliente.genero = "otro";
                        break;
                    default:
                        Console.WriteLine("la opcion no es válida");
                        band = false;
                        break;
                }
            }


            DataTable tabla = _clienteRepository.CrudCliente((int)OperacionesEnum
                 .crear, cliente);
            if (tabla.Rows.Count > 0)
            {

                ParsearTableToJson.TableToObject<ClienteEntity>(tabla);
                Console.WriteLine("El cliente fué creado satisfactoriamente");
            }
            else
            {
                throw new Exception("no se pudo crear el cliente");
            }


        }

        public void Actualizar()
        {

            ClienteEntity viejoCliente = ObtenerCliente();
            if (viejoCliente != null)
            {
                int dia = 0;
                int mes = 0;
                int año = 0;
                string fecha = "";
                ClienteEntity cliente = new ClienteEntity();
                Console.WriteLine("Ingrese su nombre ");
                cliente.nombre = Console.ReadLine();
                Console.WriteLine("Ingrese su apellido ");
                cliente.apellido = Console.ReadLine();
                Console.WriteLine("Ingrese su direccion ");
                cliente.direccion = Console.ReadLine();
                do
                {
                    Console.WriteLine("Ingrese su fecha de nacimiento dia/mes/año");
                    fecha = Console.ReadLine();

                } while (fecha.Length < 10);
                cliente.fecha_nacimiento = fecha;

                Console.WriteLine("Con que genero se identifica?\n" +
                             "1. masculino\n" +
                             "2. femenino\n" +
                             "3. otro");
                string opc = Console.ReadLine();
                int opc1;
                int.TryParse(opc, out opc1);
                bool band = false;
                while (!band)
                {
                    switch (opc1)
                    {
                        case 1:
                            cliente.genero = "masculino";
                            band = true;
                            break;
                        case 2:
                            cliente.genero = "femenino";
                            band = true;
                            break;
                        case 3:
                            cliente.genero = "otro";
                            band = true;
                            break;
                        default:
                            Console.WriteLine("la opcion no es válida");
                            band = false;
                            break;
                    }
                }

                viejoCliente.genero = cliente.genero;
                viejoCliente.nombre = cliente.nombre;
                viejoCliente.apellido = cliente.apellido;
                viejoCliente.direccion = cliente.direccion;
                viejoCliente.fecha_nacimiento = cliente.fecha_nacimiento;
                DataTable tabla = _clienteRepository.CrudCliente((int)OperacionesEnum.actualizar, viejoCliente);
                if (tabla.Rows.Count > 0)
                {
                    ParsearTableToJson.TableToObject<ClienteEntity>(tabla);
                    List<ClienteEntity> lista = obtenerClientes();
                    string list = JsonConvert.SerializeObject(lista, Formatting.Indented);
                    Console.WriteLine(list);
                }
                else
                {
                    throw new Exception("no se pudo actualizar");
                }
            }
            else
            {
                throw new Exception("el cliente no existe");
            }



        }

        public ClienteEntity ObtenerCliente()
        {
            Console.WriteLine("Ingrese el id que quiere consultar, actualizar o confirmar ELIMINACIÓn\n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                // string genero= Console.ReadLine();
                DataTable tabla;
                foreach (ClienteEntity cliente in obtenerClientes())
                {
                    
                        if (cliente.id == id)
                        {
                            tabla = _clienteRepository.CrudCliente((int)OperacionesEnum.consultarPorId, cliente);
                            ClienteEntity cliente1 = ParsearTableToJson.TableToObject<ClienteEntity>(tabla);
                            return cliente1;
                        }
                        
                 }
                return new ClienteEntity { 
                
                    nombre="no existe el cliente con id " + id
                };
            }
            catch (Exception e)
            {

                throw new Exception("ingrese una pocion válida" + e.Message);
            }
            
        }

        public bool Eliminar(int id)
        {
            ClienteEntity viejoCliente = ObtenerCliente();

            if (viejoCliente != null)
            {

                DataTable tabla = _clienteRepository.CrudCliente((int)OperacionesEnum.eliminar, viejoCliente);
                if (tabla.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("no se pudo eliminar");
                }
            }
            else
            {
                throw new Exception("el cliente no existe");
            }

        }

        public List<ClienteEntity> ObtenerClienteXGenero()
        {
            Console.WriteLine("Escriba el género para filtrar la información");

            string genero = Console.ReadLine();
            DataTable tabla;
            foreach (ClienteEntity cliente in obtenerClientes())
            {
                if (cliente.genero == genero)
                {
                    cliente.genero = genero;
                    tabla = _clienteRepository.CrudCliente((int)OperacionesEnum.consultarPorGenero, cliente);
                    List<ClienteEntity> cliente1 = ParsearTableToJson.TableToList<ClienteEntity>(tabla);
                    return cliente1;
                }
                
            }
            return new List<ClienteEntity> { 
                    new ClienteEntity
                    {
                        nombre= "no existen datos"
                    }
            };
        }




    }
}
