using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Models.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.ModuloEvento
{
    /// <summary>
    /// Clase para el Acceso de datos a tblMunicipioEvento
    /// </summary>
    public class MunicipioEventoDAL
    {
        MunicipioEventoML model = new MunicipioEventoML();

        ///<summary>
        ///Buscar por idMunicipioEvento en tblMunicipioEvento
        /// </summary>
        public List<tblMunicipioEvento> getMuniEvenXidMuniE(int idMuniEven)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMuniEvento = (from me in db.tblMunicipioEvento where me.idMunicipioEvento == idMuniEven select me).ToList();
            }
            return model.listaMuniEvento;
        }

        ///<summary>
        ///Buscar por idEvento en tblMunicipioEvento
        /// </summary>
        public List<tblMunicipioEvento> getMuniEvenXidEven(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMuniEvento = (from me in db.tblMunicipioEvento where me.idEvento == idEvento select me).ToList();
            }
            return model.listaMuniEvento;
        }

        ///<summary>
        ///Insertar registro en tblMunicipioEvento
        /// </summary>
        public bool agregarMuniEvento(int idEvento, int idMunicipio)
        {
            bool agrego = false;
            if (existeMuniEvento(idEvento,idMunicipio) == false)
            {
                try
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblMunicipioEvento newMuniEvento = new tblMunicipioEvento()
                        {
                            idEvento = idEvento,
                            idMunicipio = idMunicipio,
                        };
                        db.tblMunicipioEvento.Add(newMuniEvento);
                        db.SaveChanges();
                    }
                    agrego = true;
                }
                catch (Exception)
                {
                    agrego = false;
                }
            }
            

            return agrego;
        }

        ///<summary>
        ///Eliminar registro en tblMunicipioEvento
        /// </summary>
        public bool eliminarMunicipioEven(int idMunicipioEvent)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var muniEvent = from me in db.tblMunicipioEvento where me.idMunicipioEvento == idMunicipioEvent select me;
                    db.tblMunicipioEvento.RemoveRange(muniEvent);
                    //db.tblMunicipioEvento.Remove(muniEvent);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }


        ///<summary>
        ///Eliminar todos los registros en tblMunicipioEvento por el ID
        /// </summary>
        public bool eliminarTodosMunicipioEven(int idEvento)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var muniEvent = (from me in db.tblMunicipioEvento where me.idEvento == idEvento select me).First<tblMunicipioEvento>();
                    db.tblMunicipioEvento.Remove(muniEvent);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }

        /// <summary>
        /// Obtiene un valor booleano verdadero o false, para determinar si idEvento == parámetro idEvento 
        /// y idMunicipio == parámetro idMino existen en tblMunicipioEvento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="idMuni"></param>
        /// <returns>True=Existe el idEvento y idMunicipio en tblMunicipioEvento || False=No existe el idEvento y idMunicipio en tblMunicipioEvento</returns>
        public bool existeMuniEvento(int idEvento,int idMuni)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMuniEvento = (from me in db.tblMunicipioEvento where me.idEvento==idEvento && me.idMunicipio==idMuni select me).ToList();
            }
            if (model.listaMuniEvento.Count != 0)
                return true;
            return false;
        }


    }
}
