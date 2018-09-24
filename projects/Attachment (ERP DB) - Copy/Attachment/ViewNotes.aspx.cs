using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class ViewNotes : System.Web.UI.Page
    {
        NotesClass objNotes;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["handle"] != null)
                    {
                        GetNotes();
                    }
                    else
                    {
                        GetAllNotes();
                        btnNewNotes.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
            }
        }
        private void GetNotes()
        {
            objNotes = new NotesClass();
            objNotes.NotesHandle = Request.QueryString["handle"];
            objNotes.IndexValue = Request.QueryString["index"];
            DataTable dt = objNotes.GetNotes();
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

        private void GetAllNotes()
        {
            objNotes = new NotesClass();
            DataTable dt = objNotes.GetAllNotes();
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
        protected void btnNewNotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Notes.aspx?handle="+ Request.QueryString["handle"]+ "&Index="+Request.QueryString["index"]+ "&user="+ Request.QueryString["user"]);
        }
    }
}