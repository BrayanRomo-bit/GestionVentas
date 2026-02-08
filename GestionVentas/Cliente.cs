using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas
{
    internal class Cliente: Persona
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
            set { saldo = value; }
        }   

        public Cliente() : base()
        {
            email = "";
            saldo = 0.0m;
        }       
        public Cliente(string nombre, string apellido, string email, decimal saldo) : base(nombre, apellido)
        {
            this.email = email;
            this.saldo = saldo;
        }
        public override string ToString()
        {
            return $"{Nombre} {Apellido} - Email: {email}, Saldo: {saldo:C}";
        }
        public bool RealizarCompra(decimal monto)
        {
            if (monto <= saldo)
            {
                saldo -= monto;
                return true;
            }
            return false; // No tiene suficiente saldo
        }
        public void AgregarSaldo(decimal monto)
        {
            saldo += monto;
        }


        }
}
