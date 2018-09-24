using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Attachment
{
    public partial class AssignColor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GEtUserColor();
            }
        }

        private void GEtUserColor()
        {
            NotesClass objNotesClass = new NotesClass();
            DataTable dt = objNotesClass.GEtUSerColor();
            gvdata.DataSource = dt;
            gvdata.DataBind();
        }

        protected void btnSaveColor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text != "" && hdfColor.Value != "")
                {
                    string[] user = txtUser.Text.Split('-');
                    NotesClass objNotesClass = new NotesClass();
                    objNotesClass.User = user[1].Trim();
                    objNotesClass.Color = hdfColor.Value;
                    int res = objNotesClass.InsertColor();
                    if (res > 0)
                    {
                        GEtUserColor();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Assigned');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Assigned');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Enter all fields');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Not Assigned');", true);
            }
        }

        [WebMethod]
        public static string[] GetUSer(string prefixText, int count)
        {
            NotesClass objNotes = new NotesClass();
            DataTable dt = objNotes.GetUser(prefixText);
            List<string> lst = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string UserId = dr["EmployeeName"].ToString() + "-" + dr["CardNo"].ToString();
                //  string UserName = ;
                lst.Add(UserId);
                //    lst.Add(UserName);
            }
            return lst.ToArray();
        }

        protected void gvdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfcolor = (HiddenField)e.Row.FindControl("hdfColor");
                TextBox txtColor = (TextBox)e.Row.FindControl("txtColor");
                txtColor.Style.Add("background-color", hdfcolor.Value);
            }
        }
        protected void lnkUpdateColor_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdate = (LinkButton)sender;
            string[] Value = lnkUpdate.CommandArgument.Split('&');
            txtUser.Text = Value[0] + "-" + Value[1];
            txtColor.Text = Value[2];
            txtColor.Style.Add("background-color", Value[2]);
        }
        protected void lnkDeleteColor_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDelete = (LinkButton)sender;
                string[] Value = lnkDelete.CommandArgument.Split('&');
                NotesClass objNotesClass = new NotesClass();
                objNotesClass.User = Value[1];
                int res = objNotesClass.DeleteUserColor();
                if (res > 0)
                {
                    GEtUserColor();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Deleted');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Deleted');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue data Not Deleted');", true);
            }
        }
    }
}

