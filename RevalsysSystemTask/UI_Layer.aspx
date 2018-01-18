<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UI_Layer.aspx.cs" Inherits="RevalsysSystemTask.UI_Layer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="Content/Custom.css" rel="stylesheet" type="text/css" />
    <%--<script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
         
        <div class="navbar navbar-default navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-target=".navbar-collapse" data-toggle="collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.aspx">
                    
                   <b style="color:black"><em>Employee Registrations</em></b>
                    
                </a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li class="active"><a href="index.aspx"><b>Home</b></a></li>
                    <li><a href="#">About</a></li>
                    <li><a href="#">Contact</a></li>
                    <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">Courses<b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li class="dropdown-header"><b>Programming</b></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="#">C</a></li>
                            <li><a href="#">C#</a></li>
                            <li><a href="#">javaScript</a></li>
                            
                        </ul>
                    </li>
                    <li><a href="#">Register</a></li>
                    <li><a href="#">Login</a></li>
                </ul>
            </div>
        </div>
      </div>
          
         <div class="container">
         
        <div class="center-page">
            <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
            <div class="col-xs-11">
           <asp:TextBox ID="txtEmpId" runat="server" Visible="false"></asp:TextBox>
               
                </div>
            <label class="col-xs-11">Employee Name:<b style="color:red">*</b></label>
            <div class="col-xs-11">
            <asp:TextBox ID="txtName" Class="form-control" runat="server" placeholder="Username"></asp:TextBox> <asp:RequiredFieldValidator ID="reg4" runat="server" ControlToValidate="txtName" ErrorMessage="Employee Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
               
                </div>
            <label class="col-xs-11">DOB:</label>
            <fieldset style="width:300px">
            <div class="col-xs-11">
            <div class="row">
                <div class="col-sm-4">
                    Day: <asp:DropDownList ID="ddlDay" runat="server">
                         </asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    Mon: <asp:DropDownList ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                         <%--<asp:ListItem Text="Select Month" Value="0"></asp:ListItem>--%>
                         </asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    Year: <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                         <%--<asp:ListItem Text="Select Year" Value="0"></asp:ListItem>--%>
                          </asp:DropDownList>
                </div>
            </div>
                </div>
            </fieldset>
            
            <label class="col-xs-11">Gender</label>
           
            <div class="row">
                <div class="col-sm-4">
                     <asp:RadioButton ID="rdbMale" runat="server" GroupName="gender" Text="Male" />
                </div>
                <div class="col-sm-4">
                     <asp:RadioButton ID="rdbFemale" runat="server" GroupName="gender" Text="Female" />
                </div>
                <span id="span3"></span>
            </div>
                
            <label class="col-xs-11">Designation:</label>
            <div class="col-xs-11">
            <asp:TextBox ID="txtDes" Class="form-control" runat="server" placeholder="Designation"></asp:TextBox>
                
            </div>
            <label class="col-xs-11">Salary:<b style="color:red">*</b></label>
            <div class="col-xs-11">
            <asp:TextBox ID="txtSal"  MaxLength="5" Class="form-control" runat="server" placeholder="Enter Salary"></asp:TextBox><span id="span1" style="color:red"></span>
                <asp:RequiredFieldValidator ID="rfSal" ControlToValidate="txtSal" runat="server" ErrorMessage="Salary Field required" ForeColor="Red"></asp:RequiredFieldValidator>
              
                                                                                                                                    
                </div>
            <label class="col-xs-11">Email:<b style="color:red">*</b></label>
            <div class="col-xs-11">
            <asp:TextBox ID="txtEmail" Class="form-control" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email Id Required" ForeColor="Red"></asp:RequiredFieldValidator><asp:RegularExpressionValidator runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></span>
                </div>
            <label class="col-xs-11">Mobile:<b style="color:red">*</b></label>
            <div class="col-xs-11">
                
            <asp:TextBox ID="txtMobile" MaxLength="10" Class="form-control" runat="server" placeholder="Mobile"></asp:TextBox> <span id="span2" style="color:red"></span>
                
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile Field Required" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfMob" runat="server" ControlToValidate="txtMobile" ForeColor="Red" ErrorMessage="Invalid Mobile" ValidationExpression="^([+][9][1]|[9][1]|[0]){0,1}([7-9]{1})([0-9]{9})$"></asp:RegularExpressionValidator>
           
            </div>
            <label class="col-xs-11">Qualification:</label>
            <div class="col-xs-11">
            <asp:TextBox ID="txtQualify" Class="form-control" runat="server" placeholder="Qualification"></asp:TextBox>
                </div>
            <label class="col-xs-11">Manager:</label>
            <div class="col-xs-11">
            <asp:TextBox ID="txtManager" Class="form-control" runat="server" placeholder="Manager"></asp:TextBox>
                </div>
            
            <div class="col-xs-11 space-vert">
            <asp:Button ID="btnInsert" Class="btn btn-success" runat="server" Text="Save" OnClick="btnInsert_Click" />
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnReset" CausesValidation="false" CssClass="btn btn-warning" runat="server" Text="Reset" OnClick="btnReset_Click" />
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                </div>
                          
            
   </div>
        </div>

         <!--footer contents-->
        <footer class="footer-pos">
            <div class="container">
                <asp:GridView ID="gv1" runat="server" DataKeyNames="EmpId" Caption="Employee Data" BorderColor="#66FF99" OnSelectedIndexChanged="gv1_SelectedIndexChanged" OnRowDeleting="gv1_RowDeleting">
                    <Columns>
                        <asp:CommandField HeaderText="Action" SelectText="Edit" ShowSelectButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                </div>
        </footer>
        <!--footer contents--> 



          </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnInsert").click(function () {
                if ($("input:radio[name=gender]").is(":checked") == false)
                {
                    $("#span3").html("<b style='color:red'>Please Select any option!!</b>");
                    return false;
                    
                }
                else { $("#span3").html("");}
            });
            $("#txtMobile").keypress(function (er) {
                if (er.altKey || er.ctrlKey || er.shiftKey)
                {
                    er.preventDefault();
                }
               else if(er.which !=8 && er.which !=0 &&(er.which<48 || er.which>57))
                {
                    
                    $("#span2").html("Number Only").show().fadeOut("slow");
                    return false;
                }
                
            });
            $("#txtSal").keypress(function (er) {
                if (er.altKey || er.ctrlKey || er.shiftKey) {
                    er.preventDefault();
                }
               else if(er.which !=8 && er.which !=0 &&(er.which<48 || er.which>57))
                {
                    $("#rfSal").hide("fast");
                    $("#span1").html("Numbers only").show().fadeOut("slow");
                    return false;
                }
            });
            

            
        });
    </script>
</body>
</html>