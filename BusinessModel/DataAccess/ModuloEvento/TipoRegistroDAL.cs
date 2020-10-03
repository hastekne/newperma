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
    /// Clase para el Acceso de datos a tblTipoRegistro
    /// </summary>
    public class TipoRegistroDAL
    {
        TipoRegistroML model = new TipoRegistroML();

        ///<summary>
        ///Buscar por idTipoRegistro en tblTipoRegistro
        /// </summary>
        public List<tblTipoRegistro> getTipoRegistro(string idTipoR)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaTipoRegistro = (from tr in db.tblTipoRegistro where tr.idTipoRegistro == idTipoR select tr).ToList();
            }
            return model.listaTipoRegistro;
        }

        ///<summary>
        ///Insertar registro en tblTipoRegistro
        /// </summary>
        /// Se contruye PK con 10 caracteres numeros y letra aleatorios
        public string agregarTipoReg(string desc)
        {
            string agrego = "NO";
            try
            {
                string idTR = "";
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[10];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                idTR = finalString.ToString();

                //Mientras el ID exista debe seguir caluclando el id
                while (existeID(idTR) == true)
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    finalString = new String(stringChars);
                    idTR = finalString.ToString();
                }



                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblTipoRegistro newTR = new tblTipoRegistro()
                    {
                        idTipoRegistro = idTR,
                        descripcion = desc,
                    };
                    db.tblTipoRegistro.Add(newTR);
                    db.SaveChanges();
                }
                agrego = idTR;
            }
            catch (Exception)
            {
                agrego = "NO";
            }

            return agrego;
        }

        //Validar existencia de PK en tblTipoRegistro, return Bool
        /// <summary>
        /// Obtiene el valor booleano de true o false si idTipoRegistro existe en tblTipoRegistro
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true=Existe idTipoRegistro || false=No existe idTipoRegistro</returns>
        public bool existeID(string id)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaTipoRegistro = (from tr in db.tblTipoRegistro where tr.idTipoRegistro == id select tr).ToList();
            }
            if (model.listaTipoRegistro.Count != 0)
            {
                existe = true;
            }

            return existe;
        }

        /// <summary>
        /// Eliminar el tipo de registro cuando el registro al evento haya fallado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool eliminarTipoRegistro(string id)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var tipoReg = (from tr in db.tblTipoRegistro where tr.idTipoRegistro == id select tr).First<tblTipoRegistro>();
                db.tblTipoRegistro.Remove(tipoReg);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }



    }
}
