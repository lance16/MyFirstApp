using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

namespace RevalsysSystemTask
{
    public partial class UI_Layer : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        //SqlDataAdapter da = null;
        string str = string.Empty;
        string strCon = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //rdbMale.Checked = true;
            //ddlDay.Items.Insert(0, new ListItem("Select", "0"));
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            btnUpdate.Visible = false;
            if (!Page.IsPostBack)
            {
                BindGrid();
            }


            if (!Page.IsPostBack)
            {
               
                //Fill Years
                for (int i = 1977; i <=1999; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                //ddlYear.Items.Insert(0, "Select");
                 //ddlYear.Items.Add(new ListItem("Select Year", "0"));
               // ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected
                
                //Fill Months
                //ddlMonth.Items.Add(new ListItem("Select Month", "0"));
                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(i.ToString());
                }
                //ddlMonth.Items.Insert(0, "Select");
                ddlMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected

                //Fill days
                FillDays();
            }
        }
        public void FillDays()
        {
            ddlDay.Items.Clear();
            
            //getting numbner of days in selected month & year
            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

            //Fill days
            for (int i = 1; i <=noofdays; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
            ddlDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true; //Set current date as selected
            //ddlDay.Items.Insert(0, "Select");
        }

        void BindGrid()
        {
            
            strCon = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (con = new SqlConnection(strCon))
            {
                //con.Open();
                str = "Select * from tblRamakantEmployee";
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    gv1.DataSource = ds;
                    gv1.DataBind();
                }
                else
                {
                    gv1.Caption = "<b style='color:red'>No Employee Data Available</b>";
                }


            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txtName.Text != "" && txtEmail.Text != "" && txtMobile.Text != "" && txtSal.Text != "")
                {
                    string gender = string.Empty;
                    if (rdbMale.Checked)
                    {
                        gender = rdbMale.Text;
                    }
                    else if (rdbFemale.Checked)
                    {
                        gender = rdbFemale.Text;
                    }
                    DateTime d = DateTime.Now;
                     
                    string strdate = ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
                    DateTime dt = Convert.ToDateTime(strdate);
                    
                    string cdt = dt.ToString("d");
                    int days = (d.Subtract(dt).Days)/365;
                    var dayss = (d - dt).TotalDays;
                    var totalyears = Math.Truncate(dayss / 365);
                    strCon = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    con = new SqlConnection(strCon);
                    con.Open();
                    cmd = new SqlCommand("Sp_AddEmployeeDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramName = new SqlParameter();
                    paramName.ParameterName = "@EmpName";
                    paramName.Value = txtName.Text;
                    cmd.Parameters.Add(paramName);
                    SqlParameter paramDesignation = new SqlParameter();
                    paramDesignation.ParameterName = "@Designation";
                    paramDesignation.Value = txtDes.Text;
                    cmd.Parameters.Add(paramDesignation);
                    SqlParameter paramSalary = new SqlParameter();
                    paramSalary.ParameterName = "@Salary";
                    paramSalary.Value = txtSal.Text;
                    cmd.Parameters.Add(paramSalary);
                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = txtEmail.Text;
                    cmd.Parameters.Add(paramEmail);
                    SqlParameter paramMobile = new SqlParameter();
                    paramMobile.ParameterName = "@Mobile";
                    paramMobile.Value = txtMobile.Text;
                    cmd.Parameters.Add(paramMobile);
                    SqlParameter paramQualification = new SqlParameter();
                    paramQualification.ParameterName = "@Qualification";
                    paramQualification.Value = txtQualify.Text;
                    cmd.Parameters.Add(paramQualification);
                    SqlParameter paramManager = new SqlParameter();
                    paramManager.ParameterName = "@Manager";
                    paramManager.Value = txtManager.Text;
                    cmd.Parameters.Add(paramManager);
                    SqlParameter paramGender = new SqlParameter();
                    paramGender.ParameterName = "@Gender";
                    paramGender.Value = gender;
                    cmd.Parameters.Add(paramGender);
                    SqlParameter paramAge = new SqlParameter();
                    paramAge.ParameterName = "@Age";
                    paramAge.Value = totalyears;
                    cmd.Parameters.Add(paramAge);
                    SqlParameter paramDOB = new SqlParameter();
                    paramDOB.ParameterName = "@DOB";
                    paramDOB.Value = cdt;
                    cmd.Parameters.Add(paramDOB);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Registered Successfully";
                    txtName.Text = txtDes.Text = txtSal.Text = txtEmail.Text = txtMobile.Text = txtManager.Text = txtQualify.Text = "";
                    rdbMale.Checked = false;
                    rdbFemale.Checked = false;
                    BindGrid();

                }
               
                
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = txtDes.Text = txtSal.Text = txtEmail.Text = txtMobile.Text = txtManager.Text = txtQualify.Text = "";
            lblmsg.Text = "";
            btnInsert.Visible = true;
            rdbMale.Checked = false;
            rdbFemale.Checked = false;

            //ddlDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;
            //ddlMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;
            //ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
            
        }

        protected void gv1_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow gv = gv1.SelectedRow;
            
            if ((gv.Cells[7].Text.Equals("&nbsp;") || gv.Cells[7].Text.Equals("")) && (gv.Cells[8].Text.Equals("&nbsp;") || gv.Cells[8].Text.Equals("")))
            {
                if (gv.Cells[3].Text.Equals("&nbsp;") || gv.Cells[3].Text.Equals(""))
                {

                    txtEmpId.Text = gv.Cells[1].Text;
                    txtName.Text = gv.Cells[2].Text;
                    txtDes.Text = string.Empty;
                    txtSal.Text = gv.Cells[4].Text;
                    txtEmail.Text = gv.Cells[5].Text;
                    txtMobile.Text = gv.Cells[6].Text;
                    txtQualify.Text = string.Empty;
                    txtManager.Text = string.Empty;
                    if (gv.Cells[9].Text == "Male")
                    {
                        rdbFemale.Checked = false;
                        rdbMale.Checked = true;

                    }
                    else if (gv.Cells[9].Text == "Female")
                    {
                        rdbMale.Checked = false;
                        rdbFemale.Checked = true;

                    }
                    ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                    ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                    ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
                }
                else
                {
                    txtEmpId.Text = gv.Cells[1].Text;
                    txtName.Text = gv.Cells[2].Text;
                    txtDes.Text = gv.Cells[3].Text;
                    txtSal.Text = gv.Cells[4].Text;
                    txtEmail.Text = gv.Cells[5].Text;
                    txtMobile.Text = gv.Cells[6].Text;
                    txtQualify.Text = string.Empty;
                    txtManager.Text = string.Empty;
                    if (gv.Cells[9].Text == "Male")
                    {
                        rdbFemale.Checked = false;
                        rdbMale.Checked = true;

                    }
                    else if (gv.Cells[9].Text == "Female")
                    {
                        rdbMale.Checked = false;
                        rdbFemale.Checked = true;

                    }
                    ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                    ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                    ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
                }

            }
           else if ((gv.Cells[3].Text.Equals("&nbsp;")||gv.Cells[3].Text.Equals(""))&&(gv.Cells[7].Text.Equals("&nbsp;")||gv.Cells[7].Text.Equals("")))
                {
                txtEmpId.Text = gv.Cells[1].Text;
                txtName.Text = gv.Cells[2].Text;
                txtDes.Text = string.Empty;
                txtSal.Text = gv.Cells[4].Text;
                txtEmail.Text = gv.Cells[5].Text;
                txtMobile.Text = gv.Cells[6].Text;
                txtQualify.Text = string.Empty;
                txtManager.Text = gv.Cells[8].Text;
                if (gv.Cells[9].Text == "Male")
                {
                    rdbFemale.Checked = false;
                    rdbMale.Checked = true;

                }
                else if (gv.Cells[9].Text == "Female")
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;

                }
                ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
            }
            else if ((gv.Cells[3].Text.Equals("&nbsp;") || gv.Cells[3].Text.Equals("")) && (gv.Cells[8].Text.Equals("&nbsp;") || gv.Cells[8].Text.Equals("")))
                {
                txtEmpId.Text = gv.Cells[1].Text;
                txtName.Text = gv.Cells[2].Text;
                txtDes.Text = string.Empty;
                txtSal.Text = gv.Cells[4].Text;
                txtEmail.Text = gv.Cells[5].Text;
                txtMobile.Text = gv.Cells[6].Text;
                txtQualify.Text = gv.Cells[7].Text;
                txtManager.Text = string.Empty;
                if (gv.Cells[9].Text == "Male")
                {
                    rdbFemale.Checked = false;
                    rdbMale.Checked = true;

                }
                else if (gv.Cells[9].Text == "Female")
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;

                }
                ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
            }
            else if(gv.Cells[7].Text.Equals("&nbsp;")||gv.Cells[7].Text.Equals(""))
            {
                txtEmpId.Text = gv.Cells[1].Text;
                txtName.Text = gv.Cells[2].Text;
                txtDes.Text = gv.Cells[3].Text;
                txtSal.Text = gv.Cells[4].Text;
                txtEmail.Text = gv.Cells[5].Text;
                txtMobile.Text = gv.Cells[6].Text;
                txtQualify.Text = string.Empty;
                txtManager.Text = gv.Cells[8].Text;
                if (gv.Cells[9].Text == "Male")
                {
                    rdbFemale.Checked = false;
                    rdbMale.Checked = true;

                }
                else if (gv.Cells[9].Text == "Female")
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;

                }
                ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
            }
            else if (gv.Cells[8].Text.Equals("&nbsp;") || gv.Cells[8].Text.Equals(""))
            {
                txtEmpId.Text = gv.Cells[1].Text;
                txtName.Text = gv.Cells[2].Text;
                txtDes.Text = gv.Cells[3].Text;
                txtSal.Text = gv.Cells[4].Text;
                txtEmail.Text = gv.Cells[5].Text;
                txtMobile.Text = gv.Cells[6].Text;
                txtQualify.Text = gv.Cells[7].Text;
                txtManager.Text = string.Empty;
                if (gv.Cells[9].Text == "Male")
                {
                    rdbFemale.Checked = false;
                    rdbMale.Checked = true;

                }
                else if (gv.Cells[9].Text == "Female")
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;

                }
                ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
            }
            else if(gv.Cells[3].Text.Equals("&nbsp;")||gv.Cells[3].Text.Equals(""))
            {
                txtEmpId.Text = gv.Cells[1].Text;
                txtName.Text = gv.Cells[2].Text;
                txtDes.Text = string.Empty;
                txtSal.Text = gv.Cells[4].Text;
                txtEmail.Text = gv.Cells[5].Text;
                txtMobile.Text = gv.Cells[6].Text;
                txtQualify.Text = gv.Cells[7].Text;
                txtManager.Text = gv.Cells[8].Text;
                if (gv.Cells[9].Text == "Male")
                {
                    rdbFemale.Checked = false;
                    rdbMale.Checked = true;

                }
                else if (gv.Cells[9].Text == "Female")
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;

                }
                ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
            }
            else
            {
                txtEmpId.Text = gv.Cells[1].Text;
                txtName.Text = gv.Cells[2].Text;
                txtDes.Text = gv.Cells[3].Text;
                txtSal.Text = gv.Cells[4].Text;
                txtEmail.Text = gv.Cells[5].Text;
                txtMobile.Text = gv.Cells[6].Text;
                txtQualify.Text = gv.Cells[7].Text;
                txtManager.Text = gv.Cells[8].Text;
                if (gv.Cells[9].Text=="Male")
                {
                    rdbFemale.Checked = false;
                    rdbMale.Checked = true;
                    
                }
                else if(gv.Cells[9].Text=="Female")
                {
                    rdbMale.Checked = false;
                    rdbFemale.Checked = true;
                    
                }
                ddlDay.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Day.ToString();
                ddlMonth.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Month.ToString();
                ddlYear.SelectedValue = Convert.ToDateTime(gv.Cells[11].Text).Year.ToString();
                //int Empid = Convert.ToInt32(gv1.DataKeys[e.RowIndex].Value.ToString());
                //strCon = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                //con = new SqlConnection(strCon);
                ////cmd= new SqlCommand("Select DOB from tblRamakantEmployee", con);
                //da = new SqlDataAdapter("Select DOB from tblRamakantEmployee Where EmpId=" + Empid, con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "tblRamakantEmployee");
                //ddlDay.SelectedValue = ds.ToString();
            }
            



            btnInsert.Visible = false;
            btnUpdate.Visible = true;
            lblmsg.Text = "";
        }
        
       
        void Update()
        {
            string gender = string.Empty;
            if (rdbMale.Checked)
            {
                gender = rdbMale.Text;
            }
            else if (rdbFemale.Checked)
            {
                gender = rdbFemale.Text;
            }
            DateTime d = DateTime.Now;

            string strdate = ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
            DateTime dt = Convert.ToDateTime(strdate);
            string cdt = dt.ToString();
            int days = (d.Subtract(dt).Days) / 365;
            var dayss = (d - dt).TotalDays;
            var totalyears = Math.Truncate(dayss / 365);
            strCon = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            con = new SqlConnection(strCon);
            con.Open();
            cmd = new SqlCommand("Sp_UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter paramEmpId = new SqlParameter("@EmpId", txtEmpId.Text);
            cmd.Parameters.Add(paramEmpId);
            SqlParameter paramName = new SqlParameter("@EmpName", txtName.Text);
            cmd.Parameters.Add( paramName);
            SqlParameter paramDes = new SqlParameter("@Designation", txtDes.Text);
            cmd.Parameters.Add(paramDes);
            SqlParameter paramSalary = new SqlParameter("@Salary", txtSal.Text);
            cmd.Parameters.Add( paramSalary);
            SqlParameter paramEmail = new SqlParameter("@Email", txtEmail.Text);
            cmd.Parameters.Add( paramEmail);
            SqlParameter paramMobile = new SqlParameter("@Mobile", txtMobile.Text);
            cmd.Parameters.Add( paramMobile);
            SqlParameter paramQualification = new SqlParameter("@Qualification", txtQualify.Text);
            cmd.Parameters.Add( paramQualification);
            SqlParameter paramManager = new SqlParameter("@Manager", txtManager.Text);
            cmd.Parameters.Add( paramManager);
            SqlParameter paramGender = new SqlParameter("@Gender", gender);
            cmd.Parameters.Add(paramGender);
            SqlParameter paramAge = new SqlParameter("@Age", days);
            cmd.Parameters.Add(paramAge);
            SqlParameter paramDOB = new SqlParameter("@DOB", cdt);
            cmd.Parameters.Add(paramDOB);
            cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
            txtName.Text = txtDes.Text = txtSal.Text = txtEmail.Text = txtMobile.Text = txtManager.Text = txtQualify.Text = "";
            btnInsert.Visible = true;
            lblmsg.Text = "<b style='color:green'>Updated Successfully</b>";
        }

        protected void gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Empid = Convert.ToInt32(gv1.DataKeys[e.RowIndex].Value.ToString());
            
            strCon = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            con = new SqlConnection(strCon);
            
            cmd = new SqlCommand("Sp_DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", Empid);
            con.Open();
            cmd.ExecuteNonQuery();
            
            con.Close();
            BindGrid();
            btnInsert.Visible = true;
            txtName.Text = txtDes.Text = txtSal.Text = txtEmail.Text = txtMobile.Text = txtManager.Text = txtQualify.Text = "";
            lblmsg.Text = "<b style='color:green'>Deleted Successfully</b>";
        }

        

        

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDays();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDays();
        }

        
        //void SelectedDate()
        //{
        //    string strdate = ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
        //    DateTime dt = Convert.ToDateTime(strdate);
        //    string selecteddate = dt.ToString();
        //    //string Age = string.Empty;
        //    //string Age = dt - DateTime.Now.ToString();
        //}
    }
}