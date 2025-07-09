<%@ Page Language="VB" AutoEventWireup="false" CodeFile="viewtimetableiframe.aspx.vb" Inherits="Admin_viewtimetableiframe" %>

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
            text-align: center;
        }
        .style4
        {
            background-color: #ADADED;
            text-align: center;
        }
        .style5
        {
            background-color: #F7B26D;
            text-align: center;
        }
        .style6
        {
            background-color: #72AEEA;
            text-align: center;
        }
        .style7
        {
            background-color: #D50FD5;
            text-align: center;
        }
        .style8
        {
            background-color: #98C806;
            text-align: center;
        }
        .style9
        {
            background-color: #FF5050;
            text-align: center;
        }
        .style10
        {
            font-weight: bold;
            font-size: large;
            background-color: #98C806;
            text-align: center;
        }
        .style11
        {
            font-weight: bold;
            font-size: large;
            background-color: #D50FD5;
            text-align: center;
        }
        .style12
        {
            font-weight: bold;
            font-size: large;
            background-color: #72AEEA;
            text-align: center;
        }
        .style13
        {
            font-weight: bold;
            font-size: large;
            background-color: #F7B26D;
            text-align: center;
        }
        .style14
        {
            font-weight: bold;
            font-size: large;
            background-color: #ADADED;
            text-align: center;
        }
        .style15
        {
            font-weight: bold;
            font-size: large;
            background-color: #00CC99;
            text-align: center;
        }
        .style16
        {
            font-weight: bold;
            font-size: large;
            color: #FFFFFF;
            background-color: #FF5050;
            text-align: center;
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
        .stylelblTeacher
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #fff;
        }
        .stylelblSubject
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight:bold;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="divsearch">
    <p>
                <span class="style17"><strong><em>
                View Time Table for Class</em></strong></span>&nbsp;
     <asp:DropDownList ID="ddlClass" runat="server" Height="25px" Width="150px" AutoPostBack="true"  AppendDataBoundItems="true" class="select-search">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Literal ID="litmsg" runat="server"></asp:Literal>
          </p>             </div>
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
                <asp:Label ID="lblSubject11" runat="server" CssClass="stylelblSubject"></asp:Label><br />
                <asp:Label ID="lblTeacher11" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject12" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher12" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject13" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher13" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject14" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher14" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject15" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher15" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject16" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher16" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                2</td>
            <td class="style3">
                <asp:Label ID="lblSubject21" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher21" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject22" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher22" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject23" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher23" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject24" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher24" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject25" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher25" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject26" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher26" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                3</td>
            <td class="style3">
                <asp:Label ID="lblSubject31" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher31" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject32" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher32" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject33" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher33" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject34" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher34" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject35" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher35" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject36" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher36" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                4</td>
            <td class="style3">
                <asp:Label ID="lblSubject41" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher41" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject42" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher42" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject43" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher43" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject44" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher44" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject45" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher45" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject46" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher46" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                5</td>
            <td class="style3">
                <asp:Label ID="lblSubject51" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher51" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject52" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher52" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject53" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher53" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject54" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher54" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject55" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher55" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject56" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher56" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="7">
                <strong>LUNCH - LUNCH - LUNCH - LUNCH - LUNCH - LUNCH - LUNCH</strong></td>
        </tr>
        <tr>
            <td class="style16">
                6</td>
            <td class="style3">
                <asp:Label ID="lblSubject61" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher61" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject62" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher62" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject63" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher63" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject64" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher64" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject65" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher65" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject66" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher66" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                7</td>
            <td class="style3">
                <asp:Label ID="lblSubject71" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher71" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject72" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher72" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject73" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher73" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject74" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher74" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject75" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher75" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject76" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher76" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                8</td>
            <td class="style3">
                <asp:Label ID="lblSubject81" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher81" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject82" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher82" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject83" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher83" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject84" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher84" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject85" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher85" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject86" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher86" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                9</td>
            <td class="style3">
                <asp:Label ID="lblSubject91" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher91" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style4">
                <asp:Label ID="lblSubject92" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher92" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblSubject93" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher93" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lblSubject94" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher94" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSubject95" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher95" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblSubject96" runat="server" CssClass="stylelblSubject"></asp:Label>
                <br />
                <asp:Label ID="lblTeacher96" runat="server" CssClass="stylelblTeacher"></asp:Label>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
