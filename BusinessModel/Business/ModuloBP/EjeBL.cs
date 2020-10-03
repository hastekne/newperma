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
    /// Clase para el acceso de datos a tblEje
    /// </summary>
    public class EjeBL
    {
        EjeDAL ejeDAL = new EjeDAL();

        /// <summary>
        /// Obtiene todos los registros de tblEje
        /// </summary>
        /// <returns>model.listaEjes</returns>
        public List<tblEje> getListaEje()
        {
            try
            {
                return ejeDAL.getListaEje();
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene todos los registros de tblEje donde nombre == parámetro nombreEje
        /// </summary>
        /// <param name="nombreEje"></param>
        /// <returns>model.listaEjes</returns>
        public List<tblEje> getListaEje(string nombreEje)
        {
            try
            {
                return ejeDAL.getListaEje(nombreEje);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Inserta registro en tblEje, el nombre no debe de existir en la tabla
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Error en registro</returns>
        public bool agregar(string nombre, bool estado)
        {
            try
            {
                return ejeDAL.agregar(nombre,estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actualiza el nombre del eje en tblEje donde idEje == parámetro idEje
        /// </summary>
        /// <param name="idEje"></param>
        /// <param name="nombre"></param>
        /// <returns>TRUE==Update exitoso || FALSE==Error en Update</returns>
        public bool editarEje(int idEje, string nombre)
        {
            try
            {
                return ejeDAL.editarEje(idEje,nombre);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene todos los registros de tblEje donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listaEjes</returns>
        public List<tblEje> getListaEje(bool estado)
        {
            try
            {
                return ejeDAL.getListaEje(estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene el model.idEje de tblEje donde nombre == parámetro nombreEJE
        /// </summary>
        /// <param name="nombreEJE"></param>
        /// <returns>model.idEje</returns>
        public int getIdEje(string nombreEJE)
        {
            try
            {
                return ejeDAL.getIdEje(nombreEJE);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene el nombre del eje de tblEje donde idEje == parámetro id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nombre Eje</returns>
        public string getNombreEje(int id)
        {
            try
            {
                return ejeDAL.getNombreEje(id);
            }catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actualiza el estado del eje en tblEje donde idEje == parámetro idEje
        /// </summary>
        /// <param name="idEje"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE=Update exitoso || FALSE=Error en Update</returns>
        public bool actualizarEstado(int idEje, bool estado)
        {
            try
            {
                return ejeDAL.actualizarEstado(idEje,estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        }
}
