using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class CreateDatabase : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindLibrary();
                    if (Request.QueryString["DBID"] != null)
                    {
                        GetDatabase();
                        lnkDelete.Visible = true;
                    }
                    else
                    {
                        lnkDelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void BindLibrary()
        {
            objAttachmentcls = new AttachmentCls();
            DataTable dt = objAttachmentcls.GetAllActiveLibrary();
            ddlLibrary.DataSource = dt;
            ddlLibrary.DataTextField = "t_lbcd";
            ddlLibrary.DataValueField = "t_lbcd";
            ddlLibrary.DataBind();
        }
        private void GetDatabase()
        {
            try
            {
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.DBID = Request.QueryString["DBID"];
                DataTable dt = objAttachmentcls.GetDBID();
                if (dt.Rows.Count > 0)
                {
                    txtDBID.Text = dt.Rows[0]["t_dbid"].ToString();
                    txtDBServerName.Text = dt.Rows[0]["t_serv"].ToString();
                    txtDatabaseName.Text = dt.Rows[0]["t_dbnm"].ToString();
                    txtDBDesc.Text = dt.Rows[0]["t_desc"].ToString();
                    ddlLibrary.SelectedValue = dt.Rows[0]["t_lbcd"].ToString();
                    btnSaveDatabaseDetails.Text = "Update";
                    txtDBID.Enabled = false;
                    divHeader.InnerText = "Update Database";
                }
                else
                {
                    btnSaveDatabaseDetails.Text = "Save";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnSaveDatabaseDetails_Click(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                if (txtDBID.Text != "" && txtDBServerName.Text != "" && txtDatabaseName.Text != "" && txtDBDesc.Text != "")
                {
                    objAttachmentcls = new AttachmentCls();
                    objAttachmentcls.LibraryCode = ddlLibrary.SelectedValue;
                    objAttachmentcls.DBID = txtDBID.Text.Trim();
                    objAttachmentcls.DBServer = txtDBServerName.Text.Trim();
                    objAttachmentcls.DatabaseName = txtDatabaseName.Text.Trim();
                    objAttachmentcls.DBDescription = txtDBDesc.Text.Trim();
                    if (btnSaveDatabaseDetails.Text == "Save")
                    {
                        DataTable dt = objAttachmentcls.GetDBID();
                        if (dt.Rows.Count == 0)
                        {
                            res = objAttachmentcls.InsertDBdetails();
                            if (res > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Saved Successfully');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Not Saved');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('DBID already exist');", true);
                        }
                    }
                    else
                    {
                        res = objAttachmentcls.UpdateDatabase();
                        if (res > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Updated Successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Not Updated');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please enter all data');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.DBID = Request.QueryString["DBID"];
                DataTable dtUSedDBID = objAttachmentcls.GetDBIDInHandle();
                if (dtUSedDBID.Rows[0]["Dbidcount"].ToString() == "0")
                {
                    int res = objAttachmentcls.DeleteDatabase();
                    if (res > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Deleted Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('DBID used somewhere');", true);
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}