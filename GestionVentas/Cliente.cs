using System;

namespace GestionVentas
{
    internal class Cliente : Persona
    {
        public string Email { get; set; }
        public decimal Saldo { get; private set; }

        public Cliente(string nombre, string apellido, string email, decimal saldoInicial) : base(nombre, apellido)
        {
            this.Email = email;
            this.Saldo = saldoInicial;
        }

        public void Depositar(decimal monto)
        {
            if (monto > 0) Saldo += monto;
        }

        // Esta función devuelve TRUE solo si alcanza el dinero
        public bool RealizarCompra(decimal monto)
        {
            if (Saldo >= monto) // Si el saldo es mayor o igual al monto
            {
                Saldo -= monto; // Descontamos
                return true;    // Éxito
            }
            return false;       // Fallo
        }

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - Saldo: {Saldo:C}";
        }

        // Función extra para compatibilidad si tu código la llama
        public void AgregarSaldo(decimal monto)
        {
            Depositar(monto);
        }
    }
}