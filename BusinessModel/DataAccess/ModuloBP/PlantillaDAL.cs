using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.ModuloBP;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblPlantilla
    /// </summary>
    public class PlantillaDAL
    {
        PlantillaML model = new PlantillaML();

        /// <summary>
        /// Obtiene todos los registros en tblPlantilla
        /// </summary>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPlantilla = (from p in db.tblPlantilla select p).ToList();
            }
            return model.listPlantilla;
        }

        /// <summary>
        /// Obtiene los registros de tblPlantilla donde nombre == parámetro nomPlantilla
        /// </summary>
        /// <param name="nomPlantilla"></param>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla(string nomPlantilla)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPlantilla = (from p in db.tblPlantilla where p.nombre==nomPlantilla select p).ToList();
            }
            return model.listPlantilla;
        }

        /// <summary>
        /// Obtiene los registros de tblPlantilla donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla(bool estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPlantilla = (from p in db.tblPlantilla where p.estado==estado select p).ToList();
            }
            return model.listPlantilla;
        }

        /// <summary>
        /// Obtiene los registro de tblPlantilla donde idPlantilla == parámetro idPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla(int idPlantilla)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPlantilla = (from p in db.tblPlantilla where p.idPlantilla==idPlantilla select p).ToList();
            }
            return model.listPlantilla;
        }

        /// <summary>
        /// Inserta Plantilla en tblPlantilla
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="estado"></param>
        /// <returns>True=Insert exitoso || False=Error en Insert</returns>
        public bool agregarPlantilla(string nombre, string descripcion, bool estado)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblPlantilla plan = new tblPlantilla()
                    {
                        nombre = nombre,
                        descripcion = descripcion,
                        estado = estado,
                    };
                    db.tblPlantilla.Add(plan);
                    db.SaveChanges();
                }
                agrego = true;
            }
            catch (Exception ex)
            {
                agrego = false;
                throw ex;
            }
            return agrego;
        }

        /// <summary>
        /// Actualización de nombre, descrición y estado en tblPlantilla donde idPlantilla == parámetro idPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="estado"></param>
        /// <returns>True=Update exitoso || False=Error en Update</returns>
        public bool editarPlantilla(int idPlantilla, string nombre, string descripcion, bool estado)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var plan = (from p in db.tblPlantilla where p.idPlantilla == idPlantilla select p).First<tblPlantilla>();
                    plan.nombre = nombre;
                    plan.descripcion = descripcion;
                    plan.estado = estado;
                    db.SaveChanges();

                }
            }
            catch (Exception ex) { edito = false; throw ex; }
            return edito;
        }


    }
}
