using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BusinessModel.Models.ModuloEvento;
using BusinessModel.Persistence.BD;

using BusinessModel.Business.ModuloEvento;
using BusinessModel.Business.Global;

namespace BusinessModel.DataAccess.ModuloEvento
{
    /// <summary>
    /// Clase para el acceso de datos a tblRegistroEvento
    /// </summary>
    public class RegistroEventoDAL
    {
        RegistroEventoML model = new RegistroEventoML();


        ///<summary>
        ///Buscar por folio en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXfolio(int folio)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.folio == folio select re).ToList();
            }
            return model.listaRegistroEvento;
        }

        ///<summary>
        ///Buscar por idEvento en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXidEv(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.idEvento == idEvento select re).ToList();
            }
            return model.listaRegistroEvento;
        }

        ///<summary>
        ///Buscar por idTipoRegistro en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXidTR(string idTR)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.idTipoRegistro == idTR select re).ToList();
            }
            return model.listaRegistroEvento;
        }

        ///<summary>
        ///Buscar por idEvento e idTipoRegistro en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXidEvidTR(int idEvento, string idTR)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.idEvento == idEvento && re.idTipoRegistro == idTR select re).ToList();
            }
            return model.listaRegistroEvento;
        }

        ///<summary>
        ///Insertar Registro en tblRegistroEvento para Registro INDIVIDUAL
        /// </summary>
        public string agregarUserEnalceEve(int idEvento, string idTR, string cct, string nomplantel, string municipio, string regSEG, string nivel, string instisub, string curp, string nom, string ap1, string ap2, string email, string tel, string sexo, string funcion, List<int> listaIDmesa)
        {
            string agrego = "";
            bool error = false;
            bool MT = false;
            CampoEventoDAL cEventoDAL = new CampoEventoDAL();
            MesaParticipanteDAL mpDAL = new MesaParticipanteDAL();
            MesaParticipanteML modelMesaPart = new MesaParticipanteML();

            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        tblRegistroEvento newRegEvento = new tblRegistroEvento();
                        newRegEvento.idEvento = idEvento;
                        newRegEvento.idTipoRegistro = idTR;
                        List<string> listaCampos = cEventoDAL.camposEvento(idEvento);
                        foreach (var item in listaCampos)
                        {
                            model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.idEvento == idEvento && re.CCT == cct select re).ToList();
                            int cant = model.listaRegistroEvento.Count;
                            int cantidadRegistrada = cant + 1;

                            switch (item)
                            {
                                case "CCT":
                                    if (cantidadRegEscuelaCCT(idEvento, cct, cantidadRegistrada) == true)
                                        newRegEvento.CCT = cct;//Si aun acepta la cantidad se agrega
                                    else
                                    {
                                        error = true;
                                        agrego = "Falla en el registro. No hay lugares disponiles para la CCT elegida.";
                                    }
                                    break;
                                case "nombrePlantel":
                                    newRegEvento.nombrePlantel = nomplantel;
                                    break;
                                case "municipio":
                                    newRegEvento.municipio = municipio;
                                    break;
                                case "regionSEG":
                                    newRegEvento.regionSEG = regSEG;
                                    break;
                                case "nivel":
                                    newRegEvento.nivel = nivel;
                                    break;
                                case "instiSubsis":
                                    if (cantidadRegSubsis(idEvento, instisub, cantidadRegistrada) == true)
                                        newRegEvento.instiSubsis = instisub;
                                    else
                                    {
                                        error = true;
                                        agrego = "Falla en el registro. No hay lugares disponiles para la Institución/Subsistema elegido.";
                                    }
                                    break;
                                case "CURP":
                                    newRegEvento.CURP = curp;
                                    break;
                                case "nombre":
                                    newRegEvento.nombre = nom;
                                    break;
                                case "apellidoPat":
                                    newRegEvento.apellidoPat = ap1;
                                    break;
                                case "apellidoMat":
                                    newRegEvento.apellidoMat = ap2;
                                    break;
                                case "correoElectronico":
                                    newRegEvento.correoElectronico = email;
                                    break;
                                case "telefono":
                                    newRegEvento.telefono = tel;
                                    break;
                                case "sexo":
                                    newRegEvento.sexo = sexo;
                                    break;
                                case "funcion":
                                    newRegEvento.funcion = funcion;
                                    break;
                                case "MT":
                                    MT = true;//Existe Mesa de trabajo  
                                    break;

                            }
                        }


                        db.tblRegistroEvento.Add(newRegEvento);
                        db.SaveChanges();

                        int f = newRegEvento.folio;
                        if (MT == true)
                        {
                            //transaction.Commit();
                            if (listaIDmesa.Count > 0)
                            {
                                foreach (var item in listaIDmesa)
                                {
                                    //f = item;
                                    tblMesaTrabParticipante newMesaTPart = new tblMesaTrabParticipante
                                    {
                                        folioParticipante = f,
                                        idMesaTrab = item
                                    };
                                    db.tblMesaTrabParticipante.Add(newMesaTPart);
                                    db.SaveChanges();

                                    int cantidadRegistrada = 0;
                                    modelMesaPart.listaMesaParticipante = (from mtp in db.tblMesaTrabParticipante join r in db.tblRegistroEvento on mtp.folioParticipante equals r.folio where mtp.idMesaTrab == item && r.idEvento == idEvento select mtp).ToList();
                                    cantidadRegistrada = modelMesaPart.listaMesaParticipante.Count;
                                    MesaEventoDAL mesaEdal = new MesaEventoDAL();
                                    int cantidadAceptada = mesaEdal.cantidadAceptadaMesaT(idEvento, item);
                                    if ((cantidadRegistrada <= cantidadAceptada) == false)
                                    {
                                        error = true;
                                        agrego = "No fue posible realizar el registro a la mesa de trabajo, no hay espacio.";
                                        break;
                                    }
                                }
                            }
                        }

                        if (error == true)
                        {

                            transaction.Rollback();
                        }
                        else
                        {
                            agrego = f.ToString();
                            transaction.Commit();
                        }




                    }

                }

            }
            catch (Exception)
            {
                agrego = "false";
            }

            return agrego;
        }

        ///<summary>
        ///Realizar Registro masivo a traves de una transacción con un DataTable
        /// </summary>
        public string registroMasivo(int idEvento, string idTR, DataTable dt)
        {
            string mensaje = "Listo";
            bool error = false;
            bool fallaMT = false;
            MesaParticipanteDAL mpDAL = new MesaParticipanteDAL();
            CampoEventoDAL cEventoDAL = new CampoEventoDAL();
            SubsistemaEventoDAL dal = new SubsistemaEventoDAL();
            MunicipioEventoDAL muniEDAL = new MunicipioEventoDAL();
            MunicipioBL muniBL = new MunicipioBL();
            FuncionParticipanteDAL funcDAL = new FuncionParticipanteDAL();
            List<string[,]> listaFolioIdMT = new List<string[,]>();
            string idMTLista = "";
            List<string> listSubsistemas = new List<string>();
            List<string> listEscuelasCCT = new List<string>();

            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    //using System.Data.Entity; to use DbContextTransaction
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //Comenzar con la inserción de participantes 
                            //Si la cantidad de participantes asignadas por el admin se supera hacer roolback, NOTIFICAR
                            //Validar los tipos de datos a insetar, datos incompatibles; hacer roolback, NOTIFICAR

                            //Obtener datosde DataTable

                            

                            foreach (DataRow row in dt.Rows)//Recorrer renglones de DataTable
                            {
                                tblRegistroEvento newRegEvento = new tblRegistroEvento();

                                List<string> idMesaTrabajoParticipante = new List<string>();
                                newRegEvento.idEvento = idEvento;
                                newRegEvento.idTipoRegistro = idTR;

                                foreach (DataColumn col in dt.Columns)//Recorrer las columnas del renglon en DataTable
                                {
                                    //Llenar la lista con los id de la mesa de trabajo si el campo Mesa de trabajo esta habilitado
                                    if (cEventoDAL.campoHabilitado(idEvento, "mesaTrabajo") == true)
                                    {
                                        if (col.ColumnName.Contains("Mesa de trabajo") == true)//Si la columna contiene Mesa de trabajo
                                        {
                                            string participa = row[col.ColumnName].ToString();//Obtener el valor del renglon NO o SI
                                            if (participa == "SI")
                                            {
                                                string nombreMT = col.ColumnName;
                                                string[] splitCol = nombreMT.Split('-');
                                                if (splitCol.Length != 0)
                                                    nombreMT = splitCol[1].Trim();
                                                //Obtener el id con el nombre de la mesa de trabajo
                                                MesaEventoDAL mesaDAL = new MesaEventoDAL();
                                                idMesaTrabajoParticipante.Add(mesaDAL.getIDmesaXnombreYevento(idEvento, nombreMT).ToString());

                                            }
                                        }
                                    }
                                    //
                                    switch (col.ColumnName)
                                    {
                                        case "CCT":

                                            if (cEventoDAL.campoHabilitado(idEvento, "CCT") == true)
                                            {
                                                if (existeEscuelaCCT(idEvento, row["CCT"].ToString()) == false)
                                                {
                                                    mensaje = "CCT invalida para el registro al evento.";
                                                    error = true;
                                                }
                                                else
                                                {
                                                    newRegEvento.CCT = row["CCT"].ToString();
                                                    listEscuelasCCT.Add(row["CCT"].ToString());
                                                }
                                            }

                                            break;
                                        case "nombrePlantel":
                                            if (cEventoDAL.campoHabilitado(idEvento, "nombrePlantel") == true)
                                                newRegEvento.nombrePlantel = row["nombrePlantel"].ToString();
                                            break;
                                        case "municipio":
                                            //Validar los municipios permitidos con la tabla tblmunicipioEvento
                                            if (cEventoDAL.campoHabilitado(idEvento, "municipio") == true) { 
                                                int idMuni = muniBL.idMunicipio(row["municipio"].ToString());
                                                if (muniEDAL.existeMuniEvento(idEvento, idMuni) == true)
                                                {
                                                    newRegEvento.municipio = row["municipio"].ToString();
                                                }
                                                else
                                                {
                                                    mensaje = "El evento no esta destinado al municipio "+ row["municipio"].ToString();
                                                    error = true;
                                                }
                                            }
                                            break;
                                        case "regionSEG":
                                            if (cEventoDAL.campoHabilitado(idEvento, "regionSEG") == true)
                                                newRegEvento.regionSEG = row["regionSEG"].ToString();
                                            break;
                                        case "nivel":
                                            if (cEventoDAL.campoHabilitado(idEvento, "nivel") == true)
                                                newRegEvento.nivel = row["nivel"].ToString();
                                            break;
                                        case "instiSubsis":
                                            if (cEventoDAL.campoHabilitado(idEvento, "instiSubsis") == true)
                                            {
                                                if (dal.existeSubSis(idEvento, row["instiSubsis"].ToString()) == false)
                                                {
                                                    mensaje = "Subsistema incorrecto, no puede realizar el registro.";
                                                    error = true;
                                                }
                                                else
                                                {
                                                    newRegEvento.instiSubsis = row["instiSubsis"].ToString();
                                                    listSubsistemas.Add(row["instiSubsis"].ToString());
                                                }
                                            }
                                            break;
                                        case "CURP": //Validar CURP, no valido rolback, error==true,mensaje
                                            if (cEventoDAL.campoHabilitado(idEvento, "CURP") == true) {
                                                if (validarCURP(row["CURP"].ToString()) == true)
                                                    newRegEvento.CURP = row["CURP"].ToString();
                                                else
                                                {
                                                    error = true;
                                                    mensaje = "CURP no corresponde con la estructura.";
                                                }
                                            }
                                            break;
                                        case "nombre":
                                            if (cEventoDAL.campoHabilitado(idEvento, "nombre") == true)
                                                newRegEvento.nombre = row["nombre"].ToString();
                                            break;
                                        case "apellidoPat":
                                            if (cEventoDAL.campoHabilitado(idEvento, "apellidoPat") == true)
                                                newRegEvento.apellidoPat = row["apellidoPat"].ToString();
                                            break;
                                        case "apellidoMat":
                                            if (cEventoDAL.campoHabilitado(idEvento, "apellidoMat") == true)
                                                newRegEvento.apellidoMat = row["apellidoMat"].ToString();
                                            break;
                                        case "correoElectronico": //Validar email, no valido rolback, error==true,mensaje
                                            if (cEventoDAL.campoHabilitado(idEvento, "correoElectronico") == true)
                                            {
                                                Regex erEmail = new Regex(@".+\.[a-z]{2,}");
                                                if (erEmail.IsMatch(row["correoElectronico"].ToString()) == true)
                                                {
                                                    newRegEvento.correoElectronico = row["correoElectronico"].ToString();
                                                }
                                                else
                                                {
                                                    error = true;
                                                    mensaje = "Correo eléctronico invalido.";
                                                }
                                            }
                                            break;
                                        case "telefono": //Validar teléfono, no valido rolback, error==true,mensaje
                                            if (cEventoDAL.campoHabilitado(idEvento, "telefono") == true) {
                                                Regex erTel = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
                                                if (erTel.IsMatch(row["telefono"].ToString()) == true)
                                                {
                                                    newRegEvento.telefono = row["telefono"].ToString();
                                                }
                                                else
                                                {
                                                    error = true;
                                                    mensaje = "Télefono invalido.";
                                                }
                                            }
                                            break;
                                        case "sexo":
                                            if (cEventoDAL.campoHabilitado(idEvento, "sexo") == true)
                                                newRegEvento.sexo = row["sexo"].ToString();
                                            break;
                                        case "funcion":
                                            //Validar funcion con tabla tblFuncionParticipante
                                            if (cEventoDAL.campoHabilitado(idEvento, "funcion") == true)
                                            {
                                                if(funcDAL.existeFuncion(idEvento, row["funcion"].ToString()) == true)
                                                {
                                                    newRegEvento.funcion = row["funcion"].ToString();
                                                }
                                                else
                                                {
                                                    error = true;
                                                    mensaje = "El evento no es para "+ row["funcion"].ToString();
                                                }
                                            }

                                            break;
                                    }
                                    if (error == true)
                                        break;
                                }
                                //Al final del renglon se guarda el registro.
                                if (error == true)
                                    break;
                                db.tblRegistroEvento.Add(newRegEvento);
                                db.SaveChanges();

                                //Validar que la mesa de trabajo este habilitada para el evento


                                //Aqui validar INSERT Masivo de Registro a la MESA de trabajo
                                //obtener el folio

                                if (cEventoDAL.campoHabilitado(idEvento, "mesaTrabajo") == true)
                                {
                                    int folio = newRegEvento.folio;
                                    if (idMesaTrabajoParticipante.Count > 0)//Si la lista con los ID de la Mesa de trabajo no esta vacia
                                    {

                                        idMTLista = string.Join(",", idMesaTrabajoParticipante.ToArray());

                                        listaFolioIdMT.Add(new string[,] { { folio.ToString(), idMTLista } });
                                        //if (mpDAL.agregarMesaParticipanteMasivo(folio, idMesaTrabajoParticipante) == false)
                                        //{
                                        //    error = true;
                                        //    string idMTLista = "";
                                        //    foreach (var idlista in idMesaTrabajoParticipante)
                                        //    {
                                        //        idMTLista = idMTLista + " " + idlista;
                                        //    }
                                        //    mensaje = "folio " + folio + " Elementos Lista idMT: "+idMTLista;
                                        //}
                                    }
                                    else
                                    {//Solo se guarda el folio y un sting vacio 
                                        listaFolioIdMT.Add(new string[,] { { folio.ToString(), "" } });
                                    }
                                }

                                //if (listaIDmesa.Count > 0)
                                //{
                                //    int folio = newRegEvento.folio;
                                //    if (mpDAL.agregarMesaParticipanteMasivo(folio, listaIDmesa) == false)
                                //    {
                                //        error = true;
                                //        mensaje = "Error en la inscripción a las mesas de trabajo";
                                //    }
                                //}


                                if (error == true)
                                    break;
                            }
                            if (error == false)
                            {
                                //validar listSubSis y listEscuelaCCT capacidad antes de commit
                                bool aceptaSubsis = true;
                                bool aceptaCCTescuela = true;
                                foreach (var item in listSubsistemas.GroupBy(x => x))
                                {
                                    //Console.WriteLine(item.Key + " encontrado " + item.Count());
                                    string nomSubsis = item.Key;
                                    model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.idEvento == idEvento && re.instiSubsis == nomSubsis select re).ToList();
                                    int cant = model.listaRegistroEvento.Count;
                                    int cantidadRegistrada = cant;//+ item.Count()
                                    aceptaSubsis = cantidadRegSubsis(idEvento, item.Key, cantidadRegistrada);
                                    //mensaje = cant.ToString() + " " + item.Count().ToString() + " ";
                                    if (aceptaSubsis == false)
                                        break;
                                }

                                foreach (var item in listEscuelasCCT.GroupBy(x => x))
                                {
                                    //Console.WriteLine(item.Key + " encontrado " + item.Count());
                                    string escuelaCCT = item.Key;
                                    model.listaRegistroEvento = (from re in db.tblRegistroEvento where re.idEvento == idEvento && re.CCT == escuelaCCT select re).ToList();
                                    int cant = model.listaRegistroEvento.Count;
                                    int cantidadRegistrada = cant;//+ item.Count()
                                    aceptaCCTescuela = cantidadRegEscuelaCCT(idEvento, item.Key, cantidadRegistrada);
                                    //mensaje = cant.ToString() + " " + item.Count().ToString() + " ";
                                    if (aceptaCCTescuela == false)
                                        break;
                                }

                                if (aceptaSubsis == true && aceptaCCTescuela==true)
                                {
                                    transaction.Commit();
                                    if (cEventoDAL.campoHabilitado(idEvento, "mesaTrabajo") == true)
                                    {
                                        if (mpDAL.agregarMesaParticipanteMasivo(listaFolioIdMT, idEvento) == false)//==false
                                        {
                                            //Seccion para eliminar los folios
                                            foreach (var folio in listaFolioIdMT)
                                            {
                                                using (var dbfolio = new SeguimientoPermanenciaEntities())
                                                {
                                                    int folioActual = int.Parse(folio[0, 0]);
                                                    var EliminarFolio = (from r in dbfolio.tblRegistroEvento where r.folio == folioActual select r).First<tblRegistroEvento>();
                                                    dbfolio.tblRegistroEvento.Remove(EliminarFolio);
                                                    dbfolio.SaveChanges();
                                                }

                                            }
                                            fallaMT = true;
                                            error = true;
                                            mensaje = "Error en la inscripción a las mesas de trabajo";

                                            //break;
                                        }
                                    }
                                }
                                else
                                {
                                    transaction.Rollback();
                                    string campo = "";
                                    if (aceptaSubsis == false)
                                        campo = "SubSistemas";
                                    else
                                        campo = "Escuela";
                                    mensaje = "Ya no se aceptan más registros para el "+campo+" seleccionado.";
                                }
                                
                                

                            }

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            mensaje = "Error "+ex.Message;
                        }
                        if (error == true && fallaMT == false)
                            transaction.Rollback();

                    }
                }

            }
            catch (Exception ex)
            {
                mensaje = "Error "+ex.Message;

            }



            return mensaje;

            
        }

        /// <summary>
        /// Validar CURP
        /// </summary>
        /// <param name="CURP"></param>
        /// <returns>bool valido</returns>
        private bool validarCURP(string CURP)
        {
            bool valido = true;
            var er = "[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                     "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                     "[HM]{1}" +
                     "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                     "[B-DF-HJ-NP-TV-Z]{3}" +
                     "[0-9A-Z]{1}[0-9]{1}$";
            Regex erCURP = new Regex(er);

            valido = erCURP.IsMatch(CURP);
            int numero;
            if (valido == true)
            {
                //Comprobar los caracteres
                int fn =int.Parse(CURP.Substring(4, 2));
                string letraOnumero = CURP.Substring(16, 1);
                if(fn>50 && fn <= 99)
                {
                    if (int.TryParse(letraOnumero, out numero))
                        valido = true;
                    else
                        valido = false;
                    
                }else if(fn>=00 && fn < 50)
                {
                    if (int.TryParse(letraOnumero, out numero))
                        valido = false;
                    else
                        valido = true;
                }
                    
            }

            return valido;
        }


       
        private bool cantidadRegSubsis(int idEvento, string nomSubsis,int cantidadTransaccion)
        {
            bool aceptado = false;
            int cantidadRegistrada = cantidadTransaccion;
            SubsistemaEventoDAL dalSubSis = new SubsistemaEventoDAL();
            int cantidadAceptada = dalSubSis.cantidadAceptada(idEvento, nomSubsis);

            if (cantidadRegistrada <= cantidadAceptada)
                aceptado = true;

            return aceptado;
        }

        

        private bool existeEscuelaCCT(int idEvento,string CCT)
        {
            bool existe = false;
            int idCEO = 0;
            try
            {
                EscuelaEventoDAL dalEE = new EscuelaEventoDAL();
                //obtener el id de la CCT por CCT
                CEOBL blCEO = new CEOBL();
                foreach(var ceo in blCEO.getCEO(CCT))
                {
                    idCEO = ceo.idCEO;
                    break;
                }
                //
                 existe = dalEE.existeEscuelaCCT(idEvento, idCEO);
                
            }
            catch
            {
                existe = false;
            }
            
            
            return existe;
        }

        private bool cantidadRegEscuelaCCT(int idEvento, string CCT,int cantidadTransaccion)
        {
            int cantidadRegistrada = cantidadTransaccion;
            bool aceptado = false;
            int idCEO = 0;
            EscuelaEventoDAL dalEE = new EscuelaEventoDAL();
            //obtener el id de la CCT por CCT
            CEOBL blCEO = new CEOBL();
            foreach (var ceo in blCEO.getCEO(CCT))
            {
                idCEO = ceo.idCEO;
                break;
            }
            int cantidadAceptada = dalEE.cantidadAceptadaEscuela(idEvento, idCEO);
            if (cantidadRegistrada <= cantidadAceptada)
                aceptado = true;

            return aceptado;
        }

        
    }
}
