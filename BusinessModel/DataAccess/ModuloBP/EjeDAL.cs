using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloBP;
namespace BusinessModel.DataAccess.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblEje
    /// </summary>
    public class EjeDAL
    {
        EjeML model = new EjeML();

        /// <summary>
        /// Obtiene todos los registros de tblEje
        /// </summary>
        /// <returns>model.listaEjes</returns>
        public List<tblEje> getListaEje()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
              
                model.listaEjes = (from e in db.tblEje select e).ToList();
            }
            return model.listaEjes;
        }

        /// <summary>
        /// Obtiene el model.idEje de tblEje donde nombre == parámetro nombreEJE
        /// </summary>
        /// <param name="nombreEJE"></param>
        /// <returns>model.idEje</returns>
        public int getIdEje(string nombreEJE)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                //model.idEje = Int32.Parse((from e in db.tblEje where e.nombre==nombreEJE select e.idEje).ToString());
                //model.idEje = db.tblEje.Where(p => p.nombre == nombreEJE);
                model.listaEjes = (from e in db.tblEje where e.nombre == nombreEJE select e).ToList();
                foreach(var item in model.listaEjes)
                {
                    model.idEje = item.idEje;
                }
            }
            return model.idEje;
        }


        /// <summary>
        /// Obtiene todos los registros de tblEje donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listaEjes</returns>
        public List<tblEje> getListaEje(bool estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEjes = (from e in db.tblEje where e.estado==estado select e).ToList();
            }
            return model.listaEjes;
        }

        /// <summary>
        /// Obtiene todos los registros de tblEje donde nombre == parámetro nombreEje
        /// </summary>
        /// <param name="nombreEje"></param>
        /// <returns>model.listaEjes</returns>
        public List<tblEje> getListaEje(string nombreEje)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEjes = (from e in db.tblEje where e.nombre==nombreEje select e).ToList();
            }
            return model.listaEjes;
        }

        /// <summary>
        /// Obtiene un dato bool si el eje existe, registro de tblEje donde nombre == parámetro noombreEje
        /// </summary>
        /// <param name="noombreEje"></param>
        /// <returns>TRUE==Existe eje || FALSE==No existe eje</returns>
        public bool existeEje(string noombreEje)
        {
            bool existe = false;

            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaEjes = (from e in db.tblEje where e.nombre == noombreEje select e).ToList();
            }
            if (model.listaEjes.Count > 0)
            {
                existe = true;
            }

                return existe;
        }

        /// <summary>
        /// Inserta registro en tblEje, el nombre no debe de existir en la tabla
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Error en registro</returns>
        public bool agregar(string nombre, bool estado)
        {
            bool agrego = false;
            try
            {
                if (existeEje(nombre) == false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblEje ej = new tblEje()
                        {
                            nombre = nombre,
                            estado = true,
                        };
                        db.tblEje.Add(ej);
                        db.SaveChanges();
                    }
                    agrego = true;
                }
                
            }
            catch (Exception ex)
            {
                agrego = false;
                throw ex;
            }
            return agrego;
        }

        /// <summary>
        /// Actualiza el nombre del eje en tblEje donde idEje == parámetro idEje
        /// </summary>
        /// <param name="idEje"></param>
        /// <param name="nombre"></param>
        /// <returns>TRUE==Update exitoso || FALSE==Error en Update</returns>
        public bool editarEje(int idEje,string nombre)
        {

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var eje = (from ej in db.tblEje where ej.idEje == idEje select ej).First<tblEje>();
                    eje.nombre = nombre;
                    db.SaveChanges();
                }
                actualizo = true;
            }
            catch (Exception)
            {
                actualizo = false;
                
            }

            return actualizo;
        }

        /// <summary>
        /// Obtiene el nombre del eje de tblEje donde idEje == parámetro id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nombre Eje</returns>
        public string getNombreEje(int id)
        {
            string eje = "";
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEjes = (from e in db.tblEje where e.idEje==id select e).ToList();
            }
            foreach(var item in model.listaEjes)
            {
                eje = item.nombre;
            }

            return eje;
        }


        /// <summary>
        /// Actualiza el estado del eje en tblEje donde idEje == parámetro idEje
        /// </summary>
        /// <param name="idEje"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE=Update exitoso || FALSE=Error en Update</returns>
        public bool actualizarEstado(int idEje, bool estado)
        {
            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var eje = (from ej in db.tblEje where ej.idEje == idEje select ej).First<tblEje>();
                    eje.estado = estado;
                    db.SaveChanges();
                }
                actualizo = true;
            }
            catch (Exception)
            {
                actualizo = false;

            }

            return actualizo;
        }
    }
}
