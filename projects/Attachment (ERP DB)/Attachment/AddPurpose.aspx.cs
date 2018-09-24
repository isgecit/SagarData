using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class AddPurpose : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Type"] == "View")
                    {
                        divCreate.Visible = false;
                        divView.Visible = true;
                        BindGrid();
                        btnCreate.Visible = true;
                    }
                    else if (Request.QueryString["Type"] == "Create")
                    {
                        divCreate.Visible = true;
                        divView.Visible = false;
                        btnCreate.Visible = false;
                    }
                    else
                    {
                        if (Request.QueryString["PurposeCode"] != null)
                        {
                            GetPurpose();
                            divCreate.Visible = true;
                            divView.Visible = false;
                            lnkDelete.Visible = true;
                            btnCreate.Visible = false;
                        }
                        else
                        {
                            divCreate.Visible = true;
                            divView.Visible = false;
                            btnSavePurpose.Text = "Save";
                            txtPurposeCode.Enabled = true;
                            divHeader.InnerText = "Create Purpose";
                            lnkDelete.Visible = false;
                            btnCreate.Visible = false; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void GetPurpose()
        {
            objAttachmentcls = new AttachmentCls();
            objAttachmentcls.PurposeCode = Request.QueryString["PurposeCode"];
            DataTable dt = objAttachmentcls.GetALLPurposeByID();
            if (dt.Rows.Count > 0)
            {
                txtPurposeCode.Text = dt.Rows[0]["t_prcd"].ToString();
                txtPDescription.Text = dt.Rows[0]["t_desc"].ToString();
                btnSavePurpose.Text = "Update";
                txtPurposeCode.Enabled = false;
                divHeader.InnerText = "Update Purpose";
            }
            else
            {
                btnSavePurpose.Text = "Save";
            }
        }
        protected void btnSavePurpose_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPurposeCode.Text != "" && txtPDescription.Text != "")
                {
                    objAttachmentcls = new AttachmentCls();
                    objAttachmentcls.PurposeCode = txtPurposeCode.Text.Trim();
                    objAttachmentcls.PurposeDesc = txtPDescription.Text.Trim();
                    if (btnSavePurpose.Text == "Save")
                    {
                        int res = objAttachmentcls.InsertPurpose();
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
                        int res = objAttachmentcls.UpdatePurpose();
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
        private void BindGrid()
        {
            try
            {
                objAttachmentcls = new AttachmentCls();
                DataTable dt = objAttachmentcls.GetALLPurpose();
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkUpdate = (LinkButton)sender;
                Response.Redirect("AddPurpose.aspx?PurposeCode=" + lnkUpdate.CommandArgument);
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
                objAttachmentcls.PurposeCode = Request.QueryString["PurposeCode"];
                DataTable dt = objAttachmentcls.GetPurposeCodeCount();
                if (dt.Rows[0]["PurposeCodeCount"].ToString() == "0")
                {
                    int res = objAttachmentcls.DeletePurpose();
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
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Purpose Code used somewhere');", true);
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPurpose.aspx?Type=Create");
        }
    }
}