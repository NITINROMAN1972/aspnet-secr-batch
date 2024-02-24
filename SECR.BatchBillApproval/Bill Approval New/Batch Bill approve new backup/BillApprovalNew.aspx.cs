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


    //=========================={ Paging }==========================
    protected void gridSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //binding GridView to PageIndex object
        gridSearch.PageIndex = e.NewPageIndex;

        DataTable pagination = (DataTable)Session["PaginationDataSource"];

        gridSearch.DataSource = pagination;
        gridSearch.DataBind();
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

    private DataTable GetUnitsDT(string unitOrOfficeCode)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Units751 where unitCode = @unitCode";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@unitCode", unitOrOfficeCode);
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

    private void BillNo_Dropdown_Bind(string unitOrOfficeCode)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Bills1751 where Unit = @Unit order by RefNo desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Unit", unitOrOfficeCode);

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
            BillNo_Dropdown_Bind(unitOrOffice);

            searchGridDiv.Visible = false;
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





    //=========================={ Drop Down Event }==========================
    protected void ddBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        //Response.Write(selectedBillRefNo.ToString());

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                //string sql = "SELECT * FROM Bills1751 WHERE RefNo IN (" + string.Join(",", selectedBillRefNo.Select(bill => "'" + bill + "'")) + ")";
                string sql = $@"SELECT * FROM Bills1751 as bill INNER JOIN Units751 as unit ON bill.Unit = unit.unitCode WHERE RefNo IN ({string.Join(",", selectedBillRefNo.Select(bill => $"'{bill}'"))})";

                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.Parameters.AddWithValue("@RefNo", billNo);
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                con.Close();

                if (dt.Rows.Count > 0)
                {
                    searchGridDiv.Visible = true;

                    gridSearch.DataSource = dt;
                    gridSearch.DataBind();

                    Session["PaginationDataSource"] = dt;


                    // sum of total bill amount
                    double? totalBillAmount = dt.AsEnumerable().Sum(row => row["NetAmount"] is DBNull ? (double?)null : Convert.ToDouble(row["NetAmount"])) ?? 0.0;
                    txtBillAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";
                }
                else getSweetAlertErrorMandatory("Fail", $"DT has no records");
            }
        }
        catch (Exception ex)
        {
            getSweetAlertErrorMandatory("Execution Failed", $"Got Into Exception");
        }
    }



    //=========================={ Submit Button Click Event }==========================
    protected void btnEventClick_btnBack(object sender, EventArgs e)
    {
        Response.Redirect("BillApproval.aspx");
    }

    protected void btnEventClick_btnSubmit(object sender, EventArgs e)
    {
        // de-selecting initial heading list item
        ddBillNo.Items[0].Selected = false;

        // checking for unit sansction amount

        string unitCode = ddUnitOffice.SelectedValue; // unit code

        DataTable unitDT = GetUnitsDT(unitCode);
        string unitRefID = unitDT.Rows[0]["RefId"].ToString();

        bool sanctionedAmountAove50Per = CheckSanctionAmountOfUnitOffice(unitRefID);


        //// creating list for storing items
        //List<string> selectedBillRefNo = new List<string>();

        //foreach (ListItem li in ddBillNo.Items)
        //{
        //    if (li.Selected == true)
        //    {
        //        selectedBillRefNo.Add(li.Value);
        //    }
        //}

        //InsertIntoBatchBill(selectedBillRefNo);

        //// sweet alert - success redirect
        //string title = "Saved!";
        //string message = "bills added to batch successfully";
        //string href = "BillApproval.aspx";
        //getSweetAlertSuccessRedirectMandatory(title, message, href);
    }

    private bool CheckSanctionAmountOfUnitOffice(string unitRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select top 1 * from Topupcard751 where tpImprestNo = @tpImprestNo order by tpDate desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tpImprestNo", unitRefID);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            alert($"dt : {dt.Rows[0]["tpUnit"]}");

            if (dt.Rows.Count > 0) return true;
            else return false;
        }
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