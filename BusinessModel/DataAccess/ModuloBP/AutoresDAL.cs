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
    /// Clase para el acceso de datos a tblAutores
    /// </summary>
    public class AutoresDAL
    {
        AutoresML model = new AutoresML();

        /// <summary>
        /// Obtiene todos los registros en tblAutores
        /// </summary>
        /// <returns>model.listAutores</returns>
        public List<tblAutores> getAutores()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listAutores = (from a in db.tblAutores select a).ToList();
            }
            return model.listAutores;
        }

        /// <summary>
        /// Obtiene los registro en tblAutores donde buenaPractica=parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listAutores</returns>
        public List<tblAutores> getAutores(int BP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listAutores = (from a in db.tblAutores where a.buenaPractica==BP select a).ToList();
            }
            return model.listAutores;
        }

        /// <summary>
        /// Elimina un registro en tblAutores donde idAutor == parámetro idAutor
        /// </summary>
        /// <param name="idAutor"></param>
        /// <returns>TRUE==Registro Eliminado || FALSE==Falla en eliminar registro</returns>
        public bool eliminarAutor(int idAutor)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var autorEli = (from a in db.tblAutores where a.idAutor == idAutor select a).First<tblAutores>();
                db.tblAutores.Remove(autorEli);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }

        /// <summary>
        /// Obtiene true o false realizando la validación de los autores de una buena práctica
        /// solo se permiten 10 autores en una BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>TRUE==Más de 10 Autores || FALSE==Menos de 10 Autores</returns>
        public bool existenciaAutores(int BP)
        {
            bool existenMas10 = false;
            int numeroA = 0;
            using (var db=new SeguimientoPermanenciaEntities())
            {
                numeroA = (from a in db.tblAutores where a.buenaPractica == BP select a.idAutor).Count();
            }
            if (numeroA == 9)
            {
                existenMas10 = true;
            }
            return existenMas10;
        }

        /// <summary>
        /// Inserta registro en tblAutores, solo si existenciaAutores(BP) == false  
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="nombre"></param>
        /// <param name="ape1"></param>
        /// <param name="ape2"></param>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Falla en el registro</returns>
        public bool agregarAutor(int BP, string nombre, string ape1, string ape2, string email, string tel)
        {
            bool agrego = false;
            try
            {
                if (existenciaAutores(BP) == false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblAutores newAutor = new tblAutores()
                        {
                            buenaPractica = BP,
                            nombreA = nombre,
                            pApellidoA = ape1,
                            mApellidoA = ape2,
                            correoElectronicoA = email,
                            telefonoA = tel,
                        };
                        db.tblAutores.Add(newAutor);
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

    }
}
