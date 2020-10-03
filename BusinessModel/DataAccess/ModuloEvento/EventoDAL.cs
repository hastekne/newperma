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
    /// Clase para el acceso de datos a tblEvento
    /// </summary>
    public class EventoDAL
    {
        EventoML model = new EventoML();

        /// <summary>
        /// Buscar en tblEvento por idEvento
        /// </summary>
        public List<tblEvento> getEvento(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEventos = (from ev in db.tblEvento where ev.idEvento == idEvento select ev).ToList();
            }
            return model.listaEventos;
        }

        ///<summary>
        ///Buscar en tblEvento por estado
        /// </summary>
        public List<tblEvento> getEvento(string estado)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEventos = (from ev in db.tblEvento where ev.estado == estado select ev).ToList();
            }
            return model.listaEventos;
        }

        ///<summary>
        ///Agregar registro a tblEvento por idEvento
        /// </summary>
        public bool agregarEvento(string nombre,string objetivo,string rutaIcono,DateTime fvi,DateTime fvf,DateTime fri,DateTime frf,string rutaPlantilla,string tipoEvento,string estado)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    //Validar que el nombre del evento no exista
                    model.listaEventos = (from ev in db.tblEvento where ev.nombre == nombre select ev).ToList();
                    if (model.listaEventos.Count != 0)
                    {
                        return false;
                    }
                    tblEvento newEvento = new tblEvento()
                    {
                        nombre=nombre,
                        objetivo=objetivo,
                        rutaIcono=rutaIcono,
                        fVigenciaInicio=fvi,
                        fVigenciaFin=fvf,
                        fRegistroInicio=fri,
                        fRegistroFin=frf,
                        rutaPlantillaExcel=rutaPlantilla,
                        tipoEvento=tipoEvento,
                        estado=estado,
                    };
                    db.tblEvento.Add(newEvento);
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

        ///<summary>
        ///Editar registro en tblEvento por idEvento
        /// </summary>
        public bool editarEvento(int idEvento,string nombre, string objetivo, string rutaIcono, DateTime fvi, DateTime fvf, DateTime fri, DateTime frf, string rutaPlantilla, string tipoEvento)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoEvento = (from ev in db.tblEvento where ev.idEvento == idEvento select ev).First<tblEvento>();
                    campoEvento.nombre = nombre;
                    campoEvento.objetivo = objetivo;
                    campoEvento.rutaIcono = rutaIcono;
                    campoEvento.fVigenciaFin = fvf;
                    campoEvento.fVigenciaInicio = fvi;
                    campoEvento.fRegistroInicio = fri;
                    campoEvento.fRegistroFin = frf;
                    campoEvento.rutaPlantillaExcel = rutaPlantilla;
                    campoEvento.tipoEvento = tipoEvento;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        ///<summary>
        ///Actualizar el estado del evento en tblEvento
        /// </summary>
        public bool editarEstado(int idEvento, string estado)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoEvento = (from ev in db.tblEvento where ev.idEvento == idEvento select ev).First<tblEvento>();
                    campoEvento.estado = estado;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        /// <summary>
        /// Obtiene el id del evento con el nombre del evento
        /// </summary>
        /// <param name="nombreEvento"></param>
        /// <returns></returns>
        public int getIdEvento(string nombreEvento)
        {
            int idEvento = 0;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaEventos = (from ev in db.tblEvento where ev.nombre == nombreEvento select ev).ToList();
            }
            foreach(var item in model.listaEventos)
            {
                idEvento = item.idEvento;
                break;
            }
            return idEvento;
        }

        /// <summary>
        /// Retorna en una lista los datos del evento correspondiente al nomrbe
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<tblEvento> getEventoXnombre(string nombre)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEventos = (from ev in db.tblEvento where ev.nombre == nombre select ev).ToList();
            }
            return model.listaEventos;
        }

        /// <summary>
        /// Obtener todos la lista de todos los Eventos dados de alta
        /// </summary>
        /// <returns></returns>
        public List<tblEvento> getTodosEvento()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEventos = (from ev in db.tblEvento orderby ev.fRegistroInicio descending select ev).ToList();
            }
            return model.listaEventos;
        }


        /// <summary>
        /// Obtiene el contenido de la tabla eventos y se utilizara para filtrar por palabra clave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns></returns>
        public List<EventoEnlace> getEventoTabla(string palabraClave)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.tablaEventoEnlace = (from ev in db.tblEvento where ev.nombre.Contains(palabraClave) select new EventoEnlace { idEvento = ev.idEvento,nombre=ev.nombre,estado=ev.estado }).ToList();
            }
            return model.tablaEventoEnlace;
        }

        /// <summary>
        /// Se utiliza para update a la ruta del icono cuando se modifica la imagen de icono desde la pag. web
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="nuevaRI"></param>
        /// <returns></returns>
        public bool editarRutaIcono(int idEvento, string nuevaRI)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoEvento = (from ev in db.tblEvento where ev.idEvento == idEvento select ev).First<tblEvento>();
                    campoEvento.rutaIcono = nuevaRI;
                    db.SaveChanges();
                    edito = true;
                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        /// <summary>
        /// Se utiliza para modificar la ruta de la plantilla de excel para el evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="nuevaRPE"></param>
        /// <returns></returns>
        public bool editarRutaPExcel(int idEvento, string nuevaRPE)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoEvento = (from ev in db.tblEvento where ev.idEvento == idEvento select ev).First<tblEvento>();
                    campoEvento.rutaPlantillaExcel = nuevaRPE;
                    db.SaveChanges();
                    edito = true;
                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        /// <summary>
        /// Modificar el evento para la sección _datosEvento
        /// </summary>
        /// <returns></returns>
        public bool modificarEvento(int idEvento, string nombre, string objetivo, DateTime fvi, DateTime fvf, DateTime fri, DateTime frf, string tipoEvento,string estado)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoEvento = (from ev in db.tblEvento where ev.idEvento == idEvento select ev).First<tblEvento>();
                    campoEvento.nombre = nombre;
                    campoEvento.objetivo = objetivo;
                    campoEvento.fVigenciaFin = fvf;
                    campoEvento.fVigenciaInicio = fvi;
                    campoEvento.fRegistroInicio = fri;
                    campoEvento.fRegistroFin = frf;
                    campoEvento.tipoEvento = tipoEvento;
                    campoEvento.estado = estado;
                    db.SaveChanges();
                    edito = true;

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }


        /// <summary>
        /// Obtiene la lista de MisRegistro para un usuario determinado y con la opción de filtrar por palabra clave
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="palabraClave"></param>
        /// <returns></returns>
        public List<MisRegistros> getMisRegistros(string usuario,string palabraClave)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                //model.tablaMisRegistros = (from ev in db.tblEvento join re in db.tblRegistroEvento on ev.idEvento equals re.idEvento join user in db.tblUserEnlaceEvento on re.idTipoRegistro equals user.idTipoRegistro where user.idUserEnlace==usuario && ev.nombre.Contains(palabraClave) select new MisRegistros { idEvento =ev.idEvento , nomEvento = ev.nombre, idTipoRegistro = re.idTipoRegistro }).ToList();
                model.tablaMisRegistros = (from ev in db.tblEvento join user in db.tblUserEnlaceEvento on ev.idEvento equals user.idEvento where user.idUserEnlace == usuario && ev.nombre.Contains(palabraClave) orderby ev.fRegistroFin descending select new MisRegistros { idEvento = ev.idEvento, nomEvento = ev.nombre, idTipoRegistro = user.idTipoRegistro }).ToList();
            }
            return model.tablaMisRegistros;
        }

        /// <summary>
        /// obtiene los Eventos con fecha actual dentro del rango de fecha de registro del evento y el estado = publicado
        /// </summary>
        /// <param name="fechaActual"></param>
        /// <returns></returns>
        public List<HomeEvento> getHomeEvento(DateTime fechaActual)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEventos = (from ev in db.tblEvento where (ev.fRegistroInicio <= fechaActual && ev.fRegistroFin >=fechaActual) && ev.estado== "PUBLICADO" select ev).ToList();
            }
            
            foreach(var item in model.listaEventos)
            {
                HomeEvento evento = new HomeEvento();
                evento.idEvento = item.idEvento;
                evento.nombreEvento = item.nombre;
                evento.tipoEvento = item.tipoEvento;
                evento.rutaImgIcono = item.rutaIcono;
                evento.dia1R = item.fRegistroInicio.ToString("dd");
                evento.dia2R = item.fRegistroFin.ToString("dd");
                evento.mes1R = item.fRegistroInicio.ToString("MMM");
                evento.mes2R = item.fRegistroFin.ToString("MMM");
                evento.dia1E = item.fVigenciaInicio.ToString("dd");
                evento.dia2E = item.fVigenciaFin.ToString("dd");
                evento.mes1E = item.fVigenciaInicio.ToString("MMM");
                evento.mes2E = item.fVigenciaFin.ToString("MMM");

                if (evento.dia1R != evento.dia2R && evento.mes1R == evento.mes2R)
                    evento.tipoFechaR = 1;
                else if ((evento.dia1R == evento.dia2R || evento.dia1R != evento.dia2R) && evento.mes1R != evento.mes2R)
                    evento.tipoFechaR = 2;
                else if (evento.dia1R == evento.dia2R && evento.mes1R == evento.mes2R)
                    evento.tipoFechaR = 3;

                if (evento.dia1E != evento.dia2E && evento.mes1E == evento.mes2E)
                    evento.tipoFechaE = 1;
                else if ((evento.dia1E == evento.dia2E || evento.dia1E != evento.dia2E) && evento.mes1E != evento.mes2E)
                    evento.tipoFechaE = 2;
                else if (evento.dia1E == evento.dia2E && evento.mes1E == evento.mes2E)
                    evento.tipoFechaE = 3;

                model.listaEventosHome.Add(evento);
            }
            return model.listaEventosHome;
        }

        /// <summary>
        /// Utiliza la clase SesionEnlace, obtiene los registros en tblEvento join tblUserEnlaceEvento
        /// donde idUserEnlace == parámetro usuario
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns>model.listSesionEnlace</returns>
        public List<SesionEnlace> getEventosSesionEnlace(string Usuario)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listSesionEnlace = (from ev in db.tblEvento join usu in db.tblUserEnlaceEvento on ev.idEvento equals usu.idEvento where usu.idUserEnlace==Usuario orderby usu.idUserEnlaceEve descending select new SesionEnlace {idEvento=ev.idEvento,nombreEvento=ev.nombre,idTipoEventoReg=usu.idTipoRegistro,fechaInicio=ev.fVigenciaInicio,fecahFin=ev.fVigenciaFin,rutaImg=ev.rutaIcono }).ToList();
            }
            return model.listSesionEnlace;
        }

        /// <summary>
        /// Recupera el estado del evento por su nombre
        /// </summary>
        /// <param name="nomE"></param>
        /// <returns></returns>
        public string estadoEvento(string nomE)
        {
            model.estado = "";
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaEventos = (from ev in db.tblEvento where ev.nombre == nomE select ev).ToList();
            }
            foreach(var item in model.listaEventos)
            {
                model.estado = item.estado;
            }
            return model.estado;
        }
    }
}
