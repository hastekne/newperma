using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.ModuloBP;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblImagenBP
    /// </summary>
    public class ImagenBPDAL
    {
        ImagenBPML model = new ImagenBPML();

        /// <summary>
        /// Obtener todos los registros en tblImagenBP
        /// </summary>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImagenBP = (from img in db.tblImagenBP select img).ToList();
            }
            return model.listImagenBP;
        }

        /// <summary>
        /// Obtener los registros en tblImagenBP donde imagen == parámetro rutaImagen
        /// </summary>
        /// <param name="rutaImagen"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes(string rutaImagen)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImagenBP = (from img in db.tblImagenBP where img.imagen==rutaImagen select img).ToList();
            }
            return model.listImagenBP;
        }

        /// <summary>
        /// Obtener los registros en tblImagenBP donde buenaPractica == parámetro BP y 
        /// campoBP == parámetro campoBP
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes(int BP, int campoBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImagenBP = (from img in db.tblImagenBP where img.buenaPractica==BP && img.campoBP==campoBP select img).ToList();
            }
            return model.listImagenBP;
        }

        /// <summary>
        /// Obtener los registros de tblImagenBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes(int BP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImagenBP = (from img in db.tblImagenBP where img.buenaPractica==BP select img).ToList();
            }
            return model.listImagenBP;
        }

        /// <summary>
        /// Obtener los registros de tblImagenBP donde idImagen == parámetro idImg
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenesXID(int idImg)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImagenBP = (from img in db.tblImagenBP where img.idImagen == idImg select img).ToList();
            }
            return model.listImagenBP;
        }

        /// <summary>
        /// Eliminar un registro de tblImagenBP donde imagen == parámetro rutaImg
        /// </summary>
        /// <param name="rutaImg"></param>
        /// <returns>True=Delete exitoso || False=Error delete</returns>
        public bool eliminarImagen(string rutaImg)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var imagenEli = (from i in db.tblImagenBP where i.imagen == rutaImg select i).First<tblImagenBP>();
                db.tblImagenBP.Remove(imagenEli);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }

        /// <summary>
        /// Eliminar un registro de tblImagenBP donde idImagen == parámetro idImagen
        /// </summary>
        /// <param name="idImagen"></param>
        /// <returns>True=Delete exitoso || False=Error delete</returns>
        public bool eliminarImagen(int idImagen)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var imagenEli = (from i in db.tblImagenBP where i.idImagen == idImagen select i).First<tblImagenBP>();
                db.tblImagenBP.Remove(imagenEli);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }

        /// <summary>
        /// Obtiene un valor bool si la enumeración existe dentro de una buena práctica en tblImagenBP
        /// </summary>
        /// <param name="enumeracion"></param>
        /// <param name="BP"></param>
        /// <returns>True=Exite enumeración || False=No existe la enumeración</returns>
        public bool existeEnumeracion(string enumeracion,int BP)
        {
            bool existe = false;
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listImagenBP = (from i in db.tblImagenBP where i.enumeracion == enumeracion && i.buenaPractica == BP select i).ToList();
            }

            if (model.listImagenBP.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Nuevo registr en tblImagen, valida que la enumeración no exista.
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="campo"></param>
        /// <param name="rutaImg"></param>
        /// <param name="titulo"></param>
        /// <param name="enumeracion"></param>
        /// <returns>True=Insert exitoso || False=Falla en Insert</returns>
        public bool agregarImagen(int BP, int campo,string rutaImg,string titulo, string enumeracion)
        {
            bool agrego = false;
            try { 


                if (existeEnumeracion(enumeracion, BP) == false)
            {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblImagenBP newImagen = new tblImagenBP()
                        {
                            buenaPractica = BP,
                            campoBP = campo,
                            imagen = rutaImg,
                            titulo = titulo,
                            enumeracion = enumeracion,
                        };
                        db.tblImagenBP.Add(newImagen);
                        db.SaveChanges();
                    }
                    agrego = true;
                }
               
            }
            catch (Exception)
            {
                agrego = false;
                throw;
            }

            return agrego;
        }

        /// <summary>
        /// Obtiene valor bool, validando que minimo existan dos archivos tipo Anexos para la BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>True=mayor o igual a 2 anexos || False=Menos a dos anexos</returns>
        public bool existenciaMinimaAnexos(int BP)
        {
            bool minimo2 = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listImagenBP = (from a in db.tblImagenBP where a.campoBP == 10 && a.buenaPractica==BP select a).ToList();
            }
            if (model.listImagenBP.Count >= 2)
            {
                minimo2 = true;
            }
            return minimo2;
        }

        /// <summary>
        /// Obtiene valor bool, con base a la cantidad máxima de anexos permitidos por BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>True=Mayor a 5 anexos || False = menos a 5 anexos</returns>
        public bool existenciaMaximaAnexos(int BP)
        {
            bool maximo5 = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listImagenBP = (from a in db.tblImagenBP where a.campoBP == 10 && a.buenaPractica==BP select a).ToList();
            }
            if (model.listImagenBP.Count > 5)
            {
                maximo5 = true;
            }
            return maximo5;
        }

        /// <summary>
        /// Utiliza la clase TabImagenBP, obtiene los registros de tblImagenBP join tblCampoBP
        /// donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listaTabImagenBP</returns>
        public List<TabImagenBP> getListaTabImagenBP(int BP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaTabImagenBP = (from img in db.tblImagenBP join c in db.tblCampoBP on img.campoBP equals c.idCampo where img.buenaPractica == BP select new TabImagenBP { idImagen = img.idImagen, figura = img.enumeracion, Titulo = img.titulo,campoBP=c.nombre }).ToList();

            }

            return model.listaTabImagenBP;
        }

        /// <summary>
        /// Obtiene la cantidad de archivos de una sección de la buena práctica
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="seccion"></param>
        /// <returns>int cantidad de archivos</returns>
        public int contarArchivos(int BP, string seccion)
        {
            int contador = 0;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listImagenBP = (from img in db.tblImagenBP join c in db.tblCampoBP on img.campoBP equals c.idCampo where img.buenaPractica == BP && c.nombre==seccion select img).ToList();

            }
            foreach(var item in model.listImagenBP)
            {
                contador = contador + 1;
            }
            return contador;
        }
        
    }
}
