<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ParentRegister.aspx.vb" Inherits="Admin_ParentRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
  <!-- Form components -->
            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Parent Registration</h6>  <asp:Button ID="btnaddstu" runat="server" class="btn btn-danger"  PostBackUrl="~/Admin/StudentRegistration.aspx" Text="Add Student" /></div>
                    <div class="panel-body">
                        <asp:HiddenField ID="hfId" runat="server" />
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Father Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtFname" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="ReqIntro" runat="server" ControlToValidate="txtFname"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Mother Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtMname" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMname"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <%--<div class="form-group">
                            <label class="col-sm-2 control-label">Gender </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlGender" class="select-full" runat="server">
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGender"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>--%>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Nationality </label>
                            <div class="col-sm-10">
                               <asp:DropDownList runat="server" class="select-search"  ID="ddlNationality">
                <asp:ListItem Value="">-- select Nationalty --</asp:ListItem>
                <asp:ListItem Value="afghan">Afghan</asp:ListItem>
                <asp:ListItem Value="albanian">Albanian</asp:ListItem>
                <asp:ListItem Value="algerian">Algerian</asp:ListItem>
                <asp:ListItem Value="american">American</asp:ListItem>
                <asp:ListItem Value="andorran">Andorran</asp:ListItem>
                <asp:ListItem Value="angolan">Angolan</asp:ListItem>
                <asp:ListItem Value="antiguans">Antiguans</asp:ListItem>
                <asp:ListItem Value="argentinean">Argentinean</asp:ListItem>
                <asp:ListItem Value="armenian">Armenian</asp:ListItem>
                <asp:ListItem Value="australian">Australian</asp:ListItem>
                <asp:ListItem Value="austrian">Austrian</asp:ListItem>
                <asp:ListItem Value="azerbaijani">Azerbaijani</asp:ListItem>
                <asp:ListItem Value="bahamian">Bahamian</asp:ListItem>
                <asp:ListItem Value="bahraini">Bahraini</asp:ListItem>
                <asp:ListItem Value="bangladeshi">Bangladeshi</asp:ListItem>
                <asp:ListItem Value="barbadian">Barbadian</asp:ListItem>
                <asp:ListItem Value="barbudans">Barbudans</asp:ListItem>
                <asp:ListItem Value="batswana">Batswana</asp:ListItem>
                <asp:ListItem Value="belarusian">Belarusian</asp:ListItem>
                <asp:ListItem Value="belgian">Belgian</asp:ListItem>
                <asp:ListItem Value="belizean">Belizean</asp:ListItem>
                <asp:ListItem Value="beninese">Beninese</asp:ListItem>
                <asp:ListItem Value="bhutanese">Bhutanese</asp:ListItem>
                <asp:ListItem Value="bolivian">Bolivian</asp:ListItem>
                <asp:ListItem Value="bosnian">Bosnian</asp:ListItem>
                <asp:ListItem Value="brazilian">Brazilian</asp:ListItem>
                <asp:ListItem Value="british">British</asp:ListItem>
                <asp:ListItem Value="bruneian">Bruneian</asp:ListItem>
                <asp:ListItem Value="bulgarian">Bulgarian</asp:ListItem>
                <asp:ListItem Value="burkinabe">Burkinabe</asp:ListItem>
                <asp:ListItem Value="burmese">Burmese</asp:ListItem>
                <asp:ListItem Value="burundian">Burundian</asp:ListItem>
                <asp:ListItem Value="cambodian">Cambodian</asp:ListItem>
                <asp:ListItem Value="cameroonian">Cameroonian</asp:ListItem>
                <asp:ListItem Value="canadian">Canadian</asp:ListItem>
                <asp:ListItem Value="cape verdean">Cape Verdean</asp:ListItem>
                <asp:ListItem Value="central african">Central African</asp:ListItem>
                <asp:ListItem Value="chadian">Chadian</asp:ListItem>
                <asp:ListItem Value="chilean">Chilean</asp:ListItem>
                <asp:ListItem Value="chinese">Chinese</asp:ListItem>
                <asp:ListItem Value="colombian">Colombian</asp:ListItem>
                <asp:ListItem Value="comoran">Comoran</asp:ListItem>
                <asp:ListItem Value="congolese">Congolese</asp:ListItem>
                <asp:ListItem Value="costa rican">Costa Rican</asp:ListItem>
                <asp:ListItem Value="croatian">Croatian</asp:ListItem>
                <asp:ListItem Value="cuban">Cuban</asp:ListItem>
                <asp:ListItem Value="cypriot">Cypriot</asp:ListItem>
                <asp:ListItem Value="czech">Czech</asp:ListItem>
                <asp:ListItem Value="danish">Danish</asp:ListItem>
                <asp:ListItem Value="djibouti">Djibouti</asp:ListItem>
                <asp:ListItem Value="dominican">Dominican</asp:ListItem>
                <asp:ListItem Value="dutch">Dutch</asp:ListItem>
                <asp:ListItem Value="east timorese">East Timorese</asp:ListItem>
                <asp:ListItem Value="ecuadorean">Ecuadorean</asp:ListItem>
                <asp:ListItem Value="egyptian">Egyptian</asp:ListItem>
                <asp:ListItem Value="emirian">Emirian</asp:ListItem>
                <asp:ListItem Value="equatorial guinean">Equatorial Guinean</asp:ListItem>
                <asp:ListItem Value="eritrean">Eritrean</asp:ListItem>
                <asp:ListItem Value="estonian">Estonian</asp:ListItem>
                <asp:ListItem Value="ethiopian">Ethiopian</asp:ListItem>
                <asp:ListItem Value="fijian">Fijian</asp:ListItem>
                <asp:ListItem Value="filipino">Filipino</asp:ListItem>
                <asp:ListItem Value="finnish">Finnish</asp:ListItem>
                <asp:ListItem Value="french">French</asp:ListItem>
                <asp:ListItem Value="gabonese">Gabonese</asp:ListItem>
                <asp:ListItem Value="gambian">Gambian</asp:ListItem>
                <asp:ListItem Value="georgian">Georgian</asp:ListItem>
                <asp:ListItem Value="german">German</asp:ListItem>
                <asp:ListItem Value="ghanaian">Ghanaian</asp:ListItem>
                <asp:ListItem Value="greek">Greek</asp:ListItem>
                <asp:ListItem Value="grenadian">Grenadian</asp:ListItem>
                <asp:ListItem Value="guatemalan">Guatemalan</asp:ListItem>
                <asp:ListItem Value="guinea-bissauan">Guinea-Bissauan</asp:ListItem>
                <asp:ListItem Value="guinean">Guinean</asp:ListItem>
                <asp:ListItem Value="guyanese">Guyanese</asp:ListItem>
                <asp:ListItem Value="haitian">Haitian</asp:ListItem>
                <asp:ListItem Value="herzegovinian">Herzegovinian</asp:ListItem>
                <asp:ListItem Value="honduran">Honduran</asp:ListItem>
                <asp:ListItem Value="hungarian">Hungarian</asp:ListItem>
                <asp:ListItem Value="icelander">Icelander</asp:ListItem>
                <asp:ListItem Value="indian">Indian</asp:ListItem>
                <asp:ListItem Value="indonesian">Indonesian</asp:ListItem>
                <asp:ListItem Value="iranian">Iranian</asp:ListItem>
                <asp:ListItem Value="iraqi">Iraqi</asp:ListItem>
                <asp:ListItem Value="irish">Irish</asp:ListItem>
                <asp:ListItem Value="israeli">Israeli</asp:ListItem>
                <asp:ListItem Value="italian">Italian</asp:ListItem>
                <asp:ListItem Value="ivorian">Ivorian</asp:ListItem>
                <asp:ListItem Value="jamaican">Jamaican</asp:ListItem>
                <asp:ListItem Value="japanese">Japanese</asp:ListItem>
                <asp:ListItem Value="jordanian">Jordanian</asp:ListItem>
                <asp:ListItem Value="kazakhstani">Kazakhstani</asp:ListItem>
                <asp:ListItem Value="kenyan">Kenyan</asp:ListItem>
                <asp:ListItem Value="kittian and nevisian">Kittian and Nevisian</asp:ListItem>
                <asp:ListItem Value="kuwaiti">Kuwaiti</asp:ListItem>
                <asp:ListItem Value="kyrgyz">Kyrgyz</asp:ListItem>
                <asp:ListItem Value="laotian">Laotian</asp:ListItem>
                <asp:ListItem Value="latvian">Latvian</asp:ListItem>
                <asp:ListItem Value="lebanese">Lebanese</asp:ListItem>
                <asp:ListItem Value="liberian">Liberian</asp:ListItem>
                <asp:ListItem Value="libyan">Libyan</asp:ListItem>
                <asp:ListItem Value="liechtensteiner">Liechtensteiner</asp:ListItem>
                <asp:ListItem Value="lithuanian">Lithuanian</asp:ListItem>
                <asp:ListItem Value="luxembourger">Luxembourger</asp:ListItem>
                <asp:ListItem Value="macedonian">Macedonian</asp:ListItem>
                <asp:ListItem Value="malagasy">Malagasy</asp:ListItem>
                <asp:ListItem Value="malawian">Malawian</asp:ListItem>
                <asp:ListItem Value="malaysian">Malaysian</asp:ListItem>
                <asp:ListItem Value="maldivan">Maldivan</asp:ListItem>
                <asp:ListItem Value="malian">Malian</asp:ListItem>
                <asp:ListItem Value="maltese">Maltese</asp:ListItem>
                <asp:ListItem Value="marshallese">Marshallese</asp:ListItem>
                <asp:ListItem Value="mauritanian">Mauritanian</asp:ListItem>
                <asp:ListItem Value="mauritian">Mauritian</asp:ListItem>
                <asp:ListItem Value="mexican">Mexican</asp:ListItem>
                <asp:ListItem Value="micronesian">Micronesian</asp:ListItem>
                <asp:ListItem Value="moldovan">Moldovan</asp:ListItem>
                <asp:ListItem Value="monacan">Monacan</asp:ListItem>
                <asp:ListItem Value="mongolian">Mongolian</asp:ListItem>
                <asp:ListItem Value="moroccan">Moroccan</asp:ListItem>
                <asp:ListItem Value="mosotho">Mosotho</asp:ListItem>
                <asp:ListItem Value="motswana">Motswana</asp:ListItem>
                <asp:ListItem Value="mozambican">Mozambican</asp:ListItem>
                <asp:ListItem Value="namibian">Namibian</asp:ListItem>
                <asp:ListItem Value="nauruan">Nauruan</asp:ListItem>
                <asp:ListItem Value="nepalese">Nepalese</asp:ListItem>
                <asp:ListItem Value="new zealander">New Zealander</asp:ListItem>
                <asp:ListItem Value="ni-vanuatu">Ni-Vanuatu</asp:ListItem>
                <asp:ListItem Value="nicaraguan">Nicaraguan</asp:ListItem>
                <asp:ListItem Value="nigerien">Nigerien</asp:ListItem>
                <asp:ListItem Value="north korean">North Korean</asp:ListItem>
                <asp:ListItem Value="northern irish">Northern Irish</asp:ListItem>
                <asp:ListItem Value="norwegian">Norwegian</asp:ListItem>
                <asp:ListItem Value="omani">Omani</asp:ListItem>
                <asp:ListItem Value="pakistani">Pakistani</asp:ListItem>
                <asp:ListItem Value="palauan">Palauan</asp:ListItem>
                <asp:ListItem Value="panamanian">Panamanian</asp:ListItem>
                <asp:ListItem Value="papua new guinean">Papua New Guinean</asp:ListItem>
                <asp:ListItem Value="paraguayan">Paraguayan</asp:ListItem>
                <asp:ListItem Value="peruvian">Peruvian</asp:ListItem>
                <asp:ListItem Value="polish">Polish</asp:ListItem>
                <asp:ListItem Value="portuguese">Portuguese</asp:ListItem>
                <asp:ListItem Value="qatari">Qatari</asp:ListItem>
                <asp:ListItem Value="romanian">Romanian</asp:ListItem>
                <asp:ListItem Value="russian">Russian</asp:ListItem>
                <asp:ListItem Value="rwandan">Rwandan</asp:ListItem>
                <asp:ListItem Value="saint lucian">Saint Lucian</asp:ListItem>
                <asp:ListItem Value="salvadoran">Salvadoran</asp:ListItem>
                <asp:ListItem Value="samoan">Samoan</asp:ListItem>
                <asp:ListItem Value="san marinese">San Marinese</asp:ListItem>
                <asp:ListItem Value="sao tomean">Sao Tomean</asp:ListItem>
                <asp:ListItem Value="saudi">Saudi</asp:ListItem>
                <asp:ListItem Value="scottish">Scottish</asp:ListItem>
                <asp:ListItem Value="senegalese">Senegalese</asp:ListItem>
                <asp:ListItem Value="serbian">Serbian</asp:ListItem>
                <asp:ListItem Value="seychellois">Seychellois</asp:ListItem>
                <asp:ListItem Value="sierra leonean">Sierra Leonean</asp:ListItem>
                <asp:ListItem Value="singaporean">Singaporean</asp:ListItem>
                <asp:ListItem Value="slovakian">Slovakian</asp:ListItem>
                <asp:ListItem Value="slovenian">Slovenian</asp:ListItem>
                <asp:ListItem Value="solomon islander">Solomon Islander</asp:ListItem>
                <asp:ListItem Value="somali">Somali</asp:ListItem>
                <asp:ListItem Value="south african">South African</asp:ListItem>
                <asp:ListItem Value="south korean">South Korean</asp:ListItem>
                <asp:ListItem Value="spanish">Spanish</asp:ListItem>
                <asp:ListItem Value="sri lankan">Sri Lankan</asp:ListItem>
                <asp:ListItem Value="sudanese">Sudanese</asp:ListItem>
                <asp:ListItem Value="surinamer">Surinamer</asp:ListItem>
                <asp:ListItem Value="swazi">Swazi</asp:ListItem>
                <asp:ListItem Value="swedish">Swedish</asp:ListItem>
                <asp:ListItem Value="swiss">Swiss</asp:ListItem>
                <asp:ListItem Value="syrian">Syrian</asp:ListItem>
                <asp:ListItem Value="taiwanese">Taiwanese</asp:ListItem>
                <asp:ListItem Value="tajik">Tajik</asp:ListItem>
                <asp:ListItem Value="tanzanian">Tanzanian</asp:ListItem>
                <asp:ListItem Value="thai">Thai</asp:ListItem>
                <asp:ListItem Value="togolese">Togolese</asp:ListItem>
                <asp:ListItem Value="tongan">Tongan</asp:ListItem>
                <asp:ListItem Value="trinidadian or tobagonian">Trinidadian or Tobagonian</asp:ListItem>
                <asp:ListItem Value="tunisian">Tunisian</asp:ListItem>
                <asp:ListItem Value="turkish">Turkish</asp:ListItem>
                <asp:ListItem Value="tuvaluan">Tuvaluan</asp:ListItem>
                <asp:ListItem Value="ugandan">Ugandan</asp:ListItem>
                <asp:ListItem Value="ukrainian">Ukrainian</asp:ListItem>
                <asp:ListItem Value="uruguayan">Uruguayan</asp:ListItem>
                <asp:ListItem Value="uzbekistani">Uzbekistani</asp:ListItem>
                <asp:ListItem Value="venezuelan">Venezuelan</asp:ListItem>
                <asp:ListItem Value="vietnamese">Vietnamese</asp:ListItem>
                <asp:ListItem Value="welsh">Welsh</asp:ListItem>
                <asp:ListItem Value="yemenite">Yemenite</asp:ListItem>
                <asp:ListItem Value="zambian">Zambian</asp:ListItem>
                <asp:ListItem Value="zimbabwean">Zimbabwean</asp:ListItem>
            </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNationality"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Address </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtaddress" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtaddress"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">State </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" class="select-search"  runat="server" AutoPostBack="True">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             

                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlState"
                ErrorMessage="*" ValidationGroup="UserRegistration" InitialValue="">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">City </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" class="select-search"  runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCity"
                ErrorMessage="*" ValidationGroup="UserRegistration" InitialValue="">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Locality </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlLocality" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValu="0" ControlToValidate="ddlLocality"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Contact No. </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtContactno" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtContactno"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Email</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmailid" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmailid"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailid"
                ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Status </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlactive" class="select-full" runat="server">
                                <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">Deactive</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlactive"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
            <!-- /form components -->

</asp:Content>

