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
    public class VagasController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/Vagas
        public IQueryable<Vaga> GetVagas()
        {
            return db.Vagas;
        }

        // GET: api/Vagas/5
        [ResponseType(typeof(Vaga))]
        public async Task<IHttpActionResult> GetVaga(int id)
        {
            Vaga vaga = await db.Vagas.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            return Ok(vaga);
        }

        // PUT: api/Vagas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVaga(int id, Vaga vaga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaga.Id)
            {
                return BadRequest();
            }

            db.Entry(vaga).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagaExists(id))
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

        // POST: api/Vagas
        [ResponseType(typeof(Vaga))]
        public async Task<IHttpActionResult> PostVaga(Vaga vaga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vagas.Add(vaga);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vaga.Id }, vaga);
        }

        // DELETE: api/Vagas/5
        [ResponseType(typeof(Vaga))]
        public async Task<IHttpActionResult> DeleteVaga(int id)
        {
            Vaga vaga = await db.Vagas.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            db.Vagas.Remove(vaga);
            await db.SaveChangesAsync();

            return Ok(vaga);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VagaExists(int id)
        {
            return db.Vagas.Count(e => e.Id == id) > 0;
        }
    }
}