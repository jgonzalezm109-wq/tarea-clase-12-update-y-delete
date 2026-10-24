using System.Collections.Generic;

namespace CrudGastos
{
    public class GastoRepositorio
    {
        private static List<Gasto> listaGastos = new List<Gasto>();
        private static int siguienteId = 1;

       
        static GastoRepositorio()
        {
            listaGastos.Add(new Gasto { Id = siguienteId++, Descripcion = "Cuaderno", Monto = 20.50m, Categoria = "Estudios" });
            listaGastos.Add(new Gasto { Id = siguienteId++, Descripcion = "Gasolina", Monto = 250.00m, Categoria = "Transporte" });
            listaGastos.Add(new Gasto { Id = siguienteId++, Descripcion = "Cena", Monto = 100.00m, Categoria = "Comida" });
        }

        public void Insertar(Gasto g)
        {
            g.Id = siguienteId++;
            listaGastos.Add(g);
        }

        public List<Gasto> ListarTodos()
        {
            return listaGastos;
        }

       
        public Gasto BuscarPorId(int id)
        {
            foreach (Gasto g in listaGastos)
            {
                if (g.Id == id)
                {
                    return g;
                }
            }
            return null;
        }

        
        public bool Actualizar(Gasto gastoActualizado)
        {
            Gasto g = BuscarPorId(gastoActualizado.Id);
            if (g != null)
            {
                g.Descripcion = gastoActualizado.Descripcion;
                g.Monto = gastoActualizado.Monto;
                g.Categoria = gastoActualizado.Categoria;
                return true;
            }
            return false;
        }

       
        public bool Eliminar(int id)
        {
            Gasto g = BuscarPorId(id);
            if (g != null)
            {
                listaGastos.Remove(g);
                return true;
            }
            return false;
        }
    }
}