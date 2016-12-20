using clinical;
using ctWebApi.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

using System.Configuration;
using System.Data;

namespace clinical
{
    public class DBConnection
    {

        private string _lang;
        public string Lang
        {
            get { return this._lang; }
            set { this._lang = value; }
        }

        public DBConnection(string lang)
        {
            this._lang = lang;
        }

        private string CtDBConnection
        {
            get { return ConfigurationManager.ConnectionStrings["ctweb"].ToString(); }
        }




        public List<Status> GetAllStatus()
        {
            var items = new List<Status>();
            string commandText = "SELECT CTA_STATUS_ID, ";
            if (this.Lang.Equals("fr"))
            {
                commandText += " CTA_STATUS_FR AS CTA_STATUS";
            }
            else
            {
                commandText += " CTA_STATUS_EN AS CTA_STATUS";
            }
            commandText += " FROM CTA_OWNER.CTA_STATUS";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Status();
                                item.status_id = dr["CTA_STATUS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_STATUS_ID"]);
                                item.status = dr["CTA_STATUS"] == DBNull.Value ? string.Empty : dr["CTA_STATUS"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllStatus()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;
        }

        public Status GetStatusById(int id)
        {
            var status = new Status();

            string commandText = "SELECT CTA_STATUS_ID, ";
            if (this.Lang.Equals("fr"))
            {
                commandText += " CTA_STATUS_FR AS CTA_STATUS";
            }
            else
            {
                commandText += " CTA_STATUS_EN AS CTA_STATUS";
            }
            commandText += " FROM CTA_OWNER.CTA_STATUS WHERE CTA_STATUS_ID = " + id;

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                status.status_id = dr["CTA_STATUS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_STATUS_ID"]);
                                status.status = dr["CTA_STATUS"] == DBNull.Value ? string.Empty : dr["CTA_STATUS"].ToString().Trim();
                             }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetStatusById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return status;
        }

    }

}
