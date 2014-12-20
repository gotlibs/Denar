<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Placilo.aspx.cs" Inherits="Denar.Placilo" %>

<%@ Register Src="Opozorilo/ucOpozorilo.ascx" TagName="ucOpozorilo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControls/ucModalPopup.ascx" TagName="ucModalPopUp" TagPrefix="ucpopup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Opozorilo/ucOpozorilo.ascx" TagName="ucOpozorilo" TagPrefix="uc20" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/ico" href="Images/money.gif" />
    <link href="~/Styles/dizajn.css" type="text/css" rel="stylesheet" />
    <script src="~/Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="~/Scripts/Denar.js" type="text/javascript"></script>
    <title></title>
</head>
<body onclick="bodyClick(event)">
    <script language="javascript" type="text/javascript">
        function MsgBoxOpen() {
            if (confirm("Ali resnično želite izbrisati plačilo?") == true)
                return true;
            else
                return false;
        }
    </script>
    <form id="form1" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlFilter">
        <ContentTemplate>
            <div style="width: 1000px; padding-left: 20px;">
                <asp:ScriptManager ID="scriptMgrMain" runat="server">
                </asp:ScriptManager>
                <div class="naslov">
                    <asp:Label ID="lblNaslov" runat="server" Text="Pregled odhodkov družine GOTLIB & FRANKO" />
                </div>
                <uc20:ucOpozorilo ID="ucOpozorilo1" runat="server" />
                <div runat="server" id="divDodajPlacilo" style="margin-bottom: 20px;">
                    <asp:ImageButton runat="server" ID="ibDodajPlacilo" OnClick="lbVnosNovegaPlacila_Click"
                        ImageUrl="~/Images/add.gif" />
                    <asp:LinkButton runat="server" ID="lbDodajPlacilo" CssClass="textDecNone hover" Text="Dodaj novo plačilo"
                        OnClick="lbVnosNovegaPlacila_Click"></asp:LinkButton>
                </div>
                <asp:Panel runat="server" ID="pnlMainProgramFilter" DefaultButton="btnFilterPrikazi">
                    <div class="form" style="width: 528px">
                        <div class="formHead" runat="server" id="divFormHead" enableviewstate="true">
                            <div class="formTitle">
                                Filter plačil
                                <asp:Label runat="server" ID="lblSteviloUpostevanihFiltrov" /></div>
                            <div class="formToogle">
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div id="divFilterBody" runat="server" class="formBody formClosed">
                            <div class="formContent">
                                <table cellspacing="10">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFilterLeto" Text="Leto:" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlFilterLeto" DataValueField="Key" DataTextField="Value"
                                                CssClass="width200" OnDataBound="ddlFilterLeto_DataBound" OnSelectedIndexChanged="ddlFilterLeto_SelectedIndexChanged"
                                                AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFilterMesec" Text="Mesec" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlFilterMesec" OnDataBound="ddlMesec_DataBound"
                                                DataValueField="Key" DataTextField="Value" CssClass="width200" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFilterUporabnikPlacal" Text="Plačnik" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlFilterUporabnikPlacal" OnDataBound="ddlInt_DataBound"
                                                DataValueField="uporabnik_id" DataTextField="uporabnik_naziv" DataSourceID="odsFilterUporabnikPlacal"
                                                CssClass="width200">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsFilterUporabnikPlacal" runat="server" SelectMethod="UporabnikGet"
                                                TypeName="Denar.BazaDB"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFilterTip" Text="Tip plačila:" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlFilterTip" OnDataBound="ddlInt_DataBound"
                                                DataValueField="placilo_tip_id" DataTextField="naziv" DataSourceID="odsFilterTip"
                                                CssClass="width200">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsFilterTip" runat="server" SelectMethod="PlaciloTipGet"
                                                TypeName="Denar.BazaDB"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFilterOpis" Text="Opis:" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbFilterOpis" Width="198px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="formToolbar">
                                    <asp:Button runat="server" ID="btnFilterPrikazi" CssClass="btn btnApply" Text="Prikaži"
                                        OnClick="btnFilterPrikazi_Click" />
                                    <asp:Button Text="Počisti" runat="server" ID="btnPocisti" CssClass="btn btnReset"
                                        OnClick="btnPocisti_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </asp:Panel>
                <div id="divPregledPlacil">
                    <asp:GridView runat="server" ID="gvPregledPlacil" GridLines="None" AutoGenerateColumns="False"
                        CssClass="grid" DataSourceID="odsPregledPlacil" AllowSorting="True" CellPadding="3"
                        OnRowCommand="gvPregledPlacil_RowCommand" OnSorting="gvPregledPlacil_Sorting"
                        DataKeyNames="PLACILO_ID,PLACILO_TIP_ID,PLACILO_TIP_NAZIV,UPORABNIK_ID_VNESEL,UPORABNIK_NAZIV_VNESEL,UPORABNIK_ID_PLACAL,UPORABNIK_NAZIV_PLACAL,DATUM_VNOS,DATUM_PLACILO,ZNESEK,OPIS"
                        PagerSettings-Position="Bottom" PagerStyle-CssClass="gridPager" AllowPaging="True"
                        PageSize="15">
                        <AlternatingRowStyle CssClass="gridViewAlternatingRowStyle"></AlternatingRowStyle>
                        <HeaderStyle CssClass="gvHeader" />
                        <EmptyDataTemplate>
                            <asp:Label runat="server" ID="lblNiPodatkov" Text="Ni podatkov za prikaz." />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbZapStGlava" runat="server" Text="Zap. st." CssClass="textDecNone colorBlack hover"
                                        CommandName="Sort" CommandArgument="zap_st"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblZapSt" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# Eval("zap_st") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="lbPlaciloIdGlava" runat="server" Text="Id plačila" CssClass="textDecNone colorBlack hover"
                                CommandName="Sort" CommandArgument="placilo_id"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPlaciloId" runat="server" Style="text-decoration: none; color: Black;"
                                Text='<%# Eval("placilo_id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbTipGlava" runat="server" Text="Tip plačila" CssClass="textDecNone colorBlack hover"
                                        CommandName="Sort" CommandArgument="placilo_tip_naziv"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaciloTip" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# Eval("placilo_tip_naziv") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbUporabnikVneselGlava" runat="server" Text="Vnesel uporabnik"
                                        CssClass="textDecNone colorBlack hover" CommandName="Sort" CommandArgument="uporabnik_naziv_vnesel"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUporabnikVnesel" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# Eval("uporabnik_naziv_vnesel") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbUporabnikPlacalGlava" runat="server" Text="Plačal uporabnik"
                                        CssClass="textDecNone colorBlack hover" CommandName="Sort" CommandArgument="uporabnik_naziv_placal"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUporabnikPlacal" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# Eval("uporabnik_naziv_placal") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbDatumGlava" runat="server" Text="Datum plačila" CssClass="textDecNone colorBlack hover"
                                        CommandName="Sort" CommandArgument="datum_placilo"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDatum" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# DatumShortDateGet(Convert.ToDateTime(Eval("datum_placilo"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbZnesekGlava" runat="server" Text="Znesek" CssClass="textDecNone colorBlack hover"
                                        CommandName="Sort" CommandArgument="znesek"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblZnesek" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# String.Format("{0} EUR", Eval("znesek").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbOpisGlava" runat="server" Text="Opis" CssClass="textDecNone colorBlack hover"
                                        CommandName="Sort" CommandArgument="opis"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOpis" runat="server" Style="text-decoration: none; color: Black;"
                                        Text='<%# PredolgiOpisSkrajsaj(Eval("opis").ToString()) %>' ToolTip='<%# Eval("opis") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="220px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibUredi" runat="server" CommandName="uredi" ToolTip="Uredi plačilo"
                                        CommandArgument='<%# Eval("placilo_id") %>' ImageUrl="~/Images/edit.gif" />
                                    <asp:ImageButton ID="ibIzbrisi" runat="server" OnClientClick="return MsgBoxOpen()"
                                        CommandName="izbrisi" ToolTip="Izbriši plačilo" CommandArgument='<%# Eval("placilo_id") %>'
                                        ImageUrl="~/Images/delete.gif" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                                <%--                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblFooterZnesekSkupaj"></asp:Label>
                        </FooterTemplate>--%>
                            </asp:TemplateField>
                            <%--                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblFooterZnesekSkupaj"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                    <table runat="server" id="tblFooterStatistika" style="float: right; font-weight: bold;">
                        <tr>
                            <td style="width: 250px; text-align: right;">
                                <asp:Label runat="server" ID="lblFooterPlacilSkupaj" Text="Število plačil:"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:Label runat="server" ID="lblFooterPlacilSkupajB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label runat="server" ID="lblFooterZnesekSkupaj" Text="Znesek skupaj:"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:Label runat="server" ID="lblFooterZnesekSkupajB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label runat="server" ID="lblFooterZnesekSimon" Text="Znesek Simon Gotlib (vse skupaj):"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:Label runat="server" ID="lblFooterZnesekSimonB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label runat="server" ID="lblFooterZnesekAnita" Text="Znesek Anita Franko (vse skupaj):"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:Label runat="server" ID="lblFooterZnesekAnitaB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label runat="server" ID="lblFooterZnesekSkupnoSimon" Text="Znesek Simon Gotlib (skupno):"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:Label runat="server" ID="lblFooterZnesekSkupnoSimonB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label runat="server" ID="lblFooterZnesekSkupnoAnita" Text="Znesek Anita Franko (skupno):"></asp:Label>
                            </td>
                            <td style="padding-left: 10px;">
                                <asp:Label runat="server" ID="lblFooterZnesekSkupnoAnitaB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; color: Red;">
                                <asp:Label runat="server" ID="lblFooterDolgSimon" Text="Dolg Simon Gotlib (skupno):"></asp:Label>
                            </td>
                            <td style="padding-left: 10px; color: Red;">
                                <asp:Label runat="server" ID="lblFooterDolgSimonB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; color: Red;">
                                <asp:Label runat="server" ID="lblFooterDolgAnita" Text="Dolg Anita Franko (skupno):"></asp:Label>
                            </td>
                            <td style="padding-left: 10px; color: Red;">
                                <asp:Label runat="server" ID="lblFooterDolgAnitaB"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:ObjectDataSource ID="odsPregledPlacil" runat="server" SelectMethod="PlaciloGet"
                        TypeName="Denar.BazaBL" OldValuesParameterFormatString="original_{0}" OnSelecting="odsPregledPlacil_Selecting"
                        OnSelected="odsPregledPlacil_Selected">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="placilo_id" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlFilterTip" DefaultValue="-1" Name="placilo_tip_id"
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlFilterUporabnikPlacal" DefaultValue="-1" Name="uporabnik_id_placal"
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:Parameter DefaultValue="01.01.0001" Name="datum_placilo_od" Type="DateTime" />
                            <asp:Parameter DefaultValue="01.01.2001" Name="datum_placilo_do" Type="DateTime" />
                            <asp:ControlParameter ControlID="tbFilterOpis" DefaultValue="" Name="opis" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div id="divPopup">
                    <ucpopup:ucModalPopUp ID="mpuPlaciloPopup" runat="server" DivCssClass="popupMain width920 maxHeight570">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hdfPlaciloId" />
                            <asp:HiddenField runat="server" ID="hdfPageMode" />
                            <div class="naslovPopup">
                                <asp:Label ID="lblNaslovPopup" runat="server" />
                            </div>
                            <div>
                                <uc1:ucOpozorilo ID="ucOpozoriloPU" runat="server" />
                            </div>
                            <div style="margin-bottom: 30px;">
                                <asp:Label runat="server" ID="lblZvezdica" Style="font-style: italic;" Text="Zvezdica (*) označuje obvezna polja."></asp:Label></div>
                            <div style="margin-top: 10px;">
                                <table cellspacing="10">
                                    <tr>
                                        <td style="width: 130px; text-align: right;">
                                            <asp:Label runat="server" ID="lblDatum" Text="Datum *"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" CssClass="width200 margL10" ID="tbDatumPopup"></asp:TextBox>
                                            <asp:Image runat="server" ID="imgCalDatumLastnosti" Style="vertical-align: middle"
                                                ImageUrl="~/Images/calendar.png" />
                                            <ajax:CalendarExtender ID="ceDatumPopup" runat="server" TargetControlID="tbDatumPopup"
                                                Format="d.M.yyyy" PopupPosition="BottomLeft" PopupButtonID="imgCalDatumLastnosti"
                                                FirstDayOfWeek="Monday">
                                            </ajax:CalendarExtender>
                                            <%--                                            <asp:RangeValidator ID="rvDatum" runat="server" ControlToValidate="tbDatumPopup"
                                                ErrorMessage="*Vnešen nepravilen datum." ForeColor="Red" MaximumValue="1.1.2100"
                                                MinimumValue="1.1.1900" Type="Date" Display="Dynamic" ValidationGroup="popup"></asp:RangeValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvDatum" runat="server" ValidationGroup="popup"
                                                ForeColor="Red" ErrorMessage="*Datum je obvezno polje." ControlToValidate="tbDatumPopup"
                                                Display="Dynamic" />
                                            <asp:Label runat="server" ID="lblDatumErrorPopup" ForeColor="Red" Text="*Napačen datum."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblPopupTip" Text="Tip plačila *" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlPopupTip" DataValueField="placilo_tip_id"
                                                DataTextField="naziv" DataSourceID="odsPopupTip" CssClass="width200 margL10"
                                                OnSelectedIndexChanged="ddlPopupTip_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsPopupTip" runat="server" SelectMethod="PlaciloTipGet"
                                                TypeName="Denar.BazaDB"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblPopupUporabnikVnos" Text="Uporabnik, ki vnaša *"
                                                Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlPopupUporabnikVnos" DataValueField="uporabnik_id"
                                                DataTextField="uporabnik_naziv" DataSourceID="odsPopupUporabnikVnos" CssClass="width200 margL10">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsPopupUporabnikVnos" runat="server" SelectMethod="UporabnikGet"
                                                TypeName="Denar.BazaDB"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblPopupUporabnikPlacal" Text="Uporabnik, ki plačuje *"
                                                Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlPopupUporabnikPlacal" DataValueField="uporabnik_id"
                                                DataTextField="uporabnik_naziv" DataSourceID="odsPopupUporabnikPlacal" CssClass="width200 margL10">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsPopupUporabnikPlacal" runat="server" SelectMethod="UporabnikGet"
                                                TypeName="Denar.BazaDB"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblZnesek" Text="Znesek *" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbZnesek" CssClass="width200 margL10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvZnesek" runat="server" ValidationGroup="popup"
                                                ForeColor="Red" ErrorMessage="*Znesek je obvezno polje." ControlToValidate="tbZnesek"
                                                Display="Dynamic" />
                                            <%--                                            <asp:RegularExpressionValidator ID="revZnesek" runat="server" ErrorMessage="*Vnesti morate samo številke."
                                                ControlToValidate="tbZnesek" ValidationExpression="^[0-9]*$" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="popup" />--%>
                                            <asp:Label runat="server" ID="lblZnesekError" ForeColor="Red" Text="*Napačen vnos."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblOpomba" Text="Opomba" Style="float: right;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbOpomba" MaxLength="1000" TextMode="MultiLine" Columns="50"
                                                Rows="5" CssClass="margL10"></asp:TextBox>
                                            <div style="vertical-align: top; width: 200px; float: right;">
                                                <asp:Label runat="server" ID="lblOpombaPomoc" Text="Pomoč za opombe" CssClass="margL10"></asp:Label><br />
                                                <asp:DropDownList runat="server" ID="ddlOpombaPomoc" DataValueField="Key" DataTextField="Value"
                                                    DataSourceID="odsOpombaPomoc" CssClass="width200 margL10" Style="vertical-align: top;"
                                                    OnSelectedIndexChanged="ddlOpombaPomoc_SelectedIndexChanged" AutoPostBack="true"
                                                    OnDataBound="ddlOpombaPomoc_DataBound">
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="odsOpombaPomoc" runat="server" SelectMethod="OpisGetAll"
                                                    TypeName="Denar.BazaBL"></asp:ObjectDataSource>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--                  <div style="margin-top: 10px;">
                        <asp:Label runat="server" ID="lblOpomba" Text="Vnesite opombo"></asp:Label>
                        <br />
                        <asp:TextBox runat="server" ID="tbOpomba" MaxLength="1000" TextMode="MultiLine" Columns="50"
                            Rows="5"></asp:TextBox>
                    </div>--%>
                            <div class="popupToolbar" style="margin-top: 10px;">
                                <asp:LinkButton runat="server" ID="lbPotrdi" Text="Shrani" CssClass="btn btnSave"
                                    CommandArgument="shrani" OnCommand="lbtnShrani_Command" ValidationGroup="popup" />
                                <asp:LinkButton runat="server" ID="lbPreklici" Text="Prekliči" CssClass="btn btnCancel"
                                    CommandArgument="preklici" OnCommand="lbtnShrani_Command" />
                                <div class="clear">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ucpopup:ucModalPopUp>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
