using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.DataAccess.ModuloEvento;
using BusinessModel.Models.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el acceso de datos a tblMesaTrabParticipante
    /// </summary>
    public class MesaParticipanteBL
    {
        MesaParticipanteDAL dal = new MesaParticipanteDAL();

        ///<summary>
        ///Buscar por idMesaTrabParti en tblMesaTrabParticipante
        /// </summary>
        public List<tblMesaTrabParticipante> getMesaPartiXidmesa(int idMesa)
        {
            try
            {
                return dal.getMesaPartiXidmesa(idMesa);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por folio participante en tblMesaTrabParticipante
        /// </summary>
        public List<tblMesaTrabParticipante> getMesaPartiXfolio(int folio)
        {
            try
            {
                return dal.getMesaPartiXfolio(folio);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblMesaTrabParticipante
        /// </summary>
        public bool agregarMesaParticipante(int folio, int idmesaEvento)
        {
            try
            {
                return dal.agregarMesaParticipante(folio, idmesaEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Inscripción a una mesa de trabajo a través de la carga masiva
        /// Utilizando una lista con id de mesas de trabajo a las cual se inscribe 
        /// </summary>
        /// <returns></returns>
        public bool agregarMesaParticipanteMasivo(int folio, List<int> listaIDmesa)
        {
            try
            {
                return dal.agregarMesaParticipanteMasivo(folio, listaIDmesa);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Regresa un T/F si el registro a la mesa de trabajo existe para el folio
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="nombreMT"></param>
        /// <returns></returns>
        public bool existeRegistroMT(int folio, string nombreMT)
        {
            try
            {
                return dal.existeRegistroMT(folio, nombreMT);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Usa clase MesaParticipanteRI, obtiene los registros en tblMesaTrabParticipante join tblMesaTrabEvento
        /// donde folioParticipante == parámetro folio
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>model.listaMesaPRI</returns>
        public List<MesaParticipanteRI> mesaParticipanteRegIn(int folio)
        {
            try
            {
                return dal.mesaParticipanteRegIn(folio);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        }
}
