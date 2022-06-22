using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Doctype)]
    public class DepartmentApiController : EgovApiBaseController
    {
        public DepartmentApiController()
        {
           
        }
        private string sqlstring = @"server=192.168.1.28; port=3306 ; user id =smartcity;Password=Alkjfls32s@#$fawfw;Database=wso2_mpi;";
        /// <summary>
        /// 
        /// </summary>s
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public List<DepartmentApi> GetsAllDepartments()
        {
           
            MySqlConnection conn = new MySqlConnection(sqlstring);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            string Query = "SELECT * FROM r_dm_truong WHERE namhoc=2019";
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            MySqlDataReader MSQLRD = cmd.ExecuteReader();
            List<DepartmentApi> GetList = new List<DepartmentApi>();

            if (MSQLRD.HasRows)
            {

                while (MSQLRD.Read())
                {
                    DepartmentApi de = new DepartmentApi();
                    de.IdCard = (MSQLRD["matruong"].ToString());
                    de.Name = (MSQLRD["tentruong"].ToString());
                    de.Address = (MSQLRD["tentruong"].ToString());
                    GetList.Add(de);
                }
            }
            conn.Close();
            return GetList;
        }

        /// <summary>
        /// 
        /// </summary>s
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public List<DepartmentApi> GetsAllClass(string MaTruong)
        {

            MySqlConnection conn = new MySqlConnection(sqlstring);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            string Query = "SELECT *  FROM r_dm_lop lop where lop.Matruong ='"+MaTruong+"'";
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            MySqlDataReader MSQLRD = cmd.ExecuteReader();
            List<DepartmentApi> GetList = new List<DepartmentApi>();

            if (MSQLRD.HasRows)
            {

                while (MSQLRD.Read())
                {
                    DepartmentApi de = new DepartmentApi();
                    de.IdCard = (MSQLRD["malop"].ToString());
                    de.Name = (MSQLRD["tenlop"].ToString());
                    de.Address = (MSQLRD["makhoi"].ToString());
                    GetList.Add(de);
                }
            }
            conn.Close();
            return GetList;
        }

        /// <summary>
        /// 
        /// </summary>s
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public List<DepartmentApi> GetsAllStudents(string MaLop)
        {

            MySqlConnection conn = new MySqlConnection(sqlstring);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            string Query = "select * from  r_diemdanh_chitiet where malop = '"+MaLop+"'";
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            MySqlDataReader MSQLRD = cmd.ExecuteReader();
            List<DepartmentApi> GetList = new List<DepartmentApi>();

            if (MSQLRD.HasRows)
            {

                while (MSQLRD.Read())
                {
                    DepartmentApi de = new DepartmentApi();
                    de.IdCard = (MSQLRD["malop"].ToString());
                    de.Name = (MSQLRD["hoten"].ToString());
                    de.Address = (MSQLRD["trangthai"].ToString());
                    GetList.Add(de);
                }
            }
            conn.Close();
            return GetList;
        }
        /// <summary>
        /// 
        /// </summary>s
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public List<DepartmentApi> GetsAllStudents()
        {

            MySqlConnection conn = new MySqlConnection(sqlstring);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            string Query = "select * from  r_diemdanh_chitiet where malop = 'shcm_2019_79000301_16_79000301_2495455' ";
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            MySqlDataReader MSQLRD = cmd.ExecuteReader();
            List<DepartmentApi> GetList = new List<DepartmentApi>();

            if (MSQLRD.HasRows)
            {

                while (MSQLRD.Read())
                {
                    DepartmentApi de = new DepartmentApi();
                    de.IdCard = (MSQLRD["malop"].ToString());
                    de.Name = (MSQLRD["hoten"].ToString());
                    de.Address = (MSQLRD["trangthai"].ToString());
                    GetList.Add(de);
                }
            }
            conn.Close();
            return GetList;
        }
    }
}