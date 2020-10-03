using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.DataAccess.Usuario;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.Usuario
{
    public class PerteneceInsSisBL
    {
        PerteneceInsSisDAL insSisDAL = new PerteneceInsSisDAL();

        /// <summary>
        /// Obtiene todos los registros en tblPerteneceInsSis
        /// </summary>
        /// <returns>model.listaInsSis</returns>
        public List<tblPerteneceInsSis> getInstiSub()
        {
            try
            {
                return insSisDAL.getInstiSub();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene todos los datos en tblPerteneceInsSis donde idUserRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listaInsSis</returns>
        public List<tblPerteneceInsSis> getInstiSub(string revisor)
        {
            try
            {
                return insSisDAL.getInstiSub(revisor);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actializa la institucionSistema en tblPerteneceInsSis donde idUserRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="insSis"></param>
        /// <returns>True=Update exitoso || False=Falla en Update</returns>
        public bool editarPerInsSis(string revisor, string insSis)
        {
            try
            {
                return insSisDAL.editarPerInsSis(revisor,insSis);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene un valor booleano si existe o no el idUserRevisor en tblPerteneceInsSis
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>True=Existe revisor en tblPerteneceInsSis || False=No existe revisor en tblPerteneceInsSis</returns>
        public bool existeUserInsSis(string revisor)
        {
            try
            {
                return insSisDAL.existeUserInsSis(revisor);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Nuevo Registro en tblPerteneceInsSis,se genera el registro solo si existeUserInsSis(revisor)==false
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="insSis"></param>
        /// <returns>True=Registro exitoso || False=Error en el registro</returns>
        public bool agregarUserInsSis(string revisor, string insSis)
        {
            try
            {
                return insSisDAL.agregarUserInsSis(revisor, insSis);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
