using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas
{
    internal class Producto
    {
        private string nombre;
        private decimal precio;
        private int cantidad;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }       
        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }   
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        // Constructor vacío
        public Producto()
        {
            Nombre = "";
            Precio = 0.0m;
            Cantidad = 0;
        }

        // Constructor con datos
        public Producto(string nombre, decimal precio, int cantidad)
        {
            this.Nombre = nombre;
            this.Precio = precio;
            this.Cantidad = cantidad;
        }

       
        public override string ToString()
        {
            return $"{Nombre} - Precio: {Precio:C} (Disponibles: {Cantidad})";
        }

        // Función nueva para rellenar inventario
        public void AgregarStock(int cantidadExtra)
        {
            if (cantidadExtra > 0)
            {
                Cantidad += cantidadExtra;
            }
        }
    }
}