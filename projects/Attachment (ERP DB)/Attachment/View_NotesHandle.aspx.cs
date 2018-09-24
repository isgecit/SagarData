using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class View_NotesHandle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNotesHandle();
            }
        }
        private void BindNotesHandle()
        {
            NotesClass objNotes = new NotesClass();
            DataTable dt = objNotes.GetALLNotesHandle();
            gvData.DataSource = dt;
            gvData.DataBind();
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Note_Handle.aspx");
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdate = (LinkButton)sender;
            Response.Redirect("Note_Handle.aspx?handle=" + lnkUpdate.CommandArgument);
        }
    }
}