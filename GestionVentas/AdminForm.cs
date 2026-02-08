using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionVentas
{
    internal class AdminForm : Form
    {
        private readonly Producto producto;

        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblPrecio;
        private NumericUpDown nudPrecio;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
        private Button btnGuardar;
        private Button btnCancelar;

        public AdminForm(Producto producto)
        {
            this.producto = producto ?? throw new ArgumentNullException(nameof(producto));
            Text = "Administrar producto";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(360, 170);

            InitializeControls();
            LoadProductoEnFormulario();
        }

        private void InitializeControls()
        {
            lblNombre = new Label
            {
                AutoSize = true,
                Location = new Point(12, 15),
                Name = "lblNombre",
                Text = "Nombre:"
            };
            Controls.Add(lblNombre);

            txtNombre = new TextBox
            {
                Location = new Point(90, 12),
                Name = "txtNombre",
                Size = new Size(250, 20)
            };
            Controls.Add(txtNombre);

            lblPrecio = new Label
            {
                AutoSize = true,
                Location = new Point(12, 45),
                Name = "lblPrecio",
                Text = "Precio:"
            };
            Controls.Add(lblPrecio);

            nudPrecio = new NumericUpDown
            {
                Location = new Point(90, 42),
                Name = "nudPrecio",
                Size = new Size(120, 20),
                DecimalPlaces = 2,
                Increment = 0.01m,
                Minimum = 0,
                Maximum = 1000000m
            };
            Controls.Add(nudPrecio);

            lblCantidad = new Label
            {
                AutoSize = true,
                Location = new Point(12, 75),
                Name = "lblCantidad",
                Text = "Cantidad:"
            };
            Controls.Add(lblCantidad);

            nudCantidad = new NumericUpDown
            {
                Location = new Point(90, 72),
                Name = "nudCantidad",
                Size = new Size(120, 20),
                Minimum = 0,
                Maximum = 1000000,
                DecimalPlaces = 0,
                Increment = 1
            };
            Controls.Add(nudCantidad);

            btnGuardar = new Button
            {
                Location = new Point(90, 110),
                Name = "btnGuardar",
                Size = new Size(100, 25),
                Text = "Guardar"
            };
            btnGuardar.Click += BtnGuardar_Click;
            Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
                Location = new Point(200, 110),
                Name = "btnCancelar",
                Size = new Size(100, 25),
                Text = "Cancelar"
            };
            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(btnCancelar);

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        private void LoadProductoEnFormulario()
        {
            txtNombre.Text = producto.Nombre ?? string.Empty;
            nudPrecio.Value = ClampDecimal(producto.Precio, nudPrecio.Minimum, nudPrecio.Maximum);
            nudCantidad.Value = ClampDecimal(producto.Cantidad, nudCantidad.Minimum, nudCantidad.Maximum);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones sencillas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (nudPrecio.Value < 0)
            {
                MessageBox.Show("El precio debe ser mayor o igual a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrecio.Focus();
                return;
            }

            if (nudCantidad.Value < 0)
            {
                MessageBox.Show("La cantidad debe ser mayor o igual a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCantidad.Focus();
                return;
            }

            // Aplicar cambios al producto compartido
            producto.Nombre = txtNombre.Text.Trim();
            producto.Precio = nudPrecio.Value;
            producto.Cantidad = (int)nudCantidad.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private decimal ClampDecimal(decimal value, decimal min, decimal max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}