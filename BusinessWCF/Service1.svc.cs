using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BusinessWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void AgregarUsuario(Usuario usuario)
        {
            PersonalContext context = new PersonalContext();
            context.Usuario_CRUD(1, usuario.Nombre, usuario.FechaNacimiento, usuario.Sexo, "CREATE");
            context.SaveChanges();
        }

        public List<Usuario> ConsultarUsuarios( )
        {
            PersonalContext context = new PersonalContext();
           var preResult =   context.Usuario_CRUD(0, "0", new DateTime(), "M", "READ");
           List<Usuario> result = new List<Usuario>();

            foreach(var item in preResult)
            {
                Usuario newUsuario = new Usuario();
                newUsuario.Id = item.Id;    
                newUsuario.Nombre = item.Nombre;
                newUsuario.FechaNacimiento  = item.FechaNacimiento;
                newUsuario.Sexo = item.Sexo;
                result.Add(newUsuario);                   
            }
            return result;
        }

        public void EliminarUsuario(Usuario usuario)
        {
            PersonalContext context = new PersonalContext();
            context.Usuario_CRUD(usuario.Id, usuario.Nombre, usuario.FechaNacimiento, usuario.Sexo, "DELETE");
            context.SaveChanges();
        }

        public void ModificarUsuario(Usuario usuario)
        {
            PersonalContext context = new PersonalContext();
            context.Usuario_CRUD(usuario.Id, usuario.Nombre, usuario.FechaNacimiento, usuario.Sexo, "UPDATE");
            context.SaveChanges();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

     
    }
}
