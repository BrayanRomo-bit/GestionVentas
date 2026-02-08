using System;
using System.Windows.Forms;

namespace GestionVentas
{
    public partial class Form1 : Form
    {
        private Producto productoEjemplo;

        public Form1()
        {
            InitializeComponent();
           
            EnsureControlsInitialized();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Producto de ejemplo para mostrar precio y cantidad
            productoEjemplo = new Producto("Ejemplo", 9.99m, 100);


            EnsureControlsInitialized();

            // Mostrar precio en la etiqueta (formato monetario)
            if (lblPrecio != null)
            {       
                lblPrecio.Text = productoEjemplo.Precio.ToString("C");
            }

            // Ajustar máximo del selector de cantidad al stock disponible
            if (nudCantidad != null)
            {
                nudCantidad.Maximum = productoEjemplo.Cantidad;
            }
        }

        private void btnComprarProducto_Click(object sender, EventArgs e)
        {
            if (nudCantidad == null || productoEjemplo == null)
            {
                MessageBox.Show("La aplicación no está inicializada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int cantidad = (int)nudCantidad.Value;

            var venta = new Venta(productoEjemplo, cantidad);

            // Llamadas que expondrán los errores lógicos intencionales en la clase Venta
            var total = venta.CalcularTotal();
            venta.Procesar();

            MessageBox.Show("Total calculado: " + total.ToString("C"), "Compra");
            // Actualizar máximo por si el stock cambió (aunque la clase Venta contiene un error lógico)
            nudCantidad.Maximum = productoEjemplo.Cantidad;
        }

        private void EnsureControlsInitialized()
        {
            if (lblPrecio == null)
            {
                lblPrecio = new Label
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(12, 9),
                    Name = "lblPrecio",
                    Text = "Precio"
                };
                this.Controls.Add(lblPrecio);
            }

            if (nudCantidad == null)
            {
                nudCantidad = new NumericUpDown
                {
                    Location = new System.Drawing.Point(12, 30),
                    Name = "nudCantidad",
                    Minimum = 0,
                    Maximum = 100,
                };
                this.Controls.Add(nudCantidad);
            }

            if (btnComprarProducto == null)
            {
                btnComprarProducto = new Button
                {
                    Location = new System.Drawing.Point(12, 60),
                    Name = "btnComprarProducto",
                    Size = new System.Drawing.Size(100, 23),
                    Text = "Comprar"
                };
                btnComprarProducto.Click += btnComprarProducto_Click;
                this.Controls.Add(btnComprarProducto);
            }
        }
    }
}
