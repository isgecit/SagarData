using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Attachment
{
    public class NotesClass
    {
        public static string Con = ConfigurationManager.AppSettings["Connection"];

        public string IndexValue { get; set; }
        public string Remarks { get; set; }
        public string DBID { get; set; }
        public string TableName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string NoteID { get; set; }
        public string NotesHandle { get; set; }
        public string TableDesc { get; set; }
        public string RemiderMailId { get; set; }
        public string ReminderDateTime { get; set; }
        public string Color { get; set; }
        public string SendEmailTo { get; set; }
        public DataTable GetALLNotesHandle()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select NotesHandle,AccessIndex,TableDescription,TableName,Remarks from Note_Handle").Tables[0];
        }

        public DataTable GetNotesReminder()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select ReminderTo,ReminderDateTime,Status,Title,Description from Note_reminder NR
                                    INNER JOIN  Note_Notes NN ON NN.NotesId = NR.NotesId
                                    order by NR.Status, ReminderDateTime desc
                                    ").Tables[0];
        }

        public DataTable GetALLDBID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select DBID,DbDescription,DbServerName,DatabaseName,libraryCode FROM ATH_Database").Tables[0];
        }
        public DataTable GetALLHandleByID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select NotesHandle,AccessIndex,DBID,TableName,TableDescription,Remarks from Note_Handle Where NotesHandle='" + NotesHandle + "'").Tables[0];
        }
        public DataTable GetUsedHandle()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select * from Note_Notes Where NotesHandle='" + NotesHandle + "'").Tables[0];
        }
        public DataTable GetNotesByRunningId()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Select Notes_RunningNo,NN.NotesId,NotesHandle,IndexValue,Title,Description,
                     NN.UserId, Created_Date, NR.ReminderTo,NR.ReminderDateTime,NUC.ColorId,NN.SendEmailTo  from Note_Notes NN 
                     LEFT JOIN Note_Reminder NR ON NR.NotesId = NN.NotesId
                     LEFT JOIN Note_UserColor NUC ON NUC.UserId=NN.UserId
                     Where NN.NotesId  ='" + NoteID + "'").Tables[0];
        }
        public DataTable GetNotes()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Select Notes_RunningNo,NotesId,NN.NotesHandle,IndexValue,Title,Description,
                        NN.UserId, Created_Date, TableDescription,EMP.EmployeeName,NUC.ColorId,NN.SendEmailTo from Note_Notes NN
                        INNER JOIN Note_Handle NH ON NH.NotesHandle = NN.NotesHandle
                        INNER JOIN HRM_Employees EMP ON EMP.CardNo=NN.USerId
                        LEFT JOIN Note_UserColor NUC ON NUC.UserId=NN.UserId
                        Where NN.NotesHandle='" + NotesHandle + "' and NN.IndexValue='" + IndexValue + "' order by Notes_RunningNo").Tables[0];
        }

        public DataTable GetNoteID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select IndexValue from Note_Notes where NotesId='" + IndexValue + "'").Tables[0]; //
        }
        public DataTable GetNotesFromASPNETUSer()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Select Notes_RunningNo, NotesId, NN.NotesHandle,IndexValue,Title,Description,
                        NN.UserId, Created_Date, TableDescription,EMP.UserFullName as EmployeeName,NUC.ColorId,
                        NN.SendEmailTo from Note_Notes NN
                        INNER JOIN Note_Handle NH ON NH.NotesHandle = NN.NotesHandle
                        INNER JOIN aspnet_users EMP ON EMP.LoginId= NN.USerId
                        LEFT JOIN Note_UserColor NUC ON NUC.UserId= NN.UserId
                        Where NN.NotesHandle='" + NotesHandle + "' and NN.IndexValue='" + IndexValue + "' order by Notes_RunningNo").Tables[0];
        }
        public DataTable GetAllNotes()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"Select Notes_RunningNo,NotesId,NN.NotesHandle,IndexValue,Title,Description,
                        NN.UserId, Created_Date, TableDescription,EMP.EmployeeName,NUC.ColorId,NN.SendEmailTo from Note_Notes NN
                        INNER JOIN Note_Handle NH ON NH.NotesHandle = NN.NotesHandle
                        INNER JOIN HRM_Employees EMP ON EMP.CardNo=NN.USerId
                        LEFT JOIN Note_UserColor NUC ON NUC.UserId=NN.UserId
                        order by Notes_RunningNo").Tables[0];
        }
        public DataTable GetEmployeeDetails()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select EmployeeName, EmailID from HRM_Employees
                                        where CardNo ='" + User + "'").Tables[0];
        }
        public int InsertNotesHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [Note_Handle]
           ([NotesHandle]
           ,[DBID]
           ,[TableName]
           ,[TableDescription]
           ,[AccessIndex]
           ,[Remarks])
     VALUES
           ('" + NotesHandle + "','" + DBID + "','" + TableName + "','" + TableDesc + "','" + IndexValue + "','" + Remarks + "')");
        }
        public int UpdateNotesHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [Note_Handle]
                       SET [DBID] = '" + DBID + @"'
                          ,[TableName] = '" + TableName + @"'
                          ,[AccessIndex] = '" + IndexValue + @"'
                          ,[Remarks] = '" + Remarks + @"'
                     WHERE NotesHandle= '" + NotesHandle + @"'");
        }
        public int DeleteNotesHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [Note_Handle]
                     WHERE  NotesHandle= '" + NotesHandle + @"'");
        }
        public DataTable Insertdata()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@NotesHandle", NotesHandle));
            sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@Title", Title));
            sqlParamater.Add(new SqlParameter("@Description", Description));
            sqlParamater.Add(new SqlParameter("@User", @User));
            sqlParamater.Add(new SqlParameter("@ReminderEmailId", RemiderMailId));
            sqlParamater.Add(new SqlParameter("@ReminderDateTime", ReminderDateTime));
            sqlParamater.Add(new SqlParameter("@SendEmailTo", SendEmailTo));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "[sp_InsertNotes]", sqlParamater.ToArray()).Tables[0];
        }
        public int UpdateNotes()
        {
            if (RemiderMailId != "" && ReminderDateTime != "")
            {
                DataTable dtNoteID = SqlHelper.ExecuteDataset(Con, CommandType.Text, "select NotesID from Note_Reminder where NotesId='" + NoteID + "'").Tables[0];
                if (dtNoteID.Rows.Count == 0)
                {
                    DataTable dtReminderId = SqlHelper.ExecuteDataset(Con, CommandType.Text, "SELECT(ISNULL(MAX(ReminderId), 0) + 1) as ReminderId FROM Note_Reminder").Tables[0];
                    SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"
                        INSERT INTO[Note_Reminder]
                       ([ReminderID]
                       ,[NotesId]
                       ,[ReminderTo]
                       ,[ReminderDateTime]
                       ,[Status]
                       ,[User]
                       ,[SendEmailTo]
                       )
                 VALUES('" + dtReminderId.Rows[0][0] + @"'
                       , '" + NoteID + @"'
                       , '" + RemiderMailId + @"'
                       , '" + Convert.ToDateTime(ReminderDateTime).ToString("yyyy-MM-dd HH:mm") + @"'
                       ,'New'
                       , '" + User + @"'
                       , '" + SendEmailTo + @"'
                       )");
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "update Note_Reminder set ReminderTo='" + RemiderMailId + "', ReminderDateTime='" + Convert.ToDateTime(ReminderDateTime).ToString("yyyy-MM-dd HH:mm") + "',Status='New' WHERE NotesId='" + NoteID + "'");
                }
            }
            else
            {

            }
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [Note_Notes]
                       SET [Title] = '" + Title + @"'
                          ,[Description] = '" + Description + @"'
                     WHERE NotesId= '" + NoteID + @"'");
        }
        public int DeleteNotes()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [Note_Notes]
                     WHERE  NotesId= '" + NoteID + @"'");
        }

        public int InsertColor()
        {
            DataTable dt = SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select UserId from Note_UserColor where USerId='" + User + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
              return  SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update Note_UserColor SET ColorId='" + Color + "' Where UserId='" + User + "'");
            }
            else
            {
                return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [Note_UserColor]
           ([UserId]
           ,[ColorId]
           )
     VALUES
           ('" + User + "','" + Color + "')");
            }
        }

        public DataTable GetUser(string UserID)
        {
            //string q="select CardNo, EmployeeName  from HRM_Employees where EmployeeName Like '"+ UserID + "%' or CardNo Like '"+ UserID + "%'";
          return  SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName  from HRM_Employees where EmployeeName Like '"+ UserID + "%' or CardNo Like '"+ UserID + "%'").Tables[0];
        }

        public DataTable GEtUSerColor()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select USerId, ColorId, EmployeeNAme from
                                  Note_UserColor NU
                                  INNER JOIN HRM_Employees E ON E.CardNo = NU.UserID").Tables[0];
        }
        public int DeleteUserColor()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Delete from Note_UserColor where userId='" + User + "'");
        }

        public string GetTempNoteID()
        {
            DataTable dt= SqlHelper.ExecuteDataset(Con, CommandType.Text, @"SELECT {fn CONCAT(CAST(Series as varchar),CAST(ISNULL(MAX(RunningNo),0) + 1  as varchar))}
                        as DocumentID FROM Note_DocumentSeries where Active='Y'
                        GROUP By Series").Tables[0];

            string TempNoteId = dt.Rows[0][0].ToString();
            return TempNoteId;

        }
    }
}