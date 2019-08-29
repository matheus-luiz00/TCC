using LocacaoGaragens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LocacaoGaragens.Utils
{
    public class Validacoes
    {
        /// <summary>
        /// Valida se uma string contendo uma placa está em um formato válido e se esta placa já existe no banco
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="db"></param>
        /// <returns>Retorna false se a placa não está em um formato válido ou se ela já existe no banco</returns>
        public bool ValidarPlacaUnique(string placaIn, ContextDB db)
        {
            if (string.IsNullOrEmpty(placaIn))
                return false;

            string placa = placaIn.Replace("-", "");
            bool placaMSul = Regex.IsMatch(placa, @"^[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$");

            bool placaBrasil = Regex.IsMatch(placa, @"^[a-zA-Z]{3}[0-9]{4}$");

            bool placaMoto = Regex.IsMatch(placa, @"^[a-zA-Z]{3}[0-9]{2}[a-zA-Z]{1}[0-9]{1}$");

            //Caso seja uma placa valida
            if (placaMSul || placaBrasil || placaMoto)
            {
                //caso a placa seja nao cadastrada no banco
                var existe = db.locacoes.FirstOrDefault(x => x.Placa == placa);
                if (existe == null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CodigoMarcaExists(int codigoMarca, ContextDB db)
        {
            if (db.Marcas.FirstOrDefault(x => x.Codigo == codigoMarca)  != null)
                return true;

            return false;
        }

        public bool ModeloExists(int id, ContextDB db)
        {
            if (db.Modelos.FirstOrDefault(x => x.Id == id) != null)
                return true;

            return false;
        }

        public bool CorExists(int id, ContextDB db)
        {
            if (db.cores.FirstOrDefault(x => x.Id == id) != null)
                return true;

            return false;
        }

        public bool TipoVeiculoExists(int codigo, ContextDB db)
        {
            if (db.TipoVeiculos.FirstOrDefault(x => x.Codigo == codigo) != null)
                return true;

            return false;
        }

        public bool PeriodoLocacaoExists(int id, ContextDB db)
        {
            if (db.periodoLocacoes.FirstOrDefault(x => x.Id == id) != null)
                return true;

            return false;
        }
        public bool UsuarioExists(int id, ContextDB db)
        {
            if (db.usuarios.FirstOrDefault(x => x.Id == id) != null)
                return true;

            return false;
        }

        public bool ValidarUsuarioLocou(Locacao obj, ContextDB db)
        {
            var same = db.locacoes.Where(x => x.Usuario == obj.Usuario && x.Periodo == obj.Periodo).FirstOrDefault();

            if (same == null)
                return true;

            return false;
        }
    }
}