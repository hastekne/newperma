using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.Global;
using BusinessModel.Persistence.BD;


namespace BusinessModel.DataAccess.Global
{
    /// <summary>
    /// Clase para el acceso a datos de tblModulo
    /// </summary>
    public class ModuloDAL
    {
        ModuloML model = new ModuloML();

        /// <summary>
        /// Obtiene todos los registros en tblModulo
        /// </summary>
        /// <returns>model.listaModulos</returns>
        public List<tblModulo> getListModulo()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaModulos = (from m in db.tblModulo select m).ToList();
            }
            return model.listaModulos;
        }

        /// <summary>
        /// Obtiene los registros en tblModulo donde idModulo = parámetro idModulo
        /// </summary>
        /// <param name="idModulo"></param>
        /// <returns>model.listaModulos</returns>
        public List<tblModulo> getListModulo(string idModulo)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaModulos = (from m in db.tblModulo where m.idModulo==idModulo select m).ToList();
            }
            return model.listaModulos;
        }

        /// <summary>
        /// Genera una lista de todos los modulos dentro de la tabla tblModulo
        /// </summary>
        /// <returns>Retorna lista con Modulos</returns>
        public List<string> getSoloModulos()
        {
            model.soloModulos = new List<string>();
            using (var db = new SeguimientoPermanenciaEntities()) {
                model.listaModulos = (from m in db.tblModulo select m).ToList();
            }
            
            foreach(var item in model.listaModulos)
            {
                string dig2 = item.idModulo.Substring(0,2);
                string digFaltates = item.idModulo.Substring(2, 6);
                if (digFaltates.Equals("000000"))
                {
                    model.soloModulos.Add(item.nombreModulo);
                }
            }
            return model.soloModulos;
        }

        /// <summary>
        /// Actualización a titulo en tblModulo donde idModlo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="newTitulo"></param>
        /// <returns></returns>
        public bool editarTitulo(string modulo,string newTitulo)
        {

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var mod = (from mT in db.tblModulo where mT.idModulo == modulo select mT).First<tblModulo>();
                    mod.titulo = newTitulo;
                    db.SaveChanges();
                }
                actualizo = true;
            }
            catch (Exception ex)
            {
                actualizo = false;
                throw ex;
            }

            return actualizo;
        }

        /// <summary>
        /// Actualización archivoIntroduccion en tblModulo donde idModlo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="newArchivo"></param>
        /// <returns></returns>
        public bool editarArchivo(string modulo,string newArchivo)
        {
            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var mod = (from mT in db.tblModulo where mT.idModulo == modulo select mT).First<tblModulo>();
                    mod.archivoIntroduccion = newArchivo;
                    db.SaveChanges();
                }
                actualizo = true;
            }
            catch (Exception ex)
            {
                actualizo = false;
                throw ex;
            }

            return actualizo;
        }
    }
}
