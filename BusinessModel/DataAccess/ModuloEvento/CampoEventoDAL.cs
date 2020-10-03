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
    /// Clase para el Acceso de datos a tblCampoEvento
    /// </summary>
    public class CampoEventoDAL
    {
        CampoEventoML model = new CampoEventoML();

        /// <summary>
        /// Buscar registro en tblCampoEvento por idEvento
        /// </summary>
        public List<tblCampoEvento> getCampoEventoXidEvento(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaCampoEvento = (from e in db.tblCampoEvento where e.idEvento == idEvento select e).ToList();
            }
            return model.listaCampoEvento;
        }

        /// <summary>
        /// Método para nuevo registro en tblCampoEvento
        /// </summary>
        public bool agregarCamposRegistro(int idEvento, bool cct, bool nomplantel, bool municipio, bool regSEG, bool nivel, bool instisub, bool curp, bool nom, bool ap1, bool ap2, bool email, bool tel, bool sexo, bool funcion, bool mesaTrab)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblCampoEvento newCampoRegistroEvento = new tblCampoEvento()
                    {
                        idEvento = idEvento,
                        CCT = cct,
                        nombrePlantel = nomplantel,
                        municipio = municipio,
                        regionSEG = regSEG,
                        nivel = nivel,
                        instiSubsis = instisub,
                        CURP = curp,
                        nombre = nom,
                        apellidoPat = ap1,
                        apellidoMat = ap2,
                        correoElectronico = email,
                        telefono = tel,
                        sexo = sexo,
                        funcion = funcion,
                        mesaTrabajo = mesaTrab,
                    };
                    db.tblCampoEvento.Add(newCampoRegistroEvento);
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

        /// <summary>
        /// Actualizar registro en tblCampoEvento
        /// </summary>
        public bool editarCamposRegistro(int idCampoEvento, bool cct, bool nomplantel, bool municipio, bool regSEG, bool nivel, bool instisub, bool curp, bool nom, bool ap1, bool ap2, bool email, bool tel, bool sexo, bool funcion, bool mesaTrab)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoEvento = (from e in db.tblCampoEvento where e.idCampoEvento == idCampoEvento select e).First<tblCampoEvento>();
                    campoEvento.CCT = cct;
                    campoEvento.nombrePlantel = nomplantel;
                    campoEvento.municipio = municipio;
                    campoEvento.regionSEG = regSEG;
                    campoEvento.nivel = nivel;
                    campoEvento.instiSubsis = instisub;
                    campoEvento.CURP = curp;
                    campoEvento.nombre = nom;
                    campoEvento.apellidoPat = ap1;
                    campoEvento.apellidoMat = ap2;
                    campoEvento.correoElectronico = email;
                    campoEvento.telefono = tel;
                    campoEvento.sexo = sexo;
                    campoEvento.funcion = funcion;
                    campoEvento.mesaTrabajo = mesaTrab;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        /// <summary>
        /// Retorna el estado del campo para el Evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="campo"></param>
        /// <returns></returns>
        public bool campoHabilitado(int idEvento, string campo)
        {
            bool habilitado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {

                    model.listaCampoEvento = (from e in db.tblCampoEvento where e.idEvento == idEvento select e).ToList();
                }
                switch (campo)
                {
                    case "CCT":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.CCT;
                        }
                        break;
                    case "nombrePlantel":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.nombrePlantel;
                        }
                        break;
                    case "municipio":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.municipio;
                        }
                        break;
                    case "regionSEG":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.regionSEG;
                        }
                        break;
                    case "nivel":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.nivel;
                        }
                        break;
                    case "instiSubsis":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.instiSubsis;
                        }
                        break;
                    case "CURP":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.CURP;
                        }
                        break;
                    case "nombre":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.nombre;
                        }
                        break;
                    case "apellidoPat":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.apellidoPat;
                        }
                        break;
                    case "apellidoMat":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.apellidoMat;
                        }
                        break;
                    case "correoElectronico":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.correoElectronico;
                        }
                        break;
                    case "telefono":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.telefono;
                        }
                        break;
                    case "sexo":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.sexo;
                        }
                        break;
                    case "funcion":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.funcion;
                        }
                        break;
                    case "mesaTrabajo":
                        foreach (var item in model.listaCampoEvento)
                        {
                            habilitado = item.mesaTrabajo;
                        }
                        break;

                }
            }
            catch (Exception) { habilitado = false; }
            return habilitado;
        }

        /// <summary>
        /// Retorna lista con todos los campos aceptados de un evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public List<string> camposEvento(int idEvento)
        {
            List<string> camposAceptados = new List<string>();
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaCampoEvento = (from e in db.tblCampoEvento where e.idEvento == idEvento select e).ToList();
            }

            foreach (var item in model.listaCampoEvento)
            {
                if (item.CCT == true)
                    camposAceptados.Add("CCT");
                if (item.nombrePlantel == true)
                    camposAceptados.Add("nombrePlantel");
                if (item.municipio == true)
                    camposAceptados.Add("municipio");
                if (item.regionSEG == true)
                    camposAceptados.Add("regionSEG");
                if (item.nivel == true)
                    camposAceptados.Add("nivel");
                if (item.instiSubsis == true)
                    camposAceptados.Add("instiSubsis");
                if (item.CURP == true)
                    camposAceptados.Add("CURP");
                if (item.nombre == true)
                    camposAceptados.Add("nombre");
                if (item.apellidoPat == true)
                    camposAceptados.Add("apellidoPat");
                if (item.apellidoMat == true)
                    camposAceptados.Add("apellidoMat");
                if (item.correoElectronico == true)
                    camposAceptados.Add("correoElectronico");
                if (item.telefono == true)
                    camposAceptados.Add("telefono");
                if (item.sexo == true)
                    camposAceptados.Add("sexo");
                if (item.funcion == true)
                    camposAceptados.Add("funcion");
                if (item.mesaTrabajo == true)
                    camposAceptados.Add("MT");

            }

            return camposAceptados;
        }

        /// <summary>
        /// Eliminar todos los campos del Evento por el ID del Evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool eliminarTodosCamposEvento(int idEvento)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoE = (from ce in db.tblCampoEvento where ce.idEvento == idEvento select ce).First<tblCampoEvento>();
                    db.tblCampoEvento.Remove(campoE);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }
    }
}
