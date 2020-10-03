using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.DataAccess.ModuloEvento;
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloEvento;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el acceso de datos a tblEvento
    /// </summary>
    public class EventoBL
    {
        EventoDAL dal = new EventoDAL();

        /// <summary>
        /// Buscar en tblEvento por idEvento
        /// </summary>
        public List<tblEvento> getEvento(int idEvento)
        {
            try
            {
                return dal.getEvento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar en tblEvento por estado
        /// </summary>
        public List<tblEvento> getEvento(string estado)
        {
            try
            {
                return dal.getEvento(estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Agregar registro a tblEvento por idEvento
        /// </summary>
        public bool agregarEvento(string nombre, string objetivo, string rutaIcono, DateTime fvi, DateTime fvf, DateTime fri, DateTime frf, string rutaPlantilla, string tipoEvento, string estado)
        {
            try
            {
                return dal.agregarEvento(nombre, objetivo, rutaIcono, fvi, fvf, fri, frf, rutaPlantilla, tipoEvento, estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Editar registro en tblEvento por idEvento
        /// </summary>
        public bool editarEvento(int idEvento, string nombre, string objetivo, string rutaIcono, DateTime fvi, DateTime fvf, DateTime fri, DateTime frf, string rutaPlantilla, string tipoEvento)
        {
            try
            {
                return dal.editarEvento(idEvento, nombre, objetivo, rutaIcono, fvi, fvf, fri, frf, rutaPlantilla, tipoEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Actualizar el estado del evento en tblEvento
        /// </summary>
        public bool editarEstado(int idEvento, string estado)
        {
            try
            {
                return dal.editarEstado(idEvento, estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el id del evento con el nombre del evento
        /// </summary>
        /// <param name="nombreEvento"></param>
        /// <returns></returns>
        public int getIdEvento(string nombreEvento)
        {
            try
            {
                return dal.getIdEvento(nombreEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Retorna en una lista los datos del evento correspondiente al nomrbe
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<tblEvento> getEventoXnombre(string nombre)
        {
            try
            {
                return dal.getEventoXnombre(nombre);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtener todos la lista de todos los Eventos dados de alta
        /// </summary>
        /// <returns></returns>
        public List<tblEvento> getTodosEvento()
        {
            try
            {
                return dal.getTodosEvento();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el contenido de la tabla eventos y se utilizara para filtrar por palabra clave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns></returns>
        public List<EventoEnlace> getEventoTabla(string palabraClave)
        {
            try
            {
                return dal.getEventoTabla(palabraClave);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Se utiliza para update a la ruta del icono cuando se modifica la imagen de icono desde la pag. web
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="nuevaRI"></param>
        /// <returns></returns>
        public bool editarRutaIcono(int idEvento, string nuevaRI)
        {
            try
            {
                return dal.editarRutaIcono(idEvento, nuevaRI);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Se utiliza para modificar la ruta de la plantilla de Excel del evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="nuevaRPE"></param>
        /// <returns></returns>
        public bool editarRutaPExcel(int idEvento, string nuevaRPE)
        {
            try
            {
                return dal.editarRutaPExcel(idEvento, nuevaRPE);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Modificar el evento para la sección _datosEvento
        /// </summary>
        /// <returns></returns>
        public bool modificarEvento(int idEvento, string nombre, string objetivo, DateTime fvi, DateTime fvf, DateTime fri, DateTime frf, string tipoEvento, string estado)
        {
            try
            {
                return dal.modificarEvento(idEvento, nombre, objetivo, fvi, fvf, fri, frf, tipoEvento, estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene la lista de MisRegistro para un usuario determinado y con la opción de filtrar por palabra clave
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="palabraClave"></param>
        /// <returns></returns>
        public List<MisRegistros> getMisRegistros(string usuario, string palabraClave)
        {
            try
            {
                return dal.getMisRegistros(usuario, palabraClave);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// obtiene los Eventos con fecha actual dentro del rango de fecha de registro del evento y el estado = publicado
        /// </summary>
        /// <param name="fechaActual"></param>
        /// <returns></returns>
        public List<HomeEvento> getHomeEvento(DateTime fechaActual)
        {
            try
            {
                return dal.getHomeEvento(fechaActual);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase SesionEnlace, obtiene los registros en tblEvento join tblUserEnlaceEvento
        /// donde idUserEnlace == parámetro usuario
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns>model.listSesionEnlace</returns>
        public List<SesionEnlace> getEventosSesionEnlace(string Usuario)
        {
            try
            {
                return dal.getEventosSesionEnlace(Usuario);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Recupera el estado del evento por su nombre
        /// </summary>
        /// <param name="nomE"></param>
        /// <returns></returns>
        public string estadoEvento(string nomE)
        {
            try
            {
                return dal.estadoEvento(nomE);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

    }
}
