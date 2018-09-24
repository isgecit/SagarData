using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class CreateLibrary : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["LibCode"] != null)
                    {
                        GetLibrary();
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void GetLibrary()
        {
            try
            {
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.LibraryCode = Request.QueryString["LibCode"];
                DataTable dt = objAttachmentcls.GetLibCode();
                if (dt.Rows.Count > 0)
                {
                    txtLibraryCode.Text = dt.Rows[0]["t_lbcd"].ToString();
                    txtLibDesc.Text = dt.Rows[0]["t_desc"].ToString();
                    txtPath.Text = dt.Rows[0]["t_path"].ToString();
                    txtServerName.Text = dt.Rows[0]["t_serv"].ToString();
                    ddlIsActive.SelectedValue = dt.Rows[0]["t_acti"].ToString();
                    btnSaveLibrary.Text = "Update";
                    txtLibraryCode.Enabled = false;
                    divHeader.InnerText = "Update Library";
                    btnDelete.Visible = true;
                }
                else
                {
                    btnSaveLibrary.Text = "Save";
                    btnDelete.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnSaveLibrary_Click(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                if (txtLibraryCode.Text != "" && txtLibDesc.Text != "" && txtPath.Text != "" && txtServerName.Text != "")
                {
                    objAttachmentcls = new AttachmentCls();
                    objAttachmentcls.LibraryCode = txtLibraryCode.Text.Trim();
                    objAttachmentcls.LibraryDescription = txtLibDesc.Text.Trim();
                    objAttachmentcls.Path = txtPath.Text.Trim();
                    objAttachmentcls.IsActive = ddlIsActive.SelectedValue;
                    objAttachmentcls.ServerName = txtServerName.Text.Trim();

                    if (btnSaveLibrary.Text == "Save")
                    {
                        DataTable dt = objAttachmentcls.GetLibCode();
                        if (dt.Rows.Count == 0)
                        {
                            res = objAttachmentcls.InsertLibrary();
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
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Library Code already exist');", true);
                        }
                    }
                    else
                    {
                        if (ddlIsActive.SelectedValue == "N")
                        {
                            DataTable dtLibCode = objAttachmentcls.GetLibraryCodeFromDataBase();
                            if (dtLibCode.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Library Code Already in used so it is not Deactivated');", true);
                            }
                            else
                            {
                                res = objAttachmentcls.UpdateLibrary();
                            }
                        }
                        else
                        {
                            res = objAttachmentcls.UpdateLibrary();

                        }

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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.LibraryCode = Request.QueryString["LibCode"];
                DataTable dtLibCode = objAttachmentcls.GetLibraryCodeFromDataBase();
                if (dtLibCode.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Library Code already in used so it is not deleted');", true);
                }
                else
                {
                    int res = objAttachmentcls.DeleteLibrary();
                    if (res > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Deleted Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted');", true);
                    }
                }
            }
            catch (Exception ex)
            {
               // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('DBID used somewhere');", true);
            }
        }
    }
}