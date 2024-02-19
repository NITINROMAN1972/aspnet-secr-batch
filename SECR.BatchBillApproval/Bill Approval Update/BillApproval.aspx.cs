using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;

public partial class Bill_Approval_Update_BillApproval : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Project Code : 751

        if (!IsPostBack)
        {
            BatchNo_Search_DD();
            Unit_Search_DD();
        }
    }


    //=========================={ Paging & Alert }==========================

    protected void gridSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //binding GridView to PageIndex object
        gridSearch.PageIndex = e.NewPageIndex;

        DataTable pagination = (DataTable)Session["PaginationDataSource"];

        gridSearch.DataSource = pagination;
        gridSearch.DataBind();
    }

    private void alert(string mssg)
    {
        // alert pop-up with only message
        string message = mssg;
        string script = $"alert('{message}');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
    }



    //=========================={ Binding Search Dropdowns }==========================

    private void BatchNo_Search_DD()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from BatchExpns751 order by RefNo desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddScBatchNo.DataSource = dt;
            ddScBatchNo.DataTextField = "RefNo";
            ddScBatchNo.DataValueField = "RefNo";
            ddScBatchNo.DataBind();
            ddScBatchNo.Items.Insert(0, new ListItem("------ Select Batch No ------", "0"));
        }
    }

    private void Unit_Search_DD()
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

            ddScUnit.DataSource = dt;
            ddScUnit.DataTextField = "unitName";
            ddScUnit.DataValueField = "unitName";
            ddScUnit.DataBind();
            ddScUnit.Items.Insert(0, new ListItem("------ Select Unit / Office ------", "0"));
        }
    }

    

    //=========================={ Fetch Datatable }==========================
    private DataTable GetBatchNumberDT(string batchNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from BatchExpns751 where RefNo = @RefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", batchNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable GetUnitDT(string unitRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Units751 where RefId = @RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", unitRefID);
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

    // sweet alert - question only
    private void getSweetAlertQuestionOnly(string titl, string mssg)
    {
        string title = titl;
        string message = mssg;
        string icon = "question";
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





    //=========================={ Search Dropdown }==========================

    protected void btnEventClick_btnNewBatch(object sender, EventArgs e)
    {
        Response.Redirect("BillApprovalNew.aspx");
    }

    protected void btnEventClick_btnSearch(object sender, EventArgs e)
    {
        BindGridView();
    }

    private void BindGridView()
    {
        searchGridDiv.Visible = true;

        // dropdown values
        string batchNo = ddScBatchNo.SelectedValue; // ref no
        string unitRefID = ddScUnit.SelectedValue; // ref id

        DateTime fromDate;
        DateTime toDate;

        if (!DateTime.TryParse(ScFromDate.Text, out fromDate)) { fromDate = SqlDateTime.MinValue.Value; }
        if (!DateTime.TryParse(ScToDate.Text, out toDate)) { toDate = SqlDateTime.MaxValue.Value; }

        // DTs
        DataTable batchDT = GetBatchNumberDT(batchNo);
        DataTable unitDT = GetUnitDT(unitRefID);

        // dt values
        string batchNumber = (batchDT.Rows.Count > 0) ? batchDT.Rows[0]["RefNo"].ToString() : string.Empty;
        string unitName = (unitDT.Rows.Count > 0) ? unitDT.Rows[0]["RefId"].ToString() : string.Empty;

        DataTable searchResultDT = SearchRecords(batchNumber, fromDate, toDate, unitName);

        // binding the search grid
        gridSearch.DataSource = searchResultDT;
        gridSearch.DataBind();

        Session["PaginationDataSource"] = searchResultDT;
    }

    public DataTable SearchRecords(string batchNumber, DateTime fromDate, DateTime toDate, string unitName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = $@"SELECT batch.*, unit.unitName 
                            FROM BatchExpns751 as batch inner join Units751 as unit 
                            on batch.Unit = unit.unitCode 
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(batchNumber))
            {
                sql += " AND RefNo = @RefNo";
            }

            if (fromDate != null)
            {
                sql += " AND BchDate >= @FromDate";
            }

            if (toDate != null)
            {
                sql += " AND BchDate <= @ToDate";
            }

            if (!string.IsNullOrEmpty(unitName))
            {
                sql += " AND Unit = @Unit";
            }

            sql += " ORDER BY RefNo DESC";






            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (!string.IsNullOrEmpty(batchNumber))
                {
                    command.Parameters.AddWithValue("@RefNo", batchNumber);
                }

                if (fromDate != null)
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                }

                if (toDate != null)
                {
                    command.Parameters.AddWithValue("@ToDate", toDate);
                }

                if (!string.IsNullOrEmpty(unitName))
                {
                    command.Parameters.AddWithValue("@Unit", unitName);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataColumn billCountColumn = new DataColumn("BillCount", typeof(int));
                    dt.Columns.Add(billCountColumn);

                    foreach (DataRow row in dt.Rows)
                    {
                        string bchChilds = row["BchChilds"].ToString();
                        int billCount = bchChilds.Split(',').Length;
                        row["BillCount"] = billCount;
                    }

                    //dt.Columns.Remove("BchChilds");



                    return dt;
                }
            }
        }
    }






    //=========================={ Update - Fill Searched Details }==========================
    protected void gridSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkView")
        {
            int rowId = Convert.ToInt32(e.CommandArgument);
            Session["RowID"] = rowId;

            searchGridDiv.Visible = false;
            divTopSearch.Visible = false;
            UpdateDiv.Visible = true;

            FillBatchDetails(rowId.ToString());

            FillBillNumbers(rowId.ToString());
        }
    }

    private void FillBatchDetails(string batchReferenceNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select batch.*, unit.unitName from BatchExpns751 as batch inner join Units751 as unit on batch.Unit = unit.unitCode where RefNo = @RefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", batchReferenceNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            Session["BatchDT"] = dt;

            // updating batch number
            batchRefNo.Text = batchReferenceNo;

            // updating batch date
            DateTime batchDate = DateTime.Parse(dt.Rows[0]["BchDate"].ToString());
            BatchDate.Text = batchDate.ToString("yyyy-MM-dd");

           // updating unit or office dd
            ddUnitOffice.DataSource = dt;
            ddUnitOffice.DataTextField = "unitName";
            ddUnitOffice.DataValueField = "Unit";
            ddUnitOffice.DataBind();
            ddUnitOffice.Items.Insert(0, new ListItem("------ Select Unit / Office ------", "0"));

            if (ddUnitOffice.SelectedIndex < 2) ddUnitOffice.SelectedIndex = 1;
        }
    }

    private void FillBillNumbers(string batchReferenceNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // fetching all bills
            con.Open();
            string sql = "select * from Bills1751";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddBillNo.DataSource = dt;
            //ddBillNo.DataTextField = "VouNo";
            ddBillNo.DataValueField = "RefNo";
            ddBillNo.DataBind();
            ddBillNo.Items.Insert(0, new ListItem("Select Bill No", "0"));

            // showing batch number and 
            foreach (ListItem item in ddBillNo.Items)
            {
                DataRow[] rows = dt.Select("RefNo = '" + item.Value + "'");

                if (rows.Length > 0)
                {
                    string text = rows[0]["VouNo"].ToString() + " - " + rows[0]["RefNo"].ToString();
                    item.Text = text;
                }
            }


            // fetching saved bills
            con.Open();
            string sql1 = "select * from BatchExpns751 where RefNo = @RefNo";
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd1.Parameters.AddWithValue("@RefNo", batchReferenceNo);
            cmd1.ExecuteNonQuery();

            SqlDataAdapter ad1 = new SqlDataAdapter(cmd1);
            DataTable savedBillNoDT = new DataTable();
            ad1.Fill(savedBillNoDT);
            con.Close();


            // separating the comma-separated values
            List<string> bill_seperate_numbers = new List<string>();

            if (savedBillNoDT.Rows.Count > 0 && savedBillNoDT.Rows[0]["BchChilds"] != DBNull.Value)
            {
                string joinedBillNos = savedBillNoDT.Rows[0]["BchChilds"].ToString();
                bill_seperate_numbers = joinedBillNos.Split(',').ToList();
            }

            // making checked items only those are saved
            foreach (ListItem item in ddBillNo.Items)
            {
                if (bill_seperate_numbers.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
        }
    }






    //=========================={ Submit Button Click Event }==========================
    protected void btnEventClick_btnBack(object sender, EventArgs e)
    {
        Response.Redirect("BillApproval.aspx");
    }

    protected void btnEventClick_btnSubmit(object sender, EventArgs e)
    {
        // updating bill nos
        UpdateBillNumbers();
       
        getSweetAlertSuccessRedirectMandatory("Updated", "batch updated successfully", "BillApproval.aspx");
    }

    private void UpdateBillNumbers()
    {
        DataTable batchDT = (DataTable)Session["BatchDT"];

        string batchBillRefNo = batchDT.Rows[0]["RefNo"].ToString();


        // creating list of checked items
        List<string> selectedBillRefNo = new List<string>();

        foreach (ListItem li in ddBillNo.Items)
        {
            if (li.Selected == true)
            {
                selectedBillRefNo.Add(li.Value);
            }
        }

        // comma seperated bill nos
        string joinedBillNos = string.Join(",", selectedBillRefNo);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"UPDATE BatchExpns751 SET BchChilds = @BchChilds WHERE RefNo = @RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@BchChilds", joinedBillNos);
            cmd.Parameters.AddWithValue("@RefNo", batchBillRefNo);
            cmd.ExecuteNonQuery();

            //SqlDataAdapter ad = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //ad.Fill(dt);

            con.Close();
        }
    }

}