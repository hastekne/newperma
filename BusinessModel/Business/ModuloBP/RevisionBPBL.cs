using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.DataAccess.ModuloBP;
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloBP;

namespace BusinessModel.Business.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de dato a tblRevisionBP
    /// </summary>
    public class RevisionBPBL
    {
        RevisionBPDAL revisionDAL = new RevisionBPDAL();

        /// <summary>
        /// Obtiene tods los registros en tblRevisionBP
        /// </summary>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones()
        {
            try
            {
                return revisionDAL.getRevisiones();
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        //o	Obtener datos de la revisión por el ID de la BP.
        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde buenaPractica == parámetro ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisionXid(int ID)
        {
            try
            {
                return revisionDAL.getRevisionXid(ID);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblRevisionBPdonde usuarioRevisor == paráetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(string revisor)
        {
            try
            {
                return revisionDAL.getRevisiones(revisor);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(int BP)
        {
            try
            {
                return revisionDAL.getRevisiones(BP);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde usuarioRevisor == parámetro revisor
        /// y buenaPractica == parámetro BP
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="BP"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(string revisor, int BP)
        {
            try
            {
                return revisionDAL.getRevisiones(revisor, BP);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde usuarioRevisor == parámetro revisor
        /// y fechaSoliRevi == parámetro fechaSoliRevi
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="fechaSoliRevi"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(string revisor, System.DateTime fechaSoliRevi)
        {
            try
            {
                return revisionDAL.getRevisiones(revisor, fechaSoliRevi);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde buenaPractica == parámetro BP y 
        /// fechaSoliRevi == parámetro fechaSoliRevi
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="fechaSoliRevi"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(int BP, System.DateTime fechaSoliRevi)
        {
            try
            {
                return revisionDAL.getRevisiones(BP, fechaSoliRevi);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtener registros en tblRevisionBP donde usuarioRevisor == parámetro revisor y 
        /// buenaPractica == parámetro BP
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="BP"></param>
        /// <param name="fechaSoliRevi"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(string revisor, int BP, System.DateTime fechaSoliRevi)
        {
            try
            {
                return revisionDAL.getRevisiones(revisor, BP, fechaSoliRevi);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        //identificar el nombreuser al cual se le debera asignar la BP para revision y registrar en la tabla tblRevision
        /// <summary>
        /// Obtiene el usuario revisor al cual se le debará asignar la siguiente BP, se para esto se basa en la cantidad 
        /// de BP asignadas con estado REVISANDO o CORRIGIENDO  que tiene un revisor, la BP esasignada al revisor con menos BP 
        /// </summary>
        /// <returns>model.nombreUsuario</returns>
        public string asignarUsuarioRev()
        {
            try
            {
                return revisionDAL.asignarUsuarioRev();
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        //Validar si el ID existe en tblRevision
        /// <summary>
        /// Valida si un IDBP existe en tblRevisionBP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>True=Existe IdBP || False=No existe IdBP</returns>
        public bool existe(int BP)
        {
            try
            {
                return revisionDAL.existe(BP);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Nuevo registro en tblRevisionBP
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="fechaC"></param>
        /// <returns>True=Registro exitoso || False=Error en registro</returns>
        public bool agregarRevision(int BP, System.DateTime fechaC)
        {
            try
            {
                return revisionDAL.agregarRevision(BP, fechaC);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Actualiza la fechaListaPubli en tblRevisionBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="fechaPublic"></param>
        /// <returns>True=Update exitoso || False=Error update</returns>
        public bool editarFechaListPublicacion(int BP, System.DateTime fechaPublic)
        {
            try
            {
                return revisionDAL.editarFechaListPublicacion(BP, fechaPublic);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Actualiza comentarios de TituloBP, SituacionMejora, DiagnosticoRealizo, CaracteristicasContexto, DescripcionActividadesRealizadas,
        /// ElementoInnovador, ResultadosObtenidos, ObservacionesBP, FuentesInf y Generales en tblRevisionBP
        /// donde buenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="comentarioTituloBP"></param>
        /// <param name="comentarioSituacionMejora"></param>
        /// <param name="comentarioDiagnosticoRealizo"></param>
        /// <param name="comentarioCaracteristicasContexto"></param>
        /// <param name="comentarioDescripcionActividadesRealizadas"></param>
        /// <param name="comentarioElementoInnovador"></param>
        /// <param name="comentarioResultadosObtenidos"></param>
        /// <param name="comentarioObservacionesBP"></param>
        /// <param name="comentarioFuentesInf"></param>
        /// <param name="comentarioGeneral"></param>
        /// <returns>True=Update exitoso || False=Error en update</returns>
        public bool editarRev(int idBP, string comentarioTituloBP, string comentarioSituacionMejora, string comentarioDiagnosticoRealizo, string comentarioCaracteristicasContexto, string comentarioDescripcionActividadesRealizadas, string comentarioElementoInnovador, string comentarioResultadosObtenidos, string comentarioObservacionesBP, string comentarioFuentesInf, string comentarioGeneral)
        {
            try
            {
                return  revisionDAL.editarRev(idBP, comentarioTituloBP, comentarioSituacionMejora, comentarioDiagnosticoRealizo, comentarioCaracteristicasContexto, comentarioDescripcionActividadesRealizadas, comentarioElementoInnovador, comentarioResultadosObtenidos, comentarioObservacionesBP,comentarioFuentesInf,comentarioGeneral);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase BPRevisionTable, obtiene los registros de tblRevisionBP join tblBuenaPractica
        /// donde idBuenaPractica == parámetro revisor y estado =="REVISANDO || CORRIGIENDO || LISTO"
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listRevisionTable</returns>
        public List<BPRevisionTable> getTablaRevision(string revisor)
        {
            try
            {
                return revisionDAL.getTablaRevision(revisor);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Utiliza la clase BPRevisionTable, obtiene los registros de tblRevisionBP join tblBuenaPractica
        /// donde tituloBP contiene el parámetro palabraClave y usuarioRevisor == parámetro revisor y estado =="REVISANDO || CORRIGIENDO || LISTO"
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <param name="revisor"></param>
        /// <returns>model.listRevisionTable</returns>
        public List<BPRevisionTable> getPCReviBP(string palabraClave, string revisor)
        {
            try
            {
                return revisionDAL.getPCReviBP(palabraClave,revisor);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el idRevision de tblRevisionBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.idRevision</returns>
        public int getIdRevisionXIdBP(int BP)
        {
            try
            {
                return revisionDAL.getIdRevisionXIdBP(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
