using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Attachment
{
    public partial class ViewLibrary : System.Web.UI.Page
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
            DataTable dt = objAttachmentcls.GetAllLibrary();
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
            Response.Redirect("CreateLibrary.aspx?LibCode=" + lnkUpdate.CommandArgument);
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnCreateLibrary_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateLibrary.aspx");
        }
    }
}