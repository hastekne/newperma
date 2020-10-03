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
    /// Clase para el acceso de datos a tblFuncionDesarrolloP
    /// </summary>
    public class FuncionDesarrolloPDAL
    {
        FuncionDesarrolloPML model = new FuncionDesarrolloPML();

        /// <summary>
        /// Obtiene todos los registros en tblFuncionDesarrolloP
        /// </summary>
        /// <returns>model.listFunciones</returns>
        public List<tblFuncionDesarrolloP> getFuncionesDesarrollo()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listFunciones = (from f in db.tblFuncionDesarrolloP select f).ToList();
            }
            return model.listFunciones;
        }

        /// <summary>
        /// Obtiene idFuncionD en tblFuncionDesarrolloP donde nombreFunc == parámetro nombreFunc
        /// </summary>
        /// <param name="nombreFunc"></param>
        /// <returns>model.idFuncionD</returns>
        public int getIdFuncion(string nombreFunc)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                //model.idFuncionD = Int32.Parse((from f in db.tblFuncionDesarrolloP where f.nombreFunc==nombreFunc select f.idFuncionD).ToString());
                model.listFunciones = (from f in db.tblFuncionDesarrolloP where f.nombreFunc == nombreFunc select f).ToList();
                foreach(var item in model.listFunciones)
                {
                    model.idFuncionD = item.idFuncionD;
                }
            }
            return model.idFuncionD;
        }

        /// <summary>
        /// Obtiene los registros en tblFuncionDesarrolloP donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listFunciones</returns>
        public List<tblFuncionDesarrolloP> getFuncionesDesarrollo(bool estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listFunciones = (from f in db.tblFuncionDesarrolloP where f.estado==estado select f).ToList();
            }
            return model.listFunciones;
        }

        /// <summary>
        /// Obtiene los registros de tblFuncionDesarrolloP donde nombreFunc == parámetro nombreFunc
        /// </summary>
        /// <param name="nombreFunc"></param>
        /// <returns>model.listFunciones</returns>
        public List<tblFuncionDesarrolloP> getFuncionesDesarrollo(string nombreFunc)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listFunciones = (from f in db.tblFuncionDesarrolloP where f.nombreFunc==nombreFunc select f).ToList();
            }
            return model.listFunciones;
        }

        /// <summary>
        /// Obtiene un dato bool si la función existe en tblFuncionDesarrolloP donde nombre == parámetro nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>TRUE=Función existe || FALSE=No existe la función</returns>
        public bool existeFunc(string nombre)
        {
            bool existe = false;
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listFunciones = (from f in db.tblFuncionDesarrolloP where f.nombreFunc == nombre select f).ToList();
            }
            if (model.listFunciones.Count > 0)
            {
                existe = true;
            }
                return existe;
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
            bool agrego = false;
            try
            {
                if (existeFunc(nombre) == false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblFuncionDesarrolloP fd = new tblFuncionDesarrolloP()
                        {
                            nombreFunc = nombre,
                            estado = true,
                        };
                        db.tblFuncionDesarrolloP.Add(fd);
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
        /// Actualiza el nombreFunc en tblFuncionDesarrolloP donde idFuncionD == parámetro idFD
        /// </summary>
        /// <param name="idFD"></param>
        /// <param name="nombre"></param>
        /// <returns>TRUE=Update exitoso || FALSE=Error en Update</returns>
        public bool editarFD(int idFD,string nombre)
        {

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var fude = (from fd in db.tblFuncionDesarrolloP where fd.idFuncionD == idFD select fd).First<tblFuncionDesarrolloP>();
                    fude.nombreFunc = nombre;
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
        /// Obtiene el nombre de la función en tblFuncionDesarrolloP donde idFuncionD == parámetro idFunc
        /// </summary>
        /// <param name="idFunc"></param>
        /// <returns>model.nombreFunc</returns>
        public string getNomFunc(int idFunc)
        {
            string nomF = "";
            using (var db = new SeguimientoPermanenciaEntities())
            {
                 model.listFunciones = (from f in db.tblFuncionDesarrolloP where f.idFuncionD==idFunc select f).ToList();
            }
            foreach(var item in model.listFunciones)
            {
                nomF = item.nombreFunc;
            }
            return nomF;
        }

        /// <summary>
        /// Actualiza estado en tblFuncionDesarrolloP donde idFuncionD == parámetro idFD
        /// </summary>
        /// <param name="idFD"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE=Update exitoso || FALSE=Error en Update</returns>
        public bool actualizarEstado(int idFD, bool estado)
        {
            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var fude = (from fd in db.tblFuncionDesarrolloP where fd.idFuncionD == idFD select fd).First<tblFuncionDesarrolloP>();
                    fude.estado = estado;
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
