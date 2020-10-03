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
    /// Clase para el acceso de datos a tblFuncionDesarrolloP
    /// </summary>
    public class FuncionDesarrolloPBL
    {
        FuncionDesarrolloPDAL funcionDAL = new FuncionDesarrolloPDAL();

        /// <summary>
        /// Obtiene todos los registros en tblFuncionDesarrolloP
        /// </summary>
        /// <returns>model.listFunciones</returns>
        public List<tblFuncionDesarrolloP> getFuncionesDesarrollo()
        {
            try
            {
                return funcionDAL.getFuncionesDesarrollo();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblFuncionDesarrolloP donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listFunciones</returns>
        public List<tblFuncionDesarrolloP> getFuncionesDesarrollo(bool estado)
        {
            try
            {
                return funcionDAL.getFuncionesDesarrollo(estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblFuncionDesarrolloP donde nombreFunc == parámetro nombreFunc
        /// </summary>
        /// <param name="nombreFunc"></param>
        /// <returns>model.listFunciones</returns>
        public List<tblFuncionDesarrolloP> getFuncionesDesarrollo(string nombreFunc)
        {
            try
            {
                return funcionDAL.getFuncionesDesarrollo(nombreFunc);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene un dato bool si la función existe en tblFuncionDesarrolloP donde nombre == parámetro nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>TRUE=Función existe || FALSE=No existe la función</returns>
        public bool existeFunc(string nombre)
        {
            try
            {
                return funcionDAL.existeFunc(nombre);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Insertar registro en tblFuncionDesarrolloP, valida que el nombre no exista para realizar el 
        /// registro
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE=Registro Exitoso || FALSE=Error en registro</returns>
        public bool agregarFD(string nombre, bool estado)
        {
            try
            {
                return funcionDAL.agregarFD(nombre, estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actualiza el nombreFunc en tblFuncionDesarrolloP donde idFuncionD == parámetro idFD
        /// </summary>
        /// <param name="idFD"></param>
        /// <param name="nombre"></param>
        /// <returns>TRUE=Update exitoso || FALSE=Error en Update</returns>
        public bool editarFD(int idFD, string nombre)
        {
            try
            {
                return funcionDAL.editarFD(idFD, nombre);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene idFuncionD en tblFuncionDesarrolloP donde nombreFunc == parámetro nombreFunc
        /// </summary>
        /// <param name="nombreFunc"></param>
        /// <returns>model.idFuncionD</returns>
        public int getIdFuncion(string nombreFunc)
        {
            try
            {
                return funcionDAL.getIdFuncion(nombreFunc);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene el nombre de la función en tblFuncionDesarrolloP donde idFuncionD == parámetro idFunc
        /// </summary>
        /// <param name="idFunc"></param>
        /// <returns>model.nombreFunc</returns>
        public string getNomFunc(int idFunc)
        {
            try
            {
                return funcionDAL.getNomFunc(idFunc);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actualiza estado en tblFuncionDesarrolloP donde idFuncionD == parámetro idFD
        /// </summary>
        /// <param name="idFD"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE=Update exitoso || FALSE=Error en Update</returns>
        public bool actualizarEstado(int idFD, bool estado)
        {
            try
            {
                return funcionDAL.actualizarEstado(idFD,estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
