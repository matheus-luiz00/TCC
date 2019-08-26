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
    public class TermoesController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/Termoes
        public IQueryable<Termo> Gettermos()
        {
            return db.termos;
        }

        // GET: api/Termoes/5
        [ResponseType(typeof(Termo))]
        public async Task<IHttpActionResult> GetTermo(int id)
        {
            Termo termo = await db.termos.FindAsync(id);
            if (termo == null)
            {
                return NotFound();
            }

            return Ok(termo);
        }

        // PUT: api/Termoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTermo(int id, Termo termo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != termo.Id)
            {
                return BadRequest();
            }

            db.Entry(termo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermoExists(id))
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

        // POST: api/Termoes
        [ResponseType(typeof(Termo))]
        public async Task<IHttpActionResult> PostTermo(Termo termo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.termos.Add(termo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = termo.Id }, termo);
        }

        // DELETE: api/Termoes/5
        [ResponseType(typeof(Termo))]
        public async Task<IHttpActionResult> DeleteTermo(int id)
        {
            Termo termo = await db.termos.FindAsync(id);
            if (termo == null)
            {
                return NotFound();
            }

            db.termos.Remove(termo);
            await db.SaveChangesAsync();

            return Ok(termo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TermoExists(int id)
        {
            return db.termos.Count(e => e.Id == id) > 0;
        }
    }
}