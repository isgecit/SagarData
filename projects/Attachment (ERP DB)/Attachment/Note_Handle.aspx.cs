using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attachment
{
    public partial class Note_Handle : System.Web.UI.Page
    {
        NotesClass objNotes;
        AttachmentCls objAttachmentcls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                   // BindDBID();

                    if (Request.QueryString["handle"] != null)
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
        //private void BindDBID()
        //{
        //    objNotes = new NotesClass();
        //    DataTable dt = objNotes.GetALLDBID();
        //    ddlDBID.DataSource = dt;
        //    ddlDBID.DataTextField = "DBID";
        //    ddlDBID.DataValueField = "DBID";
        //    ddlDBID.DataBind();
        //}
        private void GetHandle()
        {
            try
            {
                objNotes = new NotesClass();
                objNotes.NotesHandle = Request.QueryString["handle"];
                DataTable dt = objNotes.GetALLHandleByID();
                if (dt.Rows.Count > 0)
                {
                    txtNotesHandle.Text = dt.Rows[0]["NotesHandle"].ToString();
                    txtAcessIndex.Text = dt.Rows[0]["AccessIndex"].ToString();
                    txtTableName.Text = dt.Rows[0]["TableName"].ToString();
                    txtTableDesc.Text= dt.Rows[0]["TableDescription"].ToString(); 
                    txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                    txtDBID.Text = dt.Rows[0]["DBID"].ToString();
                    btnSaveHandle.Text = "Update";
                    txtNotesHandle.Enabled = false;
                    divHeader.InnerText = "Update Notes Handle";
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
                if (txtNotesHandle.Text != "" && txtAcessIndex.Text != "" && txtTableName.Text != "")
                {
                    objNotes = new NotesClass();
                    objNotes.DBID = txtDBID.Text.Trim(); 
                    objNotes.NotesHandle = txtNotesHandle.Text.Trim();
                    objNotes.IndexValue = txtAcessIndex.Text.Trim();
                    objNotes.TableName = txtTableName.Text.Trim();
                    objNotes.TableDesc = txtTableDesc.Text.Trim();
                    objNotes.Remarks = txtRemarks.Text.Trim();
                    if (btnSaveHandle.Text == "Save")
                    {
                        int res = objNotes.InsertNotesHandle();
                        if (res > 0)
                        {
                            txtNotesHandle.Text = "";
                            txtAcessIndex.Text = "";
                            txtTableName.Text = "";
                            txtTableDesc.Text = "";
                            txtDBID.Text = "";
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
                        int res = objNotes.UpdateNotesHandle();
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
                objNotes = new NotesClass();
                objNotes.NotesHandle = Request.QueryString["handle"];
                DataTable dtHandle = objNotes.GetUsedHandle();
                if (dtHandle.Rows.Count == 0)
                {
                    int res = objNotes.DeleteNotesHandle();
                    if (res > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Deleted Successfully');", true);
                        Response.Redirect("View_NotesHandle.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Notes Handle used somewhere');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
    }
}