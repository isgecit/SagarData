using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class ViewDatabase : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void BindGrid()
        {
            objAttachmentcls = new AttachmentCls();
            DataTable dt = objAttachmentcls.GetALLDBID();
            if (dt.Rows.Count > 0)
            {
                gvData.DataSource = dt;
                gvData.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found');", true);
            }
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdate = (LinkButton)sender;
            Response.Redirect("CreateDatabase.aspx?DBID=" + lnkUpdate.CommandArgument);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateDatabase.aspx");
        }
    }
}