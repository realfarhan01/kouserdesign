<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddEmplyee.aspx.vb" Inherits="Admin_AddEmplyee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <!-- Form components -->
    <div class="form-horizontal">



        <!-- Basic inputs -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Employee Registration</h6>
            </div>
            <div class="panel-body">

                <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                <asp:HiddenField ID="hfid" runat="server" />
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>

                
                <div class="form-group">
                    <label class="col-sm-2 control-label">Employee Code </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmployeeCode" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmployeeCode"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">First Name </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtname" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtname"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Last Name </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtLName" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtLName"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Father Name </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFname" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqIntro" runat="server" ControlToValidate="txtFname"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Mother Name </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtMname" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMname"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-2 control-label">Date Of Birth </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" Width="57px" class="select" ID="ddlDate">
                            <asp:ListItem Value="1">01
                            </asp:ListItem>
                            <asp:ListItem Value="2">02
                            </asp:ListItem>
                            <asp:ListItem Value="3">03
                            </asp:ListItem>
                            <asp:ListItem Value="4">04
                            </asp:ListItem>
                            <asp:ListItem Value="5">05
                            </asp:ListItem>
                            <asp:ListItem Value="6">06
                            </asp:ListItem>
                            <asp:ListItem Value="7">07
                            </asp:ListItem>
                            <asp:ListItem Value="8">08
                            </asp:ListItem>
                            <asp:ListItem Value="9">09
                            </asp:ListItem>
                            <asp:ListItem Value="10">10
                            </asp:ListItem>
                            <asp:ListItem Value="11">11
                            </asp:ListItem>
                            <asp:ListItem Value="12">12
                            </asp:ListItem>
                            <asp:ListItem Value="13">13
                            </asp:ListItem>
                            <asp:ListItem Value="14">14
                            </asp:ListItem>
                            <asp:ListItem Value="15">15
                            </asp:ListItem>
                            <asp:ListItem Value="16">16
                            </asp:ListItem>
                            <asp:ListItem Value="17">17
                            </asp:ListItem>
                            <asp:ListItem Value="18">18
                            </asp:ListItem>
                            <asp:ListItem Value="19">19
                            </asp:ListItem>
                            <asp:ListItem Value="20">20
                            </asp:ListItem>
                            <asp:ListItem Value="21">21
                            </asp:ListItem>
                            <asp:ListItem Value="22">22
                            </asp:ListItem>
                            <asp:ListItem Value="23">23
                            </asp:ListItem>
                            <asp:ListItem Value="24">24
                            </asp:ListItem>
                            <asp:ListItem Value="25">25
                            </asp:ListItem>
                            <asp:ListItem Value="26">26
                            </asp:ListItem>
                            <asp:ListItem Value="27">27
                            </asp:ListItem>
                            <asp:ListItem Value="28">28
                            </asp:ListItem>
                            <asp:ListItem Value="29">29
                            </asp:ListItem>
                            <asp:ListItem Value="30">30
                            </asp:ListItem>
                            <asp:ListItem Value="31">31
                            </asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" Width="130px" class="select" ID="ddlMonth">
                            <asp:ListItem Value="1">January
                            </asp:ListItem>
                            <asp:ListItem Value="2">February
                            </asp:ListItem>
                            <asp:ListItem Value="3">March
                            </asp:ListItem>
                            <asp:ListItem Value="4">April
                            </asp:ListItem>
                            <asp:ListItem Value="5">May
                            </asp:ListItem>
                            <asp:ListItem Value="6">June
                            </asp:ListItem>
                            <asp:ListItem Value="7">July
                            </asp:ListItem>
                            <asp:ListItem Value="8">August
                            </asp:ListItem>
                            <asp:ListItem Value="9">September
                            </asp:ListItem>
                            <asp:ListItem Value="10">October
                            </asp:ListItem>
                            <asp:ListItem Value="11">November
                            </asp:ListItem>
                            <asp:ListItem Value="12">December
                            </asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" Width="100px" class="select" ID="ddlYear">
                            <asp:ListItem Value="1900">1900</asp:ListItem>
                            <asp:ListItem Value="1901">1901</asp:ListItem>
                            <asp:ListItem Value="1902">1902</asp:ListItem>
                            <asp:ListItem Value="1903">1903</asp:ListItem>
                            <asp:ListItem Value="1904">1904</asp:ListItem>
                            <asp:ListItem Value="1905">1905</asp:ListItem>
                            <asp:ListItem Value="1906">1906</asp:ListItem>
                            <asp:ListItem Value="1907">1907</asp:ListItem>
                            <asp:ListItem Value="1908">1908</asp:ListItem>
                            <asp:ListItem Value="1909">1909</asp:ListItem>
                            <asp:ListItem Value="1910">1910</asp:ListItem>
                            <asp:ListItem Value="1911">1911</asp:ListItem>
                            <asp:ListItem Value="1912">1912</asp:ListItem>
                            <asp:ListItem Value="1913">1913</asp:ListItem>
                            <asp:ListItem Value="1914">1914</asp:ListItem>
                            <asp:ListItem Value="1915">1915</asp:ListItem>
                            <asp:ListItem Value="1916">1916</asp:ListItem>
                            <asp:ListItem Value="1917">1917</asp:ListItem>
                            <asp:ListItem Value="1918">1918</asp:ListItem>
                            <asp:ListItem Value="1919">1919</asp:ListItem>
                            <asp:ListItem Value="1920">1920</asp:ListItem>
                            <asp:ListItem Value="1921">1921</asp:ListItem>
                            <asp:ListItem Value="1922">1922</asp:ListItem>
                            <asp:ListItem Value="1923">1923</asp:ListItem>
                            <asp:ListItem Value="1924">1924</asp:ListItem>
                            <asp:ListItem Value="1925">1925</asp:ListItem>
                            <asp:ListItem Value="1926">1926</asp:ListItem>
                            <asp:ListItem Value="1927">1927</asp:ListItem>
                            <asp:ListItem Value="1928">1928</asp:ListItem>
                            <asp:ListItem Value="1929">1929</asp:ListItem>
                            <asp:ListItem Value="1930">1930</asp:ListItem>
                            <asp:ListItem Value="1931">1931</asp:ListItem>
                            <asp:ListItem Value="1932">1932</asp:ListItem>
                            <asp:ListItem Value="1933">1933</asp:ListItem>
                            <asp:ListItem Value="1934">1934</asp:ListItem>
                            <asp:ListItem Value="1935">1935</asp:ListItem>
                            <asp:ListItem Value="1936">1936</asp:ListItem>
                            <asp:ListItem Value="1937">1937</asp:ListItem>
                            <asp:ListItem Value="1938">1938</asp:ListItem>
                            <asp:ListItem Value="1939">1939</asp:ListItem>
                            <asp:ListItem Value="1940">1940</asp:ListItem>
                            <asp:ListItem Value="1941">1941</asp:ListItem>
                            <asp:ListItem Value="1942">1942</asp:ListItem>
                            <asp:ListItem Value="1943">1943</asp:ListItem>
                            <asp:ListItem Value="1944">1944</asp:ListItem>
                            <asp:ListItem Value="1945">1945</asp:ListItem>
                            <asp:ListItem Value="1946">1946</asp:ListItem>
                            <asp:ListItem Value="1947">1947</asp:ListItem>
                            <asp:ListItem Value="1948">1948</asp:ListItem>
                            <asp:ListItem Value="1949">1949</asp:ListItem>
                            <asp:ListItem Value="1950">1950</asp:ListItem>
                            <asp:ListItem Value="1951">1951</asp:ListItem>
                            <asp:ListItem Value="1952">1952</asp:ListItem>
                            <asp:ListItem Value="1953">1953</asp:ListItem>
                            <asp:ListItem Value="1954">1954</asp:ListItem>
                            <asp:ListItem Value="1955">1955</asp:ListItem>
                            <asp:ListItem Value="1956">1956</asp:ListItem>
                            <asp:ListItem Value="1957">1957</asp:ListItem>
                            <asp:ListItem Value="1958">1958</asp:ListItem>
                            <asp:ListItem Value="1959">1959</asp:ListItem>
                            <asp:ListItem Value="1960">1960</asp:ListItem>
                            <asp:ListItem Value="1961">1961</asp:ListItem>
                            <asp:ListItem Value="1962">1962</asp:ListItem>
                            <asp:ListItem Value="1963">1963</asp:ListItem>
                            <asp:ListItem Value="1964">1964</asp:ListItem>
                            <asp:ListItem Value="1965">1965</asp:ListItem>
                            <asp:ListItem Value="1966">1966</asp:ListItem>
                            <asp:ListItem Value="1967">1967</asp:ListItem>
                            <asp:ListItem Value="1968">1968</asp:ListItem>
                            <asp:ListItem Value="1969">1969</asp:ListItem>
                            <asp:ListItem Value="1970">1970</asp:ListItem>
                            <asp:ListItem Value="1971">1971</asp:ListItem>
                            <asp:ListItem Value="1972">1972</asp:ListItem>
                            <asp:ListItem Value="1973">1973</asp:ListItem>
                            <asp:ListItem Value="1974">1974</asp:ListItem>
                            <asp:ListItem Value="1975">1975</asp:ListItem>
                            <asp:ListItem Value="1976">1976</asp:ListItem>
                            <asp:ListItem Value="1977">1977</asp:ListItem>
                            <asp:ListItem Value="1978">1978</asp:ListItem>
                            <asp:ListItem Value="1979">1979</asp:ListItem>
                            <asp:ListItem Value="1980">1980</asp:ListItem>
                            <asp:ListItem Value="1981">1981</asp:ListItem>
                            <asp:ListItem Value="1982">1982</asp:ListItem>
                            <asp:ListItem Value="1983">1983</asp:ListItem>
                            <asp:ListItem Value="1984">1984</asp:ListItem>
                            <asp:ListItem Value="1985">1985</asp:ListItem>
                            <asp:ListItem Value="1986">1986</asp:ListItem>
                            <asp:ListItem Value="1987">1987</asp:ListItem>
                            <asp:ListItem Value="1988">1988</asp:ListItem>
                            <asp:ListItem Value="1989">1989</asp:ListItem>
                            <asp:ListItem Value="1990">1990</asp:ListItem>
                            <asp:ListItem Value="1991">1991</asp:ListItem>
                            <asp:ListItem Value="1992">1992</asp:ListItem>
                            <asp:ListItem Value="1993">1993</asp:ListItem>
                            <asp:ListItem Value="1994">1994</asp:ListItem>
                            <asp:ListItem Value="1995">1995</asp:ListItem>
                            <asp:ListItem Value="1996">1996</asp:ListItem>
                            <asp:ListItem Value="1997">1997</asp:ListItem>
                            <asp:ListItem Value="1998">1998</asp:ListItem>
                            <asp:ListItem Value="1999">1999</asp:ListItem>
                            <asp:ListItem Value="2000">2000</asp:ListItem>
                            <asp:ListItem Value="2001">2001</asp:ListItem>
                            <asp:ListItem Value="2002">2002</asp:ListItem>
                            <asp:ListItem Value="2003">2003</asp:ListItem>
                            <asp:ListItem Value="2004">2004</asp:ListItem>
                            <asp:ListItem Value="2005">2005</asp:ListItem>
                            <asp:ListItem Value="2006">2006</asp:ListItem>
                            <asp:ListItem Value="2007">2007</asp:ListItem>
                            <asp:ListItem Value="2008">2008</asp:ListItem>
                            <asp:ListItem Value="2009">2009</asp:ListItem>
                            <asp:ListItem Value="2010">2010</asp:ListItem>
                            <asp:ListItem Value="2011">2011</asp:ListItem>
                            <asp:ListItem Value="2012">2012</asp:ListItem>
                            <asp:ListItem Value="2013">2013</asp:ListItem>
                            <asp:ListItem Value="2014">2014</asp:ListItem>
                            <asp:ListItem Value="2015">2015</asp:ListItem>
                            <asp:ListItem Value="2016">2016</asp:ListItem>
                            <asp:ListItem Value="2017">2017</asp:ListItem>
                            <asp:ListItem Value="2018">2018</asp:ListItem>
                            <asp:ListItem Value="2019">2019</asp:ListItem>
                            <asp:ListItem Value="2020">2020</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlGender"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Gender </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlGender" class="select-search" runat="server">
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGender"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Nationality </label>
                    <div class="col-sm-3">
                        <asp:DropDownList runat="server" class="select-search" ID="ddlNationality">
                            <asp:ListItem Value="">-- Select Nationalty --</asp:ListItem>
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
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Address </label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtaddress" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">State </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" class="select-search" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                        </asp:DropDownList>


                    </div>
                    <label class="col-sm-2 control-label">City </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" class="select-search" runat="server">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-2 control-label">Locality </label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlLocality" class="select-search" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Contact No. </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContactno" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">Email</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmailid" class="form-control" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailid"
                            ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Status </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlactive" class="select-full" runat="server">
                            <asp:ListItem Value="0">Active</asp:ListItem>
                            <asp:ListItem Value="1">Deactive</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlactive"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Designation </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddldesignation" AppendDataBoundItems="true" class="select-search" runat="server">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Department Name </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDepartment" class="select-full" runat="server">
                            <asp:ListItem Value="Technical">Technical</asp:ListItem>
                            <asp:ListItem Value="Non-Technical">Non-Technical</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="col-sm-2 control-label">EMP Type </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlEmpType" AppendDataBoundItems="true" class="select-search" runat="server">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Bank</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtbank" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">PAN No.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPan" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
               <%-- <div class="form-group">
                    <label class="col-sm-2 control-label">ESI</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtESI" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">PF</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPF" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>--%>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Pay Scale</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlpayScale" class="select-full" AppendDataBoundItems="true" runat="server">
                        </asp:DropDownList>
                  <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlactive"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>--%>
                    </div>
                    <label class="col-sm-2 control-label">Basic Salary </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtSalary" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtSalary"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Joining Date</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtJoinDate" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Confirmation </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtConfirm" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="form-group">
                    <label class="col-sm-2 control-label">Increment</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtIncrement" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtMname"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Leaving </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtLeaving" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtMname"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>--%>
                <div class="form-group">
                    <label class="col-sm-2 control-label">EMP File No.</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFileNo" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Retirenment </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRetirnment" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            <%--    <div class="form-group">
  <label class="col-sm-2 control-label">Next Increment Date </label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtNextIncr" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Subject </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtSubject" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>--%>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Qualification</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtQualification" class="form-control" runat="server"></asp:TextBox>
                    </div>
                  
                    <label class="col-sm-2 control-label">Category </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCategory" class="select-full" runat="server">
                            <asp:ListItem Value="" Selected="True">-Select-</asp:ListItem>
                            <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                            <asp:ListItem Value="OBC">OBC</asp:ListItem>
                            <asp:ListItem Value="SC">SC</asp:ListItem>
                            <asp:ListItem Value="ST">ST</asp:ListItem>
                            <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Provision Date</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtProvisionDate" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Contract Expiry Date </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContractExDate" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">PF Start Date</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPFStratDate" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">Grade </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGrade" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Marital Status</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtMaritalStatus" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Blood </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlblood" class="select-full" runat="server">
                            <asp:ListItem Value="">- Select Blood Group-</asp:ListItem>

                            <asp:ListItem Value="A_positive">A+</asp:ListItem>
                            <asp:ListItem Value="A_negative">A-</asp:ListItem>
                            <asp:ListItem Value="B_positive">B+</asp:ListItem>
                            <asp:ListItem Value="B_negative">B-</asp:ListItem>
                            <asp:ListItem Value="O_positive">O+</asp:ListItem>
                            <asp:ListItem Value="O_negative">O-</asp:ListItem>
                            <asp:ListItem Value="AB_positive">AB+</asp:ListItem>
                            <asp:ListItem Value="AB_negative">AB-</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Adhar No.</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtAdhar" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Driving Licence </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDrivLicnce" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Election Card</label>

                    <div class="col-sm-3">
                        <asp:TextBox ID="txtElectionCard" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label">Ration Card </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRationCard" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>

                    <div class="col-sm-3">
                    </div>

                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">

                        <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />


                    </div>
                </div>



            </div>
        </div>

    </div>

    <!-- /form components -->
</asp:Content>

