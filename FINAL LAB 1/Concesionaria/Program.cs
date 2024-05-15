using System;
using System.Collections.Generic;
using System.Linq;

namespace Concesionaria
{
    public class Venta
    {
        // propiedades 
        public DateTime Fecha { get; set; }
        public string Dueño { get; set; }
        public decimal PrecioVenta { get; set; }
        public override string ToString() =>
            $"Fecha: {Fecha.ToShortDateString()}, Dueño: {Dueño}, Precio: {PrecioVenta:C}";
    }

    public class Vehiculo
    {
        // definición de clases con sus propiedades
        public string Dominio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Antiguedad { get; set; }
        public string MesOaño { get; set; }
        public List<Venta> Ventas { get; set; } = new List<Venta>(); // lista con las ventas del vehículo

        public override string ToString() =>
            $"Dominio: {Dominio}, Marca: {Marca}, Modelo: {Modelo}, Antiguedad: {Antiguedad} {MesOaño}\n" +
            $"Ventas:\n{(Ventas.Any() ? string.Join("\n", Ventas) : "No hay ventas registradas.")}";
    }

    class Program
    {
        static List<Vehiculo> vehiculos = new List<Vehiculo>(); // lista vehículos concesionaria

        static void Main()
        {
            int opcion;
            do
            {
                MostrarMenu();
                opcion = int.Parse(Console.ReadLine());
                EjecutarOpcion(opcion);
            } while (opcion != 0); // bucle
        }

        // mostrar el menú 
        static void MostrarMenu()
        {
            Console.WriteLine("\r\n   __________  _   ___________________ ________  _   _____    ____  _______       _       ________   _____ ____  _   __\r\n  / ____/ __ \\/ | / / ____/ ____/ ___//  _/ __ \\/ | / /   |  / __ \\/  _/   |     | |     / /  _/ /  / ___// __ \\/ | / /\r\n / /   / / / /  |/ / /   / __/  \\__ \\ / // / / /  |/ / /| | / /_/ // // /| |     | | /| / // // /   \\__ \\/ / / /  |/ / \r\n/ /___/ /_/ / /|  / /___/ /___ ___/ // // /_/ / /|  / ___ |/ _, _// // ___ |     | |/ |/ // // /______/ / /_/ / /|  /  \r\n\\____/\\____/_/ |_/\\____/_____//____/___/\\____/_/ |_/_/  |_/_/ |_/___/_/  |_|     |__/|__/___/_____/____/\\____/_/ |_/   \r\n                                                                                                                       \r\n");
            Console.WriteLine("1) Cargar vehiculo");
            Console.WriteLine("2) Modificar vehiculo");
            Console.WriteLine("3) Borrar vehiculo");
            Console.WriteLine("4) Agregar venta");
            Console.WriteLine("5) Datos de un vehiculo");
            Console.WriteLine("6) Vehiculos");
            Console.WriteLine("7) Vehiculos por dueño");
            Console.WriteLine("8) Vehiculos por antiguedad");
            Console.WriteLine("9) Vehiculos ordenados por marca y modelo");
            Console.WriteLine("0) Salir");
            Console.Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄ Seleccionar opción: ");
        }

        // ejecutar la opción seleccionada . se llaman los metodos
        static void EjecutarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1: CargarVehiculo(); break;
                case 2: ModificarVehiculo(); break;
                case 3: BorrarVehiculo(); break;
                case 4: AgregarVenta(); break;
                case 5: MostrarDatosVehiculo(); break;
                case 6: ListaDeVehiculos(); break;
                case 7: MostrarVehiculosPorDueño(); break;
                case 8: MostrarVehiculosPorAntiguedad(); break;
                case 9: ListaDeVehiculosOrdenados(); break;
                case 0: Console.WriteLine("Fin"); break;
                default: Console.WriteLine("INVALIDO, INTENTAR OTRA VEZ"); break;
            }
        }

        // cargar un vehículo nuevo 
        static void CargarVehiculo()
        {
            var vehiculo = new Vehiculo
            {
                Dominio = LeerEntrada("Dominio: "),
                Marca = LeerEntrada("Marca: "),
                Modelo = LeerEntrada("Modelo: "),
                Antiguedad = int.Parse(LeerEntrada("Antiguedad: ")),
                MesOaño = LeerEntrada("Años/Meses: ")
            };
            vehiculos.Add(vehiculo);
            Console.WriteLine("VEHICULO CARGADO CON EXITO.");
        }

        // modificar los datos de un vehículo
        static void ModificarVehiculo()
        {
            var vehiculo = BuscarVehiculoPorDominio();
            if (vehiculo != null)
            {
                // modificar los datos con nuevos valores
                vehiculo.Marca = LeerEntrada("Marca: ");
                vehiculo.Modelo = LeerEntrada("Modelo: ");
                vehiculo.Antiguedad = int.Parse(LeerEntrada("Antiguedad: "));
                vehiculo.MesOaño = LeerEntrada("Años/Meses: ");
                Console.WriteLine("DATOS MODIFICADOS CON EXITO.");
            }
        }

        // borrar un vehículo de la lista
        static void BorrarVehiculo()
        {
            var vehiculo = BuscarVehiculoPorDominio();
            if (vehiculo != null) // si encuentra el vehículo
            {
                vehiculos.Remove(vehiculo);
                Console.WriteLine("VEHICULO BORRADO.");
            }
        }

        // agregar una venta a un vehículo
        static void AgregarVenta()
        {
            var vehiculo = BuscarVehiculoPorDominio();
            if (vehiculo != null)
            {
                // nueva venta con los datos que se ingresen
                var venta = new Venta
                {
                    Fecha = DateTime.Parse(LeerEntrada("Fecha de venta (dd-MM-yyyy): ")),
                    Dueño = LeerEntrada("Nombre del dueño: "),
                    PrecioVenta = decimal.Parse(LeerEntrada("Precio de venta: "))
                };
                vehiculo.Ventas.Add(venta); // se agrega la venta
                Console.WriteLine("VENTA AGREGADA CON EXITO");
            }
        }

        // mostrar los datos de un vehículo
        static void MostrarDatosVehiculo()
        {
            var vehiculo = BuscarVehiculoPorDominio();
            if (vehiculo != null)
            {
                Console.WriteLine(vehiculo);
            }
        }

        // mostrar la lista de vehículos
        static void ListaDeVehiculos()
        {
            vehiculos.ForEach(Console.WriteLine); // mostramos cada vehículo en la lista
            Console.WriteLine($"Cantidad de unidades: {vehiculos.Count}"); // mostramos la cantidad de vehículos
        }

        // mostrar los vehículos de un dueño específico
        static void MostrarVehiculosPorDueño()
        {
            var Dueño = LeerEntrada("Nombre del dueño: ");
            // aca filtramos los vehículos con el mismo dueño ingresado
            var VehiculosDueño = vehiculos.Where(v => v.Ventas.Any(venta => venta.Dueño == Dueño)).ToList();
            VehiculosDueño.ForEach(Console.WriteLine); // mostramos los vehiculos
        }

        // mostrar los vehículos por antigüedad
        static void MostrarVehiculosPorAntiguedad()
        {
            var antiguedad = int.Parse(LeerEntrada("Antiguedad: "));
            var MesOaño = LeerEntrada("Años/Meses: ");
            // filtramos vehículos por antigüedad
            var vehiculosAntiguedad = vehiculos.Where(v => v.Antiguedad == antiguedad && v.MesOaño == MesOaño).ToList();
            vehiculosAntiguedad.ForEach(Console.WriteLine); // vehículos que cumplen con el filtro y abajo la cantidad que lo hacen
            Console.WriteLine($"Vehiculos por antiguedad {antiguedad} {MesOaño}: {vehiculosAntiguedad.Count}");
        }

        // mostrar la lista de vehículos ordenados por marca y modelo
        static void ListaDeVehiculosOrdenados()
        {
            // vehículos por marca y luego por modelo
            var vehiculosOrdenados = vehiculos.OrderBy(v => v.Marca).ThenBy(v => v.Modelo).ToList();
            vehiculosOrdenados.ForEach(Console.WriteLine);
        }

        static string LeerEntrada(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }

        // buscar un vehículo por su dominio
        static Vehiculo BuscarVehiculoPorDominio()
        {
            var dominio = LeerEntrada("Dominio del vehiculo: ");
            var vehiculo = vehiculos.FirstOrDefault(v => v.Dominio == dominio);
            if (vehiculo == null) Console.WriteLine("VEHICULO NO ENCONTRADO.");
            return vehiculo; // si encuentra el vehiculo lo devuelve, caso contrario null
        }
    }
}