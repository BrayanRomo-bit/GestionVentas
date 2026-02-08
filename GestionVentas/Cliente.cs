using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas
{
    internal class Cliente : Persona
    {
        private string email;   
        private decimal saldo;          

        public string Email
        {   
            get { return email; }   
            set { email = value; }
        }
        public decimal Saldo
        {
            get { return saldo; }
            private set { saldo = value; } // El saldo solo se puede modificar desde dentro de la clase
        }

        public Cliente(string nombre, string apellido, string email, decimal saldoInicial) : base(nombre, apellido)
        {
            this.Email = email;
            this.Saldo = saldoInicial;
        }

        // Función para meter dinero a la cuenta
        public void Depositar(decimal monto)
        {
            if (monto > 0)
            {
                Saldo += monto;
            }
        }

        // Función para intentar cobrar
        // Devuelve TRUE si se pudo cobrar, FALSE si no le alcanzó
        public bool IntentarCobrar(decimal monto)
        {
            if (Saldo >= monto)
            {
                Saldo -= monto; // Restamos el dinero
                return true;    // Éxito
            }
            return false;       // Fallo (no tiene saldo suficiente)
        }

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - Saldo: {Saldo:C}";
        }
    }
}