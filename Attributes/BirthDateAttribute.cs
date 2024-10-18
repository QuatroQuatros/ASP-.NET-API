using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.Attributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public BirthDateAttribute()
        {
            ErrorMessage = "A Data de Nascimento não pode ser no futuro.";
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime birthDate)
            {
                return birthDate <= DateTime.Now;
            }
            return false;
        }
    }
}