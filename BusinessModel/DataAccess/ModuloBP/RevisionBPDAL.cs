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
    /// Clase para el acceso de dato a tblRevisionBP
    /// </summary>
    public class RevisionBPDAL
    {
        RevisionBPML model = new RevisionBPML();

        /// <summary>
        /// Obtiene tods los registros en tblRevisionBP
        /// </summary>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP select r).ToList();
            }
            return model.listRevision;
        }

        //o	Obtener datos de la revisión por el ID de la BP.
        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde buenaPractica == parámetro ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisionXid(int ID)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listRevision = (from r in db.tblRevisionBP where r.buenaPractica == ID select r).ToList();
            }
            return model.listRevision;
        }

        /// <summary>
        /// Obtiene los registros en tblRevisionBPdonde usuarioRevisor == paráetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(string revisor)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.usuarioRevisor == revisor select r).ToList();
            }
            return model.listRevision;
        }

        /// <summary>
        /// Obtiene los registros en tblRevisionBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listRevision</returns>
        public List<tblRevisionBP> getRevisiones(int BP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.buenaPractica == BP select r).ToList();
            }
            return model.listRevision;
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
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.usuarioRevisor == revisor && r.buenaPractica == BP select r).ToList();
            }
            return model.listRevision;
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
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.usuarioRevisor == revisor && r.fechaSoliRevi == fechaSoliRevi select r).ToList();
            }
            return model.listRevision;
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
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.buenaPractica == BP && r.fechaSoliRevi == fechaSoliRevi select r).ToList();
            }
            return model.listRevision;
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
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.usuarioRevisor == revisor && r.buenaPractica == BP select r).ToList();
            }
            return model.listRevision;
        }


        //identificar el nombreuser al cual se le debera asignar la BP para revision y registrar en la tabla tblRevision
        /// <summary>
        /// Obtiene el usuario revisor al cual se le debará asignar la siguiente BP, se para esto se basa en la cantidad 
        /// de BP asignadas con estado REVISANDO o CORRIGIENDO  que tiene un revisor, la BP esasignada al revisor con menos BP 
        /// </summary>
        /// <returns>model.nombreUsuario</returns>
        public string asignarUsuarioRev()
        {
            Usuario.UsuarioDAL userDAL = new Usuario.UsuarioDAL();
            // Utilizado para asignar al primer revisor de la lista en caso de 
            //que la tabla RevisionesBP se encuentre vacía
            model.listRevision = getRevisiones();
            if (model.listRevision.Count <= 0)
            {
                foreach (var item in userDAL.getListRevi())
                {
                    return item.nombreUsuario;
                }
            }

           //List<string> revisoresActi;
            int cantiRevisiones = 0;
            string userElegido = "";
            int cantMAX = 0;
            bool primeraVez = true;
            //using (var db = new SeguimientoPermanenciaEntities())
            //{

            //    revisoresActi = (from revi in db.tblUsuario join per in db.tblPerfil on revi.perfil equals per.idPerfil where per.perfil == "Revisor" && revi.estado == true orderby revi.nombreUsuario select revi.nombreUsuario).ToList();
            //}
            
            
            foreach(var item in userDAL.getListRevi())
            {
                
                using (var db=new SeguimientoPermanenciaEntities())
                {
                    cantiRevisiones = (from revi in db.tblRevisionBP join bp in db.tblBuenaPractica on revi.buenaPractica equals bp.idBuenaPractica where revi.usuarioRevisor == item.nombreUsuario && (bp.estado== "REVISANDO" || bp.estado== "CORRIGIENDO") select revi).Count();
                }
                if (cantiRevisiones == 0)
                {
                    userElegido = item.nombreUsuario;
                    break;
                }
                if (primeraVez == true)
                {
                    cantMAX = cantiRevisiones;
                    userElegido = item.nombreUsuario;
                    primeraVez = false;
                }
                else
                {
                    if (cantiRevisiones > cantMAX)
                    {
                        cantMAX = cantiRevisiones;
                       
                    }
                    else
                    {
                        userElegido = item.nombreUsuario;
                    }
                }
            }
            return userElegido;
        }

        //Validar si el ID existe en tblRevision
        /// <summary>
        /// Valida si un IDBP existe en tblRevisionBP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>True=Existe IdBP || False=No existe IdBP</returns>
        public bool existe(int BP)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listRevision = (from r in db.tblRevisionBP where r.buenaPractica == BP select r).ToList();
            }
            if (model.listRevision.Count > 0)
            {
                existe = true;
            }
            return existe;
        }



        /// <summary>
        /// Nuevo registro en tblRevisionBP
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="fechaC"></param>
        /// <returns>True=Registro exitoso || False=Error en registro</returns>
        public bool agregarRevision(int BP, System.DateTime fechaC)
        {
            bool agrego = false;
            try
            {
                if (existe(BP) == false) // Solo se registra si el no existe la BP en la tabla
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblRevisionBP newRev = new tblRevisionBP()
                        {
                            buenaPractica = BP,
                            usuarioRevisor = asignarUsuarioRev(),// Se obtiene al usaurio Revisor encargado de la BP
                            fechaSoliRevi = fechaC,
                         };
                        db.tblRevisionBP.Add(newRev);
                        db.SaveChanges();
                    }
                    agrego = true;
                }

            }
            catch (Exception)
            {
                agrego = false;
              
            }

            return agrego;
        }

        /// <summary>
        /// Actualiza la fechaListaPubli en tblRevisionBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="fechaPublic"></param>
        /// <returns>True=Update exitoso || False=Error update</returns>
        public bool editarFechaListPublicacion(int BP, System.DateTime fechaPublic)
        {

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var fech = (from f in db.tblRevisionBP where f.buenaPractica == BP select f).First<tblRevisionBP>();
                    fech.fechaListaPubli = fechaPublic;
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
        public bool editarRev(int idBP ,string comentarioTituloBP, string comentarioSituacionMejora,string comentarioDiagnosticoRealizo, string comentarioCaracteristicasContexto,string comentarioDescripcionActividadesRealizadas,string comentarioElementoInnovador,string comentarioResultadosObtenidos, string comentarioObservacionesBP, string comentarioFuentesInf, string comentarioGeneral)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var RbueP = (from r in db.tblRevisionBP where r.buenaPractica == idBP select r).First<tblRevisionBP>();
                    RbueP.comentarioTituloBP = comentarioTituloBP;
                    RbueP.comentarioSituacionMejora = comentarioSituacionMejora;
                    RbueP.comentarioDiagnosticoRealizo = comentarioDiagnosticoRealizo;
                    RbueP.comentarioCaracteristicasContexto = comentarioCaracteristicasContexto;
                    RbueP.comentarioDescripcionActividadesRealizadas = comentarioDescripcionActividadesRealizadas;
                    RbueP.comentarioElementoInnovador = comentarioElementoInnovador;
                    RbueP.comentarioResultadosObtenidos = comentarioResultadosObtenidos;
                    RbueP.comentarioObservacionesBP = comentarioObservacionesBP;
                    RbueP.comentarioFuenteInformacion = comentarioFuentesInf;
                    RbueP.comentarioGeneral = comentarioGeneral;
                    db.SaveChanges();
                }
                edito = true;
            }
            catch (Exception ex) { edito = false; throw ex; }
            return edito;
        }

        /// <summary>
        /// Utiliza la clase BPRevisionTable, obtiene los registros de tblRevisionBP join tblBuenaPractica
        /// donde usuarioRevisor == parámetro revisor y estado =="REVISANDO || CORRIGIENDO || LISTO"
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listRevisionTable</returns>
        public List<BPRevisionTable> getTablaRevision(string revisor)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listRevisionTable = (from revi in db.tblRevisionBP join bp in db.tblBuenaPractica on revi.buenaPractica equals bp.idBuenaPractica where revi.usuarioRevisor == revisor && (bp.estado== "REVISANDO" || bp.estado== "CORRIGIENDO" || bp.estado == "LISTO") orderby revi.fechaSoliRevi descending select new BPRevisionTable { titulo = bp.tituloBP, fechaSolicitud = revi.fechaSoliRevi, fechaListaP = revi.fechaListaPubli, estado = bp.estado }).ToList();
            }

            return model.listRevisionTable;
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
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listRevisionTable = (from revi in db.tblRevisionBP join bp in db.tblBuenaPractica on revi.buenaPractica equals bp.idBuenaPractica where bp.tituloBP.Contains(palabraClave) && revi.usuarioRevisor == revisor && (bp.estado == "REVISANDO" || bp.estado == "CORRIGIENDO" || bp.estado == "LISTO") orderby revi.fechaSoliRevi descending select new BPRevisionTable { titulo = bp.tituloBP, fechaSolicitud = revi.fechaSoliRevi, fechaListaP = revi.fechaListaPubli,estado=bp.estado }).ToList();

            }

            return model.listRevisionTable;
        }

        /// <summary>
        /// Obtiene el idRevision de tblRevisionBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.idRevision</returns>
        public int getIdRevisionXIdBP(int BP)
        {
            int idRev=0;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listRevision = (from r in db.tblRevisionBP where r.buenaPractica == BP select r).ToList();
            }
            foreach(var item in model.listRevision)
            {
                idRev = item.idRevision;
            }
            return idRev; ;
        }

    }
}
