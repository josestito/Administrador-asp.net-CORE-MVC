﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_De_Titulo_Organizado.Validaciones
{
    public class ValidarRutChilenoAtributte : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {

                var rut = value.ToString().Trim().ToUpper();
                if (rut.Contains("."))
                {
                    // Realizar alguna acción si el RUT contiene un punto
                    // Por ejemplo, lanzar una excepción o devolver un mensaje de error
                    return new ValidationResult("El RUT no puede contener puntos.");
                }
                if (rut.Contains("-"))
                {
                    // Realizar alguna acción si el RUT contiene un punto
                    // Por ejemplo, lanzar una excepción o devolver un mensaje de error
                    return new ValidationResult("El RUT no puede contener guion.");
                }
                rut = rut.Replace(".", "").Replace("-", "");

                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));
                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }

                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    return ValidationResult.Success; // La validación es exitosa si el RUT es válido
                }
                else
                {
                    return new ValidationResult("El RUT no es válido."); // La validación falla si el RUT no es válido
                }
            }
            catch (Exception)
            {
                return new ValidationResult("Error al validar el RUT."); // La validación falla si ocurre algún error durante el proceso
            }
        }
    }

    public class ValidarSoloCaracterAtributte : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();
                if (!string.IsNullOrEmpty(stringValue))
                {
                    foreach (char c in stringValue)
                    {
                        if (!char.IsLetter(c))
                        {
                            return new ValidationResult("El campo solo admite letras");
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
    }

}


