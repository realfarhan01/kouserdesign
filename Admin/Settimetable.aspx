<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Settimetable.aspx.vb" Inherits="Admin_Settimetable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            border: 1px solid #333333;
        }
        .style2
        {
            font-size: x-large;
            color: #FFFFFF;
            text-align: center;
            background-color: #FF5050;
            font-weight: bold;
        }
        .style3
        {
            background-color: #00CC99;
        }
        .style4
        {
            background-color: #CCCCFF;
        }
        .style5
        {
            background-color: #FFCC99;
        }
        .style6
        {
            background-color: #99CCFF;
        }
        .style7
        {
            background-color: #FF99FF;
        }
        .style8
        {
            background-color: #CCFF33;
        }
        .style9
        {
            background-color: #FF5050;
        }
        .style10
        {
            font-weight: bold;
            font-size: large;
            background-color: #CCFF33;
        }
        .style11
        {
            font-weight: bold;
            font-size: large;
            background-color: #FF99FF;
        }
        .style12
        {
            font-weight: bold;
            font-size: large;
            background-color: #99CCFF;
        }
        .style13
        {
            font-weight: bold;
            font-size: large;
            background-color: #FFCC99;
        }
        .style14
        {
            font-weight: bold;
            font-size: large;
            background-color: #CCCCFF;
        }
        .style15
        {
            font-weight: bold;
            font-size: large;
            background-color: #00CC99;
        }
        .style16
        {
            font-weight: bold;
            font-size: large;
            color: #FFFFFF;
            background-color: #FF5050;
        }
        .style17
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #003399;
        }
        .style18
        {
            width: 60px;
            height: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p>
                <asp:ImageButton CssClass="style18" ID="btnBack" ImageUrl="../login/img/back.jpg" runat="server" />   &nbsp; <span class="style17"><strong><em>Set Time Table for Class</em></strong></span>&nbsp;
     <asp:DropDownList ID="ddlClass" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" AutoPostBack="true"  class="select-search">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnSubmit" runat="server" BackColor="#006699" BorderStyle="Ridge" Height="25px"
             class="btn btn-primary" ForeColor="White" Text="Submit" />
                        
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
          </p>             
    <table align="center" cellpadding="1" class="style1" 
        style="line-height: 30px; font-family: Arial, Helvetica, sans-serif;" 
        border="1" frame="box">
        <tr>
            <td style="width:4%" class="style9">
                &nbsp;</td>
            <td style="width:16%" class="style15">
                MON</td>
            <td style="width:16%" class="style14">
                TUE</td>
            <td style="width:16%" class="style13">
                WED</td>
            <td style="width:16%" class="style12">
                THU</td>
            <td style="width:16%" class="style11">
                FRI</td>
            <td style="width:16%" class="style10">
                SAT</td>
        </tr>
        <tr>
            <td class="style16">
                1</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher11" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject11" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher12" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject12" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher13" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject13" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher14" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject14" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher15" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject15" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher16" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject16" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                2</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher21" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject21" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher22" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject22" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher23" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject23" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher24" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject24" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher25" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject25" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher26" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject26" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                3</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher31" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject31" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher32" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject32" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher33" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject33" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher34" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject34" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher35" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject35" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher36" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject36" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                4</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher41" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject41" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher42" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject42" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher43" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject43" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher44" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject44" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher45" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject45" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher46" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject46" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                5</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher51" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject51" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher52" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject52" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher53" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject53" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher54" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject54" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher55" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject55" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher56" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject56" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="7">
                <strong>LUNCH - LUNCH - LUNCH - LUNCH - LUNCH - LUNCH - LUNCH - LUNCH</strong></td>
        </tr>
        <tr>
            <td class="style16">
                6</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher61" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject61" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher62" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject62" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher63" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject63" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher64" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject64" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher65" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject65" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher66" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject66" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                7</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher71" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject71" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher72" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject72" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher73" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject73" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher74" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject74" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher75" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject75" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher76" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject76" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                8</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher81" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject81" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher82" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject82" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher83" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject83" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher84" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject84" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher85" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject85" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher86" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject86" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style16">
                9</td>
            <td class="style3">
                <asp:DropDownList ID="ddlTeacher91" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject91" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddlTeacher92" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject92" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style5">
                <asp:DropDownList ID="ddlTeacher93" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject93" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlTeacher94" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject94" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                <asp:DropDownList ID="ddlTeacher95" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject95" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style8">
                <asp:DropDownList ID="ddlTeacher96" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlSubject96" runat="server" Height="25px" Width="150px" AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="">----</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
