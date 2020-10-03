using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Models.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.ModuloEvento
{
    /// <summary>
    /// Clase para el acceso de datos a tblMesaTrabParticipante
    /// </summary>
    public class MesaParticipanteDAL
    {
        MesaParticipanteML model = new MesaParticipanteML();

        ///<summary>
        ///Buscar por idMesaTrabParti en tblMesaTrabParticipante
        /// </summary>
        public List<tblMesaTrabParticipante> getMesaPartiXidmesa(int idMesa)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesaParticipante = (from mtp in db.tblMesaTrabParticipante where mtp.idMesaTrabParti == idMesa select mtp).ToList();
            }
            return model.listaMesaParticipante;
        }

        ///<summary>
        ///Buscar por folio participante en tblMesaTrabParticipante
        /// </summary>
        public List<tblMesaTrabParticipante> getMesaPartiXfolio(int folio)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesaParticipante = (from mtp in db.tblMesaTrabParticipante where mtp.folioParticipante == folio select mtp).ToList();
            }
            return model.listaMesaParticipante;
        }

        ///<summary>
        ///Insertar registro en tblMesaTrabParticipante
        /// </summary>
        public bool agregarMesaParticipante(int folio, int idmesaEvento)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblMesaTrabParticipante newMesaTPart = new tblMesaTrabParticipante()
                    {
                        folioParticipante = folio,
                        idMesaTrab = idmesaEvento,
                    };
                    db.tblMesaTrabParticipante.Add(newMesaTPart);
                    db.SaveChanges();
                }
                agrego = true;
            }
            catch (Exception)
            {
                agrego = false;
            }

            return agrego;
        }
        
        //Validar El total de Participantes registrados a una mesa de trabajo comparandolo con el total de participantes permitidos por mesa de trabajo
        private bool validarRegistroMesaT(int idEvento,int idMT)
        {
            bool aceptado = true;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesaParticipante = (from mtp in db.tblMesaTrabParticipante join r in db.tblRegistroEvento on mtp.folioParticipante equals r.folio where mtp.idMesaTrab == idMT && r.idEvento==idEvento select mtp).ToList();
            }
            MesaEventoDAL mesaEdal = new MesaEventoDAL();
            
            int cantidadAceptada = mesaEdal.cantidadAceptadaMesaT(idEvento, idMT);
            int cantidadRegistrada = model.listaMesaParticipante.Count;

            if (cantidadRegistrada == 0)
                return aceptado;
            if (cantidadRegistrada > cantidadAceptada)
            {
                aceptado = false;
            }

            return aceptado;
        }

        /// <summary>
        /// Inscripción a una mesa de trabajo a través de la carga masiva
        /// Utilizando una lista con id de mesas de trabajo a las cual se inscribe 
        /// </summary>
        /// <returns></returns>
        public bool agregarMesaParticipanteMasivo(int folio, List<int> listaIDmesa)
        {
            bool agrego = true;
            int idEvento = 0;
            RegistroEventoDAL dalReg = new RegistroEventoDAL();
            MesaEventoDAL dalMesaE = new MesaEventoDAL();
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            tblMesaTrabParticipante newMesaTPart = new tblMesaTrabParticipante();
                            foreach(var itemL in listaIDmesa)
                            {
                                newMesaTPart.folioParticipante = folio;
                                newMesaTPart.idMesaTrab = itemL;
                                db.tblMesaTrabParticipante.Add(newMesaTPart);
                                db.SaveChanges();

                                //validar cantidad permitida de participantes en la mesa de trabajo
                                foreach(var item in dalReg.getRegistroEXfolio(folio))
                                {
                                    idEvento = item.idEvento;
                                }
                                if (validarRegistroMesaT(idEvento, itemL) == false)
                                {
                                    agrego = false;
                                    transaction.Rollback();
                                    break;
                                }

                            }
                            if (agrego == true)
                                transaction.Commit();
                        }
                        catch
                        {
                            agrego = false;
                            transaction.Rollback();
                        }
                    }

                }
                
            }
            catch (Exception)
            {
                agrego = false;
            }

            return agrego;
        }

        //Para registrar la mesa de trabajo del participante
        /// <summary>
        /// Nuevo registro en tblMesaTrabParticipante
        /// Es una transacción utilizada para el registro masivo
        /// Obtiene el folio y el idMesa de la lista 'listaFolioIdMT'
        /// Valida las cantidades aceptadas por registro a cada mesa de trabajo, en caso de no
        /// haber lugar para registrar a la mesa de trabajo se realiza un RollBack
        /// </summary>
        /// <param name="listaFolioIdMT"></param>
        /// <param name="idEvento"></param>
        /// <returns>true=Registro Exitoso || false=Error en el registro</returns>
        public bool agregarMesaParticipanteMasivo(List<string[,]> listaFolioIdMT,int idEvento)//int folio, string listaIDmesa
        {
            bool agrego = true;
            //string retorno = "";
            //int idEvento = 1;
            RegistroEventoDAL dalReg = new RegistroEventoDAL();
            MesaEventoDAL dalMesaE = new MesaEventoDAL();
            //MesaEventoDAL mesaEdal = new MesaEventoDAL();

            //split para obtener del string listaIDmesa los id de la mesa de trabajo

            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    using (DbContextTransaction transaction = db.Database.BeginTransaction())//Comienza Transacción
                    {
                        foreach (var listaItem in listaFolioIdMT)//Ciclo, en la lista se encuentras los folios y las mesas de trabajo en las cuales participaran
                        {
                            string listaIDmesa = listaItem[0, 1];
                            int folio = int.Parse(listaItem[0, 0]);

                            string[] splitIdMT = listaIDmesa.Split(',');

                            if (splitIdMT[0] != "")
                            {


                                try
                                {

                                    for (int i = 0; i < splitIdMT.Length; i++)
                                    {
                                        int MT = int.Parse(splitIdMT[i]);
                                        int cantidadRegistrada = (from mtp in db.tblMesaTrabParticipante where mtp.idMesaTrab == MT select mtp).Count();//Obtiene la cantidad registrada, el dato será utilizado para validar si se pueden realizar o no más registros a la MT
                                        MesaEventoDAL mesaEdal = new MesaEventoDAL();
                                        int cantidadAceptada = mesaEdal.cantidadAceptadaMesaT(idEvento, MT);


                                        tblMesaTrabParticipante newMesaTPart = new tblMesaTrabParticipante();
                                        newMesaTPart.folioParticipante = folio;
                                        newMesaTPart.idMesaTrab = MT;
                                        db.tblMesaTrabParticipante.Add(newMesaTPart);
                                        db.SaveChanges();

                                        //agrego = "CR "+cantidadRegistrada.ToString() + " CA" + cantidadAceptada.ToString()+" "+idEvento.ToString()+" "+MT.ToString();
                                        if ((cantidadRegistrada + i + 1 <= cantidadAceptada) == false)//En caso de no aceptar mas participantes para la mesa de trabajo se realiza un RollBack
                                        {
                                            agrego = false;
                                            transaction.Rollback();
                                            break;
                                        }



                                    }

                                }
                                catch
                                {
                                    agrego = false;
                                    transaction.Rollback();
                                }

                            }
                            if (agrego == false)
                                break;

                        }
                        if (agrego == true)
                            transaction.Commit();

                    }

                }

            }
            catch (Exception)
            {
                agrego = false;
            }

            return agrego;
        }

        /// <summary>
        /// Regresa un T/F si el registro a la mesa de trabajo existe para el folio
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="nombreMT"></param>
        /// <returns></returns>
        public bool existeRegistroMT(int folio, string nombreMT)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesaParticipante = (from mtp in db.tblMesaTrabParticipante join mt in db.tblMesaTrabEvento on mtp.idMesaTrab equals mt.idMesaTrab where mtp.folioParticipante == folio && mt.nombreMT == nombreMT select mtp).ToList();
            }
            if (model.listaMesaParticipante.Count() != 0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Usa clase MesaParticipanteRI, obtiene los registros en tblMesaTrabParticipante join tblMesaTrabEvento
        /// donde folioParticipante == parámetro folio
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>model.listaMesaPRI</returns>
        public List<MesaParticipanteRI> mesaParticipanteRegIn(int folio)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaMesaPRI = (from mtp in db.tblMesaTrabParticipante join mt in db.tblMesaTrabEvento on mtp.idMesaTrab equals mt.idMesaTrab where mtp.folioParticipante == folio select new MesaParticipanteRI { folio = mtp.folioParticipante, idMesa = mtp.idMesaTrab, nombreMT = mt.nombreMT }).ToList();
            }
            return model.listaMesaPRI;
        }
    }
}
