using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.DataAccess.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el Acceso de datos a tblCampoEvento
    /// </summary>
    public class CampoEventoBL
    {
        CampoEventoDAL dal = new CampoEventoDAL();

        /// <summary>
        /// Buscar registro en tblCampoEvento por idEvento
        /// </summary>
        public List<tblCampoEvento> getCampoEventoXidEvento(int idEvento)
        {
            try
            {
                return dal.getCampoEventoXidEvento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Método para nuevo registro en tblCampoEvento
        /// </summary>
        public bool agregarCamposRegistro(int idEvento, bool cct, bool nomplantel, bool municipio, bool regSEG, bool nivel, bool instisub, bool curp, bool nom, bool ap1, bool ap2, bool email, bool tel, bool sexo, bool funcion, bool mesaTrab)
        {
            try
            {
                return dal.agregarCamposRegistro(idEvento, cct, nomplantel, municipio, regSEG, nivel, instisub, curp, nom, ap1, ap2, email, tel, sexo, funcion, mesaTrab);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actualizar registro en tblCampoEvento
        /// </summary>
        public bool editarCamposRegistro(int idCampoEvento, bool cct, bool nomplantel, bool municipio, bool regSEG, bool nivel, bool instisub, bool curp, bool nom, bool ap1, bool ap2, bool email, bool tel, bool sexo, bool funcion, bool mesaTrab)
        {
            try
            {
                return dal.editarCamposRegistro(idCampoEvento, cct, nomplantel, municipio, regSEG, nivel, instisub, curp, nom, ap1, ap2, email, tel, sexo, funcion, mesaTrab);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Retorna el estado del campo para el Evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="campo"></param>
        /// <returns></returns>
        public bool campoHabilitado(int idEvento, string campo)
        {
            try
            {
                return dal.campoHabilitado(idEvento, campo);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Retorna lista con todos los campos aceptados de un evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public List<string> camposEvento(int idEvento)
        {
            try
            {
                return dal.camposEvento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        // <summary>
        /// Eliminar todos los campos del Evento por el ID del Evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool eliminarTodosCamposEvento(int idEvento)
        {
            try
            {
                return dal.eliminarTodosCamposEvento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        

    }
}
