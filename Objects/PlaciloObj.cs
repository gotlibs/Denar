using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Denar.Objects
{
    public class PlaciloObj
    {
        private DataSetBaza.PlaciloRow row;

        public PlaciloObj()
        {
            //DataSetTest.PlaciloDataTable dt = new DataSetTest.PlaciloDataTable();
            //row = dt.NewPlaciloRow();
        }

        public PlaciloObj(DataSetBaza.PlaciloRow row)
        {
            this.row = row;
        }

        public PlaciloObj(int placilo_id, int placilo_tip_id, int uporabnik_id_vnesel, int uporabnik_id_placal, DateTime datum_vnos, DateTime datum_placilo, decimal znesek, string opis, string placilo_tip_naziv = "", string uporabnik_naziv_vnesel = "", string uporabnik_naziv_placal = "", int zap_st = -1)
        {
            DataSetBaza.PlaciloDataTable dt = new DataSetBaza.PlaciloDataTable();
            row = dt.NewPlaciloRow();
            row.placilo_id = placilo_id;
            row.placilo_tip_id = placilo_tip_id;
            row.placilo_tip_naziv = placilo_tip_naziv;
            row.uporabnik_id_vnesel = uporabnik_id_vnesel;
            row.uporabnik_naziv_vnesel = uporabnik_naziv_vnesel;
            row.uporabnik_id_placal = uporabnik_id_placal;
            row.uporabnik_naziv_placal = uporabnik_naziv_placal;
            row.datum_vnos = datum_vnos;
            row.datum_placilo = datum_placilo;
            row.znesek = znesek;
            row.opis = opis;
            row.zap_st = zap_st;
            dt.AddPlaciloRow(row);
            dt.AcceptChanges();
        }

        #region Public properties
        public int PLACILO_ID 
        {
            get
            {
                return row.placilo_id;
            }
            set
            {
                row.placilo_id = value;
            }
        }

        public int PLACILO_TIP_ID
        {
            get
            {
                return row.placilo_tip_id;
            }
            set
            {
                row.placilo_tip_id = value;
            }
        }

        public int UPORABNIK_ID_VNESEL
        {
            get
            {
                return row.uporabnik_id_vnesel;
            }
            set
            {
                row.uporabnik_id_vnesel = value;
            }
        }

        public int UPORABNIK_ID_PLACAL
        {
            get
            {
                return row.uporabnik_id_placal;
            }
            set
            {
                row.uporabnik_id_placal = value;
            }
        }

        public int ZAP_ST
        {
            get
            {
                if (row.Iszap_stNull())
                    return -1;
                else
                    return row.zap_st;
            }
            set
            {
                row.zap_st = value;
            }
        }

        public DateTime DATUM_VNOS
        {
            get
            {
                if (row.Isdatum_vnosNull())
                    return DateTime.MinValue;
                else
                    return row.datum_vnos;
            }
            set
            {
                row.datum_vnos = value;
            }
        }

        public DateTime DATUM_PLACILO
        {
            get
            {
                if (row.Isdatum_placiloNull())
                    return DateTime.MinValue;
                else
                    return row.datum_placilo;
            }
            set
            {
                row.datum_placilo = value;
            }
        }

        public string UPORABNIK_NAZIV_VNESEL
        {
            get
            {
                if (row.Isuporabnik_naziv_vneselNull())
                    return String.Empty;
                else
                    return row.uporabnik_naziv_vnesel;
            }
            set
            {
                row.uporabnik_naziv_vnesel = value;
            }
        }

        public string PLACILO_TIP_NAZIV
        {
            get
            {
                if (row.Isplacilo_tip_nazivNull())
                    return String.Empty;
                else
                    return row.placilo_tip_naziv;
            }
            set
            {
                row.placilo_tip_naziv = value;
            }
        }

        public string UPORABNIK_NAZIV_PLACAL
        {
            get
            {
                if (row.Isuporabnik_naziv_placalNull())
                    return String.Empty;
                else
                    return row.uporabnik_naziv_placal;
            }
            set
            {
                row.uporabnik_naziv_placal = value;
            }
        }

        public decimal ZNESEK
        {
            get
            {
                if (row.IsznesekNull())
                    return -1;
                else
                    return row.znesek;
            }
            set
            {
                row.znesek = value;
            }
        }

        public string OPIS
        {
            get
            {
                if (row.IsopisNull())
                    return String.Empty;
                else
                    return row.opis;
            }
            set
            {
                row.opis = value;
            }
        }
        #endregion
    }
}