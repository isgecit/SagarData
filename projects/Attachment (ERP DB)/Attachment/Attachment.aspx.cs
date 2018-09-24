using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        //ConnectionTest ConnectionLive
        public static string csLive = ConfigurationManager.AppSettings["ConnectionLive"];
        public static string csERPLive = ConfigurationManager.AppSettings["Connection"];
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
                        // this.lnkDelete.Visible = false;

                        if (Request.QueryString["RefHandle"] == null && Request.QueryString["RefIndex"] == null)
                        {
                            divViewExistingAttachment.Visible = false;
                            btnCopyAttachment.Visible = false;
                            btnSystemAttachmentUpload.Visible = false;
                        }
                    }
                    else
                    {
                        divUploadAttachment.Visible = false;
                        btnSystemAttachmentUpload.Visible = false;
                        btnCopyAttachment.Visible = false;
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
                if (Request.QueryString["RefHandle"] != null && Request.QueryString["RefIndex"] != null)
                {
                    objAttachmentcls = new AttachmentCls();
                    objAttachmentcls.AttachmentHandle = Request.QueryString["AthHandle"];
                    objAttachmentcls.IndexValue = Request.QueryString["Index"];
                    dt = objAttachmentcls.GetAllExistingAttachments();
                    // HttpContext.Current.Cache.Insert("ATHData", dt, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    if (dt.Rows.Count > 0)
                    {
                        gvAttachment.DataSource = dt;
                        gvAttachment.DataBind();
                        divNoRecord.Visible = false;
                      //  gvExistingAttachments.Visible = true;
                        divViewAttachment.Visible = true;
                      //  divExistingAttachNotExists.Visible = false;
                    }
                    else
                    {
                        divNoRecord.Visible = true;
                       // divExistingAttachNotExists.Visible = true;
                        divViewAttachment.Visible = false;
                    }

                }

                else if (Request.QueryString["AthHandle"] != null && Request.QueryString["Index"] != null && Request.QueryString["AttachedBy"] != null)
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
               // else
               // {
                    
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No Data found');", true);
                    }
                   
               // }
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
                    string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
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
                                    int nRecord = new Random(Guid.NewGuid().GetHashCode()).Next();
                                    objAttachmentcls.RunningId = nRecord;
                                    objAttachmentcls.DocumentId = "AAA"+nRecord.ToString();
                                    // DataTable dtFile = objAttachmentcls.GetFileName();
                                    //  if (dtFile.Rows.Count == 0)
                                    //  {
                                    //int nRecord = new Random(Guid.NewGuid().GetHashCode()).Next();
                                    //string sDocumentId = "AAA" + nRecord;
                                    //DataTable dtDocID = objAttachmentcls.Insertdata(nRecord, sDocumentId);
                                    //DataTable dtDocID = objAttachmentcls.Insertdata();
                                    DataTable dtDocID = objAttachmentcls.InsertAttachmentdata();
                                    if (dtDocID.Rows[0][0].ToString() != "0")
                                    {
                                        try
                                        {
                                            PostedFile.SaveAs(ServerPath + dtDocID.Rows[0][0]);
                                        }
                                        catch (Exception ex)
                                        {// err.Text = ex.Message; 
                                        }
                                        // FileUpload.SaveAs(LocalPath + fileName + fileExtension);
                                        HttpContext.Current.Cache.Remove("ATHData");
                                        BindData();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
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
                string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\" + values[0];
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

        protected void gvAttachment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSystemAttachUpload_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["RefHandle"] != null && Request.QueryString["RefIndex"] != null)
            {
                objAttachmentcls = new AttachmentCls();
                // objAttachmentcls.IndexValue = Request.QueryString["Index"];
                objAttachmentcls.AttachmentHandle = Request.QueryString["RefHandle"];
                objAttachmentcls.IndexValue = Request.QueryString["RefIndex"];
                DataTable dt = objAttachmentcls.GetExistingAttachment();
                if (dt.Rows.Count > 0)
                {
                    // Session["sDocumentId"] = dt.Rows[0]["t_dcid"].ToString();
                    //// Session["sFileName"] = dt.Rows[0]["t_fnam"].ToString();
                    // Session["sLibCode"] = dt.Rows[0]["t_lbcd"].ToString();
                    // Session["sattachedby"] = dt.Rows[0]["t_atby"].ToString();
                    divViewExistingAttachment.Visible = true;
                    divExistingAttachNotExists.Visible = false;
                    btnCopyAttachment.Visible = true;
                    gvExistingAttachments.DataSource = dt;
                    gvExistingAttachments.DataBind();

                }
                else
                {
                    divExistingAttachNotExists.Visible = true;
                    divViewExistingAttachment.Visible = false;
                }

            }
            {
                // No Attachment exists for the provide reference handle and reference Index
            }

        }

        protected void btnCopyAttachment_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvExistingAttachments.Rows)
            {
                //for (int i = 0; i < gvExistingAttachments.Columns.Count; i++)
                //{
                //    if (((CheckBox)row.FindControl("Checkbox1")).Checked)
                //    {
                //        String header = gvExistingAttachments.Columns[i].HeaderText;
                //        String cellText = row.Cells[i].Text;
                //    }
                //}
                if (((CheckBox)row.FindControl("Checkbox1")).Checked)
                {
                    using (SqlConnection con = new SqlConnection(csLive))   // change cs to csLive everywhere inside sql connection ....this is just for testing sagar

                    {
                        int nRecord = 0;
                        string sDocumentId = ((Label)row.FindControl("lblDocId")).Text;
                        string sFileName = ((Label)row.FindControl("lblFilename")).Text;
                        //row.FindControl("lblDocId").ToString();lblFilename
                        //row.Cells[0].Text;
                        //row.Cells[0].ToString();
                        //row.FindControl("lblDocId").ToString();

                        //row.Cells[0].Text;
                        //string sFileName = row.Cells[1].Text;
                       
                       
                        string sCopyhandle = Request.QueryString["AthHandle"];
                        string sCopyIndex = Request.QueryString["Index"];
                        string sattachedby = Request.QueryString["AttachedBy"];
                        //string sCount = @"select MAX(t_rnum) from ttcisg131200";

                        //Recent Attachment Changes ignoring t_rnum from 131 table date -25/08/2018
                        // string sCount = @"SELECT (ISNULL(MAX(t_rnum),0) + 1)as RunningNo FROM ttcisg131200 where t_acti='Y'";
                        // string count = (Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(con, CommandType.Text, sCount)).ToString();
                        // nRecord = Convert.ToInt32(count);
                        //// nRecord = nRecord + 1;
                        // string sUpdateRecordNumber = @"Update ttcisg131200 set t_rnum = '" + nRecord + "' where t_acti = 'Y'";
                        // SqlCommand cmdUpdate = new SqlCommand(sUpdateRecordNumber, con);
                        // cmdUpdate.CommandType = CommandType.Text;
                        // if (con.State == ConnectionState.Closed)
                        // {
                        //     con.Open();
                        // }
                        // cmdUpdate.ExecuteNonQuery();
                        nRecord = new Random(Guid.NewGuid().GetHashCode()).Next();
                        sDocumentId = "AAA" + nRecord.ToString();
                        string InsertAttachment = @"Insert into ttcisg132200 (t_atby,t_aton,t_prcd,t_drid,t_dcid,t_fnam,t_lbcd,t_hndl,t_indx,t_Refcntd,t_Refcntu) 
                                            values('" + sattachedby + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','Dmisg134_1_vendor','" + nRecord + "','" + sDocumentId + "','" + sFileName + "','LIB000001','"+ sCopyhandle + "','" + sCopyIndex + "','','')";
                        //string sUpdatedon
                        SqlCommand cmdInsert = new SqlCommand(InsertAttachment, con);
                        cmdInsert.CommandType = CommandType.Text;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmdInsert.ExecuteNonQuery();
                        con.Close();
                       
                       // BindAllAttachments();
                    }
                }
            }
            BindData();
        }
    }
}