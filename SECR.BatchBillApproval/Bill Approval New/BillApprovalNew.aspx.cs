using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Net.Http;

public partial class Bill_Approval_New_BillApprovalNew : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Project Code : 751

        if (!IsPostBack)
        {
            BatchDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            UnitOffice_Dropdown_Bind();

            batchRefNo.Text = GetBatchBillRefNo().ToString();
        }
    }

    private void alert(string mssg)
    {
        // alert pop-up with only message
        string message = mssg;
        string script = $"alert('{message}');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
    }

    private int GetBatchBillRefNo()
    {
        string nextRefID = "10001";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 10000) + 1 AS NextRefID FROM BatchExpns751";
            SqlCommand cmd = new SqlCommand(sql, con);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value) { nextRefID = result.ToString(); }

            con.Close();
            return Convert.ToInt32(nextRefID);
        }
    }




    //=========================={ Fetching Datatable }==========================
    private DataTable GetBillNumbersDT()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Bills1751";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@icNumber", imprestCardNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }





    //=========================={ Sweet Alert JS }==========================

    // sweet alert - success only
    private void getSweetAlertSuccessOnly()
    {
        string title = "Saved!";
        string message = "Record saved successfully!";
        string icon = "success";
        string confirmButtonText = "OK";

        string sweetAlertScript =
            $@"<script>
                Swal.fire({{ 
                    title: '{title}', 
                    text: '{message}', 
                    icon: '{icon}', 
                    confirmButtonText: '{confirmButtonText}' 
                }});
            </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - success redirect
    private void getSweetAlertSuccessRedirect(string redirectUrl)
    {
        string title = "Saved!";
        string message = "Record saved successfully!";
        string icon = "success";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false";

        string sweetAlertScript =
            $@"<script>
                Swal.fire({{ 
                    title: '{title}', 
                    text: '{message}', 
                    icon: '{icon}', 
                    confirmButtonText: '{confirmButtonText}',
                    allowOutsideClick: {allowOutsideClick}
                }}).then((result) => {{
                    if (result.isConfirmed) {{
                        window.location.href = '{redirectUrl}';
                    }}
                }});
            </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - success redirect block
    private void getSweetAlertSuccessRedirectMandatory(string titles, string mssg, string redirectUrl)
    {
        string title = titles;
        string message = mssg;
        string icon = "success";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }}).then((result) => {{
                if (result.isConfirmed) {{
                    window.location.href = '{redirectUrl}';
                }}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - error only block
    private void getSweetAlertErrorMandatory(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "error";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }




    //=========================={ Dropdown Bind }==========================

    private void UnitOffice_Dropdown_Bind()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Units751";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddUnitOffice.DataSource = dt;
            ddUnitOffice.DataTextField = "unitName";
            ddUnitOffice.DataValueField = "unitCode";
            ddUnitOffice.DataBind();
            ddUnitOffice.Items.Insert(0, new ListItem("------Select Unit / Office------", "0"));
        }
    }

    private void BillNo_Dropdown_Bind()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Bills1751";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddBillNo.DataSource = dt;
            ddBillNo.DataTextField = "VouNo";
            ddBillNo.DataValueField = "RefNo";
            ddBillNo.DataBind();
            ddBillNo.Items.Insert(0, new ListItem("Select Bill No", "0"));
            
            ListItem selectValuesItem = ddBillNo.Items.FindByValue("0");
            if (selectValuesItem != null)
            {
                selectValuesItem.Selected = true;
            }
        }
    }



    //=========================={ Drop Down Event }==========================
    protected void ddEventClick_ddUnitOffice(object sender, EventArgs e)
    {
        string unitOrOffice = ddUnitOffice.SelectedValue; // unit code

        if (ddUnitOffice.SelectedValue != "0")
        {
            // binding approval bill no checkboxes
            BillNo_Dropdown_Bind();
        }
        else
        {
            // clearing all selections and removing all items from the ListBox
            ddBillNo.ClearSelection();
            ddBillNo.Items.Clear();
            ddBillNo.Items.Insert(0, new ListItem("Select Bill No", "0"));

            if(ddBillNo.Items.Count == 0) ddBillNo.Items[0].Selected = true;
        }
    }



    //=========================={ Submit Button Click Event }==========================
    protected void btnEventClick_btnBack(object sender, EventArgs e)
    {
        Response.Redirect("BillApproval.aspx");
    }

    protected void btnEventClick_btnSubmit(object sender, EventArgs e)
    {
        if (ddBillNo.SelectedValue == "0")
        {
            // Clear all selections
            //ddBillNo.ClearSelection();
            //return;
        }

        // de-selecting initial heading list item
        ddBillNo.Items[0].Selected = false;

        // creating list for storing items
        List<string> selectedBillRefNo = new List<string>();

        foreach (ListItem li in ddBillNo.Items)
        {
            if (li.Selected == true)
            {
                selectedBillRefNo.Add(li.Value);
            }
        }

        InsertIntoBatchBill(selectedBillRefNo);

        // sweet alert - success redirect
        string title = "Saved!";
        string message = "bills added to batch successfully";
        string href = "BillApproval.aspx";
        getSweetAlertSuccessRedirectMandatory(title, message, href);
    }

    private void InsertIntoBatchBill(List<string> selectedBillRefNo)
    {
        string batchBillRefNo = batchRefNo.Text.ToString();
        DateTime batchDate = DateTime.Parse(BatchDate.Text);
        string unitOrOffice = ddUnitOffice.SelectedValue;

        string joinedBillNos = string.Join(",", selectedBillRefNo);

        //alert($"joinedBillNos : {joinedBillNos}");

        // inserting only checked items
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"INSERT INTO BatchExpns751 
                            (RefNo, BchDate, Unit, BchChilds) 
                            VALUES 
                            (@RefNo, @BchDate, @Unit, @BchChilds)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", batchBillRefNo);
            cmd.Parameters.AddWithValue("@BchDate", batchDate);
            cmd.Parameters.AddWithValue("@Unit", unitOrOffice);
            cmd.Parameters.AddWithValue("@BchChilds", joinedBillNos);
            cmd.ExecuteNonQuery();

            //SqlDataAdapter ad = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //ad.Fill(dt);

            con.Close();
        }
    }
}