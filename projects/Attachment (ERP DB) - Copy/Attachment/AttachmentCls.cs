using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Attachment
{
    public class AttachmentCls
    {
        public static string Con = ConfigurationManager.AppSettings["ConnectionTest"]; // for test

        public string DocumentId { get; set; }
        public string IndexValue { get; set; }
        public string AttachmentHandle { get; set; }
        public string PurposeCode { get; set; }
        public string FileName { get; set; }
        public string LibraryCode { get; set; }
        public string AttachedBy { get; set; }
        public string LibraryDescription { get; set; }
        public string ServerName { get; set; }
        public string Path { get; set; }
        public string IsActive { get; set; }
        public string DBID { get; set; }
        public string DatabaseName { get; set; }
        public string DBServer { get; set; }
        public string DBDescription { get; set; }
        public string Remarks { get; set; }
        public string TableName { get; set; }
        public string PurposeDesc { get; set; }
        public string TableDesc { get; set; }
        public DataTable GetPath()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            //  sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_GetAttachmentPath", sqlParamater.ToArray()).Tables[0];
        }
        public DataTable Insertdata()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@AttachmentHandle", AttachmentHandle));
            sqlParamater.Add(new SqlParameter("@IndexValue", IndexValue));
            sqlParamater.Add(new SqlParameter("@PurposeCode", PurposeCode));
            sqlParamater.Add(new SqlParameter("@FileName", FileName));
            sqlParamater.Add(new SqlParameter("@LibraryCode", LibraryCode));
            sqlParamater.Add(new SqlParameter("@AttachedBy", AttachedBy));
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_InsertAttachment", sqlParamater.ToArray()).Tables[0];
        }
        public DataTable GetFileName()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"
                                    SELECT [t_drid]
                                          ,[t_dcid]                                        
                                          ,[t_fnam]
                                      FROM [ttcisg132200]                                     
                                      WHERE t_fnam = '" + FileName + "'").Tables[0]; //
        }
        public DataTable GetAttachments()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"
                                    SELECT [t_drid]
                                          ,[t_dcid]
                                          ,[t_hndl]
                                          ,[t_indx]
                                          ,[t_fnam]
                                          ,ATH.[t_prcd]
                                          ,[t_lbcd]
                                          ,[t_atby]
                                          ,[t_aton]
                                          ,t_desc
                                      FROM [ttcisg132200]  ATH
                                      LEFT JOIN  ttcisg129200 AP ON AP.t_prcd=ATH.t_prcd
                                      WHERE t_hndl = '" + AttachmentHandle + "' and t_indx ='" + IndexValue + "'").Tables[0]; //
        }
        public DataTable GetAllAttachments()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"
                                    SELECT [t_drid]
                                          ,[t_dcid]
                                          ,[t_hndl]
                                          ,[t_indx]
                                          ,[t_fnam]
                                          ,ATH.[t_prcd]
                                          ,[t_lbcd]
                                          ,[t_atby]
                                          ,[t_aton]
                                          ,t_desc
                                      FROM [ttcisg132200] ATH
                                      LEFT JOIN  ttcisg129200 AP ON AP.t_prcd=ATH.t_prcd").Tables[0]; //
        }
        public DataTable GetLibCode()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_lbcd,t_desc,t_serv,t_path,t_acti FROM ttcisg127200 where t_lbcd='" + LibraryCode + "'").Tables[0];
        }
        public DataTable GetAllLibrary()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_lbcd,t_desc,t_serv,t_path,t_acti FROM ttcisg127200").Tables[0];
        }
        public DataTable GetAllActiveLibrary()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_lbcd,t_desc,t_serv,t_path,t_acti FROM ttcisg127200 Where t_acti='Y'").Tables[0];
        }
        public DataTable GetDBID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_dbid,t_desc,t_serv,t_dbnm,t_lbcd FROM ttcisg128200 where t_dbid='" + DBID + "'").Tables[0];
        }
        public DataTable GetALLDBID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_dbid,t_desc,t_serv,t_dbnm,t_lbcd FROM ttcisg128200").Tables[0];
        }
        public DataTable GetDBIDInHandle()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select count(*) as Dbidcount from ttcisg130200 where t_dbid='" + DBID + "'").Tables[0];
        }
        public DataTable GetALLHandle()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_hndl,t_indx,t_dbid,t_tabl,t_tdes,t_rema from ttcisg130200").Tables[0];
        }
        public DataTable GetALLHandleByID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_hndl,t_indx,t_dbid,t_tabl,t_tdes,t_rema from ttcisg130200 Where t_hndl='" + AttachmentHandle+"'").Tables[0];
        }
        public DataTable GetIndexByHandle(string Attachment_Handle)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_hndl,t_indx  from ttcisg130200 where t_hndl='"+ Attachment_Handle + "'").Tables[0];
        }
        public DataTable GetPurposeCodeCount()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select COUNT(*) as PurposeCodeCount from ttcisg132200 where t_prcd='"+ PurposeCode + "'").Tables[0];
        }
        public DataTable GetHandleId()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_hndl from ttcisg132200 where t_hndl='" + AttachmentHandle + "'").Tables[0];
        }
        public DataTable GetALLPurpose()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_prcd,t_desc from ttcisg129200").Tables[0];
        }
        public DataTable GetALLPurposeByID()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select t_prcd,t_desc from ttcisg129200 Where t_prcd='"+PurposeCode+"'").Tables[0];
        }
        public DataTable GetLibraryCodeFromDataBase()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "Select t_lbcd FROM ttcisg128200 where t_lbcd='" + LibraryCode + "'").Tables[0];
        }
        public int InsertLibrary()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [ttcisg127200]
                       ([t_lbcd]
                       ,[t_desc]
                       ,[t_serv]
                       ,[t_path]
                       ,[t_acti]
                       ,[t_Refcntd]
                       ,[t_Refcntu])
                 VALUES
                       ('" + LibraryCode + "','" + LibraryDescription + "','" + ServerName + "','" + Path + "','" + IsActive + "','0','0')");
        }
        public int UpdateLibrary()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [ttcisg127200]
                       SET [t_desc] = '" + LibraryDescription + @"'
                          ,[t_serv] = '" + ServerName + @"'
                          ,[t_path] = '" + Path + @"'
                          ,[t_acti] = '" + IsActive + @"'
                     WHERE t_lbcd= '" + LibraryCode + @"'");
        }
        public int InsertDBdetails()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [ttcisg128200]
           ([t_dbid]
           ,[t_desc]
           ,[t_serv]
           ,[t_dbnm]
           ,[t_lbcd]
           ,[t_Refcntd]
           ,[t_Refcntu])
     VALUES
           ('" + DBID + "','" + DBDescription + "','" + DBServer + "','" + DatabaseName + "','" + LibraryCode + "','0','0')");
        }
        public int UpdateDatabase()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [ttcisg128200]
                       SET [t_desc] = '" + DBDescription + @"'
                          ,[t_serv] = '" + DBServer + @"'
                          ,[t_dbnm] = '" + DatabaseName + @"'
                          ,[t_lbcd] = '" + LibraryCode + @"'
                     WHERE t_dbid= '" + DBID + @"'");
        }
        public int InsertHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [ttcisg130200]
           ([t_hndl]
           ,[t_dbid]
           ,[t_tabl]
           ,[t_indx]
           ,[t_tdes]
           ,[t_rema] ,[t_Refcntd]
                       ,[t_Refcntu])
     VALUES
           ('" + AttachmentHandle + "','" + DBID + "','" + TableName + "','" + IndexValue + "','"+TableDesc+"','" + Remarks + "','0','0')");
        }
        public int UpdateHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [ttcisg130200]
                       SET [t_dbid] = '" + DBID + @"'
                          ,[t_tabl] = '" + TableName + @"'
                          ,[t_indx] = '" + IndexValue + @"'
                          ,[t_tdes] = '" + TableDesc + @"'
                          ,[t_rema] = '" + Remarks + @"'
                     WHERE t_hndl= '" + AttachmentHandle + @"'");
        }
        public int InsertPurpose()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"INSERT INTO [ttcisg129200]
           ([t_prcd]
           ,[t_desc] ,[t_Refcntd]
                       ,[t_Refcntu])
     VALUES
           ('" + PurposeCode + "','" + PurposeDesc + "','0','0')");
        }
        public int UpdatePurpose()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"UPDATE [ttcisg129200]
                       SET [t_desc] = '" + PurposeDesc + @"'
                     WHERE t_prcd= '" + PurposeCode + @"'");
        }
        public int DeleteLibrary()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [ttcisg127200]
                     WHERE t_lbcd= '" + LibraryCode + @"'");
        }
        public int DeletePurpose()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [ttcisg129200]
                     WHERE t_prcd= '" + PurposeCode + @"'");
        }
        public int DeleteDatabase()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [ttcisg128200]
                     WHERE t_dbid= '" + DBID + @"'");
        }
        public int DeleteHandle()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [ttcisg130200]
                     WHERE t_hndl= '" + AttachmentHandle + @"'");
        }
        public int DeleteAttachment()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, @"Delete from [ttcisg132200]
                     WHERE t_dcid= '" + DocumentId + @"'");
        }
    }
}