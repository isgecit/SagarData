using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Attachment
{
    public partial class NotesReminder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetNotesReminder();
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
            }
        }
        private void GetNotesReminder()
        {
            NotesClass objNotes = new NotesClass();
            DataTable dt = objNotes.GetNotesReminder();
            if (dt.Rows.Count > 0)
            {
                gvData.DataSource = dt;
                gvData.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No record found');", true);
            }
        }
        protected void gvData_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            GetNotesReminder();
        }

        protected void gvData_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;
                string Status = row["Status"].ToString();
                if (Status == "New")
                {
                    e.Row.Cells[4].ForeColor = Color.Blue;
                }
                else
                {
                    e.Row.Cells[4].ForeColor = Color.Green;
                }
            }
        }
    }
}