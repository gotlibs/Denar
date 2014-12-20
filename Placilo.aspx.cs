using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Denar.Opozorilo;
using Denar.UserControls;
using System.Collections.Specialized;
using System.Data;
using Denar.Objects;

namespace Denar
{
    public partial class Placilo : System.Web.UI.Page, IPostBackEventHandler
    {
        #region Public Properties
        #region GridViewSortDirection
        private string GridViewSortDirection
        {
            get
            {
                return ViewState["SortDirection"] as string ?? "asc";
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }
        #endregion

        #region GridViewSortExpression
        private string GridViewSortExpression
        {
            get
            {
                return ViewState["SortExpression"] as string ?? "izdelek_id";
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        #endregion
        //end sorting

        public EnumsDenar.PageMode PageMode
        {
            get
            {
                if (String.IsNullOrEmpty(hdfPageMode.Value))
                    hdfPageMode.Value = EnumsDenar.PageMode.Insert.ToString();
                return (EnumsDenar.PageMode)Enum.Parse(typeof(EnumsDenar.PageMode), hdfPageMode.Value);
            }
            set
            {
                hdfPageMode.Value = value.ToString();
            }
        }
        #endregion

        #region Div click & Support methods
        public void RaisePostBackEvent(string eventArgument)
        {
            if (!string.IsNullOrEmpty(eventArgument))
            {
                if (eventArgument == "divFormHead_Click")
                    divFormHead_Click();
            }
        }

        private void divFormHead_Click()
        {
            bool zapri = true;
            if (!string.IsNullOrEmpty(divFilterBody.Attributes["class"]) && divFilterBody.Attributes["class"] == "formBody formClosed")
                zapri = false;

            divFilterBody.Attributes.Remove("class");
            divFormHead.Attributes.Remove("class");

            if (zapri)
            {
                divFilterBody.Attributes.Add("class", "formBody formClosed");
                divFormHead.Attributes.Add("class", "formHead");
            }
            else
            {
                divFormHead.Attributes.Add("class", "formHead formHeadOpened");
                divFilterBody.Attributes.Add("class", "formBody");
            }
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            divFormHead.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this, "divFormHead_Click");

            if (!Page.IsPostBack)
            {
                EkranLetoGet();
                EkranMesecGet();
                //divFormHead.Attributes.Add("class", "formHead formHeadOpened");
                //divFilterBody.Attributes.Add("class", "formBody");
            }
        }
        #endregion

        #region lbVnosNovegaPlacila_Click
        protected void lbVnosNovegaPlacila_Click(object sender, EventArgs e)
        {
            lblZnesekError.Visible = false;
            lblDatumErrorPopup.Visible = false;
            lblNaslovPopup.Text = "Vnos plačila";
            hdfPageMode.Value = EnumsDenar.PageMode.Insert.ToString();
            PocistiPopup();
            hdfPlaciloId.Value = "-1";
            tbDatumPopup.Text = DateTime.Now.ToShortDateString();
            mpuPlaciloPopup.Show();
        }
        #endregion

        #region PocistiPopup
        public void PocistiPopup()
        {
            tbDatumPopup.Text = tbOpomba.Text = tbZnesek.Text = String.Empty;
            ddlPopupTip.SelectedIndex = ddlPopupUporabnikVnos.SelectedIndex = ddlPopupUporabnikPlacal.SelectedIndex = 0;
            ddlOpombaPomoc.SelectedIndex = 0;
        } 
        #endregion

        #region gvPregledPlacil_Sorting
        protected void gvPregledPlacil_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            GridViewSortDirection = (GridViewSortDirection == "desc") ? "asc" : "desc";
            gvPregledPlacil.DataBind();
        }
        #endregion

        #region gvPregledPlacil_RowCommand
        protected void gvPregledPlacil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            HideOpozorilo(ucOpozorilo1);

            ImageButton lb = e.CommandSource as ImageButton;
            if (lb == null)
                return;

            GridViewRow row = (lb).NamingContainer as GridViewRow;
            if (row != null)
                gvPregledPlacil.SelectedIndex = row.RowIndex;

            if (e.CommandName == "izbrisi")
            {
                PlaciloObj placiloObj = CreatePlaciloObjFromGridRow(row);
                if (placiloObj == null)
                    return;

                int st_affected_rows = BazaDB.PlaciloDelete(placiloObj);
                if (st_affected_rows <= 0)
                {
                    FillOpozorilo("Neuspešno brisanje plačila.", 0, ucOpozorilo1);
                    return;
                }
                FillOpozorilo("Uspešno brisanje plačila.", 1, ucOpozorilo1);
                gvPregledPlacil.DataBind();
            }
            else if (e.CommandName == "uredi")
            {
                PlaciloObj placiloObj = CreatePlaciloObjFromGridRow(row);
                if (placiloObj == null)
                    return;

                lblZnesekError.Visible = false;
                lblDatumErrorPopup.Visible = false;
                tbDatumPopup.Text = Convert.ToDateTime(placiloObj.DATUM_PLACILO).ToShortDateString();
                ddlPopupUporabnikVnos.SelectedValue = placiloObj.UPORABNIK_ID_VNESEL.ToString();
                ddlPopupUporabnikPlacal.SelectedValue = placiloObj.UPORABNIK_ID_PLACAL.ToString();
                ddlPopupTip.SelectedValue = placiloObj.PLACILO_TIP_ID.ToString();
                tbZnesek.Text = placiloObj.ZNESEK.ToString();
                tbOpomba.Text = placiloObj.OPIS.ToString();
                hdfPlaciloId.Value = placiloObj.PLACILO_ID.ToString();
                hdfPageMode.Value = EnumsDenar.PageMode.Edit.ToString();
                lblNaslovPopup.Text = "Vnos plačila";
                mpuPlaciloPopup.Show();
            }
        }
        #endregion

        #region CreatePlaciloObjFromGridRow
        private PlaciloObj CreatePlaciloObjFromGridRow(GridViewRow row)
        {
            DataView dv = odsPregledPlacil.Select() as DataView;
            DataSetBaza.PlaciloDataTable dtPlacilo = dv.Table as DataSetBaza.PlaciloDataTable;

            DataKey dataKeys = gvPregledPlacil.DataKeys[row.RowIndex];
            string where_pogoj = string.Format("PLACILO_ID = {0}", dataKeys["PLACILO_ID"].ToString());
            DataSetBaza.PlaciloRow[] rows = dtPlacilo.Select(where_pogoj) as DataSetBaza.PlaciloRow[];

            if (rows.Length != 1)
                return null;

            return new PlaciloObj(rows[0]);
        }
        #endregion

        #region ddlFilterLeto_SelectedIndexChanged
        protected void ddlFilterLeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            EkranMesecGet();
        }
        #endregion

        #region EkranLetoGet
        public void EkranLetoGet()
        {
            int zacetnoLeto = 2013;
            int trenutnoLeto = DateTime.Now.Year;
            ListDictionary ar = new ListDictionary();

            for (int i = zacetnoLeto; i <= trenutnoLeto; i++)
                ar.Add(i, i.ToString());

            ddlFilterLeto.DataSource = ar;
            ddlFilterLeto.DataBind();
        }
        #endregion

        #region EkranMesecGet
        public void EkranMesecGet()
        {
            int trenutniMesec = DateTime.Now.Month;
            int trenutnoLeto = DateTime.Now.Year;
            ListDictionary ar = new ListDictionary();

            DateTime month = Convert.ToDateTime("1.1.2013");
            for (int i = 0; i < 12; i++)
            {
                DateTime trenutniDatum = month.AddMonths(i);
                String trenutniMesecNaziv = trenutniDatum.ToString("MMMM");
                int trenutniMesecValue = trenutniDatum.Month;
                ar.Add(trenutniMesecValue, trenutniMesecNaziv);
                if (Convert.ToInt32(ddlFilterLeto.SelectedValue) == trenutnoLeto && trenutniMesec == (i + 1))
                {
                    //ddlFilterMesec.SelectedValue = (i + 1).ToString();
                    break;
                }
            }

            ddlFilterMesec.DataSource = ar;
            ddlFilterMesec.DataBind();

            if (ddlFilterMesec.Items.Count > 0)
                ddlFilterMesec.SelectedIndex = 0;
        }
        #endregion

        #region ddlFilterLeto_DataBound
        protected void ddlFilterLeto_DataBound(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl == null)
                return;

            ddl.Items.Insert(0, new ListItem("Vse", "-1"));

            //int trenutnoLeto = DateTime.Now.Year;
            //ListItem li = new ListItem(trenutnoLeto.ToString(), trenutnoLeto.ToString());
            //if (ddlFilterLeto.Items.Contains(li))
            //    ddlFilterLeto.SelectedValue = trenutnoLeto.ToString();
        }
        #endregion

        #region ddlMesec_DataBound
        protected void ddlMesec_DataBound(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl == null)
                return;

            ddl.Items.Insert(0, new ListItem("Vse", "-1"));
        }
        #endregion

        #region btnPocisti_Click
        protected void btnPocisti_Click(object sender, EventArgs e)
        {
            ClearFilterValues();
        }
        #endregion

        #region ClearFilterValues
        private void ClearFilterValues()
        {
            EkranLetoGet();
            EkranMesecGet();
            ddlFilterUporabnikPlacal.SelectedValue = "-1";
            ddlFilterTip.SelectedValue = "-1";
            tbFilterOpis.Text = String.Empty;
        }
        #endregion

        #region ddlInt_DataBound
        protected void ddlInt_DataBound(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl == null)
                return;

            ddl.Items.Insert(0, new ListItem("Vse", "-1"));
        }
        #endregion

        #region btnFilterPrikazi_Click
        protected void btnFilterPrikazi_Click(object sender, EventArgs e)
        {
            FilterPrikazi();
        }
        #endregion

        #region FilterPrikazi
        public void FilterPrikazi()
        {
            HideOpozorilo(ucOpozorilo1);
            odsPregledPlacil.DataBind();
            gvPregledPlacil.DataBind();
        }
        #endregion

        #region btnFilterPrikaziInZapri_Click
        protected void btnFilterPrikaziInZapri_Click(object sender, EventArgs e)
        {
            divFilterBody.Attributes.Add("class", "formBody formClosed");
            divFormHead.Attributes.Add("class", "formHead");

            FilterPrikazi();
        }
        #endregion

        #region lbtnShrani_Command
        protected void lbtnShrani_Command(object sender, CommandEventArgs e)
        {
            HideOpozorilo(ucOpozorilo1);
            HideOpozorilo(ucOpozoriloPU);
            lblZnesekError.Visible = false;
            lblDatumErrorPopup.Visible = false;
            int shranjevanjeUspesno = 0;
            switch (e.CommandArgument.ToString())
            {
                case "shrani":
                    shranjevanjeUspesno = LastnostiShrani();
                    break;
                case "preklici":
                    shranjevanjeUspesno = 1;
                    break;
            }

            //ni uspesno -> prikazemo popup
            if (shranjevanjeUspesno == EnumsDenar.RezultatShranjevanja.Napaka)
                mpuPlaciloPopup.Show();
            else if (shranjevanjeUspesno == EnumsDenar.RezultatShranjevanja.Uspesno)
                mpuPlaciloPopup.Hide();
            else
                mpuPlaciloPopup.Hide();
        }
        #endregion

        #region LastnostiShrani
        public int LastnostiShrani()
        {
            DataTable dtPlaciloDatum = new DataTable();
            dtPlaciloDatum.Columns.Add("DATUM", typeof(string));

            //todo: transakcija!
            if (tbDatumPopup.Text.Contains(';'))
            {
                string[] placilaDatum = tbDatumPopup.Text.ToString().Split(';');

                if (placilaDatum.Length > 2)
                {
                    lblDatumErrorPopup.Visible = true;
                    return 0;
                }

                TimeSpan steviloDni = Convert.ToDateTime(placilaDatum[1]) - Convert.ToDateTime(placilaDatum[0]).AddDays(-1);
                DateTime datum = Convert.ToDateTime(placilaDatum[0]);

                for (int i = 0; i < steviloDni.TotalDays; i++)
                {

                    if (datum.AddDays(i).DayOfWeek == DayOfWeek.Saturday || datum.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        continue;

                    dtPlaciloDatum.Rows.Add(datum.AddDays(i));
                }
            }
            else
            {
                string[] placilaDatum = tbDatumPopup.Text.ToString().Split(',');
                foreach (string placiloDatum in placilaDatum)
                    dtPlaciloDatum.Rows.Add(placiloDatum);
            }
            
            if(dtPlaciloDatum.Rows.Count == 0)
            {
                lblDatumErrorPopup.Visible = true;
                return 0;
            }

            foreach (DataRow rowDatum in dtPlaciloDatum.Rows)
            {
                int shranjevanjeUspesno = PlaciloShrani(rowDatum["DATUM"].ToString().Trim());

                if (shranjevanjeUspesno != 1)
                    return shranjevanjeUspesno;
            }

            return EnumsDenar.RezultatShranjevanja.Uspesno;
        } 
        #endregion

        public int PlaciloShrani(string placiloDatum)
        {
            DateTime datum;
            if (!DateTime.TryParse(placiloDatum, out datum))
            {
                lblDatumErrorPopup.Visible = true;
                return 0;
            }

            decimal znesek = -1;
            if (!Decimal.TryParse(tbZnesek.Text.Trim(), out znesek))
            {
                lblZnesekError.Visible = true;
                return 0;
            }

            //if (datum > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            //{
            //    FillOpozorilo("Datum ne sme biti večji od današnjega.", 0, ucOpozoriloPU);
            //    return 0;
            //}

            if (hdfPageMode.Value == EnumsDenar.PageMode.Edit.ToString())
            {
                PlaciloObj placiloObj = PlaciloObjCreate(datum);
                if (placiloObj == null)
                    return 2;

                int st_affected_rows = BazaDB.PlaciloUpdate(placiloObj);
                if (st_affected_rows <= 0)
                {
                    FillOpozorilo("Neuspešno urejanje plačila.", 0, ucOpozorilo1);
                    return 2;
                }
                FillOpozorilo("Uspešno urejanje plačila.", 1, ucOpozorilo1);
                gvPregledPlacil.DataBind();
            }
            else
            {
                PlaciloObj placiloObj = PlaciloObjCreate(datum);
                if (placiloObj == null)
                    return 2;

                int st_affected_rows = BazaDB.PlaciloInsert(placiloObj);
                if (st_affected_rows <= 0)
                {
                    FillOpozorilo("Neuspešno dodano plačilo.", 0, ucOpozorilo1);
                    return 2;
                }
                FillOpozorilo("Uspešno dodano plačilo.", 1, ucOpozorilo1);
                gvPregledPlacil.DataBind();
            }

            return 1;
        }

        #region PlaciloObjCreate
        public PlaciloObj PlaciloObjCreate(DateTime datum)
        {
            if (ddlPopupTip.SelectedValue == "-1" || ddlPopupUporabnikVnos.SelectedValue == "-1" || ddlPopupUporabnikPlacal.SelectedValue == "-1")
                return null;

            //return new PlaciloObj(Convert.ToInt32(hdfPlaciloId.Value), Convert.ToInt32(ddlPopupTip.SelectedValue), Convert.ToInt32(ddlPopupUporabnikVnos.SelectedValue), Convert.ToInt32(ddlPopupUporabnikPlacal.SelectedValue), DateTime.MinValue, Convert.ToDateTime(tbDatumPopup.Text.Trim()), ddlPopupTip.SelectedValue == EnumsDenar.PlaciloTip.PartnerjuVSkupno.ToString() ? Convert.ToDecimal(tbZnesek.Text.Trim()) * 2 : Convert.ToDecimal(tbZnesek.Text.Trim()), tbOpomba.Text.Trim());
            return new PlaciloObj(Convert.ToInt32(hdfPlaciloId.Value), Convert.ToInt32(ddlPopupTip.SelectedValue), Convert.ToInt32(ddlPopupUporabnikVnos.SelectedValue), Convert.ToInt32(ddlPopupUporabnikPlacal.SelectedValue), DateTime.MinValue, datum, Convert.ToDecimal(tbZnesek.Text.Trim()), tbOpomba.Text.Trim());
        } 
        #endregion

        #region DatumShortDateGet
        public string DatumShortDateGet(DateTime datum)
        {
            return Convert.ToDateTime(datum).ToShortDateString();
        }
        #endregion

        #region odsPregledPlacil_Selecting
        protected void odsPregledPlacil_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            DateTime datumPlacilaDo;
            DateTime datumPlacilaOd;

            datumPlacilaOd = Convert.ToDateTime(String.Format("01.{0}.{1}", ddlFilterMesec.SelectedValue == "-1" ? "01" : ddlFilterMesec.SelectedValue, ddlFilterLeto.SelectedValue == "-1" ? DateTime.MinValue.Year.ToString() : ddlFilterLeto.SelectedValue));

            if (ddlFilterLeto.SelectedValue == "-1")
            {
                datumPlacilaDo = Convert.ToDateTime(String.Format("01.{0}.{1}", ddlFilterMesec.SelectedValue == "-1" ? "12" : ddlFilterMesec.SelectedValue, ddlFilterLeto.SelectedValue == "-1" ? DateTime.MaxValue.Year.ToString() : ddlFilterLeto.SelectedValue));
            }
            else
            {
                datumPlacilaDo = Convert.ToDateTime(String.Format("01.{0}.{1}", ddlFilterMesec.SelectedValue == "-1" ? "12" : ddlFilterMesec.SelectedValue, ddlFilterLeto.SelectedValue == "-1" ? DateTime.MaxValue.Year.ToString() : ddlFilterLeto.SelectedValue)).AddMonths(1).AddDays(-1);
            }

            e.InputParameters["datum_placilo_od"] = datumPlacilaOd;
            e.InputParameters["datum_placilo_do"] = datumPlacilaDo;
            e.InputParameters["opis"] = tbFilterOpis.Text.Trim();
        } 
        #endregion

        //#region gvPregledPlacil_RowDataBound
        //protected void gvPregledPlacil_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        DataTable dt = new DataTable();
        //        DataView dv = new DataView();
        //        dv = odsPregledPlacil.Select() as DataView;
        //        dt = dv.ToTable();
        //        DataSetBaza.PlaciloDataTable dtPlacilo = new DataSetBaza.PlaciloDataTable();
        //        dtPlacilo.Merge(dt);

        //        decimal znesekSkupaj = (from x in dtPlacilo select PlaciloZnesekGet(x.znesek)).Sum();
        //        lblFooterPlacilSkupajB.Text = dtPlacilo.Rows.Count.ToString();
        //        lblFooterZnesekSkupajB.Text = String.Format("{0} EUR", znesekSkupaj.ToString());

        //        //Label lblFooterZnesekSkupaj = e.Row.FindControl("lblFooterZnesekSkupaj") as Label;
        //        //if (lblFooterZnesekSkupaj == null)
        //        //    return;
        //        //lblFooterZnesekSkupaj.Text = String.Format("{0} EUR", znesekSkupaj.ToString());
        //        //e.Row.Cells[4].Text = String.Format("<b/> {0} EUR </b>", znesekSkupaj.ToString());
        //    }
        //} 
        //#endregion

        #region PredolgiOpisSkrajsaj
        public string PredolgiOpisSkrajsaj(string opis)
        {
            if (opis.Length > 30)
                opis = opis.Substring(0, 30) + "...";

            return opis;
        } 
        #endregion

        #region odsPregledPlacil_Selected
        protected void odsPregledPlacil_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            tblFooterStatistika.Visible = true;
            DataSetBaza.PlaciloDataTable dtPlacilo = new DataSetBaza.PlaciloDataTable();
            dtPlacilo = ((DataSetBaza.PlaciloDataTable)e.ReturnValue);

            if (dtPlacilo.Rows.Count <= 0)
            {
                tblFooterStatistika.Visible = false;
                return;
            }

            decimal znesekSkupaj = (from x in dtPlacilo select PlaciloZnesekGet(x.znesek)).Sum();
            decimal znesekSkupajSimon = (from x in dtPlacilo select PlaciloOsebaGet(x.znesek, x.uporabnik_id_placal, EnumsDenar.Uporabnik.SimonGotlib, x.placilo_tip_id)).Sum();
            decimal znesekSkupajAnita = (from x in dtPlacilo select PlaciloOsebaGet(x.znesek, x.uporabnik_id_placal, EnumsDenar.Uporabnik.AnitaFranko, x.placilo_tip_id)).Sum();
            decimal znesekSkupajSimonSkupno = (from x in dtPlacilo select PlaciloDolgGet(x.znesek, x.uporabnik_id_placal, EnumsDenar.Uporabnik.SimonGotlib, x.placilo_tip_id)).Sum();
            decimal znesekSkupajAnitaSkupno = (from x in dtPlacilo select PlaciloDolgGet(x.znesek, x.uporabnik_id_placal, EnumsDenar.Uporabnik.AnitaFranko, x.placilo_tip_id)).Sum();

            //Label lblFooterZnesekSkupaj = e.Row.FindControl("lblFooterZnesekSkupaj") as Label;
            //if (lblFooterZnesekSkupaj == null)
            //    return;
            lblFooterPlacilSkupajB.Text = dtPlacilo.Rows.Count.ToString();
            lblFooterZnesekSkupajB.Text = String.Format("{0} EUR", znesekSkupaj.ToString());
            lblFooterZnesekSimonB.Text = String.Format("{0} EUR", znesekSkupajSimon);
            lblFooterZnesekAnitaB.Text = String.Format("{0} EUR", znesekSkupajAnita);
            lblFooterZnesekSkupnoSimonB.Text = String.Format("{0} EUR", znesekSkupajSimonSkupno);
            lblFooterZnesekSkupnoAnitaB.Text = String.Format("{0} EUR", znesekSkupajAnitaSkupno);
            lblFooterDolgSimonB.Text = znesekSkupajSimonSkupno - znesekSkupajAnitaSkupno >= 0 ? "/" : String.Format("{0} EUR", znesekSkupajAnitaSkupno - znesekSkupajSimonSkupno);
            lblFooterDolgAnitaB.Text = znesekSkupajAnitaSkupno - znesekSkupajSimonSkupno >= 0 ? "/" : String.Format("{0} EUR", znesekSkupajSimonSkupno - znesekSkupajAnitaSkupno);
        } 
        #endregion

        #region PlaciloZnesekGet
        protected decimal PlaciloZnesekGet(decimal stevilo)
        {
            if (stevilo == null || stevilo <= 0)
            {
                return 0;
            }
            return stevilo;
        }
        #endregion

        #region PlaciloOsebaGet
        protected decimal PlaciloOsebaGet(decimal stevilo, int uporabnik_id_placal, int uporabnik_id_upostevaj, int placilo_tip_id)
        {
            if (stevilo == null || stevilo <= 0 || uporabnik_id_placal != uporabnik_id_upostevaj)
            {
                return 0;
            }
            return stevilo;
        }
        #endregion

        #region PlaciloDolgGet
        protected decimal PlaciloDolgGet(decimal stevilo, int uporabnik_id_placal, int uporabnik_id_upostevaj, int placilo_tip_id)
        {
            if (stevilo == null || stevilo <= 0 || uporabnik_id_placal != uporabnik_id_upostevaj || placilo_tip_id == EnumsDenar.PlaciloTip.KotOsebniStrosek || placilo_tip_id == EnumsDenar.PlaciloTip.PartnerjuKotOsebniStrosek)
            {
                return 0;
            }

            if(EnumsDenar.PlaciloTip.PartnerjuVSkupno == placilo_tip_id)
            {
                return stevilo * 2;
            }

            return stevilo;
        }
        #endregion

        /// <summary>
        /// Metoda napolne objekt z parametri.
        /// </summary>
        /// <param name="vsebina"></param>
        /// <param name="vrsta">0-error, 1-opozorilo</param>
        #region FillOpozorilo
        public void FillOpozorilo(string vsebina, int vrsta, ucOpozorilo ucOpozorilo)
        {
            OpozoriloObj opozorilo = new OpozoriloObj();
            opozorilo.VSEBINA = vsebina;
            opozorilo.VRSTA = vrsta;
            ShowOpozorilo(opozorilo, ucOpozorilo);
        }
        #endregion
        /// <summary>
        /// Metoda prikaze opozorilo.
        /// </summary>
        /// <param name="opozoriloVsebina"></param>
        #region ShowOpozorilo
        public void ShowOpozorilo(OpozoriloObj opozorilo, ucOpozorilo ucOpozorilo)
        {
            ucOpozorilo.Visible = true;
            ucOpozorilo.Text = opozorilo.VSEBINA.ToString();
            ucOpozorilo.Vrsta = opozorilo.VRSTA;
        }
        #endregion

        /// <summary>
        /// Metoda skrije opozorilo.
        /// </summary>
        #region HideOpozorilo
        public void HideOpozorilo(ucOpozorilo ucOpozorilo)
        {
            ucOpozorilo.Visible = false;
            ucOpozorilo.Text = String.Empty;
        }
        #endregion

        #region ddlPopupTip_SelectedIndexChanged
        protected void ddlPopupTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPopupTip.SelectedItem.Value == EnumsDenar.PlaciloTip.IzSkupnega.ToString())
            {
                ddlPopupUporabnikPlacal.SelectedItem.Text = String.Format("{0} in {1}", EnumsDenar.Uporabnik.AllValues[EnumsDenar.Uporabnik.SimonGotlib].ToString(), EnumsDenar.Uporabnik.AllValues[EnumsDenar.Uporabnik.AnitaFranko].ToString());
                ddlPopupUporabnikPlacal.Enabled = false;
            }
            else
            {
                ddlPopupUporabnikPlacal.DataBind();
                ddlPopupUporabnikPlacal.Enabled = true;
            }

            mpuPlaciloPopup.Show();
        } 
        #endregion

        #region ddlOpombaPomoc_SelectedIndexChanged
        protected void ddlOpombaPomoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            NastaviVrednostiPopup(ddlOpombaPomoc);

            mpuPlaciloPopup.Show();
        }
        #endregion


        #region ddlOpombaPomoc_DataBound
        protected void ddlOpombaPomoc_DataBound(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl == null)
                return;

            ddl.Items.Insert(0, new ListItem("", "-1"));
        }
        #endregion

        #region NastaviVrednostiPopup
        public void NastaviVrednostiPopup(DropDownList ddlopombaValue)
        {
            int opombaValue;
            if (String.IsNullOrEmpty(ddlopombaValue.SelectedItem.Value) || !Int32.TryParse(ddlopombaValue.SelectedItem.Value, out opombaValue))
                return;

            if (opombaValue == EnumsDenar.OpombaOpis.SluzbaMalicaAdacta)
            {
                ddlPopupUporabnikPlacal.SelectedValue = EnumsDenar.Uporabnik.SimonGotlib.ToString();
                ddlPopupTip.SelectedValue = EnumsDenar.PlaciloTip.KotOsebniStrosek.ToString();
                tbZnesek.Text = EnumsDenar.Znesek.SluzbaMalicaAdacta;
            }
            else if (opombaValue == EnumsDenar.OpombaOpis.Zavarovanje)
            {
                ddlPopupUporabnikPlacal.SelectedValue = EnumsDenar.Uporabnik.SimonGotlib.ToString();
                ddlPopupTip.SelectedValue = EnumsDenar.PlaciloTip.KotOsebniStrosek.ToString();
                tbZnesek.Text = EnumsDenar.Znesek.Zavarovanje;
            }
            else if (opombaValue == EnumsDenar.OpombaOpis.Mobitel)
            {
                ddlPopupUporabnikPlacal.SelectedValue = EnumsDenar.Uporabnik.SimonGotlib.ToString();
                ddlPopupTip.SelectedValue = EnumsDenar.PlaciloTip.KotOsebniStrosek.ToString();
                tbZnesek.Text = EnumsDenar.Znesek.Mobitel;
            }
            else if (opombaValue == EnumsDenar.OpombaOpis.PodjetjePrispevki)
            {
                ddlPopupUporabnikPlacal.SelectedValue = EnumsDenar.Uporabnik.SimonGotlib.ToString();
                ddlPopupTip.SelectedValue = EnumsDenar.PlaciloTip.KotOsebniStrosek.ToString();
                tbZnesek.Text = EnumsDenar.Znesek.PodjetjePrispevki;
            }
            else if (opombaValue == EnumsDenar.OpombaOpis.PodjetjeDohodnina)
            {
                ddlPopupUporabnikPlacal.SelectedValue = EnumsDenar.Uporabnik.SimonGotlib.ToString();
                ddlPopupTip.SelectedValue = EnumsDenar.PlaciloTip.KotOsebniStrosek.ToString();
                tbZnesek.Text = EnumsDenar.Znesek.PodjetjeDohodnina;
            }

            tbOpomba.Text = ddlOpombaPomoc.SelectedItem.Text;
        } 
        #endregion
    }
}