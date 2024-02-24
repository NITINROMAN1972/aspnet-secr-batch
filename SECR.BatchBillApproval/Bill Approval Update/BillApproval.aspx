<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BillApproval.aspx.cs" Inherits="Bill_Approval_Update_BillApproval" %>

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

    <script src="bill-approve.js"></script>
    <link rel="stylesheet" href="bill-approve-new.css" />


</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <!-- Update Control Div Starts -->
        <div id="divTopSearch" runat="server" visible="true">
            <div class="col-md-11 mx-auto">


                <!-- Control starts -->
                <div class="">

                    <!-- Heading -->
                    <div class="justify-content-end d-flex mb-0 mt-4 px-0 text-body-secondary">
                        <div class="col-md-6 px-0">
                            <div class="fw-semibold fs-3">
                                <asp:Literal ID="Literal14" Text="Batch File For Bill Approval" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="col-md-6 px-0 text-end">
                            <div class="fw-semibold fs-5">
                                <asp:Button ID="btnNewBatch" runat="server" Text="New Batch +" OnClick="btnEventClick_btnNewBatch" CssClass="btn btn-custom text-white shadow" />
                            </div>
                        </div>
                    </div>

                    <!-- Dropdown Start -->
                    <div id="divSearchEmb" runat="server" visible="true">

                        <div class="card mt-2 shadow-sm">
                            <div class="card-body">

                                <!-- 1st row -->
                                <div class="row mb-2">
                                    <div class="col-md-6 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal15" Text="" runat="server">Batch Number</asp:Literal>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddScBatchNo" runat="server" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-6 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal13" Text="" runat="server">Unit / Office</asp:Literal>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddScUnit" runat="server" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- 1st row ends -->

                                <!-- 2nd row -->
                                <div class="row mb-2">
                                    <div class="col-md-6 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal22" Text="" runat="server">From Date</asp:Literal>
                                        </div>
                                        <asp:TextBox runat="server" ID="ScFromDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal23" Text="" runat="server">To Date</asp:Literal>
                                        </div>
                                        <asp:TextBox runat="server" ID="ScToDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- 2nd row ends -->

                                <!-- Search Button -->
                                <div class="row mb-2 mt-4">
                                    <div class="col-md-10"></div>
                                    <div class="col-md-2">
                                        <div class="text-end">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnEventClick_btnSearch" CssClass="col-md-10 btn btn-custom text-white shadow" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <!-- Control ends -->

                <!-- Searched Control Grid -->
                <div id="searchGridDiv" visible="false" runat="server" class="mt-5">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="gridSearch" runat="server" AutoGenerateColumns="false" OnRowCommand="gridSearch_RowCommand" AllowPaging="true" PageSize="10"
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
                            <asp:BoundField DataField="RefNo" HeaderText="Batch Ref No" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="BchDate" HeaderText="Batch Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="unitName" HeaderText="Unit / Office" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="BillCount" HeaderText="Assigned Bill Count" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("RefNo") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
                                        <asp:Image runat="server" ImageUrl="~/portal/assests/img/pencil-square.svg" AlternateText="Edit" style="width: 16px; height: 16px;"/>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Searched Control Grid Ends -->


            </div>
        </div>
        <!-- Update Control Div Ends -->











        <!-- Update UI Starts -->
        <div id="UpdateDiv" runat="server" visible="false">


            <!-- Heading -->
            <div class="col-md-11 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-body-secondary mb-3">
                <asp:Literal Text="Batch File For Bill Approval" runat="server"></asp:Literal>
            </div>

            <!-- UI Starts -->
            <div class="card col-md-11 mx-auto mt-1 py-2 shadow-sm rounded-3">
                <div class="card-body">

                    <!-- 1st Row -->
                    <div class="row mb-3">

                        <!-- Reference No -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                                <asp:Literal ID="Literal10" Text="Batch Number" runat="server">Batch Reference No (Auto Generated)</asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="batchRefNo" type="text" ReadOnly="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Batch Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="" runat="server">Batch Date<em style="color: red">*</em></asp:Literal>
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
                            <asp:DropDownList ID="ddUnitOffice" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                        <!-- Bill Multi Checkbox -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-primary-emphasis fw-semibold fs-6">
                                <asp:Literal ID="Literal2" Text="" runat="server">Select bills for batch<em style="color: red">*</em></asp:Literal>
                            </div>
                            <asp:ListBox runat="server" ID="ddBillNo" ClientIDMode="Static" SelectionMode="Multiple" OnSelectedIndexChanged="ddBillNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control rounded-1 border-1 border-secondar-subtle"></asp:ListBox>
                        </div>

                    </div>
                    <!-- 2nd Row Ends -->


                    <!-- Searched Control Grid -->
                    <div id="BacDiv" visible="false" runat="server" class="mt-5">
                        <!-- Back Button -->
                        <div class="">
                            <div class="row mt-5 mb-2">
                                <div class="col-md-6 text-start">
                                    <asp:Button ID="Button1" runat="server" Text="Back" OnClick="btnEventClick_btnBack" CssClass="btn btn-custom text-white shadow mb-5" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Searched Control Grid -->
                    <div id="billDiv" visible="false" runat="server" class="mt-5">
                        <asp:GridView ShowHeaderWhenEmpty="true" ID="billGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                            CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center grid-custom" OnPageIndexChanging="billGrid_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
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


                        <!-- Submit & Back Button -->
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


            <!-- Assigned Bill No Grid Starts -->
            <%-- <div class="card col-md-11 mx-auto mt-1 py-2 shadow-sm rounded-3">
                <div class="card-body">

                    <asp:GridView ShowHeaderWhenEmpty="true" ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="gridSearch_RowCommand" AllowPaging="true" PageSize="10"
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
                            <asp:BoundField DataField="RefNo" HeaderText="Batch Ref No" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="BchDate" HeaderText="Batch Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="unitName" HeaderText="Unit / Office" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="BillCount" HeaderText="Assigned Bill Count" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("RefNo") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
                                        <asp:Image runat="server" ImageUrl="~/portal/assests/img/pencil-square.svg" AlternateText="Edit" style="width: 16px; height: 16px;"/>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>--%>
            <!-- Assigned Bill No Grid Ends -->




        </div>
        <!-- Update UI Ends -->

    </form>
</body>
</html>
