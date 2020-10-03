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
    /// Clase para el acceso de datos a tblAutores
    /// </summary>
    public class AutoresBL
    {
        AutoresDAL autoresDAL = new AutoresDAL();

        /// <summary>
        /// Obtiene todos los registros en tblAutores
        /// </summary>
        /// <returns>model.listAutores</returns>
        public List<tblAutores> getAutores()
        {
            try
            {
                return autoresDAL.getAutores();
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registro en tblAutores donde buenaPractica=parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listAutores</returns>
        public List<tblAutores> getAutores(int BP)
        {
            try
            {
                return autoresDAL.getAutores(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Elimina un registro en tblAutores donde idAutor == parámetro idAutor
        /// </summary>
        /// <param name="idAutor"></param>
        /// <returns>TRUE==Registro Eliminado || FALSE==Falla en eliminar registro</returns>
        public bool eliminarAutor(int idAutor)
        {
            try
            {
                return autoresDAL.eliminarAutor(idAutor);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene true o false realizando la validación de los autores de una buena práctica
        /// solo se permiten 10 autores en una BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>TRUE==Más de 10 Autores || FALSE==Menos de 10 Autores</returns>
        public bool existenciaAutores(int BP)
        {
            try
            {
                return autoresDAL.existenciaAutores(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Inserta registro en tblAutores, solo si existenciaAutores(BP) == false  
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="nombre"></param>
        /// <param name="ape1"></param>
        /// <param name="ape2"></param>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Falla en el registro</returns>
        public bool agregarAutor(int BP, string nombre, string ape1, string ape2, string email, string tel)
        {
            try
            {
                return autoresDAL.agregarAutor(BP,nombre,ape1,ape2,email,tel);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
