using System;
using System.Windows.Forms;

namespace GestionVentas
{
    public partial class Form1 : Form
    {
        private Producto productoEjemplo;
        private Button btnAdmin;
        private Button btnCliente;

        public Form1()
        {
            InitializeComponent();
            // Asegurar que los controles necesarios existen incluso si el diseñador fue modificado accidentalmente
            EnsureControlsInitialized();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Producto de ejemplo para mostrar precio y cantidad
            productoEjemplo = new Producto("Ejemplo", 9.99m, 100);

            // Asegurar controles antes de usarlos (defensiva si InitializeComponent no los creó)
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

            // Estado inicial: modo cliente activo
            SetModoCliente();
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
            if (lblPrecio != null)
            {
                lblPrecio.Text = productoEjemplo.Precio.ToString("C");
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (productoEjemplo == null)
            {
                MessageBox.Show("Producto no inicializado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var adminForm = new AdminForm(productoEjemplo))
            {
                // Cambiar ShowDialog(this) por ShowDialog() para evitar el error
                var result = adminForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Actualizar vista con los posibles cambios realizados por el admin
                    if (lblPrecio != null)
                    {
                        lblPrecio.Text = productoEjemplo.Precio.ToString("C");
                    }

                    if (nudCantidad != null)
                    {
                        nudCantidad.Maximum = productoEjemplo.Cantidad;
                        if (nudCantidad.Value > nudCantidad.Maximum)
                        {
                            nudCantidad.Value = nudCantidad.Maximum;
                        }
                    }
                }
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            SetModoCliente();
            MessageBox.Show("Modo cliente activado. Puedes comprar productos.", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetModoCliente()
        {
            // Habilitar controles de compra y deshabilitar modificaciones administrativas en la UI
            if (btnComprarProducto != null)
                btnComprarProducto.Enabled = true;
            if (nudCantidad != null)
                nudCantidad.Enabled = true;
            if (btnAdmin != null)
                btnAdmin.Enabled = true; // dejar habilitado para permitir alternar
        }

        /// <summary>
        /// Crea instancias mínimas de los controles que el formulario necesita
        /// cuando el archivo generado por el diseñador no los haya creado.
        /// Esta corrección es defensiva: lo ideal es restaurar el Form1.Designer.cs
        /// desde control de versiones o volver a añadir los controles desde el Diseñador.
        /// </summary>
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

            if (btnAdmin == null)
            {
                btnAdmin = new Button
                {
                    Location = new System.Drawing.Point(130, 60),
                    Name = "btnAdmin",
                    Size = new System.Drawing.Size(100, 23),
                    Text = "Admin"
                };
                btnAdmin.Click += btnAdmin_Click;
                this.Controls.Add(btnAdmin);
            }

            if (btnCliente == null)
            {
                btnCliente = new Button
                {
                    Location = new System.Drawing.Point(250, 60),
                    Name = "btnCliente",
                    Size = new System.Drawing.Size(100, 23),
                    Text = "Cliente"
                };
                btnCliente.Click += btnCliente_Click;
                this.Controls.Add(btnCliente);
            }
        }
    }
}
