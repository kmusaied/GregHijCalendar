using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace KhaledLabs
{
    [DefaultProperty("SelectedDate")]
    [ToolboxData("<{0}:GregHijCalendar runat=server></{0}:GregHijCalendar>")]
    [ValidationProperty("Text")]
    public class GregHijCalendar : WebControl, ICallbackEventHandler
    {

        #region Fields

        TextBox dayBox;
        TextBox monthBox;
        TextBox yearBox;
        DropDownList calBox;
        Image errorImage;
        int breakerYear = 1800;

        #endregion

        #region Properties
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {

                if (!SelectedDate.HasValue)
                    return string.Empty;
                else
                    return SelectedDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US").DateTimeFormat);

            }

        }



        [Browsable(true)]
        [Category("Calendar")]
        public string InvalidDateErrorMessage
        {
            get
            {
                return ViewState["InvalidDateErrorMessage"] == null ? "Invalid Date" : ViewState["InvalidDateErrorMessage"].ToString();
            }
            set
            {
                ViewState["InvalidDateErrorMessage"] = value;
            }
        }

        [Browsable(true)]
        [Category("Calendar")]
        public string HijriCalendarCaption
        {
            get
            {
                return ViewState["HijriCalendarCaption"] == null ? " هـ " : ViewState["HijriCalendarCaption"].ToString();
            }
            set
            {
                ViewState["HijriCalendarCaption"] = value;
            }
        }

        [Browsable(true)]
        [Category("Calendar")]
        public string GregorianCalendarCaption
        {
            get
            {
                return ViewState["GregorianCalendarCaption"] == null ? " م " : ViewState["GregorianCalendarCaption"].ToString();
            }
            set
            {
                ViewState["GregorianCalendarCaption"] = value;
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        public string ValidationGroup
        {
            get
            {
                return ViewState["ValidationGroup"] == null ? string.Empty : ViewState["ValidationGroup"].ToString();
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }

        public enum CalendarTypes
        {
            Gregorian = 0, Hijri = 1
        }

        [Browsable(true)]
        [Category("Calendar")]
        public CalendarTypes DefaultCalendar
        {
            get
            {
                object obj = ViewState["DefaultCalendar"];
                if (obj == null)
                    return CalendarTypes.Gregorian;

                CalendarTypes calender = (CalendarTypes)obj;
                return calender;
            }
            set
            { ViewState["DefaultCalendar"] = value; }
        }

        [Browsable(true)]
        [Category("Calendar")]
        public bool DualCalendar
        {
            get
            {
                object obj = ViewState["DualCalendar"];
                if (obj == null)
                    return true;

                return (bool)obj;
            }
            set
            { ViewState["DualCalendar"] = value; }
        }

        [Browsable(true)]
        [Category("Calendar")]
        public bool RegisterJQuery
        {
            get
            {
                object obj = ViewState["RegisterJQuery"];
                if (obj == null)
                    return false;

                return (bool)obj;
            }
            set
            { ViewState["RegisterJQuery"] = value; }
        }


        [Browsable(true)]
        [Category("Calendar")]
        public bool ShowToday
        {
            get
            {
                object obj = ViewState["ShowToday"];
                if (obj == null)
                    return true;

                return (bool)obj;
            }
            set
            { ViewState["ShowToday"] = value; }
        }

        [Browsable(true)]
        [Category("Calendar")]
        public DateTime? SelectedDate
        {
            get
            {


                if (dayBox == null || monthBox == null || yearBox == null)
                    return null;

                if (!string.IsNullOrEmpty(dayBox.Text) && !string.IsNullOrEmpty(monthBox.Text) && !string.IsNullOrEmpty(yearBox.Text))
                {
                    try
                    {
                        if (calBox.SelectedValue == "M")
                            return new DateTime(int.Parse(yearBox.Text), int.Parse(monthBox.Text), int.Parse(dayBox.Text), new GregorianCalendar());
                        else
                            return new DateTime(int.Parse(yearBox.Text), int.Parse(monthBox.Text), int.Parse(dayBox.Text), new HijriCalendar());
                    }
                    catch (Exception)
                    {
                        ShowErrorMessage();
                        return null;
                    }

                }

                object obj = ViewState["SelectedDate"];
                if (obj != null)
                    return (DateTime)obj;
                else
                    return null;

            }
            set
            {
                ViewState["SelectedDate"] = value;
            }
        }


        #endregion

        #region Events
        protected override void OnLoad(EventArgs e)
        {
            HideErrorMessage();

            RegisterJQueryScript();

            RegisterAjaxScript();

            BindProperties();

            base.OnLoad(e);
        }



        protected override void OnInit(EventArgs e)
        {

            BuildControl();

            base.OnInit(e);
        }




        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string resourceName = "KhaledLabs.Resources.GregHijCalendar.js";

            ClientScriptManager cs = this.Page.ClientScript;
            cs.RegisterClientScriptResource(typeof(GregHijCalendar), resourceName);

        }

        #endregion

        #region Private Methods
        private void BindProperties()
        {
            if (SelectedDate.HasValue)
            {
                CultureInfo locale = null;
                if (calBox.SelectedValue == "M")
                    locale = new CultureInfo("en-US");
                else
                {
                    locale = new CultureInfo("ar-SA");
                    //locale.DateTimeFormat.Calendar = new UmAlQuraCalendar() ;
                }

                dayBox.Text = SelectedDate.Value.ToString("dd", locale.DateTimeFormat);
                monthBox.Text = SelectedDate.Value.ToString("MM", locale.DateTimeFormat);
                yearBox.Text = SelectedDate.Value.ToString("yyyy", locale.DateTimeFormat);

            }

            errorImage.ToolTip = InvalidDateErrorMessage;
        }

        private void RegisterJQueryScript()
        {
            if (RegisterJQuery)
            {
                ClientScriptManager cm = Page.ClientScript;
                string resourceName = "KhaledLabs.Resources.jquery.js";
                cm.RegisterClientScriptResource(typeof(GregHijCalendar), resourceName);
            }
        }
        private void RegisterAjaxScript()
        {
            ClientScriptManager cm = Page.ClientScript;
            String cbReference = cm.GetCallbackEventReference(this, "arg",
                "ReceiveServerData", "");
            String callbackScript = "function CallServer(arg, context) {" +
                cbReference + "; }";
            cm.RegisterClientScriptBlock(this.GetType(),
                "CallServer", callbackScript, true);
        }

        private void BuildControl()
        {
            this.Controls.Clear();

            Table table = new Table();
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            table.ID = "calTabl" + Guid.NewGuid().GetHashCode().ToString();
            table.CssClass = "calTbl";
            table.CellPadding = 0;
            table.CellSpacing = 1;

            // Day Box
            dayBox = new TextBox();
            dayBox.CssClass = "dayBox";
            dayBox.Width = Unit.Parse("20px");
            dayBox.MaxLength = 2;
            //dayBox.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            cell.Controls.Add(dayBox);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = " / ";
            row.Cells.Add(cell);

            //Month Box
            cell = new TableCell();
            monthBox = new TextBox();
            monthBox.CssClass = "monthBox";
            //monthBox.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            monthBox.MaxLength = 2;
            monthBox.Width = Unit.Parse("20px");

            cell.Controls.Add(monthBox);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = " / ";
            row.Cells.Add(cell);

            //Year Box
            cell = new TableCell();
            yearBox = new TextBox();
            //yearBox.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            yearBox.CssClass = "yearBox";
            yearBox.MaxLength = 4;
            yearBox.Width = Unit.Parse("30px");

            cell.Controls.Add(yearBox);
            row.Cells.Add(cell);


            //Calendar Type Box
            cell = new TableCell();
            calBox = new DropDownList();
            calBox.CssClass = "calBox";

            //calBox.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            calBox.Items.Add(new ListItem(HijriCalendarCaption, "H"));
            calBox.Items.Add(new ListItem(GregorianCalendarCaption, "M"));
            if (calBox != null)
                calBox.SelectedValue = DefaultCalendar == CalendarTypes.Gregorian ? "M" : "H";
            if (!DualCalendar)
            {
                //just hide the calendar box
                calBox.Style["display"] = "none";
                cell.Controls.Add(new LiteralControl(calBox.SelectedItem.Text));
            }

            cell.Controls.Add(calBox);
            row.Cells.Add(cell);

            //Loading & Error image Cell
            cell = new TableCell();
            ClientScriptManager cs = this.Page.ClientScript;
            string resourceName;

            if (ShowToday)
            {
                resourceName = "KhaledLabs.Resources.today.png";
                ImageButton todayButton = new ImageButton();
                todayButton.ImageUrl = ResolveUrl(cs.GetWebResourceUrl(typeof(GregHijCalendar), resourceName));
                todayButton.CssClass = "todayBtn";
                cell.Controls.Add(todayButton);
            }

            resourceName = "KhaledLabs.Resources.loading.gif";
            Image loadingImage = new Image();
            loadingImage.ImageUrl = ResolveUrl(cs.GetWebResourceUrl(typeof(GregHijCalendar), resourceName));
            loadingImage.CssClass = "imgLoad";
            loadingImage.Style["display"] = "none";
            cell.Controls.Add(loadingImage);

            resourceName = "KhaledLabs.Resources.error.png";
            errorImage = new Image();
            errorImage.ImageUrl = ResolveUrl(cs.GetWebResourceUrl(typeof(GregHijCalendar), resourceName));
            errorImage.CssClass = "imgError";
            errorImage.Style["display"] = "none";
            errorImage.ToolTip = "Invalid Date";
            cell.Controls.Add(errorImage);
            row.Cells.Add(cell);

            table.Rows.Add(row);
            Controls.Add(table);
        }

        private void ShowErrorMessage()
        {
            errorImage.Style["display"] = "block";
        }

        private void HideErrorMessage()
        {
            errorImage.Style["display"] = "none";
        }

        #endregion

        #region ICallbackEventHandler Members
        string callbackArg = null;
        public string GetCallbackResult()
        {

            string[] op = callbackArg.Split('|');

            string[] dateParts = op[1].Split('/');

            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            string calType = dateParts[3];
            string elementID = dateParts[4];

            if (op[0] == "C")
            {
                DateTime newDate;
                try
                {
                    if (calType == "M")
                    {
                        //convert to hijri
                        if (year >= breakerYear)
                            throw new Exception(string.Format("Invalid Hijri Date {0}/{1}/{2}", day, month, year));

                        newDate = new DateTime(year, month, day, new UmAlQuraCalendar());
                        callbackArg = newDate.ToString("dd/MM/yyyy/", new CultureInfo("en-US").DateTimeFormat);
                    }
                    else
                    {
                        if (year <= breakerYear)
                            throw new Exception(string.Format("Invalid Gerogrian Date {0}/{1}/{2}", day, month, year));

                        newDate = new DateTime(year, month, day);
                        callbackArg = newDate.ToString("dd/MM/yyyy/", new CultureInfo("ar-SA").DateTimeFormat);
                    }
                }
                catch (Exception)
                {
                    return "V|Invalid Date/" + elementID;
                }

                callbackArg += calType == "M" ? "H" : "M";
                callbackArg += "/" + elementID;
                return "C|" + callbackArg;
            }
            else if (op[0] == "V")
            {

                try
                {
                    DateTime newDate;
                    if (calType == "H")
                    {
                        if (year >= breakerYear)
                            throw new Exception(string.Format("Invalid Hijri Date {0}/{1}/{2}", day, month, year));

                        newDate = new DateTime(year, month, day, new HijriCalendar());
                        callbackArg = string.Format("{0}/{1}/{2}/", newDate.Day, newDate.Month, newDate.Year);
                    }
                    else
                    {

                        if (year <= breakerYear)
                            throw new Exception(string.Format("Invalid Gerogrian Date {0}/{1}/{2}", day, month, year));


                        newDate = new DateTime(year, month, day);
                        callbackArg = newDate.ToString("dd/MM/yyyy/", new CultureInfo("en-US").DateTimeFormat);
                    }

                    return "V|OK/" + elementID;
                }
                catch (Exception)
                {
                    return "V|Invalid Date/" + elementID;
                }

            }
            else if (op[0] == "T")
            {
                try
                {
                    DateTime newDate;
                    if (calType == "H")
                    {
                        callbackArg = DateTime.Now.ToString("dd/MM/yyyy/", new CultureInfo("ar-SA").DateTimeFormat);
                    }
                    else
                    {
                        newDate = DateTime.Now;
                        callbackArg = newDate.ToString("dd/MM/yyyy/", new CultureInfo("en-US").DateTimeFormat);
                    }

                    return "T|" + callbackArg + calType + "/" + elementID;
                }
                catch (Exception)
                {
                    return "T|Unable to Get Today Date/" + elementID;
                }
            }
            else
            { return "E|Invalid Argument"; }
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            callbackArg = eventArgument;

        }

        #endregion
    }

}