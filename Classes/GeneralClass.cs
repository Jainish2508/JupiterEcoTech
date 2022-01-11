using Dapper;
using JupiterEcoTech.Models;
using Microsoft.Security.Application;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace JupiterEcoTech.Classes
{
    public class GeneralClass
    {
        private static GeneralModels generalModels = new GeneralModels();
        private static TimeZoneInfo IST = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private string Subject;
        public string IP_From_DB(GeneralModels generalModels)
        {
            string cip = "";
            try
            {
                if (generalModels.ip.IndexOf('.') > 0)
                {
                    string[] iprange = generalModels.ip.Split('.');
                    for (int i = 0; i < iprange.Length; i++)
                    {
                        cip += iprange[i].PadLeft(3,'0');
                    }
                }
                if(string.IsNullOrEmpty(generalModels.connection_string))
                    generalModels.connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(generalModels.connection_string);
                SqlCommand cmd = new SqlCommand("IP2COUNTRY", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@CIP", cip);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 2)
                    generalModels.country_code = dt.Rows[dt.Rows.Count - 1]["Code"].ToString();
                else if (dt.Rows.Count == 1)
                    generalModels.country_code = dt.Rows[0]["Code"].ToString();
                generalModels.country_code = "IN";
                return generalModels.country_code;
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "IP_From_DB");
            }
            return null;
        }
        public IEnumerable<HomeViewModels> Get_Random_Products()
        {
            try
            {
                if (string.IsNullOrEmpty(generalModels.connection_string))
                    generalModels.connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(generalModels.connection_string);
                var listitems = SqlMapper.Query<HomeViewModels>(conn, "GetRandomProduct", commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
                return listitems.ToList();
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "Get_Random_Products");
            }
            return null;
        }

        public IPagedList<Product> All_products_from_db(int? page, [Optional] string p_category)
        {
            try
            {
                int pageSize = 12;
                int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                if (string.IsNullOrEmpty(generalModels.connection_string))
                    generalModels.connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(generalModels.connection_string);
                var param = new DynamicParameters();
                if (p_category != null)
                {
                    param.Add("@category", p_category);
                }
                var objproduct = SqlMapper.QueryMultiple(conn, "GetProduct", param, commandType: CommandType.StoredProcedure);
                IPagedList<Product> pdl = objproduct.Read<Product>().ToList().ToPagedList(pageIndex, pageSize);
                conn.Close();
                return pdl;
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "All_products_from_db");
            }
            return null;
        }

        public IEnumerable<ProductDetails> Product_Details(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(generalModels.connection_string))
                    generalModels.connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(generalModels.connection_string);
                var param = new DynamicParameters();
                if (name != null)
                    name = name.ToLower().Replace("-", " ");
                param.Add("@pname", name);
                var objdetails = SqlMapper.Query<ProductDetails>(conn, "GetProductDetail", param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
                return objdetails.ToList();
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "Product_Details");
            }
            return null;
        }

        public List<SelectListItem> Product_Name()
        {
            try
            {
                if (string.IsNullOrEmpty(generalModels.connection_string))
                    generalModels.connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(generalModels.connection_string);
                List<SelectListItem> productList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(generalModels.connection_string))
                {
                    using (SqlCommand cmd = new SqlCommand("GetProductName", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                productList.Add(new SelectListItem
                                {
                                    Text = sdr["ProductName"].ToString(),
                                    Value = sdr["ProductName"].ToString()
                                });
                            }
                        }
                        con.Close();
                    }
                }
                productList.Add(new SelectListItem { 
                    Text = "Other product",
                    Value = "custom"
                });
                return productList;
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "Product_Name");
            }
            return null;
        }

        public static GeneralModels.CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = ConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<GeneralModels.CaptchaResponse>(jsonResult.ToString());
        }

        [Obsolete]
        public async Task<bool> Send_mail(GeneralModels generalModels,[Optional] EnquiryViewModels enquiryViewModels,[Optional] ContactViewModels contactViewModels,string page)
        {
            try
            {
                string from = ConfigurationManager.AppSettings.Get("From");
                var message = new MailMessage();
                string to = ConfigurationManager.AppSettings.Get("To");
                DateTime timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST);
                if (page.ToLower().Contains("enquiry"))
                {
                    foreach (var en in enquiryViewModels.GetType().GetProperties())
                    {
                        if(en.Name != "Product_Names")
                            en.SetValue(enquiryViewModels, Sanitizer.GetSafeHtmlFragment(en.GetValue(enquiryViewModels).ToString()));
                    }
                    var body = "<p><b>Name:</b> {0}</p>" +
                                "<p><b>Email:</b> {1}</p>" +
                                "<p><b>Phone:</b> {2}</p>" +
                                "<p><b>Product:</b> {3}</p>" +
                                "<p><b>Message: </b>{4}</p>" +
                                "<p><b>IP Address: </b>{5}/{6}</p>" +
                                "<p><b>Date: </b>{7}</p>";
                    Subject = "Enquiry for " + enquiryViewModels.Product_Name;
                    message.Subject = Subject;
                    message.Body = string.Format(body,
                        enquiryViewModels.Name,
                        enquiryViewModels.Email,
                        enquiryViewModels.Contact,
                        enquiryViewModels.Product_Name,
                        enquiryViewModels.Query,
                        generalModels.ip,
                        generalModels.country_code,
                        timestamp);

                }
                else
                {
                    foreach (var c in contactViewModels.GetType().GetProperties())
                    {
                        c.SetValue(contactViewModels, Sanitizer.GetSafeHtmlFragment(c.GetValue(contactViewModels).ToString()));
                    }
                    var body = "<p><b>Name:</b> {0}</p>" +
                                "<p><b>Email:</b> {1}</p>" +
                                "<p><b>Phone:</b> {2}</p>" +
                                "<p><b>Message: </b>{3}</p>" +
                                "<p><b>IP Address: </b>{4}/{5}</p>" +
                                "<p><b>Date: </b>{6}</p>" +
                                "<p><b>Current Page:</b>{7}</p>";
                    Subject = "Feedback mail";
                    message.Subject = Subject;
                    message.Body = string.Format(body,
                        contactViewModels.Name,
                        contactViewModels.Email,
                        contactViewModels.Phone,
                        contactViewModels.Query,
                        generalModels.ip,
                        generalModels.country_code,
                        timestamp,
                        page);

                }
                if(contactViewModels == null)
                    await Task.Run(() => Insert_Data(generalModels, enquiryViewModels, null,page));
                else
                    await Task.Run(() => Insert_Data(generalModels, null, contactViewModels,page));
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(from);
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                var credential = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings.Get("User"),
                    Password = ConfigurationManager.AppSettings.Get("Password") 
                };
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Host = ConfigurationManager.AppSettings.Get("Host").ToString();
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"));
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "Send_mail");
            }
            return false;
        }

        [Obsolete]
        public void Insert_Data(GeneralModels generalModels, EnquiryViewModels enquiryViewModels, ContactViewModels contactViewModels, string page)
        {
            try
            {
                if(string.IsNullOrEmpty(generalModels.connection_string))
                    generalModels.connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                if (page.ToLower().Contains("enquiry"))
                {
                    SqlConnection conn = new SqlConnection(generalModels.connection_string);
                    SqlCommand cmd = new SqlCommand("Insert_Enquiry", conn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.Add("@name", enquiryViewModels.Name);
                    cmd.Parameters.Add("@email", enquiryViewModels.Email);
                    cmd.Parameters.Add("@contact", enquiryViewModels.Contact);
                    cmd.Parameters.Add("@product", enquiryViewModels.Product_Name);
                    cmd.Parameters.Add("@query", enquiryViewModels.Query);
                    cmd.Parameters.Add("@ip", generalModels.ip);
                    cmd.Parameters.Add("@country", generalModels.country_code);
                    cmd.Parameters.Add("@url", page);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(generalModels.connection_string);
                    SqlCommand cmd = new SqlCommand("Insert_Message", conn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.Add("@name", contactViewModels.Name);
                    cmd.Parameters.Add("@email", contactViewModels.Email);
                    cmd.Parameters.Add("@contact", contactViewModels.Phone);
                    cmd.Parameters.Add("@subject", Subject);
                    cmd.Parameters.Add("@query", contactViewModels.Query);
                    cmd.Parameters.Add("@ip", generalModels.ip);
                    cmd.Parameters.Add("@country", generalModels.country_code);
                    cmd.Parameters.Add("@url", page);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                Error_method(generalModels, "Insert_Data");
            }
        }

        public void Error_method(GeneralModels generalModels, [Optional] string url)
        {
            DateTime timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST);
            if (string.IsNullOrEmpty(generalModels.country_code))
                generalModels.country_code = "IN";
            if (string.IsNullOrEmpty(generalModels.connection_string))
                generalModels.connection_string = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(generalModels.connection_string);
            SqlCommand cmd = new SqlCommand("error", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ex", generalModels.ex);
            cmd.Parameters.AddWithValue("@time", timestamp);
            cmd.Parameters.AddWithValue("@IP", generalModels.ip);
            cmd.Parameters.AddWithValue("@code", generalModels.country_code);
            cmd.Parameters.AddWithValue("@url", url);
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}