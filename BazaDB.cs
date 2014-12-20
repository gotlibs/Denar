using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Denar.Objects;

namespace Denar
{
    public static class BazaDB
    {
        #region PlaciloGet
        public static DataSetBaza.PlaciloDataTable PlaciloGet(int placilo_id, int placilo_tip_id, int uporabnik_id_placal, DateTime datum_placilo_od, DateTime datum_placilo_do, string opis)
        {
            DataSetBaza ds = new DataSetBaza();
            DataSetBazaTableAdapters.PlaciloTableAdapter ta = new DataSetBazaTableAdapters.PlaciloTableAdapter();
            ta.Fill(ds.Placilo, placilo_id, placilo_tip_id, uporabnik_id_placal, datum_placilo_od, datum_placilo_do, opis);
            return ds.Placilo;
        }
        #endregion

        #region PlaciloInsert
        public static int PlaciloInsert(PlaciloObj placiloObj)
        {
            int? st_affected_rows_out = 0;
            DataSetBazaTableAdapters.PlaciloTableAdapter ta = new DataSetBazaTableAdapters.PlaciloTableAdapter();
            ta.Insert(placiloObj.PLACILO_TIP_ID, placiloObj.UPORABNIK_ID_PLACAL, placiloObj.UPORABNIK_ID_VNESEL, placiloObj.DATUM_PLACILO, placiloObj.ZNESEK, placiloObj.OPIS, ref st_affected_rows_out);
            return Convert.ToInt32(st_affected_rows_out);
        }
        #endregion

        #region PlaciloUpdate
        public static int PlaciloUpdate(PlaciloObj placiloObj)
        {
            int? st_affected_rows_out = 0;
            DataSetBazaTableAdapters.PlaciloTableAdapter ta = new DataSetBazaTableAdapters.PlaciloTableAdapter();
            ta.Update(placiloObj.PLACILO_ID, placiloObj.PLACILO_TIP_ID, placiloObj.UPORABNIK_ID_PLACAL, placiloObj.UPORABNIK_ID_VNESEL, placiloObj.DATUM_PLACILO, placiloObj.ZNESEK, placiloObj.OPIS, ref st_affected_rows_out);
            return Convert.ToInt32(st_affected_rows_out);
        }
        #endregion

        #region PlaciloDelete
        public static int PlaciloDelete(PlaciloObj placiloObj)
        {
            int? st_affected_rows_out = 0;
            DataSetBazaTableAdapters.PlaciloTableAdapter ta = new DataSetBazaTableAdapters.PlaciloTableAdapter();
            ta.Delete(placiloObj.PLACILO_ID, ref st_affected_rows_out);
            return Convert.ToInt32(st_affected_rows_out);
        }
        #endregion

        #region UporabnikGet
        public static DataSetBaza.UporabnikDataTable UporabnikGet()
        {
            DataSetBaza ds = new DataSetBaza();
            DataSetBazaTableAdapters.UporabnikTableAdapter ta = new DataSetBazaTableAdapters.UporabnikTableAdapter();
            ta.Fill(ds.Uporabnik);
            return ds.Uporabnik;
        }
        #endregion

        #region PlaciloTipGet
        public static DataSetBaza.PlaciloTipDataTable PlaciloTipGet()
        {
            DataSetBaza ds = new DataSetBaza();
            DataSetBazaTableAdapters.PlaciloTipTableAdapter ta = new DataSetBazaTableAdapters.PlaciloTipTableAdapter();
            ta.Fill(ds.PlaciloTip);
            return ds.PlaciloTip;
        }
        #endregion
    }
}