using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GestionVentas
{
    public partial class Form1 : Form
    {
        // --- DATOS ---
        private List<Producto> listaProductos;
        private Cliente miCliente;

        // --- CONTROLES VISUALES ---
        private Panel panelAdmin;
        private Panel panelCliente;
        private ListBox listBoxGeneral;

        // Botones del Menú Principal
        private Button btnEntrarAdmin;
        private Button btnEntrarCliente;
        private Label lblTitulo;

        // Controles de ADMIN
        private TextBox txtNuevoNombre;
        private NumericUpDown nudNuevoPrecio;
        private NumericUpDown nudNuevoStock;
        private Button btnCrearProducto;
        private Button btnReponerStock;
        private NumericUpDown nudCantidadReponer;

        // Controles de CLIENTE
        private Label lblSaldoCliente;
        private TextBox txtDeposito;
        private Button btnDepositar;
        private NumericUpDown nudCantidadComprar;
        private Button btnComprar;

        public Form1()
        {
            InitializeComponent();
            ConfigurarDatos();
            CrearInterfaz();
            MostrarMenuPrincipal();
        }

        // AGREGADO: Esto soluciona el error CS1061 de la lista de errores
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ConfigurarDatos()
        {
            listaProductos = new List<Producto>();

            // CORREGIDO: Ahora pasamos los 4 datos que pide tu clase Cliente
            // (Nombre, Apellido, Email, Saldo)
            miCliente = new Cliente("Usuario", "Prueba", "correo@ejemplo.com", 500.00m);
        }

        private void CrearInterfaz()
        {
            if (this.Width < 650) this.Size = new Size(650, 500); // Un poco más ancho
            this.Text = "Sistema de Ventas";

            // --- MENU PRINCIPAL ---
            lblTitulo = new Label() { Text = "SELECCIONA TU ROL:", Location = new Point(20, 10), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            this.Controls.Add(lblTitulo);

            btnEntrarAdmin = new Button() { Text = "Soy ADMIN", Location = new Point(20, 40), Width = 120, BackColor = Color.LightSalmon };
            btnEntrarAdmin.Click += (s, e) => MostrarPanelAdmin();
            this.Controls.Add(btnEntrarAdmin);

            btnEntrarCliente = new Button() { Text = "Soy CLIENTE", Location = new Point(150, 40), Width = 120, BackColor = Color.LightBlue };
            btnEntrarCliente.Click += (s, e) => MostrarPanelCliente();
            this.Controls.Add(btnEntrarCliente);

            // --- LISTA DE PRODUCTOS ---
            listBoxGeneral = new ListBox() { Location = new Point(320, 40), Size = new Size(280, 400) };
            this.Controls.Add(listBoxGeneral);

            // --- PANEL ADMIN ---
            panelAdmin = new Panel() { Location = new Point(20, 80), Size = new Size(280, 350), BorderStyle = BorderStyle.FixedSingle, Visible = false };
            this.Controls.Add(panelAdmin);

            Label lblAdminTitle = new Label() { Text = "--- GESTIÓN ADMIN ---", Location = new Point(10, 10), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };
            panelAdmin.Controls.Add(lblAdminTitle);

            // === AQUI CORREGÍ EL ESPACIO (X=100 en lugar de 80) ===

            // Crear Producto
            panelAdmin.Controls.Add(new Label() { Text = "Nombre:", Location = new Point(10, 45), AutoSize = true });
            txtNuevoNombre = new TextBox() { Location = new Point(100, 42), Width = 140 }; // Movido a la derecha
            panelAdmin.Controls.Add(txtNuevoNombre);

            panelAdmin.Controls.Add(new Label() { Text = "Precio:", Location = new Point(10, 75), AutoSize = true });
            nudNuevoPrecio = new NumericUpDown() { Location = new Point(100, 72), DecimalPlaces = 2, Maximum = 10000, Width = 140 }; // Movido a la derecha
            panelAdmin.Controls.Add(nudNuevoPrecio);

            panelAdmin.Controls.Add(new Label() { Text = "Stock Inicial:", Location = new Point(10, 105), AutoSize = true });
            nudNuevoStock = new NumericUpDown() { Location = new Point(100, 102), Maximum = 1000, Width = 140 }; // Movido a la derecha
            panelAdmin.Controls.Add(nudNuevoStock);

            btnCrearProducto = new Button() { Text = "Crear Producto", Location = new Point(10, 140), Width = 230, Height = 30 };
            btnCrearProducto.Click += BtnCrearProducto_Click;
            panelAdmin.Controls.Add(btnCrearProducto);

            // Reponer Stock
            panelAdmin.Controls.Add(new Label() { Text = "Selecciona y suma stock:", Location = new Point(10, 190), AutoSize = true });

            nudCantidadReponer = new NumericUpDown() { Location = new Point(10, 215), Width = 80 };
            panelAdmin.Controls.Add(nudCantidadReponer);

            btnReponerStock = new Button() { Text = "Sumar Stock", Location = new Point(100, 213), Width = 140 };
            btnReponerStock.Click += BtnReponerStock_Click;
            panelAdmin.Controls.Add(btnReponerStock);


            // --- PANEL CLIENTE ---
            panelCliente = new Panel() { Location = new Point(20, 80), Size = new Size(280, 350), BorderStyle = BorderStyle.FixedSingle, Visible = false };
            this.Controls.Add(panelCliente);

            lblSaldoCliente = new Label() { Text = "Tu Saldo: $0.00", Location = new Point(10, 10), Font = new Font("Arial", 10, FontStyle.Bold) };
            panelCliente.Controls.Add(lblSaldoCliente);

            // Depositar
            panelCliente.Controls.Add(new Label() { Text = "Depositar $:", Location = new Point(10, 50), AutoSize = true });
            txtDeposito = new TextBox() { Location = new Point(90, 48), Width = 80 };
            panelCliente.Controls.Add(txtDeposito);

            btnDepositar = new Button() { Text = "Ok", Location = new Point(180, 46), Width = 50 };
            btnDepositar.Click += BtnDepositar_Click;
            panelCliente.Controls.Add(btnDepositar);

            // Comprar
            panelCliente.Controls.Add(new Label() { Text = "Cantidad a Comprar:", Location = new Point(10, 110), AutoSize = true });
            nudCantidadComprar = new NumericUpDown() { Location = new Point(10, 130), Minimum = 1, Width = 100 };
            panelCliente.Controls.Add(nudCantidadComprar);

            btnComprar = new Button() { Text = "COMPRAR SELECCIONADO", Location = new Point(10, 170), Width = 230, Height = 40, BackColor = Color.LightGreen };
            btnComprar.Click += BtnComprar_Click;
            panelCliente.Controls.Add(btnComprar);
        }

        private void MostrarMenuPrincipal()
        {
            panelAdmin.Visible = false;
            panelCliente.Visible = false;
            ActualizarListaVisual();
        }

        private void MostrarPanelAdmin()
        {
            panelAdmin.Visible = true;
            panelCliente.Visible = false;
            lblTitulo.Text = "MODO: ADMINISTRADOR";
            ActualizarListaVisual();
        }

        private void MostrarPanelCliente()
        {
            panelAdmin.Visible = false;
            panelCliente.Visible = true;
            lblTitulo.Text = "MODO: CLIENTE";
            ActualizarListaVisual();
        }

        private void ActualizarListaVisual()
        {
            int index = listBoxGeneral.SelectedIndex;
            listBoxGeneral.Items.Clear();
            foreach (Producto p in listaProductos)
            {
                // CORREGIDO: Usamos p.Cantidad en vez de p.Stock
                listBoxGeneral.Items.Add($"{p.Nombre} - ${p.Precio} (Disp: {p.Cantidad})");
            }

            if (index >= 0 && index < listBoxGeneral.Items.Count)
                listBoxGeneral.SelectedIndex = index;

            lblSaldoCliente.Text = $"Tu Saldo: {miCliente.Saldo:C}";
        }

        // --- LÓGICA ---

        private void BtnCrearProducto_Click(object sender, EventArgs e)
        {
            string nombre = txtNuevoNombre.Text;
            decimal precio = nudNuevoPrecio.Value;
            int cantidad = (int)nudNuevoStock.Value;

            if (string.IsNullOrWhiteSpace(nombre) || precio <= 0)
            {
                MessageBox.Show("Datos inválidos.");
                return;
            }

            Producto nuevo = new Producto(nombre, precio, cantidad);
            listaProductos.Add(nuevo);

            MessageBox.Show("Producto Creado.");
            ActualizarListaVisual();

            txtNuevoNombre.Clear(); nudNuevoPrecio.Value = 0; nudNuevoStock.Value = 0;
        }

        private void BtnReponerStock_Click(object sender, EventArgs e)
        {
            if (listBoxGeneral.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un producto.");
                return;
            }

            Producto p = listaProductos[listBoxGeneral.SelectedIndex];
            int cantidad = (int)nudCantidadReponer.Value;

            if (cantidad > 0)
            {
                // CORREGIDO: Usamos Cantidad en vez de Stock
                p.Cantidad += cantidad;
                MessageBox.Show($"Stock actualizado. Ahora hay {p.Cantidad}.");
                ActualizarListaVisual();
            }
        }

        private void BtnDepositar_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtDeposito.Text, out decimal monto))
            {
                miCliente.Depositar(monto);
                MessageBox.Show($"Depositaste {monto:C}");
                txtDeposito.Clear();
                ActualizarListaVisual();
            }
        }

        private void BtnComprar_Click(object sender, EventArgs e)
        {
            if (listBoxGeneral.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un producto.");
                return;
            }

            Producto p = listaProductos[listBoxGeneral.SelectedIndex];
            int cantidad = (int)nudCantidadComprar.Value;

            // CORREGIDO: Usamos Cantidad
            if (cantidad > p.Cantidad)
            {
                MessageBox.Show($"ALERTA: Stock insuficiente. Quedan {p.Cantidad}.");
                return;
            }

            decimal total = p.Precio * cantidad;

            DialogResult respuesta = MessageBox.Show(
                $"Producto: {p.Nombre}\nTotal: {total:C}\n¿Confirmar?",
                "Confirmar", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                // CORREGIDO: Usamos RealizarCompra
                if (miCliente.IntentarCobrar(total))
                {
                    p.Cantidad -= cantidad;
                    MessageBox.Show("¡Compra exitosa!");
                    ActualizarListaVisual();
                }
                else
                {
                    MessageBox.Show($"Saldo insuficiente. Te faltan {total - miCliente.Saldo:C}");
                }
            }
        }
    }
}