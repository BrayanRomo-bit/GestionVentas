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
        public Producto ()
        {
            nombre = "";
            precio = 0.0m;
            cantidad = 0;
        }
        public Producto(string nombre, decimal precio, int cantidad)
        {
            this.nombre = nombre;
            this.precio = precio;
            this.cantidad = cantidad;
        } 
        
        public override string ToString()
        {
            return $"{nombre} - Precio: {precio:C}, Cantidad: {cantidad}";
        }
    }
}
