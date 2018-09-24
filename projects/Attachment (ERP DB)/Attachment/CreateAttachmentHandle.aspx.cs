using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{

    public partial class CreateAttachmentHandle : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDBID();

                    if (Request.QueryString["ATH"] != null)
                    {
                        GetHandle();
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
        private void BindDBID()
        {
            objAttachmentcls = new AttachmentCls();
            DataTable dt = objAttachmentcls.GetALLDBID();
            ddlDBID.DataSource = dt;
            ddlDBID.DataTextField = "t_dbid";
            ddlDBID.DataValueField = "t_dbid";
            ddlDBID.DataBind();
            ddlDBID.Items.Insert(0, "Select");
        }

        private void GetHandle()
        {
            try
            {
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.AttachmentHandle = Request.QueryString["ATH"];
                DataTable dt = objAttachmentcls.GetALLHandleByID();
                if (dt.Rows.Count > 0)
                {
                    txtAttachmentHandle.Text = dt.Rows[0]["t_hndl"].ToString();
                    txtAcessIndex.Text = dt.Rows[0]["t_indx"].ToString();
                    txtTableName.Text = dt.Rows[0]["t_tabl"].ToString();
                    txtDescription.Text= dt.Rows[0]["t_tdes"].ToString(); 
                    txtRemarks.Text = dt.Rows[0]["t_rema"].ToString();
                    ddlDBID.SelectedValue = dt.Rows[0]["t_dbid"].ToString();
                    btnSaveHandle.Text = "Update";
                    txtAttachmentHandle.Enabled = false;
                    divHeader.InnerText = "Update Attachment Handle";
                }
                else
                {
                    btnSaveHandle.Text = "Save";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnSaveHandle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAttachmentHandle.Text != "" && txtAcessIndex.Text != "" && txtTableName.Text != "" && ddlDBID.SelectedValue!="Select")
                {
                    objAttachmentcls = new AttachmentCls();
                    objAttachmentcls.DBID = ddlDBID.SelectedValue;
                    objAttachmentcls.AttachmentHandle = txtAttachmentHandle.Text.Trim();
                    objAttachmentcls.IndexValue = txtAcessIndex.Text.Trim();
                    objAttachmentcls.TableName = txtTableName.Text.Trim();
                    objAttachmentcls.TableDesc = txtDescription.Text.Trim();
                    objAttachmentcls.Remarks = txtRemarks.Text.Trim();
                    if (btnSaveHandle.Text == "Save")
                    {
                        int res = objAttachmentcls.InsertHandle();
                        if (res > 0)
                        {
                            txtAttachmentHandle.Text = "";
                            txtAcessIndex.Text = "";
                            txtTableName.Text = "";
                            txtDescription.Text = "";
                            txtRemarks.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Saved Successfully');", true);
                          
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Not Saved');", true);
                        }
                    }
                    else
                    {
                        int res = objAttachmentcls.UpdateHandle();
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
                objAttachmentcls.AttachmentHandle = Request.QueryString["ATH"];
                DataTable dtHandle = objAttachmentcls.GetHandleId();
                if (dtHandle.Rows.Count == 0)
                {
                    int res = objAttachmentcls.DeleteHandle();
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
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle used somewhere');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }

        protected void ddlDBID_SelectedIndexChanged(object sender, EventArgs e)
        {
            objAttachmentcls = new AttachmentCls();
            objAttachmentcls.DBID = ddlDBID.SelectedValue;
            DataTable dt=  objAttachmentcls.GetDBID();
            txtDBDescription.Text = dt.Rows[0]["t_desc"].ToString();
            txtDBDescription.Visible = true;
        }
    }
}