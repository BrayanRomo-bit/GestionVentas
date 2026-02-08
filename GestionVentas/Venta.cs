using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GestionVentas
{
    internal class Venta
    {
        private Producto producto;
        private int cantidad;

        public Venta(Producto producto, int cantidad)
        {
            this.producto = producto;
            this.cantidad = cantidad;
        }

        public decimal CalcularTotal()
        {
         
            return producto.Precio * cantidad;
        }

        public bool Procesar()
        {

            producto.Cantidad += cantidad;
            return true;
        }

        public string ObtenerResumen()
        {
       
            var total = CalcularTotal();
            return "Total: " + Math.Truncate(total);
        }
        public  void ActualizarStock()
        {
            producto.Cantidad -= cantidad;
        }   
        public void RevertirStock()
        {
            producto.Cantidad += cantidad;
        }
    }
}
