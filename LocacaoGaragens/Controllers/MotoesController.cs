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
    public class MotoesController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/Motoes
        public IQueryable<Moto> Getmotos()
        {
            return db.motos;
        }

        // GET: api/Motoes/5
        [ResponseType(typeof(Moto))]
        public async Task<IHttpActionResult> GetMoto(int id)
        {
            Moto moto = await db.motos.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }

            return Ok(moto);
        }

        // PUT: api/Motoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMoto(int id, Moto moto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moto.Id)
            {
                return BadRequest();
            }

            db.Entry(moto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoExists(id))
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

        // POST: api/Motoes
        [ResponseType(typeof(Moto))]
        public async Task<IHttpActionResult> PostMoto(Moto moto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.motos.Add(moto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = moto.Id }, moto);
        }

        // DELETE: api/Motoes/5
        [ResponseType(typeof(Moto))]
        public async Task<IHttpActionResult> DeleteMoto(int id)
        {
            Moto moto = await db.motos.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }

            db.motos.Remove(moto);
            await db.SaveChangesAsync();

            return Ok(moto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MotoExists(int id)
        {
            return db.motos.Count(e => e.Id == id) > 0;
        }
    }
}