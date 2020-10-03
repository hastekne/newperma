using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.DataAccess.ModuloBP;

namespace BusinessModel.Business.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblPlantilla
    /// </summary>
    public class PlantillaBL
    {
        PlantillaDAL planDAL = new PlantillaDAL();

        /// <summary>
        /// Obtiene todos los registros en tblPlantilla
        /// </summary>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla()
        {
            try
            {
                return planDAL.getPlantilla();
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblPlantilla donde nombre == parámetro nomPlantilla
        /// </summary>
        /// <param name="nomPlantilla"></param>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla(string nomPlantilla)
        {
            try { 
            return planDAL.getPlantilla(nomPlantilla);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblPlantilla donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla(bool estado)
        {
            try
            {
                return planDAL.getPlantilla(estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registro de tblPlantilla donde idPlantilla == parámetro idPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns>model.listPlantilla</returns>
        public List<tblPlantilla> getPlantilla(int idPlantilla)
        {
            try
            {
                return planDAL.getPlantilla(idPlantilla);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
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
            try
            {
                return planDAL.agregarPlantilla(nombre, descripcion, estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return planDAL.editarPlantilla(idPlantilla, nombre, descripcion, estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

    }
}
