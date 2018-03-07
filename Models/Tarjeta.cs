using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace CreditoWeb.Models
{
    public class Tarjeta
    {
        [Required(ErrorMessage = "El número de tarjeta es necesario.")]
        //[CreditCard]
        public string TarjetaNum { get; set; }
        public TipoTarjeta TipoTarjeta { get; set; }

        public bool Valida { get; set; }
     
        public Tarjeta(string tarjetaNum)
        {
            this.TarjetaNum = tarjetaNum;
            Valida = esValida();
            TipoTarjeta = tipoDeTarjeta();            
        }


        /// Basado en el algoritmo de Luhn determinar si esta tarjeta es valida
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private bool esValida()
        {
            StringBuilder digitsOnly = new StringBuilder();
            foreach(var c in TarjetaNum){
                
                if (Char.IsDigit(c)) digitsOnly.Append(c);
            }

            if (digitsOnly.Length > 18 || digitsOnly.Length < 15) return false;

            int suma = 0;
            int numero = 0;
            int añadir = 0;
            bool Patito = false;

            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                numero = Int32.Parse(digitsOnly.ToString(i, 1));
                if (Patito)
                {
                    añadir = numero * 2;
                    if (añadir > 9)
                    {
                        añadir -= 9;
                    }
                }
                else
                {
                    añadir = numero;
                }
                suma += añadir;
                Patito = !Patito;
            }
            return (suma % 10) == 0;

        }


        /// Si la tarjeta es valida determinar de cuál tipo es VISA, MASTERCARD, AMERICANEXPRESS
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private TipoTarjeta tipoDeTarjeta()
        {
            var opc=TipoTarjeta.NOVALIDA;
            if((TarjetaNum[0]=='3'|| TarjetaNum[1]=='4')||(TarjetaNum[0]=='3'|| TarjetaNum[1]=='7')){
                opc=TipoTarjeta.AMERICANEXPRESS;
            }
            if((TarjetaNum[0]=='5'|| TarjetaNum[1]=='1')||(TarjetaNum[0]=='5'||TarjetaNum[1]=='2')||(TarjetaNum[0]=='5'|| TarjetaNum[1]=='3')||(TarjetaNum[0]=='5'||TarjetaNum[1]=='4')||(TarjetaNum[0]=='5'|| TarjetaNum[1]=='5')){
                opc=TipoTarjeta.MASTERCARD;
            }
            if((TarjetaNum[0]=='4')){
                opc=TipoTarjeta.VISA;
            }
            return opc;
        
        }



    }

    public enum TipoTarjeta
    {
        VISA,
        MASTERCARD,
        AMERICANEXPRESS,
        NOVALIDA


    }
}