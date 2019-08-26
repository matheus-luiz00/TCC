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
    public class MarcaMotoesController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/MarcaMotoes
        public IQueryable<MarcaMoto> GetmarcaMotos()
        {
            return db.marcaMotos;
        }

        // GET: api/MarcaMotoes/5
        [ResponseType(typeof(MarcaMoto))]
        public async Task<IHttpActionResult> GetMarcaMoto(int id)
        {
            MarcaMoto marcaMoto = await db.marcaMotos.FindAsync(id);
            if (marcaMoto == null)
            {
                return NotFound();
            }

            return Ok(marcaMoto);
        }

        // PUT: api/MarcaMotoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMarcaMoto(int id, MarcaMoto marcaMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marcaMoto.Id)
            {
                return BadRequest();
            }

            db.Entry(marcaMoto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaMotoExists(id))
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

        // POST: api/MarcaMotoes
        [ResponseType(typeof(MarcaMoto))]
        public async Task<IHttpActionResult> PostMarcaMoto(MarcaMoto marcaMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.marcaMotos.Add(marcaMoto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = marcaMoto.Id }, marcaMoto);
        }

        // DELETE: api/MarcaMotoes/5
        [ResponseType(typeof(MarcaMoto))]
        public async Task<IHttpActionResult> DeleteMarcaMoto(int id)
        {
            MarcaMoto marcaMoto = await db.marcaMotos.FindAsync(id);
            if (marcaMoto == null)
            {
                return NotFound();
            }

            db.marcaMotos.Remove(marcaMoto);
            await db.SaveChangesAsync();

            return Ok(marcaMoto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarcaMotoExists(int id)
        {
            return db.marcaMotos.Count(e => e.Id == id) > 0;
        }
    }
}