using System;
using System.Net.Mail;

namespace DAES.Infrastructure
{
    public static class ExtensionesString
    {
        public static string ToUpperNull(this string texto)
        {
            if (IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }
            else
            {
                return texto.ToUpper();
            }
        }

        public static bool IsBoolean(this string texto)
        {
            bool valor;
            return bool.TryParse(texto, out valor);
        }

        public static bool IsInt(this string texto)
        {
            int valor;
            return int.TryParse(texto, out valor);
        }

        public static bool IsDecimal(this string texto)
        {
            decimal valor;
            return decimal.TryParse(texto, out valor);
        }

        public static bool IsDouble(this string texto)
        {
            double valor;
            return double.TryParse(texto, out valor);
        }

        public static bool IsFloat(this string texto)
        {
            float valor;
            return float.TryParse(texto, out valor);
        }

        public static bool IsDateTime(this string texto)
        {
            DateTime valor;
            return DateTime.TryParse(texto, out valor);
        }

        public static bool IsHour(this string texto)
        {

            var arregloHora = texto.Split(':');
            if (arregloHora.Length != 2)
            {
                return false;
            }

            int valorInt;
            if (!int.TryParse(arregloHora[0], out valorInt))
            {
                return false;
            }
            if (!int.TryParse(arregloHora[1], out valorInt))
            {
                return false;
            }

            DateTime valor;
            return DateTime.TryParse(texto, out valor);
        }

        public static bool IsRut(this string texto)
        {
            if (texto == null)
            {
                return false;
            }

            int parteNumeral;
            texto = texto.Insert(texto.Length - 1, "-");
            var arregloRut = texto.Split('-');
            if (arregloRut.Length != 2)
            {
                return false;
            }

            if (!int.TryParse(arregloRut[0], out parteNumeral))
            {
                return false;
            }

            var digitoVerificador = arregloRut[1].ToUpper();

            var contador = 2;
            var acumulador = 0;
            while (parteNumeral != 0)
            {
                var multiplo = parteNumeral % 10 * contador;
                acumulador = acumulador + multiplo;
                parteNumeral = parteNumeral / 10;
                contador = contador + 1;
                if (contador == 8)
                {
                    contador = 2;
                }
            }
            var digito = 11 - acumulador % 11;
            var rutDigito = digito.ToString().Trim();
            if (digito == 10)
            {
                rutDigito = "K";
            }

            if (digito == 11)
            {
                rutDigito = "0";
            }

            return rutDigito == digitoVerificador;
        }

        public static bool IsEmail(this string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsUrl(this string texto)
        {
            Uri uri;
            return Uri.TryCreate(texto, UriKind.Absolute, out uri);
        }

        public static bool IsNullOrWhiteSpace(this string texto)
        {
            return string.IsNullOrWhiteSpace(texto);
        }

        public static bool IsGuId(this string texto)
        {
            Guid valor;
            return Guid.TryParse(texto, out valor);
        }

        public static int ToInt(this string texto)
        {
            int valor;
            int.TryParse(texto, out valor);
            return valor;
        }
    }
}