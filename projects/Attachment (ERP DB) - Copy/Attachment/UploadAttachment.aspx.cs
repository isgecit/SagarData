using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class UploadAttachment : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  BindLibrary();
                    BindHandle();
                    BindPurpose();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ddlHandle.SelectedValue != "Select" && ddlIndex.SelectedValue != "")
            {
                objAttachmentcls = new AttachmentCls();
                objAttachmentcls.IndexValue = ddlIndex.SelectedValue;// Request.QueryString["Index"];
                DataTable dt = objAttachmentcls.GetPath();
                string ServerPath = dt.Rows[0]["Path"].ToString() + "\\";// "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\"; // Server.MapPath("~/Files/");//
                if (FileUpload.HasFile)
                {
                    int filecount = 0;
                    filecount = FileUpload.PostedFiles.Count();
                    if (filecount > 0)
                    {
                        foreach (HttpPostedFile PostedFile in FileUpload.PostedFiles)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
                            string fileExtension = Path.GetExtension(PostedFile.FileName);
                            try
                            {
                                AttachmentCls objAttachmentcls = new AttachmentCls();
                                objAttachmentcls.AttachmentHandle = ddlHandle.SelectedValue;// Request.QueryString["AthHandle"];
                                objAttachmentcls.IndexValue = ddlIndex.SelectedValue;// Request.QueryString["Index"];
                                objAttachmentcls.PurposeCode = ddlPurpose.SelectedValue;// Request.QueryString["PurposeCode"];
                                objAttachmentcls.AttachedBy = "0";//Request.QueryString["AttachedBy"];
                                objAttachmentcls.FileName = fileName + fileExtension;
                                objAttachmentcls.LibraryCode = dt.Rows[0]["LibCode"].ToString();
                                DataTable dtDocID = objAttachmentcls.Insertdata();
                                if (dtDocID.Rows.Count > 0)
                                {
                                    FileUpload.SaveAs(ServerPath + dtDocID.Rows[0][0]);
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
                                }
                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please select all fields');", true);
            }
        }

        //private void BindLibrary()
        //{
        //    objAttachmentcls = new AttachmentCls();
        //    DataTable dt = objAttachmentcls.GetAllActiveLibrary();
        //    ddlLibrary.DataSource = dt;
        //    ddlLibrary.DataTextField = "LibraryCode";
        //    ddlLibrary.DataValueField = "LibraryCode";
        //    ddlLibrary.DataBind();
        //}
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
        private void BindPurpose()
        {
            objAttachmentcls = new AttachmentCls();
            DataTable dt = objAttachmentcls.GetALLPurpose();
            ddlPurpose.DataSource = dt;
            ddlPurpose.DataTextField = "PurposeDescription";
            ddlPurpose.DataValueField = "PurposeCode";
            ddlPurpose.DataBind();
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
    }
}