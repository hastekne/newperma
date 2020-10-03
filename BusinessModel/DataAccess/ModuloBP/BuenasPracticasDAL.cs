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
    /// Clase para el acceso de datos a tblBuenaPractica
    /// </summary>
    public class BuenasPracticasDAL
    {
        BuenasPracticasML model = new BuenasPracticasML();

        /// <summary>
        /// Obtiene todos los registros en tblBuenaPractica
        /// </summary>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBP()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaBP = (from bp in db.tblBuenaPractica select bp).ToList();
            }
            return model.listaBP;
        }

        //Para realizar la buscado con base a una palabra clave
        /// <summary>
        /// Obtiene los registros en tblBuenaPractica donde tituloBP contiene el parámetro palabraClave
        /// y estado = párametro estado
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBP(string palabraClave, string estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaBP = (from bp in db.tblBuenaPractica where bp.tituloBP.Contains(palabraClave) && bp.estado==estado select bp).ToList();
            }
            return model.listaBP;
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registros de tblBuenaPractica join tblEje
        /// donde usuarioColaborador = parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPNoFK(string colaborador)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaBPNoFK = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje where bp.usuarioColaborador == colaborador select new BPInformacion { titulo=bp.tituloBP, eje=ej.nombre,estado=bp.estado}).ToList();
                
            }
            
            return model.listaBPNoFK;
        }

        /// <summary>
        /// Utiliza la clase BPHomeTable, Obtiene los registros de tblBuenaPractica join tblEje join tblRevisionBP
        /// donde el estado = 'PUBLICADO'
        /// </summary>
        /// <returns>model.listaHomeBP</returns>
        public List<BPHomeTable> getListHomeBP()
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaHomeBP = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje join rev in db.tblRevisionBP on bp.idBuenaPractica equals rev.buenaPractica where bp.estado== "PUBLICADO" orderby ej.nombre ascending select new BPHomeTable { idBP = bp.idBuenaPractica, tituloBP = bp.tituloBP, eje = ej.nombre, idPantilla = bp.idPlantilla, fechaPublicacion = rev.fechaListaPubli }).ToList();
            }
            return model.listaHomeBP;
        }

        /// <summary>
        /// Utiliza la clase BPHomeTable, Obtiene los registros de tblBuenaPractica join tblEje join tblRevisionBP
        /// donde el estado = 'PUBLICADO' y tituloBP contiene parámetro palabraClave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns>model.listaHomeBP</returns>
        public List<BPHomeTable> getListHomeBP(string palabraClave)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaHomeBP = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje join rev in db.tblRevisionBP on bp.idBuenaPractica equals rev.buenaPractica where bp.estado == "PUBLICADO" && bp.tituloBP.Contains(palabraClave) orderby ej.nombre ascending select new BPHomeTable { idBP = bp.idBuenaPractica, tituloBP = bp.tituloBP, eje = ej.nombre, idPantilla = bp.idPlantilla, fechaPublicacion = rev.fechaListaPubli }).ToList();
            }
            return model.listaHomeBP;
        }

        /// <summary>
        /// Utiliza la clase BPHomeTable, Obtiene los registros de tblBuenaPractica join tblEje join tblRevisionBP
        /// donde el estado = 'PUBLICADO' y tituloBP contiene parámetro palabraClave y nomrbeEje = parámetro nomEje
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <param name="nomEje"></param>
        /// <returns>model.listaHomeBP</returns>
        public List<BPHomeTable> getListHomeBP(string palabraClave, string nomEje)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaHomeBP = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje join rev in db.tblRevisionBP on bp.idBuenaPractica equals rev.buenaPractica where bp.estado == "PUBLICADO" && bp.tituloBP.Contains(palabraClave) && ej.nombre==nomEje orderby ej.nombre ascending select new BPHomeTable { idBP = bp.idBuenaPractica, tituloBP = bp.tituloBP, eje = ej.nombre, idPantilla = bp.idPlantilla, fechaPublicacion = rev.fechaListaPubli }).ToList();
            }
            return model.listaHomeBP;
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registros de tblBuenaPractica join tblEje
        /// donde usuarioColaborador = parámetro colaborador y nombreEje= parámetro nomEje
        /// </summary>
        /// <param name="nomEje"></param>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPInfoEje(string nomEje,string colaborador)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBPNoFK = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje where ej.nombre == nomEje && bp.usuarioColaborador == colaborador orderby bp.idBuenaPractica descending select new BPInformacion { titulo = bp.tituloBP, eje = ej.nombre, estado = bp.estado }).ToList();

            }

            return model.listaBPNoFK;
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registros de tblBuenaPractica join tblEje
        /// donde usuarioColaborador = parámetro colaborador y nombreEje= parámetro nomEje y titulo contiene parámetro palabraClave
        /// </summary>
        /// <param name="nomEje"></param>
        /// <param name="palabraClave"></param>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPInfoEjePC(string nomEje,string palabraClave, string colaborador)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBPNoFK = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje where ej.nombre == nomEje && bp.usuarioColaborador == colaborador && bp.tituloBP.Contains(palabraClave) orderby bp.idBuenaPractica descending select new BPInformacion { titulo = bp.tituloBP, eje = ej.nombre, estado = bp.estado }).ToList();

            }

            return model.listaBPNoFK;
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registro de tblBuenaPractica join tblEje donde  
        /// tituloBP contiene parámetro palabraClave y usuarioColaborador==parámetro colaborador
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPInfoPC(string palabraClave, string colaborador)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBPNoFK = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje where bp.tituloBP.Contains(palabraClave) && bp.usuarioColaborador == colaborador orderby bp.idBuenaPractica descending select new BPInformacion { titulo = bp.tituloBP, eje = ej.nombre, estado = bp.estado }).ToList();

            }

            return model.listaBPNoFK;
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registro de tblBuenaPractica join tblEje donde  
        /// usuarioColaborador==parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>m.listaBPNoFK</returns>
        public List<BPInformacion> getBPNoFKInfo(string colaborador)
        {
            BPInformacion m = new BPInformacion();
            using (var db = new SeguimientoPermanenciaEntities())
            {
                m.listaInfo = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje where bp.usuarioColaborador == colaborador orderby bp.idBuenaPractica descending select new BPInformacion { titulo = bp.tituloBP, eje = ej.nombre, estado = bp.estado }).ToList();

            }

            return m.listaInfo;
        }

        // Para seleccionar las BP por los estados Guardado, Corigiendo y lista
        /// <summary>
        /// Obtiene los registros de tblBuenaPractica donde estado == "GUARDADO || LISTO || CORRIGIENDO"
        /// y usuarioColaborador == parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPMEstadoColaborador(string colaborador)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica where (bp.estado == "GUARDADO" || bp.estado == "LISTO" || bp.estado == "CORRIGIENDO") && bp.usuarioColaborador == colaborador select bp).ToList();
            }
                return model.listaBP;
        }

        //Para realizar la busqueda con base en el ID Eje
        /// <summary>
        /// Obtiene los registros en tblBuenaPractica donde eje == parámetro idEje y 
        /// estado == parámetro estado
        /// </summary>
        /// <param name="idEje"></param>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBP(int idEje, string estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaBP = (from bp in db.tblBuenaPractica where bp.eje==idEje && bp.estado==estado select bp).ToList();
            }
            return model.listaBP;
        }

        //Obtener un registro de BP por el ID
        /// <summary>
        /// Obtiene los registros de tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXID(int idBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaBP = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP select bp).ToList();
            }
            return model.listaBP;
        }

        /// <summary>
        /// Obtiene el int idPlantilla de un registro por el idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.idPlantilla</returns>
        public int idPlantillaBP(int idBP)
        {
            using(var db=new SeguimientoPermanenciaEntities())
            {
                //model.idPlantilla=Int32.Parse((from bp in db.tblBuenaPractica where bp.idBuenaPractica==idBP select bp.idPlantilla).ToString());
                model.listaBP = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP select bp).ToList();
                foreach(var item in model.listaBP)
                {
                    model.idPlantilla = item.idPlantilla;
                }
            }
            return model.idPlantilla;
        }

        //Para obtener el estado de la BP
        /// <summary>
        /// Obtiene el estado de un registro en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>string model.estado</returns>
        public string estadoBP(int idBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.estado = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP select bp.estado).ToString();
            }
            return model.estado;
        }

        /// <summary>
        /// Para obtener falso o verdadero si el tituo existe usando el parámetro titulo
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns>TRUE==Titulo existo || FALSE==Titulo no existe</returns>
        public bool existeTitulo(string titulo)
        {
            bool existe = false;
            using(var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica where bp.tituloBP == titulo select bp).ToList();
            }
            if (model.listaBP.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Para obtener True o false si existe el titulo en tblBuenaPractica donde tituloBP == parámetro titulo
        /// y idBuenaPractica == práctica BP
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="BP"></param>
        /// <returns>TRUE==Titulo existo || FALSE==Titulo no existe</returns>
        public bool existeTitulo(string titulo,int BP)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica where bp.tituloBP == titulo && bp.idBuenaPractica!=BP select bp).ToList();
            }
            if (model.listaBP.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Obtiene el idBuenaPractica de tblBuenaPractica donde tituloBP == parámetro titulo
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns>model.idBuenaPractica</returns>
        public int getIdBP(string titulo)
        {
            using(var db=new SeguimientoPermanenciaEntities())
            {
                //    model.idBuenaPractica = Int32.Parse((from bp in db.tblBuenaPractica where bp.tituloBP == titulo select bp.idBuenaPractica).ToString());
                model.listaBP = (from bp in db.tblBuenaPractica where bp.tituloBP == titulo select bp).ToList();
                foreach(var item in model.listaBP)
                {
                    model.idBuenaPractica = item.idBuenaPractica;
                }
            }
            return model.idBuenaPractica;
        }


        //Agregar una Buena Práctica
        /// <summary>
        /// Registrar BP en tblBuenaPractica, valida que el titulo no exista.
        /// </summary>
        /// <param name="eje"></param>
        /// <param name="usuarioColaborador"></param>
        /// <param name="municipio"></param>
        /// <param name="idCEO"></param>
        /// <param name="tituloBP"></param>
        /// <param name="idFuncionD"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Falla en el registro</returns>
        public bool agregarBP(int eje, string usuarioColaborador, int municipio, int idCEO, string tituloBP, int idFuncionD, string estado)
        {
            bool agrego = false;
            try
            {
                if (existeTitulo(tituloBP) == false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblBuenaPractica bp = new tblBuenaPractica()
                        {
                            eje = eje,
                            usuarioColaborador = usuarioColaborador,
                            municipio = municipio,
                            idCEO = idCEO,
                            tituloBP =tituloBP,
                            idFuncionD = idFuncionD,
                            estado = estado,
                            idPlantilla = 1,
                           };
                        db.tblBuenaPractica.Add(bp);
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

        //Editar Plantilla de la BP
        /// <summary>
        /// Edita idPlantilla en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="idPlantilla"></param>
        /// <returns>TRUE==Update exitoso || FALSE== Falla en Update</returns>
        public bool editarBP(int idBP,int idPlantilla)
        {
            bool edito = false;
            try
            {
                using(var db=new SeguimientoPermanenciaEntities())
                {
                    var bueP = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP && bp.estado=="Lista" select bp).First<tblBuenaPractica>();
                    bueP.idPlantilla = idPlantilla;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false;}
             return edito;
        }

        //Editar Plantilla de la BP
        /// <summary>
        /// Actualiza el estado en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE==Update exitoso || FALSE==Falla en Update</returns>
        public bool editarEstadoBP(int idBP, string estado)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var bueP = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP select bp).First<tblBuenaPractica>();
                    bueP.estado = estado;
                    db.SaveChanges();

                }
                edito = true;
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        //Editar una buena practica parte de la documentacion
        /// <summary>
        /// Actualización de registro en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="tituloBP"></param>
        /// <param name="situacionMejora"></param>
        /// <param name="diagnosticoRealizo"></param>
        /// <param name="caracteristicasContexto"></param>
        /// <param name="descripcionActividadesRealizadas"></param>
        /// <param name="elementoInnovador"></param>
        /// <param name="resultadosObtenidos"></param>
        /// <param name="observacionesBP"></param>
        /// <param name="fuentesInformacion"></param>
        /// <param name="estado"></param>
        /// <returns>TRUE==Update exitoso || FALSE==Falla en Update</returns>
        public bool editarBP(int idBP,string tituloBP, string situacionMejora, string diagnosticoRealizo, string caracteristicasContexto, string descripcionActividadesRealizadas, string elementoInnovador, string resultadosObtenidos, string observacionesBP, string fuentesInformacion,string estado)
        {
            bool edito = false;
            try
            {
                //if (existeTitulo(tituloBP, idBP) == false)
                //{
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        var bueP = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP select bp).First<tblBuenaPractica>();
                        bueP.tituloBP = tituloBP;
                        bueP.situacionMejora = situacionMejora;
                        bueP.diagnosticoRealizo = diagnosticoRealizo;
                        bueP.caracteristicasContexto = caracteristicasContexto;
                        bueP.descripcionActividadesRealizadas = descripcionActividadesRealizadas;
                        bueP.elementoInnovador = elementoInnovador;
                        bueP.resultadosObtenidos = resultadosObtenidos;
                        bueP.observacionesBP = observacionesBP;
                        bueP.fuentesInformacion = fuentesInformacion;
                        bueP.estado = estado;
                        db.SaveChanges();
                    }
                    edito = true;
                //}
                
            }
            catch (Exception ex)
            {
                edito = false;
                throw ex;
            }

            return edito;
        }

        /// <summary>
        /// Actualización de eje, minicipio, diFuncionD e idCEO en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="eje"></param>
        /// <param name="municipio"></param>
        /// <param name="idFuncionD"></param>
        /// <param name="idCEO"></param>
        /// <returns>TRUE==Update exitoso || FALSE==Falla en Update</returns>
        public bool editarBP(int idBP, int eje, int municipio, int idFuncionD,int idCEO)
        {
            bool edito = false;
            try
            {
                using (var db = new  SeguimientoPermanenciaEntities())
                {
                    var bueP = (from bp in db.tblBuenaPractica where bp.idBuenaPractica == idBP select bp).First<tblBuenaPractica>();
                    bueP.eje = eje;
                    bueP.municipio = municipio;
                    bueP.idFuncionD = idFuncionD;
                    bueP.idCEO = idCEO;
                    db.SaveChanges();
                }
                edito = true;
            }catch(Exception) { edito = false;}
            return edito;
        }

        /// <summary>
        /// Obtiene el registro en tblBuenaPractica donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXestado(string estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaBP = (from bp in db.tblBuenaPractica where bp.estado==estado select bp).ToList();
            }
            return model.listaBP;
        }

        /// <summary>
        /// Obtiene registros de tblBuenaPractica donde usuarioColaborador == parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXcolaborador(string colaborador)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica where bp.usuarioColaborador == colaborador select bp).ToList();
            }

            return model.listaBP;
        }

        /// <summary>
        /// Obtiene los registros en tblBuenaPractica donde usuarioColaborador == parámetro colaborador
        /// y estado == parámetro estado
        /// </summary>
        /// <param name="colaborador"></param>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXcolaborador(string colaborador, string estado)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica where bp.usuarioColaborador == colaborador && bp.estado == estado select bp).ToList();
            }

            return model.listaBP;
        }

        /// <summary>
        /// Obtiene los registros de tblBuenaPractica join tblRevisionBP donde 
        /// usuarioRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXrevisor(string revisor)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica join rbp in db.tblRevisionBP on bp.idBuenaPractica equals rbp.buenaPractica where rbp.usuarioRevisor == revisor select bp).ToList();
            }
            return model.listaBP;
        }

        /// <summary>
        /// Obtiene los registros de tblBuenaPractica join tblRevisionBP donde 
        /// usuarioRevisor == parámetro revisor y estado == parámetro estado
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXrevisor(string revisor, string estado)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica join rbp in db.tblRevisionBP on bp.idBuenaPractica equals rbp.buenaPractica where rbp.usuarioRevisor == revisor && bp.estado == estado select bp).ToList();
            }
            return model.listaBP;
        }

        /// <summary>
        /// Obtiene el registro de tblBuenaPractica join tblRevisionBP donde estado == 'REVISANDO'
        /// y usuarioRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getRevisionBP(string revisor)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaBP = (from bp in db.tblBuenaPractica join revi in db.tblRevisionBP on bp.idBuenaPractica equals revi.buenaPractica where bp.estado == "REVISANDO" && revi.usuarioRevisor == revisor select bp).ToList();
            }
            return model.listaBP;
        }

        /// <summary>
        /// Usa la clase DatosGeneralesBP, obtiene el registro de tblBuenaPractica join tblEje join tblCEO join tblMunicipio
        /// join tblFuncionDesarrolloP donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.datosGeneralesBP</returns>
        public List<DatosGeneralesBP> DatosGeneralesBP(int idBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.datosGeneralesBP = (from bp in db.tblBuenaPractica join ej in db.tblEje on bp.eje equals ej.idEje join ct in db.tblCEO on bp.idCEO equals ct.idCEO join mun in db.tblMunicipio on bp.municipio equals mun.idMunicipio join fun in db.tblFuncionDesarrolloP on bp.idFuncionD equals fun.idFuncionD where bp.idBuenaPractica == idBP select new DatosGeneralesBP {  eje = ej.nombre, claveCT=ct.claveCentroTrabajo,nombreCT=ct.nombreCentroTrabajo,municipio=mun.nombreMunicipio,funcion=fun.nombreFunc}).ToList();

            }

            return model.datosGeneralesBP;
        }

        /// <summary>
        /// Utiliza la clase AutorBP, obtiene los registros de tblBuenaPractica join tblUsuario
        /// donde idBuenaPractica == parámetro idBP para los autores Responsables
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.autorResponsableBP</returns>
        public List<AutorBP> AutorResponsableBP(int idBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.autorResponsableBP = (from bp in db.tblBuenaPractica join usu in db.tblUsuario on bp.usuarioColaborador equals usu.nombreUsuario where bp.idBuenaPractica == idBP select new AutorBP { nombreCompleto=usu.nombre+" "+usu.pApellido+" "+usu.sApellido,telefono=usu.telefono,email=usu.correoElectronico}).ToList();

            }

            return model.autorResponsableBP;
        }

        /// <summary>
        /// Utiliza la clase AutorBP, obtiene los registros de los autores de la buena practica en tblBuenaPractica join tblAutores
        /// donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.autoresBP</returns>
        public List<AutorBP> AutoresBP(int idBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.autoresBP = (from bp in db.tblBuenaPractica join aut in db.tblAutores on bp.idBuenaPractica equals aut.buenaPractica where bp.idBuenaPractica == idBP select new AutorBP { nombreCompleto = aut.nombreA + " " + aut.pApellidoA + " " + aut.mApellidoA, telefono = aut.telefonoA, email = aut.correoElectronicoA }).ToList();

            }

            return model.autoresBP;
        }

    }
}
