using System;
using System.Collections.Generic;

namespace CrudGastos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GastoRepositorio repo = new GastoRepositorio();
            string opcion;

            do
            {
                opcion = LeerOpcionMenu();

                
                try
                {
                    switch (opcion)
                    {
                        case "1":
                            RegistrarGasto(repo);
                            break;
                        case "2":
                            ListarGastos(repo);
                            break;
                        case "3":
                            BuscarGastoPorId(repo);
                            break;
                        case "4":
                            ActualizarMonto(repo);
                            break;
                        case "5":
                            EliminarGasto(repo);
                            break;
                        case "6":
                            Console.WriteLine("Saliendo del programa...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ejecutar la operación: {ex.Message}");
                }

            } while (opcion != "6");
        }

        private static string LeerOpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== SISTEMA DE CONTROL DE GASTOS (CRUD) ===");
            Console.WriteLine("1. Registrar gasto");
            Console.WriteLine("2. Listar gastos");
            Console.WriteLine("3. Buscar gasto por Id");
            Console.WriteLine("4. Actualizar monto");
            Console.WriteLine("5. Eliminar gasto");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");
            return Console.ReadLine();
        }

        private static void RegistrarGasto(GastoRepositorio repo)
        {
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine().Trim();

            Console.Write("Monto: ");
            bool montoOk = decimal.TryParse(Console.ReadLine(), out decimal monto);

            Console.Write("Categoría: ");
            string categoria = Console.ReadLine().Trim();

            if (!montoOk || monto <= 0 || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            Gasto nuevoGasto = new Gasto
            {
                Descripcion = descripcion,
                Monto = monto,
                Categoria = categoria
            };

            repo.Insertar(nuevoGasto);
            Console.WriteLine("Gasto registrado.");
        }

        private static void ListarGastos(GastoRepositorio repo)
        {
            List<Gasto> lista = repo.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
                return;
            }

            decimal totalGeneral = 0;

            foreach (Gasto g in lista)
            {
                Console.WriteLine(g);
                totalGeneral += g.Monto;
            }

            Console.WriteLine($"TOTAL GASTADO: Q {totalGeneral:N2}");
        }

        private static void BuscarGastoPorId(GastoRepositorio repo)
        {
            Console.Write("Ingrese el Id a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            Gasto g = repo.BuscarPorId(id);
            if (g != null)
            {
                Console.WriteLine(g);
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }

        private static void ActualizarMonto(GastoRepositorio repo)
        {
            Console.Write("Ingrese el Id del gasto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            Gasto gastoExistente = repo.BuscarPorId(id);
            if (gastoExistente == null)
            {
                Console.WriteLine("No existe ese Id.");
                return;
            }

            Console.WriteLine($"Gasto actual: {gastoExistente}");

            Console.Write("Ingrese el nuevo monto: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoMonto) || nuevoMonto <= 0)
            {
                Console.WriteLine("Monto inválido.");
                return;
            }

            gastoExistente.Monto = nuevoMonto;
            repo.Actualizar(gastoExistente);
            Console.WriteLine("Gasto actualizado.");
        }

        private static void EliminarGasto(GastoRepositorio repo)
        {
            Console.Write("Ingrese el Id del gasto a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            if (repo.Eliminar(id))
            {
                Console.WriteLine("Gasto eliminado.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }
    }
}