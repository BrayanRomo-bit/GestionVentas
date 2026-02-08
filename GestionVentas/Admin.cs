using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas
{
    internal class Admin: Persona
    {
        private string usuario; 
        private List<Producto> productos;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public Admin() : base()
        {
            usuario = "";
        }     
        public Admin(string nombre, string apellido, string usuario) : base(nombre, apellido)
        {
            this.usuario = usuario;
         
        }
         public override string ToString()
         {
             return $"{Nombre} {Apellido} - Usuario: {usuario}";
        }

        public void AgregarProducto(List<Producto> productos, Producto nuevoProducto)
        {
            productos.Add(nuevoProducto);
        }   
        public void EliminarProducto(List<Producto> productos, Producto productoAEliminar)
            {
                productos.Remove(productoAEliminar);
        }


    }
}
