using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LocacaoGaragens.Models;

namespace LocacaoGaragens.Controllers
{
    public class ValorLocacoesController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/ValorLocacoes
        public IQueryable<ValorLocacao> GetValorLocacoes()
        {
            return db.ValorLocacoes;
        }

        // GET: api/ValorLocacoes/5
        [ResponseType(typeof(ValorLocacao))]
        public async Task<IHttpActionResult> GetValorLocacao(int id)
        {
            ValorLocacao valorLocacao = await db.ValorLocacoes.FindAsync(id);
            if (valorLocacao == null)
            {
                return NotFound();
            }

            return Ok(valorLocacao);
        }

        // PUT: api/ValorLocacoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutValorLocacao(int id, ValorLocacao valorLocacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != valorLocacao.Id)
            {
                return BadRequest();
            }

            db.Entry(valorLocacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValorLocacaoExists(id))
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

        // POST: api/ValorLocacoes
        [ResponseType(typeof(ValorLocacao))]
        public async Task<IHttpActionResult> PostValorLocacao(ValorLocacao valorLocacao)
        {
            valorLocacao.TipoVeiculo = db.TipoVeiculos.Find(valorLocacao.TipoVeiculoFK);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ValorLocacoes.Add(valorLocacao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = valorLocacao.Id }, valorLocacao);
        }

        // DELETE: api/ValorLocacoes/5
        [ResponseType(typeof(ValorLocacao))]
        public async Task<IHttpActionResult> DeleteValorLocacao(int id)
        {
            ValorLocacao valorLocacao = await db.ValorLocacoes.FindAsync(id);
            if (valorLocacao == null)
            {
                return NotFound();
            }

            db.ValorLocacoes.Remove(valorLocacao);
            await db.SaveChangesAsync();

            return Ok(valorLocacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ValorLocacaoExists(int id)
        {
            return db.ValorLocacoes.Count(e => e.Id == id) > 0;
        }
    }
}