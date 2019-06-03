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

        //BEGIN - 1 Manufacturer 20170202
        public List<Sponsor> GetAllSponsor()
        {
            var items = new List<Sponsor>();
            string commandText = "SELECT MANUFACTURER_ID, MANUFACTURER_NAME";

            commandText += " FROM CTA_OWNER.MANUFACTURER";

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
                                var item = new Sponsor();
                                item.manufacturer_id   = dr["MANUFACTURER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MANUFACTURER_ID"]);
                                item.manufacturer_name = dr["MANUFACTURER_NAME"] == DBNull.Value ? string.Empty : dr["MANUFACTURER_NAME"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllSponsor()");
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
        public Sponsor GetSponsorById(int id)
        {
            var sponsor = new Sponsor();

            string commandText = "SELECT * ";

            commandText += " FROM CTA_OWNER.MANUFACTURER WHERE MANUFACTURER_ID = :id ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":id", id);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                sponsor.manufacturer_id   = dr["MANUFACTURER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MANUFACTURER_ID"]);
                                sponsor.manufacturer_name = dr["MANUFACTURER_NAME"] == DBNull.Value ? string.Empty : dr["MANUFACTURER_NAME"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetSponsorById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();

                }
            }
            return sponsor;
        }//END - 1 Sponsor 20170202

        //BEGIN - 2 MedicalCondition 20170203
        public List<MedicalCondition> GetAllMedicalCondition()
        {
            var items = new List<MedicalCondition>();
            string commandText = "SELECT MED_CONDITION_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " MED_CONDITION_FR AS MED_CONDITION";
            }
            else
            {
                commandText += " MED_CONDITION_EN AS MED_CONDITION";
            }
            
            commandText += " FROM CTA_OWNER.MEDICAL_CONDITION";
           
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
                                var item = new MedicalCondition();
                                item.med_condition_id = dr["MED_CONDITION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MED_CONDITION_ID"]);
                                item.med_condition    = dr["MED_CONDITION"] == DBNull.Value ? string.Empty : dr["MED_CONDITION"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllMedicalCondition()");
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
        public MedicalCondition GetMedicalConditionById(int id)
        {
            var medcondition = new MedicalCondition();

            string commandText = "SELECT MED_CONDITION_ID,  ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " MED_CONDITION_FR AS MED_CONDITION";
            }
            else
            {
                commandText += " MED_CONDITION_EN AS MED_CONDITION";
            }
            commandText += " FROM CTA_OWNER.MEDICAL_CONDITION WHERE MED_CONDITION_ID = :id ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":id", id);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                medcondition.med_condition_id = dr["MED_CONDITION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MED_CONDITION_ID"]);
                                medcondition.med_condition    = dr["MED_CONDITION"] == DBNull.Value ? string.Empty : dr["MED_CONDITION"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetMedicalConditionById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return medcondition;
        }//END - 2 Medical Condition 20170203

        //BEGIN - 3 Product Brand 20170202
        public List<DrugProduct> GetAllDrugProduct()
        {
            var items = new List<DrugProduct>();

            string commandText = "SELECT A.CTA_PROTOCOL_ID, A.SUBMISSION_NO, A.BRAND_ID, A.MANUFACTURER_ID, C.MANUFACTURER_NAME, A.BRAND_NAME_FR BRAND_NAME_FR , A.BRAND_NAME_EN BRAND_NAME_EN ";
            //if (this.Lang.Equals("fr"))
            //{
            //    commandText += " A.BRAND_NAME_FR AS BRAND_NAME ";
            //}
            //else
            //{
            //    commandText += " A.BRAND_NAME_EN AS BRAND_NAME ";
            //}
            commandText += "FROM CTA_OWNER.PRODUCT_BRAND A, CTA_OWNER.MANUFACTURER C WHERE A.MANUFACTURER_ID = C.MANUFACTURER_ID(+)";


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
                                var item = new DrugProduct();
                                item.protocol_id       = dr["CTA_PROTOCOL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_PROTOCOL_ID"]);
                                item.submission_no     = dr["SUBMISSION_NO"] == DBNull.Value ? string.Empty : dr["SUBMISSION_NO"].ToString().Trim();
                                item.brand_id          = dr["BRAND_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BRAND_ID"]);
                                item.manufacturer_id   = dr["MANUFACTURER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MANUFACTURER_ID"]);
                                //item.brand_name        = dr["BRAND_NAME"] == DBNull.Value ? string.Empty : dr["BRAND_NAME"].ToString().Trim();
                                item.manufacturer_name = dr["MANUFACTURER_NAME"] == DBNull.Value ? string.Empty : dr["MANUFACTURER_NAME"].ToString().Trim();
                                if (this.Lang != null && this.Lang.Equals("fr"))
                                {
                                    item.brand_name = dr["BRAND_NAME_FR"] == DBNull.Value ? dr["BRAND_NAME_EN"].ToString().Trim() : dr["BRAND_NAME_FR"].ToString().Trim();                                    
                                }
                                else
                                {
                                    item.brand_name = dr["BRAND_NAME_EN"] == DBNull.Value ? dr["BRAND_NAME_FR"].ToString().Trim() : dr["BRAND_NAME_EN"].ToString().Trim();
                                }
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllDrugProduct()");
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

        public DrugProduct GetDrugProductById(int id)
        {
            var drugproduct = new DrugProduct();

            string commandText = "SELECT A.CTA_PROTOCOL_ID, A.SUBMISSION_NO, A.BRAND_ID, A.MANUFACTURER_ID, C.MANUFACTURER_NAME, A.BRAND_NAME_FR AS BRAND_NAME_FR, A.BRAND_NAME_EN AS BRAND_NAME_EN ";
            //if (this.Lang.Equals("fr"))
            //{
            //    commandText += " A.BRAND_NAME_FR AS BRAND_NAME ";
            //}
            //else
            //{
            //    commandText += " A.BRAND_NAME_EN AS BRAND_NAME ";
            //}
            commandText += "FROM CTA_OWNER.PRODUCT_BRAND A, CTA_OWNER.MANUFACTURER C WHERE A.MANUFACTURER_ID = C.MANUFACTURER_ID(+) AND A.BRAND_ID = :id "; 

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":id", id);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                drugproduct.protocol_id       = dr["CTA_PROTOCOL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_PROTOCOL_ID"]);
                                drugproduct.submission_no     = dr["SUBMISSION_NO"] == DBNull.Value ? string.Empty : dr["SUBMISSION_NO"].ToString().Trim();
                                drugproduct.brand_id          = dr["BRAND_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BRAND_ID"]);
                                drugproduct.manufacturer_id   = dr["MANUFACTURER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MANUFACTURER_ID"]);
                                //drugproduct.brand_name        = dr["BRAND_NAME"] == DBNull.Value ? string.Empty : dr["BRAND_NAME"].ToString().Trim();
                                drugproduct.manufacturer_name = dr["MANUFACTURER_NAME"] == DBNull.Value ? string.Empty : dr["MANUFACTURER_NAME"].ToString().Trim();
                                if (this.Lang != null && this.Lang.Equals("fr"))
                                {
                                    drugproduct.brand_name = dr["BRAND_NAME_FR"] == DBNull.Value ? dr["BRAND_NAME_EN"].ToString().Trim() : dr["BRAND_NAME_FR"].ToString().Trim();
                                }
                                else
                                {
                                    drugproduct.brand_name = dr["BRAND_NAME_EN"] == DBNull.Value ? dr["BRAND_NAME_FR"].ToString().Trim() : dr["BRAND_NAME_EN"].ToString().Trim();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetProductBrandById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return drugproduct;
        }//END - 3 Product Brand 20170205

        //BEGIN - 4 Protocol 20170207
        public List<Protocol> GetAllProtocol()
        {
            var items = new List<Protocol>();

            string commandText = "SELECT CTA_PROTOCOL_ID, CTA_PROTOCOL_NO, SUBMISSION_NO, CTA_STATUS_ID, START_DATE, END_DATE, NOL_DATE, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " CTA_PROTOCOL_TITLE_FR AS CTA_PROTOCOL_TITLE";
            }
            else
            {
                commandText += " CTA_PROTOCOL_TITLE_EN AS CTA_PROTOCOL_TITLE";
            }
            commandText += " FROM CTA_OWNER.CTA_PROTOCOL";

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
                                var item = new Protocol();

                                item.protocol_id    = dr["CTA_PROTOCOL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_PROTOCOL_ID"]);
                                item.protocol_no    = dr["CTA_PROTOCOL_NO"] == DBNull.Value ? string.Empty : dr["CTA_PROTOCOL_NO"].ToString().Trim();
                                item.submission_no  = dr["SUBMISSION_NO"] == DBNull.Value ? string.Empty : dr["SUBMISSION_NO"].ToString().Trim();
                                item.status_id      = dr["CTA_STATUS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_STATUS_ID"]);
                                item.start_date     = dr["START_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["START_DATE"]);
                                item.end_date       = dr["END_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["END_DATE"]);
                                item.nol_date       = dr["NOL_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["NOL_DATE"]);
                                item.protocol_title = dr["CTA_PROTOCOL_TITLE"] == DBNull.Value ? string.Empty : dr["CTA_PROTOCOL_TITLE"].ToString().Trim();

                                var medConditionList = GetAllMedicalConditionByProtocol(item.protocol_id);
                                if (medConditionList != null && medConditionList.Count > 0)
                                {
                                    item.medConditionList = medConditionList;
                                }
                                var studyPopulationList = GetAllStudyPopulationByProtocol(item.protocol_id);
                                if (studyPopulationList != null && studyPopulationList.Count > 0)
                                {
                                    item.studyPopulationList = studyPopulationList;
                                }
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllProtocol()");
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

        public Protocol GetProtocolById(int id)
        {
            var protocol = new Protocol();

            string commandText = "SELECT CTA_PROTOCOL_ID, CTA_PROTOCOL_NO, SUBMISSION_NO, CTA_STATUS_ID, START_DATE, END_DATE, NOL_DATE, ";

            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " CTA_PROTOCOL_TITLE_FR AS CTA_PROTOCOL_TITLE";
            }
            else
            {
                commandText += " CTA_PROTOCOL_TITLE_EN AS CTA_PROTOCOL_TITLE";
            }
            commandText += "  FROM CTA_OWNER.CTA_PROTOCOL WHERE CTA_PROTOCOL_ID = :id ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":id", id);
                
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                protocol.protocol_id    = dr["CTA_PROTOCOL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_PROTOCOL_ID"]);
                                protocol.protocol_no    = dr["CTA_PROTOCOL_NO"] == DBNull.Value ? string.Empty : dr["CTA_PROTOCOL_NO"].ToString().Trim();
                                protocol.submission_no  = dr["SUBMISSION_NO"] == DBNull.Value ? string.Empty : dr["SUBMISSION_NO"].ToString().Trim();
                                protocol.status_id      = dr["CTA_STATUS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CTA_STATUS_ID"]);
                                protocol.start_date     = dr["START_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["START_DATE"]);
                                protocol.end_date       = dr["END_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["END_DATE"]);
                                protocol.nol_date       = dr["NOL_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["NOL_DATE"]);
                                protocol.protocol_title = dr["CTA_PROTOCOL_TITLE"] == DBNull.Value ? string.Empty : dr["CTA_PROTOCOL_TITLE"].ToString().Trim();

                                var medConditionList = GetAllMedicalConditionByProtocol(protocol.protocol_id);
                                if (medConditionList != null && medConditionList.Count > 0)
                                {
                                    protocol.medConditionList = medConditionList;
                                }
                                var studyPopulationList = GetAllStudyPopulationByProtocol(protocol.protocol_id);
                                if (studyPopulationList != null && studyPopulationList.Count > 0)
                                {
                                    protocol.studyPopulationList = studyPopulationList;
                                }
                               // protocol.Add(protocol);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetProtocolById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return protocol;
        }
        //END - 4 Protocol 20170207

        //BEGIN - 5 Status 20170202
        public List<Status> GetAllStatus()
        {
            var items = new List<Status>();
            string commandText = "SELECT CTA_STATUS_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
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
                                item.status    = dr["CTA_STATUS"] == DBNull.Value ? string.Empty : dr["CTA_STATUS"].ToString().Trim();
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
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " CTA_STATUS_FR AS CTA_STATUS";
            }
            else
            {
                commandText += " CTA_STATUS_EN AS CTA_STATUS";
            }
            commandText += " FROM CTA_OWNER.CTA_STATUS WHERE CTA_STATUS_ID = :id ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":id", id);

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
                                status.status    = dr["CTA_STATUS"] == DBNull.Value ? string.Empty : dr["CTA_STATUS"].ToString().Trim();
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
        } //END - 5 Status 20170202

        //BEGIN - 6 Study Population 20170206
        public List<StudyPopulation> GetAllStudyPopulation()
        {
            var items = new List<StudyPopulation>();
            string commandText = "SELECT STUDY_POPULATION_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " STUDY_POPULATION_FR AS STUDY_POPULATION";
            }
            else
            {
                commandText += " STUDY_POPULATION_EN AS STUDY_POPULATION";
            }
            commandText += " FROM CTA_OWNER.STUDY_POPULATION";

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
                                var item = new StudyPopulation();
                                item.study_population_id = dr["STUDY_POPULATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["STUDY_POPULATION_ID"]);
                                item.study_population    = dr["STUDY_POPULATION"]    == DBNull.Value ? string.Empty : dr["STUDY_POPULATION"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllStudyPopulation()");
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

        public StudyPopulation GetStudyPopulationById(int id)
        {
            var studypop = new StudyPopulation();
           
            string commandText = "SELECT STUDY_POPULATION_ID,  ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " STUDY_POPULATION_FR AS STUDY_POPULATION";
            }
            else
            {
                commandText += " STUDY_POPULATION_EN AS STUDY_POPULATION";
            }
            commandText += " FROM CTA_OWNER.STUDY_POPULATION WHERE STUDY_POPULATION_ID = :id ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":id", id);

                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                studypop.study_population_id = dr["STUDY_POPULATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["STUDY_POPULATION_ID"]);
                                studypop.study_population    = dr["STUDY_POPULATION"] == DBNull.Value ? string.Empty : dr["STUDY_POPULATION"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetStudyPopulationById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return studypop;
        }//END - 6 Study Population 20170206

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // The function/method below this point are only used by the GetAllProtocol & GetProtocolById.
        // They generate a list of Medical Condition(s) and Study Population(s).
        // Date: 20170209
        // Barry Martin
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        //BEGIN - 7 MedicalConditionByProtocol 20170209
        //          This function is called by GetAllProtocol to populate the list (collection list) of associated medical condition(s) to a particular Protocol.
        public List<MedicalCondition> GetAllMedicalConditionByProtocol(int protocolId)
        {
            var items = new List<MedicalCondition>();
            string commandText = "SELECT MC.MED_CONDITION_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " MED_CONDITION_FR AS MED_CONDITION";
            }
            else
            {
                commandText += " MED_CONDITION_EN AS MED_CONDITION";
            }

            commandText += " FROM CTA_OWNER.MEDICAL_CONDITION MC, CTA_OWNER.CTA_MED_CONDITION CMC";
            commandText += " WHERE CMC.MED_CONDITION_ID = MC.MED_CONDITION_ID AND CMC.CTA_PROTOCOL_ID = :protocolId ";
            
            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":protocolId", protocolId);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new MedicalCondition();
                                item.med_condition_id = dr["MED_CONDITION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MED_CONDITION_ID"]);
                                item.med_condition    = dr["MED_CONDITION"] == DBNull.Value ? string.Empty : dr["MED_CONDITION"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllMedicalConditionByProtocol()");
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

        // 20160209
        // This function is called by GetProtocolById to populate the list (collection list) of associated medical condition(s) to a particular Protocol.
        public MedicalCondition GetMedicalConditionByProtocol(int protocolId)
        {
            var medcondition = new MedicalCondition();

            string commandText = "SELECT MC.MED_CONDITION_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " MED_CONDITION_FR AS MED_CONDITION";
            }
            else
            {
                commandText += " MED_CONDITION_EN AS MED_CONDITION";
            }

            commandText += " FROM CTA_OWNER.MEDICAL_CONDITION MC, CTA_OWNER.CTA_MED_CONDITION CMC";
            commandText += " WHERE CMC.MED_CONDITION_ID = MC.MED_CONDITION_ID AND CMC.CTA_PROTOCOL_ID = :protocolId ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":protocolId", protocolId);

                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                medcondition.med_condition_id = dr["MED_CONDITION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MED_CONDITION_ID"]);
                                medcondition.med_condition    = dr["MED_CONDITION"] == DBNull.Value ? string.Empty : dr["MED_CONDITION"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetMedicalConditionByProtocol()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return medcondition;
        }

        //END - 7 MedicalConditionByProtocol 20170209
       
        //BEGIN - 8 GetAllStudyPopulationByProtocol 20170209
        //        This function is called by GetAllProtocol to populate the list (collection list) of associated study populaiton(s) to a particular Protocol.
        public List<StudyPopulation> GetAllStudyPopulationByProtocol(int protocolId)
        {
            var items = new List<StudyPopulation>();
            string commandText = "SELECT MC.STUDY_POPULATION_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " STUDY_POPULATION_FR AS STUDY_POPULATION";
            }
            else
            {
                commandText += " STUDY_POPULATION_EN AS STUDY_POPULATION";
            }

            commandText += " FROM CTA_OWNER.STUDY_POPULATION MC, CTA_OWNER.CTA_STUDY_POPULATION CMC";
            commandText += " WHERE CMC.STUDY_POPULATION_ID = MC.STUDY_POPULATION_ID AND CMC.CTA_PROTOCOL_ID = :protocolId ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":protocolId", protocolId);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new StudyPopulation();
                                item.study_population_id = dr["STUDY_POPULATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["STUDY_POPULATION_ID"]);
                                item.study_population    = dr["STUDY_POPULATION"] == DBNull.Value ? string.Empty : dr["STUDY_POPULATION"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllStudyPopulationByProtocol()");
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

        // 20160209
        // This function is called by GetProtocolById to populate the list (collection list) of associated medical condition(s) to a particular Protocol.
        public StudyPopulation GetStudyPopulationByProtocol(int protocolId)
        {
            var studypop = new StudyPopulation();

            string commandText = "SELECT MC.STUDY_POPULATION_ID, ";
            if (this.Lang != null && this.Lang.Equals("fr"))
            {
                commandText += " STUDY_POPULATION_FR AS STUDY_POPULATION";
            }
            else
            {
                commandText += " STUDY_POPULATION_EN AS STUDY_POPULATION";
            }

            commandText += " FROM CTA_OWNER.MEDICAL_CONDITION MC, CTA_OWNER.CTA_STUDY_POPULATION CMC";
            commandText += " WHERE CMC.STUDY_POPULATION_ID = MC.STUDY_POPULATION_ID AND CMC.CTA_PROTOCOL_ID = :protocolId ";

            using (OracleConnection con = new OracleConnection(CtDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                cmd.Parameters.Add(":protocolId", protocolId);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                studypop.study_population_id = dr["STUDY_POPULATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["STUDY_POPULATION_ID"]);
                                studypop.study_population    = dr["STUDY_POPULATION"] == DBNull.Value ? string.Empty : dr["STUDY_POPULATION"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetStudyPopulationByProtocol()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return studypop;
        }

    } //END of DBConnection class.

}
