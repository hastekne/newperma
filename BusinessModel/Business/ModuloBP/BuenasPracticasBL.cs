using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.DataAccess.ModuloBP;
using BusinessModel.Models.ModuloBP;

namespace BusinessModel.Business.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblBuenaPractica
    /// </summary>
    public class BuenasPracticasBL
    {
        BuenasPracticasDAL bpDAL = new BuenasPracticasDAL();

        /// <summary>
        /// Obtiene todos los registros en tblBuenaPractica
        /// </summary>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBP()
        {
            try
            {
                return bpDAL.getBP();
            }catch(Exception ex) { Console.Write(ex.Message); throw; }
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
            try
            {
                return bpDAL.getBPMEstadoColaborador(colaborador);
            }catch(Exception ex) { Console.Write(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registros de tblBuenaPractica join tblEje
        /// donde usuarioColaborador = parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPNoFK(string colaborador)
        {
            try
            {
                return bpDAL.getBPNoFK(colaborador);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }


        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registros de tblBuenaPractica join tblEje
        /// donde usuarioColaborador = parámetro colaborador y nombreEje= parámetro nomEje y titulo contiene parámetro palabraClave
        /// </summary>
        /// <param name="nomEje"></param>
        /// <param name="palabraClave"></param>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPInfoEjePC(string nomEje, string palabraClave, string colaborador)
        {
            try
            {
                return bpDAL.getBPInfoEjePC(nomEje, palabraClave, colaborador);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return bpDAL.getBPInfoEje(nomEje,colaborador);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase BPHomeTable, Obtiene los registros de tblBuenaPractica join tblEje join tblRevisionBP
        /// donde el estado = 'PUBLICADO'
        /// </summary>
        /// <returns>model.listaHomeBP</returns>
        public List<BPHomeTable> getListHomeBP()
        {
            try
            {
                return bpDAL.getListHomeBP();
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase BPHomeTable, Obtiene los registros de tblBuenaPractica join tblEje join tblRevisionBP
        /// donde el estado = 'PUBLICADO' y tituloBP contiene parámetro palabraClave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns>model.listaHomeBP</returns>
        public List<BPHomeTable> getListHomeBP(string palabraClave)
        {
            try
            {
                return bpDAL.getListHomeBP(palabraClave);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
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
            try
            {
                return bpDAL.getListHomeBP(palabraClave,nomEje);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
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
            try
            {
                return bpDAL.getBPInfoPC(palabraClave,colaborador);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase BPInformacion, obtiene los registro de tblBuenaPractica join tblEje donde  
        /// usuarioColaborador==parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBPNoFK</returns>
        public List<BPInformacion> getBPNoFKInfo(string colaborador)
        {
            try
            {
                return bpDAL.getBPNoFKInfo(colaborador);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
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
            try
            {
                return bpDAL.getBP(palabraClave,estado);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        //Para realizar la busqueda con base en el ID Eje
        /// <summary>
        /// Obtiene los registros en tblBuenaPractica donde eje == parámetro idEje y 
        /// estado == parámetro estado
        /// </summary>
        /// <param name="idEje"></param>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBP(int idEje,string estado)
        {
            try
            {
                return bpDAL.getBP(idEje,estado);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }


        //Obtener un registro de BP por el ID
        /// <summary>
        /// Obtiene los registros de tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXID(int idBP)
        {
            try
            {
                return bpDAL.getBPXID(idBP);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        //Para obtener el estado de la BP
        /// <summary>
        /// Obtiene el estado de un registro en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>string model.estado</returns>
        public string estadoBP(int idBP)
        {
            try
            {
                return bpDAL.estadoBP(idBP);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
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
            try
            {
                return bpDAL.agregarBP(eje,usuarioColaborador,municipio,idCEO,tituloBP,idFuncionD,estado);
              //  (1, 'GUARDADO', 1, 1, 1, 'Titulo BP', 'CeciMP', 1)
                //return bpDAL.agregarBP(1, "CeciMP", 1, 1, "Titulo BP", 1, "GUARDADO");
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        /// <summary>
        /// Para obtener falso o verdadero si el tituo existe usando el parámetro titulo
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns>TRUE==Titulo existo || FALSE==Titulo no existe</returns>
        public bool existeTitulo(string titulo)
        {
            try
            {
                return bpDAL.existeTitulo(titulo);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Para obtener True o false si existe el titulo en tblBuenaPractica donde tituloBP == parámetro titulo
        /// y idBuenaPractica == práctica BP
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="BP"></param>
        /// <returns>TRUE==Titulo existo || FALSE==Titulo no existe</returns>
        public bool existeTitulo(string titulo, int BP)
        {
            try
            {
                return bpDAL.existeTitulo(titulo,BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el idBuenaPractica de tblBuenaPractica donde tituloBP == parámetro titulo
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns>model.idBuenaPractica</returns>
        public int getIdBP(string titulo)
        {
            try
            {
                return bpDAL.getIdBP(titulo);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); throw;  }
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
        public bool editarBP(int idBP, string tituloBP, string situacionMejora, string diagnosticoRealizo, string caracteristicasContexto, string descripcionActividadesRealizadas, string elementoInnovador, string resultadosObtenidos, string observacionesBP, string fuentesInformacion, string estado)

        {
            try
            {
                return bpDAL.editarBP(idBP, tituloBP, situacionMejora, diagnosticoRealizo, caracteristicasContexto, descripcionActividadesRealizadas, elementoInnovador, resultadosObtenidos, observacionesBP, fuentesInformacion,  estado);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el registro en tblBuenaPractica donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXestado(string estado)
        {
            try
            {
                return bpDAL.getBPXestado(estado);
            }
            catch (Exception ex) { Console.Write(ex.Message); throw; }
        }

        //Editar Plantilla de la BP
        /// <summary>
        /// Edita idPlantilla en tblBuenaPractica donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="idPlantilla"></param>
        /// <returns>TRUE==Update exitoso || FALSE== Falla en Update</returns>
        public bool editarBP(int idBP, int idPlantilla)
        {
            try
            {
                return bpDAL.editarBP(idBP, idPlantilla);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el int idPlantilla de un registro por el idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.idPlantilla</returns>
        public int idPlantillaBP(int idBP)
        {
            try
            {
                return bpDAL.idPlantillaBP(idBP);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
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
        public bool editarBP(int idBP, int eje, int municipio, int idFuncionD, int idCEO)
        {
            try
            {
                return bpDAL.editarBP(idBP, eje, municipio, idFuncionD, idCEO);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene registros de tblBuenaPractica donde usuarioColaborador == parámetro colaborador
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXcolaborador(string colaborador)
        {
            try
            {
                return bpDAL.getBPXcolaborador(colaborador);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblBuenaPractica join tblRevisionBP donde 
        /// usuarioRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getBPXrevisor(string revisor)
        {
            try
            {
                return bpDAL.getBPXrevisor(revisor);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
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
            try
            {
                return bpDAL.getBPXcolaborador(colaborador, estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return bpDAL.getBPXrevisor(revisor, estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el registro de tblBuenaPractica join tblRevisionBP donde estado == 'REVISANDO'
        /// y usuarioRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listaBP</returns>
        public List<tblBuenaPractica> getRevisionBP(string revisor)
        {
            try
            {
                return bpDAL.getRevisionBP(revisor);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Usa la clase DatosGeneralesBP, obtiene el registro de tblBuenaPractica join tblEje join tblCEO join tblMunicipio
        /// join tblFuncionDesarrolloP donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.datosGeneralesBP</returns>
        public List<DatosGeneralesBP> DatosGeneralesBP(int idBP)
        {
            try
            {
                return bpDAL.DatosGeneralesBP(idBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase AutorBP, obtiene los registros de tblBuenaPractica join tblUsuario
        /// donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.autorResponsableBP</returns>
        public List<AutorBP> AutorResponsableBP(int idBP)
        {
            try
            {
                return bpDAL.AutorResponsableBP(idBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase AutorBP, obtiene los registros de los autores de la buena practica en tblBuenaPractica join tblAutores
        /// donde idBuenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <returns>model.autoresBP</returns>
        public List<AutorBP> AutoresBP(int idBP)
        {
            try
            {
                return bpDAL.AutoresBP(idBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return bpDAL.editarEstadoBP(idBP,estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
