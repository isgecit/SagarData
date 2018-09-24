using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;

namespace Attachment
{
    public partial class ViewAttachment : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindHandle();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void BindData()
        {
            try
            {
                //if (Request.QueryString["AthHandle"] != null)
                //{
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.AttachmentHandle = ddlHandle.SelectedValue; //Request.QueryString["AthHandle"];
                objAttachmentcls.IndexValue = ddlIndex.SelectedValue;// Request.QueryString["Index"];
                DataTable dt = objAttachmentcls.GetAttachments();
                gvAttachment.DataSource = dt;
                gvAttachment.DataBind();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No record found');", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                string[] values = lnkbtn.CommandArgument.Split('&');
                AttachmentCls objAttachmentcls = new AttachmentCls();
                objAttachmentcls.IndexValue = values[2];
                DataTable dt = objAttachmentcls.GetPath();
                string ServerPath = dt.Rows[0]["t_path"].ToString() +"\\"+ values[0];// Server.MapPath("~/Files/") + values[0]; // "\\\\" + dt.Rows[0]["ServerName"].ToString() + dt.Rows[0]["Path"].ToString() + values[0];
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + values[1] + "\"");
                byte[] data = req.DownloadData(ServerPath);
                response.BinaryWrite(data);
                response.End();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some Technical issue Data Not Uploaded');", true);
            }
        }

        private void BindHandle()
        {
            objAttachmentcls = new AttachmentCls();
            DataTable dt = objAttachmentcls.GetALLHandle();
            ddlHandle.DataSource = dt;
            ddlHandle.DataTextField = "Attachment_Handle";
            ddlHandle.DataValueField = "Attachment_Handle";
            ddlHandle.DataBind();
            ddlHandle.Items.Insert(0, "Select");
        }
        protected void ddlHandle_SelectedIndexChanged(object sender, EventArgs e)
        {
            objAttachmentcls = new AttachmentCls();
            DataTable dt = objAttachmentcls.GetIndexByHandle(ddlHandle.SelectedValue);
            ddlIndex.DataSource = dt;
            ddlIndex.DataTextField = "AccessIndex";
            ddlIndex.DataValueField = "AccessIndex";
            ddlIndex.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkUpdate = (LinkButton)sender;
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.DocumentId = lnkUpdate.CommandArgument;
                int res = objAttachmentcls.DeleteAttachment();
                if (res > 0)
                {
                    BindData();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Deleted Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted');", true);
                }
            }
            catch (Exception ex)
            {
               // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('DBID used somewhere');", true);
            }
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("UploadAttachment.aspx");
        }
    }
}