using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Attachment
{
    public partial class Attachment : System.Web.UI.Page
    {
        AttachmentCls objAttachmentcls;
        NotesClass objNotes;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["AthHandle"] != null)
                    {
                        HttpContext.Current.Cache.Remove("ATHData");
                        BindData();
                    }
                    else
                    {
                        HttpContext.Current.Cache.Remove("ALLATHData");
                        BindAllAttachments();
                    }
                    if (Request.QueryString["ed"] == "y" || Request.QueryString["ed"] == "a")
                    {
                        divUploadAttachment.Visible = true;
                    }
                    else
                    {
                        divUploadAttachment.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
            }
        }
        private void BindData()
        {
            try
            {
                DataTable dt = new DataTable();
                if (Request.QueryString["AthHandle"] != null && Request.QueryString["Index"] != null && Request.QueryString["AttachedBy"] != null)
                {
                    if (HttpContext.Current.Cache["ATHData"] == null)
                    {
                        objAttachmentcls = new AttachmentCls();
                        objAttachmentcls.AttachmentHandle = Request.QueryString["AthHandle"];
                        objAttachmentcls.IndexValue = Request.QueryString["Index"];
                        dt = objAttachmentcls.GetAttachments();
                        HttpContext.Current.Cache.Insert("ATHData", dt, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    }
                    else
                    {
                        dt = (DataTable)HttpContext.Current.Cache["ATHData"];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        gvAttachment.DataSource = dt;
                        gvAttachment.DataBind();
                        divNoRecord.Visible = false;
                        divViewAttachment.Visible = true;
                    }
                    else
                    {
                        divNoRecord.Visible = true;
                        divViewAttachment.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No Data found');", true);
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void BindAllAttachments()
        {
            try
            {
                DataTable dt = new DataTable();
                if (HttpContext.Current.Cache["ALLATHData"] == null)
                {
                    objAttachmentcls = new AttachmentCls();
                    dt = objAttachmentcls.GetAllAttachments();
                    HttpContext.Current.Cache.Insert("ALLATHData", dt, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                }
                else
                {
                    dt = (DataTable)HttpContext.Current.Cache["ALLATHData"];
                }
                if (dt.Rows.Count > 0)
                {
                    gvAttachment.DataSource = dt;
                    gvAttachment.DataBind();
                    divNoRecord.Visible = false;
                    divViewAttachment.Visible = true;
                }
                else
                {
                    divNoRecord.Visible = true;
                    divViewAttachment.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["AthHandle"] != null && Request.QueryString["Index"] != null && Request.QueryString["AttachedBy"] != null)
            {
                objAttachmentcls = new AttachmentCls();
                // objAttachmentcls.IndexValue = Request.QueryString["Index"];
                objAttachmentcls.AttachmentHandle = Request.QueryString["AthHandle"];
                DataTable dt = objAttachmentcls.GetPath();
                if (dt.Rows.Count > 0)
                {
                    //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
                    string ServerPath = "E:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
                    string LocalPath = Server.MapPath("~/Files/");
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
                                    objAttachmentcls.AttachmentHandle = Request.QueryString["AthHandle"];
                                    objAttachmentcls.IndexValue = Request.QueryString["Index"];
                                    objAttachmentcls.PurposeCode = "Dmisg134_1_vendor";// Request.QueryString["PurposeCode"];
                                    objAttachmentcls.AttachedBy = Request.QueryString["AttachedBy"];
                                    objAttachmentcls.FileName = fileName + fileExtension;
                                    objAttachmentcls.LibraryCode = dt.Rows[0]["LibCode"].ToString();
                                    // DataTable dtFile = objAttachmentcls.GetFileName();
                                    //  if (dtFile.Rows.Count == 0)
                                    //  {
                                    DataTable dtDocID = objAttachmentcls.Insertdata();
                                    if (dtDocID.Rows[0][0].ToString() != "0")
                                    {
                                        try
                                        {
                                            //  FileUpload.SaveAs(ServerPath + dtDocID.Rows[0][0]);
                                            PostedFile.SaveAs(ServerPath + dtDocID.Rows[0][0]);

                                        }
                                        catch (Exception ex)
                                        {// err.Text = ex.Message; 
                                        }
                                        // FileUpload.SaveAs(LocalPath + fileName + fileExtension);
                                        // HttpContext.Current.Cache.Remove("ATHData");
                                        HttpContext.Current.Cache.Remove("ATHData");
                                        BindData();

                                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
                                    }
                                   
                                   
                                  
                                    else
                                    {
                                        //objAttachmentcls = new AttachmentCls();
                                        //objAttachmentcls.DocumentId = dtDocID.Rows[0][0].ToString();
                                        //int res = objAttachmentcls.DeleteAttachment();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
                                    }
                                    //  }
                                    //  else
                                    //  {
                                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('This file name already exist please change your file name');", true);
                                    // }
                                }
                                catch (System.Exception ex)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
                                }
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);

                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found Properly');", true);
            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                string[] values = lnkbtn.CommandArgument.Split('@');
                AttachmentCls objAttachmentcls = new AttachmentCls();
                objAttachmentcls.AttachmentHandle = Request.QueryString["AthHandle"]; //values[2];
                DataTable dt = objAttachmentcls.GetPath();
                //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\" + values[0];// dt.Rows[0]["Path"].ToString() + "\\" + values[0];// Server.MapPath("~/Files/") + values[0]; // 
                string ServerPath = "E:\\" + dt.Rows[0]["t_path"].ToString() + "\\" + values[0];
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
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data Could not find');", true);
            }
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
                    //  File.Delete(Path);  
                    HttpContext.Current.Cache.Remove("ATHData");
                    BindData();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Deleted Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted');", true);
                }
            }
            catch (System.Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('DBID used somewhere');", true);
            }
        }

        //protected void lnkSend_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //LinkButton lnkbtn = (LinkButton)sender;
        //    //string[] values = lnkbtn.CommandArgument.Split('&');
        //    //AttachmentCls objAttachmentcls = new AttachmentCls();
        //    //objAttachmentcls.AttachmentHandle = values[2];
        //    //DataTable dt = objAttachmentcls.GetPath();
        //    //string file = dt.Rows[0]["Path"].ToString() + "\\" + values[0];

        //    string file = @"C:\Users\3100\Downloads\ViewAtch.jpg";
        //   // string file = @"C: \Users\Administrator\Downloads\ViewAtch.jpg";

        //    Outlook.Application oApp = new Outlook.Application();
        //    Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

        //    oMsg.HTMLBody = "PFA!!";
        //    String sDisplayName = "MyAttachment";
        //    int iPosition = (int)oMsg.Body.Length + 1;
        //    int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
        //    Outlook.Attachment oAttach = oMsg.Attachments.Add(file, iAttachType, iPosition, sDisplayName);
        //    oMsg.Subject = "PFA ";

        //    Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
        //    // Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add("meera.kumari@isgec.co.in");
        //    oMsg.Display();    //oMsg.Send();
        //    oRecips = null;
        //    oMsg = null;
        //    oApp = null;

        //    //  }//end of try block

        //    //catch (System.Exception ex)
        //    //{
        //    //}//end of catch
        //}



        //method to send email to Gmail
        public void lnkSend_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = (LinkButton)sender;
            string[] values = lnkbtn.CommandArgument.Split('&');
            hdfFile.Value = values[1];
            mpeMail.Show();
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTo.Text != "")
                {
                    objNotes = new NotesClass();
                    objNotes.User = Request.QueryString["AttachedBy"];
                    DataTable dtUserMail = objNotes.GetEmployeeDetails();
                    MailMessage mM = new MailMessage();
                    mM.From = new MailAddress("baansupport@isgec.co.in");
                    // mM.To.Add(txtTo.Text.Trim());
                    string[] MailTo = txtTo.Text.Split(';');
                    foreach (string Mailid in MailTo)
                    {
                        mM.To.Add(new MailAddress(Mailid));
                    }
                    mM.To.Add(dtUserMail.Rows[0]["EmailID"].ToString());
                    mM.Subject = hdfFile.Value;
                    string file = Server.MapPath("~/Files/") + hdfFile.Value;
                    mM.Attachments.Add(new System.Net.Mail.Attachment(file));
                    mM.Body = hdfFile.Value;
                    mM.IsBodyHtml = true;
                    SmtpClient sC = new SmtpClient("192.9.200.214", 25);
                    //   sC.Host = "192.9.200.214"; //"smtp-mail.outlook.com"// smtp.gmail.com
                    //   sC.Port = 25; //587
                    sC.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sC.UseDefaultCredentials = false;
                    sC.Credentials = new NetworkCredential("baansupport@isgec.co.in", "isgec");
                    //sC.Credentials = new NetworkCredential("adskvaultadmin", "isgec@123");
                    sC.EnableSsl = false;  // true
                    sC.Timeout = 10000000;
                    sC.Send(mM);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Mail has been sent');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please provide proper mail id');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Mail not sent');", true);
            }
        }

        protected void gvAttachment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAttachment.PageIndex = e.NewPageIndex;
            if (Request.QueryString["AthHandle"] != null)
            {
                BindData();
            }
            else
            {
                BindAllAttachments();
            }
        }

    }
}