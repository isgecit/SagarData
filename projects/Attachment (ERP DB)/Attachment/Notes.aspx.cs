using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class Notes : System.Web.UI.Page
    {
        NotesClass objNotes;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["handle"] != null)
                    {
                        GetNotes();
                        txtMailTo.Text = Request.QueryString["Em"];
                        txtTitle.Text = Request.QueryString["Tl"];
                        spIndex.InnerHtml = Request.QueryString["Hd"];
                    }
                    else
                    {
                      
                    }
                }
                

                //Session["NotesHandle"] = objNotes.NotesHandle;
                //Session["IndexValue"] = objNotes.IndexValue;
                //txtTitle.Text= Session["Title"].ToString();
                //txtDescription.Text =Session["Description"].ToString();
                ////Session["User"] = objNotes.User;
                //txtMailTo.Text=Session["SendEmailTo"].ToString();
                //txtMailIdReminder.Text = Session["RemiderMailId"].ToString();
                //txtDate.Text= Session["ReminderDateTime"].ToString();


            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue');", true);
            }
        }
        private void GetNotes()
        {
            try
            {
                objNotes = new NotesClass();
                objNotes.NotesHandle = Request.QueryString["handle"];
                objNotes.IndexValue = Request.QueryString["index"];
                DataTable dt = objNotes.GetNotesFromASPNETUSer();
                if (dt.Rows.Count > 0)
                {
                    if (Request.QueryString["Hd"] == null)
                    {
                        spIndex.InnerHtml = dt.Rows[0]["TableDescription"].ToString() + " , " + Request.QueryString["index"];
                    }
                    rptNotes.DataSource = dt;
                    rptNotes.DataBind();
                }
                else
                {
                    if (Request.QueryString["Hd"] == null)
                    {
                        objNotes = new NotesClass();
                        objNotes.NotesHandle = Request.QueryString["handle"];
                        DataTable dtHandle = objNotes.GetALLHandleByID();
                        spIndex.InnerHtml = dtHandle.Rows[0]["TableDescription"].ToString() + " , " + Request.QueryString["Index"];
                    }

                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('No record found');", true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void rptNotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("row"); //Where TD1 is the ID of the Table Cell
                    HiddenField hdfUser = (HiddenField)e.Item.FindControl("hdfUserID");
                    tr.Attributes.Add("style", "background-color:" + hdfUser.Value + ";");
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnSaveNotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text != "" && txtDescription.Text != "")
                {
                    objNotes = new NotesClass();
                    objNotes.NotesHandle = Request.QueryString["handle"];
                    objNotes.IndexValue = Request.QueryString["index"];
                    objNotes.Title = txtTitle.Text.Trim();
                    objNotes.Description = txtDescription.Text.Trim();
                    objNotes.User = Request.QueryString["user"];
                    objNotes.SendEmailTo = txtMailTo.Text;
                    objNotes.RemiderMailId = txtMailIdReminder.Text.Trim();
                    objNotes.ReminderDateTime = txtDate.Text != "" ? Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") + " " + "9:00" : System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + "9:00";
                    //objNotes.ReminderDateTime =txtDate.Text!=""? Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") +" "+ txtTime.Text.Trim(): System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + txtTime.Text.Trim();

                    if (btnSaveNotes.Text == "Submit")
                    {
                        if (hdfNewNoteId.Value == "")
                        {
                            DataTable dtNotesID = objNotes.Insertdata();
                            if (dtNotesID.Rows[0][0].ToString() != "0")
                            {
                                if (txtMailTo.Text != "")
                                {
                                    SendMAil();
                                }
                                txtTitle.Text = "";
                                txtDescription.Text = "";
                                GetNotes();

                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Saved');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Notes Handle does not exist');", true);
                            }
                        }
                        else
                        {
                            objNotes.NoteID = hdfNewNoteId.Value;
                            int res = objNotes.UpdateNotes();
                            if (txtMailTo.Text != "")
                            {
                                SendMAil();
                            }
                            if (res > 0)
                            {
                                txtTitle.Text = "";
                                txtDescription.Text = "";
                                hdfNewNoteId.Value = "";
                                GetNotes();
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Saved');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Updated');", true);
                            }
                        }
                    }

                    // Update
                    else
                    {
                        objNotes.NoteID = hdfNoteId.Value;
                        int res = objNotes.UpdateNotes();
                        if (txtMailTo.Text != "")
                        {
                            SendMAil();
                        }
                        if (res > 0)
                        {
                            GetNotes();
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Updated');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Updated');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Enter all fields');", true);
                }

            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not Saved');", true);
            }
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtn = (LinkButton)sender;
                string[] Value = lnkBtn.CommandArgument.Split('&');
                string uId = Value[0];
                hdfUser.Value = uId;
                hdfNoteId.Value = Value[1];
                objNotes = new NotesClass();
                objNotes.NoteID = Value[1];
                DataTable dt = objNotes.GetNotesByRunningId();
                txtMailTo.Text = dt.Rows[0]["SendEmailTo"].ToString();
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                //add color to desc and button
                txtDescription.Attributes.Add("style", "background-color:" + dt.Rows[0]["ColorId"].ToString() + ";");
                btnNewNotes.Attributes.Add("style", "background-color:" + dt.Rows[0]["ColorId"].ToString() + ";");
                //------
                txtMailIdReminder.Text = dt.Rows[0]["ReminderTo"].ToString();
                txtDate.Text = dt.Rows[0]["ReminderDateTime"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["ReminderDateTime"].ToString()).ToString("dd-MM-yyyy") : "";
                //if (uId == Request.QueryString["user"])
                //{
                //    txtTitle.Enabled = true;
                //    txtDescription.Enabled = true;
                //    btnSaveNotes.Text = "Update";
                //    btnSaveNotes.Enabled = true;
                //    btnDeleteNotes.Enabled = true;
                //    btnDeleteNotes.Visible = true;
                //    txtMailTo.Enabled = true;
                //}
                //else
                //{
                   // Request.QueryString["ed"] = "";
                    txtTitle.Enabled = false;
                    txtDescription.Enabled = false;
                    btnSaveNotes.Enabled = false;
                    btnDeleteNotes.Enabled = false;
                    txtMailTo.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Editing of Notes is not allowed');", true);
                //}
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not Update');", true);
            }
        }
        protected void btnNewNotes_Click(object sender, EventArgs e)
        {
            txtTitle.Enabled = true;
            txtDescription.Enabled = true;
            txtMailTo.Enabled = true;
            txtTitle.Text = "";
            txtDescription.Text = "";
            btnSaveNotes.Text = "Submit";
            btnDeleteNotes.Visible = false;
            btnSaveNotes.Enabled = true;
            txtMailTo.Text = Request.QueryString["Em"];
            txtTitle.Text = Request.QueryString["Tl"];
            spIndex.InnerHtml = Request.QueryString["Hd"];
            hdfNoteId.Value = "";
            hdfNewNoteId.Value = "";
        }
        protected void btnDeleteNotes_Click(object sender, EventArgs e)
        {
            try
            {
                objNotes = new NotesClass();
                objNotes.NoteID = hdfNoteId.Value;
                int res = objNotes.DeleteNotes();
                if (res > 0)
                {
                    txtTitle.Text = "";
                    txtDescription.Text = "";
                    GetNotes();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Deleted');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted ');", true);
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue data Not deleted');", true);
            }
        }
        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                string url;
                if (Request.QueryString["RefHandle"] != null && Request.QueryString["RefIndex"] != null)
                {
                    //  url = "http://localhost/Attachment/Attachment.aspx?AthHandle=JOOMLA_NOTES" + "&Index=Notes230&AttachedBy=" + Request.QueryString["user"] + "&ed=a&RefHandle=" + Request.QueryString["RefHandle"] + "&RefIndex=" + Request.QueryString["RefIndex"] + "&ed=y";
                    // url = "http://localhost/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=389&AttachedBy=" + Request.QueryString["u"] + "&ed=a&RefHandle=TRANSMITTALLINES_200&RefIndex=BOi000532_JB0973-50270100-027-0001_00";
                    url = "Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW"+"&Index="+Request.QueryString["Index"] +"&AttachedBy="+Request.QueryString["user"] +"&ed=a&RefHandle="+Request.QueryString["RefHandle"]+"&RefIndex="+Request.QueryString["RefIndex"]+"";
                    string s = "window.open('" + url + "', 'popup_window','width=900,height=800,left=100,top=100,resizable=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }

                else if (Request.QueryString["RefHandle"] == null && Request.QueryString["RefIndex"] == null)
                {

                    if (hdfNoteId.Value != "")
                    {
                        //if (Request.QueryString["RefHandle"] != null && Request.QueryString["RefIndex"] != null && hdfUser.Value == Request.QueryString["user"])
                        //{
                        //    url = "http://localhost/Attachment/Attachment.aspx?AthHandle=JOOMLA_NOTES" + "&Index=" + hdfNoteId.Value + "&AttachedBy=" + Request.QueryString["user"] + "&ed=a&RefHandle=" + Request.QueryString["RefHandle"] + "&RefIndex=" + Request.QueryString["RefIndex"] + "&ed=y";

                        //}
                        //else
                        if (hdfUser.Value == Request.QueryString["user"])
                        {

                            if (txtTitle.Enabled)
                            {
                                url = "Attachment.aspx?AthHandle=JOOMLA_NOTES" + "&Index=" + hdfNoteId.Value + "&AttachedBy=" + Request.QueryString["user"] + "&ed=y";
                            }
                            // Response.Redirect("Attachment.aspx?AthHandle=" + Request.QueryString["handle"] + "&Index=" + hdfNoteId.Value + "&AttachedBy=" + Request.QueryString["user"] + "&ed=y");
                            url = "Attachment.aspx?AthHandle=JOOMLA_NOTES" + "&Index=" + hdfNoteId.Value + "&AttachedBy=" + Request.QueryString["user"] + "&ed=n";
                        }
                        else
                        {
                            url = "Attachment.aspx?AthHandle=JOOMLA_NOTES" + "&Index=" + hdfNoteId.Value + "&AttachedBy=" + Request.QueryString["user"];
                        }

                        string s = "window.open('" + url + "', 'popup_window','width=900,height=800,left=100,top=100,resizable=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }

                    else
                    {
                        objNotes = new NotesClass();
                        objNotes.NotesHandle = Request.QueryString["handle"];
                        objNotes.IndexValue = Request.QueryString["index"];
                        objNotes.Title = txtTitle.Text != "" ? txtTitle.Text.Trim() : "Only Attachment";
                        objNotes.Description = txtDescription.Text != "" ? txtDescription.Text.Trim() : "Only Attachment";
                        objNotes.User = Request.QueryString["user"];
                        objNotes.SendEmailTo = txtMailTo.Text;
                        objNotes.RemiderMailId = txtMailIdReminder.Text.Trim();
                        objNotes.ReminderDateTime = txtDate.Text != "" ? Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") + " " + "9:00" : System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + "9:00";
                        //objNotes.ReminderDateTime =txtDate.Text!=""? Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") +" "+ txtTime.Text.Trim(): System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + txtTime.Text.Trim();

                        //Session["NotesHandle"] = objNotes.NotesHandle;
                        //Session["IndexValue"] = objNotes.IndexValue;
                        //Session["Title"] = objNotes.Title;
                        //Session["Description"] = objNotes.Description;
                        //Session["User"] = objNotes.User;
                        //Session["SendEmailTo"] = objNotes.SendEmailTo;
                        //Session["RemiderMailId"] = objNotes.RemiderMailId;
                        //Session["ReminderDateTime"] = objNotes.ReminderDateTime;

                        DataTable dtNotesID = objNotes.Insertdata();

                        if (dtNotesID.Rows[0][0].ToString() != "0")
                        {
                            //if (txtMailTo.Text != "")
                            //{
                            //    SendMAil();
                            //}

                           // txtTitle.Text = "";
                           // txtDescription.Text = "";
                           // GetNotes();
                            hdfNewNoteId.Value = dtNotesID.Rows[0][0].ToString();
                            objNotes = new NotesClass();
                            string TempNoteId = objNotes.GetTempNoteID();
                            url = "Attachment.aspx?AthHandle=JOOMLA_NOTES" + "&Index=" + dtNotesID.Rows[0][0].ToString() + "&AttachedBy=" + Request.QueryString["user"] + "&ed=y";
                            string s = "window.open('" + url + "', 'popup_window','width=900,height=800,left=100,top=100,resizable=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Notes Handle does not exist');", true);
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not Saved');", true);
            }
        }
        protected void SendMAil()
        {
            try
            {
                if (txtMailTo.Text != "")
                {
                    objNotes = new NotesClass();
                    objNotes.User = Request.QueryString["user"];
                    DataTable dtUserMail = objNotes.GetEmployeeDetails();
                    MailMessage mM = new MailMessage();
                    mM.From = new MailAddress(dtUserMail.Rows[0]["EmailID"].ToString());
                    string[] MailTo = txtMailTo.Text.Split(';');
                    foreach (string Mailid in MailTo)
                    {
                        mM.To.Add(new MailAddress(Mailid));
                    }
                    mM.To.Add(dtUserMail.Rows[0]["EmailID"].ToString());
                    mM.Subject = txtTitle.Text.Trim() + "-" + spIndex.InnerHtml;
                    // string file = Server.MapPath("~/Files/") + hdfFile.Value;
                    // mM.Attachments.Add(new System.Net.Mail.Attachment(file));
                    mM.Body = txtDescription.Text.Trim();
                    mM.IsBodyHtml = true;
                    mM.Body = mM.Body.ToString().Replace("\n", "<br />");
                    SmtpClient sC = new SmtpClient("192.9.200.214", 25);
                    mM.Body += "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />This mail has been triggered to draw your attention on the respective ERP/Joomla module. Please login to respective module to see further details and file attachments";
                    //   sC.Host = "192.9.200.214"; //"smtp-mail.outlook.com"// smtp.gmail.com
                    //   sC.Port = 25; //587
                    sC.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sC.UseDefaultCredentials = false;
                    sC.Credentials = new NetworkCredential("baansupport@isgec.co.in", "isgec");
                    //sC.Credentials = new NetworkCredential("adskvaultadmin", "isgec@123");
                    sC.EnableSsl = false;  // true
                    sC.Timeout = 10000000;
                    sC.Send(mM);
                    //  ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Mail has been sent');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please provide proper mail id and Content');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Mail not sent');", true);
            }
        }
    }
}