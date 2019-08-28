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

        // PUT: api/Locacaos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLocacao(int id, Locacao locacao)
        {
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

            //Caso seja carro ou moto
            if(locacao.TipoVeiculo > 1)
            {
                //Caso tenha placa / Modelo / Marca
                if (locacao.Placa != null && locacao.Modelo > 0 && locacao.Marca > 0)
                {
                    locacao.Placa.Trim('-');
                    bool placaMSul = Regex.IsMatch(locacao.Placa, @"^[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$");

                    bool placaBrasil = Regex.IsMatch(locacao.Placa, @"^[a-zA-Z]{3}[0-9]{4}$");

                    bool placaMoto = Regex.IsMatch(locacao.Placa, @"^[a-zA-Z]{3}[0-9]{2}[a-zA-Z]{1}[0-9]{1}$");

                    //Caso seja uma placa valida
                    if (placaMSul || placaBrasil || placaMoto)
                    {
                        //caso a placa seja nao cadastrada no banco
                        var existe = db.locacoes.First(x => x.Placa == locacao.Placa);
                        if (existe == null)
                        {
                            var prdLocacao = db.periodoLocacoes.FirstOrDefault(x => x.Id == locacao.Periodo);

                            if (prdLocacao != null)
                            {
                                locacao.PeriodoLocacao = prdLocacao;


                            }
                            return BadRequest("Periodo de locação inexistente");
                        }

                        return BadRequest("A placa já está cadastrada no sistema");
                        
                    }
                    return BadRequest("A placa informada não está no formato aceitado");
                }
                return BadRequest("Modelo, placa e marca são necessários para realizar a locação");
            } else
            {

            }

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