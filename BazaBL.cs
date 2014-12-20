using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Denar
{
    public class BazaBL
    {
        #region PlaciloGet
        public static DataSetBaza.PlaciloDataTable PlaciloGet(int placilo_id, int placilo_tip_id, int uporabnik_id_placal, DateTime datum_placilo_od, DateTime datum_placilo_do, string opis)
        {
            return BazaDB.PlaciloGet(placilo_id, placilo_tip_id, uporabnik_id_placal, datum_placilo_od, datum_placilo_do, opis);
        }
        #endregion

        #region OpisGetAll
        public ListDictionary OpisGetAll()
        {
            return EnumsDenar.OpombaOpis.AllValues;
        }
        #endregion
    }
}