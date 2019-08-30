using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LocacaoGaragens.Models;
using LocacaoGaragens.Utils;

namespace LocacaoGaragens.Controllers
{
    public class LocacaosController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/Locacaos
        public IQueryable<Locacao> Getlocacoes()
        {
            return db.locacoes;
        }

        // GET: api/Locacaos
        [Route("Api/Locacaos/{mes}/relatorio")]
        [HttpGet]
        public IQueryable GetRelatorio(int mes)
        {
            
            
            var query = (from loc in db.locacoes
                         where loc.PeriodoLocacao.DataInicial.Month <= mes && loc.PeriodoLocacao.DataFinal.Month >= mes
                         join usu in db.usuarios on loc.Usuario equals usu.Id
                         join tpv in db.TipoVeiculos on loc.TipoVeiculo equals tpv.Id
                         join mrc in db.Marcas on loc.Marca equals mrc.Id
                         join mdl in db.Modelos on loc.Modelo equals mdl.Id
                         join cor in db.cores on loc.Cor equals cor.Id
                         join vlr in db.ValorLocacoes on loc.TipoVeiculo equals vlr.TipoVeiculoFK
                         select new
                         {
                             loc.Id,
                             usu.Nome,
                             Tipo = tpv.Descricao,
                             Marca = mrc.Descricao,
                             Modelo = mdl.Descricao,
                             Cor = cor.Descricao,
                             loc.Placa,
                             loc.Status,
                             loc.TermoAceito,
                             vlr.Valor

                         });
            
            return query;
        }

        // GET: api/Locacaos/5
        [ResponseType(typeof(Locacao))]
        public async Task<IHttpActionResult> GetLocacao(int id)
        {
            Locacao locacao = await db.locacoes.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            return Ok(locacao);
        }

        [Route("Api/Locacaos/{periodo}/{tipo}/aprovartodos")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAprovarTodos(int periodo, int tipo)
        {
            int vagas = db.Vagas.Where(x => (int)x.TipoVeiculo == ((tipo > 2) ? 2 : tipo)).FirstOrDefault().Quantidade;
            int apv = db.locacoes.Where(x => x.Periodo == periodo && x.Status != Enums.Status.Aprovado).Count();
            
            if (vagas > apv + 1)
                return BadRequest();

            await db.locacoes.Where(x => x.Periodo == periodo).ForEachAsync(x => x.Status = Enums.Status.Aprovado);

            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Api/Locacaos/{id}/aprovar")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAprovar(int id)
        {
            Locacao locacao = db.locacoes.Find(id);
            int vagas = db.Vagas.Where(x => (int)x.TipoVeiculo == ((locacao.TipoVeiculo >2)?2:locacao.TipoVeiculo)).FirstOrDefault().Quantidade;
            int apv = db.locacoes.Where(x => x.PeriodoLocacao == locacao.PeriodoLocacao && x.Status != Enums.Status.Aprovado).Count();

            if (vagas > apv + 1)
                return BadRequest();

            locacao.Status = Enums.Status.Aprovado;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locacao.Id)
            {
                return BadRequest();
            }

            db.Entry(locacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Locacaos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLocacao(int id, Locacao locacao)
        {
            
            var validador = new Validacoes();

            if (!(validador.TipoVeiculoExists(locacao.TipoVeiculo, db)))
                return BadRequest("Tipo de veículo informado inexistente");

            
            //Caso seja carro ou moto
            if (locacao.TipoVeiculo <= 1)
            {

                //Valida placa
                if (!(validador.ValidarPlacaUnique(locacao.Placa, db)))
                    return BadRequest("A placa já cadastrada ou em formato invalido");

                if (!(validador.CodigoMarcaExists(locacao.Marca, db)))
                    return BadRequest("Marca informada inexistente");

                if (!(validador.ModeloExists(locacao.Modelo, db)))
                    return BadRequest("Modelo informado inexistente");

                if (!(validador.CorExists(locacao.Cor, db)))
                    return BadRequest("Cor informada inexistente");
            }

            //Valida se periodo de locação existe
            if (!(validador.PeriodoLocacaoExists(locacao.Periodo, db)))
                //return BadRequest("Periodo de locação inexistente");
                return BadRequest();

            if (!(validador.UsuarioExists(locacao.Usuario, db)))
                return BadRequest("Usuario Invalido");

            

            locacao.PeriodoLocacao = db.periodoLocacoes.Find(locacao.Periodo);
            locacao.UsuarioDb = db.usuarios.Find(locacao.Usuario);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locacao.Id)
            {
                return BadRequest();
            }

            db.Entry(locacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Locacaos
        [ResponseType(typeof(Locacao))]
        public async Task<IHttpActionResult> PostLocacao(Locacao locacao)
        {
            if (!locacao.TermoAceito)
                return BadRequest("Para realizar a locação, os termos de uso devem ser aceitos");

            var validador = new Validacoes();

            if(!(validador.TipoVeiculoExists(locacao.TipoVeiculo, db)))
                return BadRequest("Tipo de veículo informado inexistente");


            //Caso seja carro ou moto
            if (locacao.TipoVeiculo <= 1)
            {

                //Valida placa
                if(!(validador.ValidarPlacaUnique(locacao.Placa, db)))
                    return BadRequest("A placa já cadastrada ou em formato invalido");

                if (!(validador.CodigoMarcaExists(locacao.Marca, db)))
                    return BadRequest("Marca informada inexistente");

                if (!(validador.ModeloExists(locacao.Modelo, db)))
                    return BadRequest("Modelo informado inexistente");

                if(!(validador.CorExists(locacao.Cor, db)))
                    return BadRequest("Cor informada inexistente");
            }

            locacao.Status = Enums.Status.Analise;

            //Valida se periodo de locação existe
            if (!(validador.PeriodoLocacaoExists(locacao.Periodo, db)))
                //return BadRequest("Periodo de locação inexistente");
                return BadRequest(ModelState);

            if (!(validador.UsuarioExists(locacao.Usuario, db)))
                return BadRequest("Usuario Invalido");

            if (!(validador.ValidarUsuarioLocou(locacao, db)))
                return BadRequest();

            locacao.PeriodoLocacao = db.periodoLocacoes.Find(locacao.Periodo);
            locacao.UsuarioDb = db.usuarios.Find(locacao.Usuario);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.locacoes.Add(locacao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = locacao.Id }, locacao);
        }

        // DELETE: api/Locacaos/5
        [ResponseType(typeof(Locacao))]
        public async Task<IHttpActionResult> DeleteLocacao(int id)
        {
            Locacao locacao = await db.locacoes.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            db.locacoes.Remove(locacao);
            await db.SaveChangesAsync();

            return Ok(locacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocacaoExists(int id)
        {
            return db.locacoes.Count(e => e.Id == id) > 0;
        }
    }
}