using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Model;

namespace Personal.Controllers
{
    public class UsuariosController : Controller
    {
        private PersonalContext db = new PersonalContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
       
            return View(service.ConsultarUsuarios().ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,FechaNacimiento,Sexo")] ServiceReference1.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
                service.AgregarUsuario(usuario);
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

            ServiceReference1.Usuario usuario =  service.ConsultarUsuarios().Where(c=>c.Id== id).FirstOrDefault();
            //Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,FechaNacimiento,Sexo")] ServiceReference1.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
                service.ModificarUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

            ServiceReference1.Usuario usuario = service.ConsultarUsuarios().Where(c => c.Id == id).FirstOrDefault();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            ServiceReference1.Usuario usuario = service.ConsultarUsuarios().Where(c => c.Id == id).FirstOrDefault();
            service.EliminarUsuario(usuario);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
