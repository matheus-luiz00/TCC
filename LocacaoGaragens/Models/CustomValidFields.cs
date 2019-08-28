using LocacaoGaragens.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class CustomValidFields : ValidationAttribute
    {
        ContextDB db = new ContextDB();

        private ValidFields typeField;

        public CustomValidFields(ValidFields type)
        {
            typeField = type;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            switch(typeField)
            {
                case ValidFields.ValidarEmail:
                    {
                        return ValidaEmail(value);
                    }

                case ValidFields.ValidarPlaca:
                    {
                        return ValidaPlaca(value);
                    }
            }


            return new ValidationResult($"O campo {validationContext.DisplayName} é obrigatório");
        }

        private ValidationResult ValidaPlaca(object value)
        {
            value.ToString().Trim('-');
            bool placaMSul = Regex.IsMatch(value.ToString(), @"^[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$");
            
            bool placaBrasil = Regex.IsMatch(value.ToString(), @"^[a-zA-Z]{3}[0-9]{4}$");
            
            bool placaMoto = Regex.IsMatch(value.ToString(), @"^[a-zA-Z]{3}[0-9]{2}[a-zA-Z]{1}[0-9]{1}$");

            if (placaMSul || placaBrasil || placaMoto)
            {
                var existe = db.locacoes.First(x => x.Placa == value.ToString());
                if (existe == null)
                    return ValidationResult.Success;

                return new ValidationResult("A placa já existe");
            }

            return new ValidationResult("Placa Invalida");
        }

        private ValidationResult ValidaEmail(object value)
        {
            bool result = Regex.IsMatch(value.ToString(), @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (result)
                return ValidationResult.Success;

            return new ValidationResult("Email Invalido");
        }
        
    }
}