<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BillApprovalNew.aspx.cs" Inherits="Bill_Approval_New_BillApprovalNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill Approval</title>

    <!-- Boottrap CSS -->
    <link href="../assests/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assests/css/bootstrap1.min.css" rel="stylesheet" />

    <!-- Bootstrap JS -->
    <script src="../assests/js/bootstrap.bundle.min.js"></script>
    <script src="../assests/js/bootstrap1.min.js"></script>

    <!-- Popper.js -->
    <script src="../assests/js/popper.min.js"></script>
    <script src="../assests/js/popper1.min.js"></script>

    <!-- jQuery -->
    <script src="../assests/js/jquery-3.6.0.min.js"></script>
    <script src="../assests/js/jquery.min.js"></script>
    <script src="../assests/js/jquery-3.3.1.slim.min.js"></script>

    <!-- Select2 library CSS and JS -->
    <link href="../assests/select2/select2.min.css" rel="stylesheet" />
    <script src="../assests/select2/select2.min.js"></script>

    <!-- Sweet Alert CSS and JS -->
    <link href="../assests/sweertalert/sweetalert2.min.css" rel="stylesheet" />
    <script src="../assests/sweertalert/sweetalert2.all.min.js"></script>

    <!-- Sumo Select CSS and JS -->
    <link href="../assests/sumoselect/sumoselect.min.css" rel="stylesheet" />
    <script src="../assests/sumoselect/jquery.sumoselect.min.js"></script>

    <script src="bill-approve-new.js"></script>
    <link rel="stylesheet" href="bill-approve-new.css" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>


        <!-- Heading -->
        <div class="col-md-11 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-body-secondary mb-3">
            <asp:Literal Text="Batch File Wise Bill Approvals" runat="server"></asp:Literal>
        </div>

        <!-- UI Starts -->
        <div class="card col-md-11 mx-auto mt-1 py-2 shadow-sm rounded-3">
            <div class="card-body">



                <!-- 1st Row -->
                <div class="row mb-3">

                    <!-- Batch Reference No -->
                    <div class="col-md-6 align-self-end">
                        <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                            <asp:Literal ID="Literal10" Text="Bill Number" runat="server">Batch Reference No (Auto Generated)</asp:Literal>
                        </div>
                        <asp:TextBox runat="server" ID="batchRefNo" type="text" ReadOnly="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    </div>

                    <!-- Batch Date -->
                    <div class="col-md-6 align-self-end">
                        <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                            <asp:Literal ID="Literal12" Text="" runat="server">Batch Date<em style="color: red">*</em></asp:Literal>
                            <div>
                                <asp:RequiredFieldValidator ID="rr1" ControlToValidate="BatchDate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <asp:TextBox runat="server" ID="BatchDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    </div>

                </div>
                <!-- 1st Row Ends-->

                <!-- 2nd Row -->
                <div class="row mb-2">

                    <!-- Unit / Office DD -->
                    <div class="col-md-6 align-self-end">
                        <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                            <asp:Literal ID="Literal1" Text="" runat="server">Unit / Office<em style="color: red">*</em></asp:Literal>
                            <div>
                                <asp:RequiredFieldValidator ID="rr2" ControlToValidate="ddUnitOffice" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="(Please select unit / office)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <asp:DropDownList ID="ddUnitOffice" OnSelectedIndexChanged="ddEventClick_ddUnitOffice" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                    </div>

                    <!-- Bill Check Box -->
                    <div class="col-md-6 align-self-end">
                        <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                            <asp:Literal ID="Literal2" Text="" runat="server">Select bills for batch<em style="color: red">*</em></asp:Literal>
                        </div>
                        <asp:ListBox runat="server" ID="ddBillNo" ClientIDMode="Static" SelectionMode="Multiple" OnSelectedIndexChanged="ddBillNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control rounded shadow border-0"></asp:ListBox>
                    </div>
                </div>
                <!-- 2nd Row Ends -->

                <!-- Searched Control Grid -->
                <div id="searchGridDiv" visible="false" runat="server" class="mt-5">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="gridSearch" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                        CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center grid-custom" OnPageIndexChanging="gridSearch_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
                        <HeaderStyle CssClass="" />
                        <Columns>

                            <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="id" runat="server" Value="id" />
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle CssClass="col-md-1" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="RefNo" HeaderText="Bill Ref No" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="Vendor" HeaderText="Vendor" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="VouNo" HeaderText="Bill No." ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="BillDate" HeaderText="Bill Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="unitName" HeaderText="Unit / Office" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="CardNo" HeaderText="Imprest Card No" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="NetAmount" HeaderText="Bill Amount" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />

                        </Columns>
                    </asp:GridView>

                    <!-- Total Bill -->
                    <div class="row px-0">

                        <div class="col-md-9"></div>

                        <div class="col-md-3 align-self-end text-end my-4">
                            <asp:Literal ID="Literal6" Text="" runat="server">Total Bill Amount</asp:Literal>
                            <div class="input-group">
                                <span class="input-group-text fs-5 fw-semibold">₹</span>
                                <asp:TextBox runat="server" ID="txtBillAmount" CssClass="form-control fw-lighter border border-2" ReadOnly="true" placeholder="Total Bill Amount...."></asp:TextBox>
                            </div>
                        </div>

                    </div>


                    <!-- Submit Button -->
                    <div class="">
                        <div class="row mt-5 mb-2">
                            <div class="col-md-6 text-start">
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnEventClick_btnBack" CssClass="btn btn-custom text-white shadow mb-5" />
                            </div>
                            <div class="col-md-6 text-end">
                                <asp:Button ID="btnSubmit" Enabled="true" runat="server" Text="Submit" OnClick="btnEventClick_btnSubmit" ValidationGroup="finalSubmit" CssClass="btn btn-custom text-white shadow mb-5" />
                            </div>
                        </div>
                    </div>

                </div>
                <!-- Searched Control Grid Ends -->




            </div>
        </div>
        <!-- UI Ends -->


    </form>
</body>
</html>
