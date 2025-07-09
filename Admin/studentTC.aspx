<%@ Page Language="VB" AutoEventWireup="false" CodeFile="studentTC.aspx.vb" Inherits="Admin_studentTC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style>
        input{
                border-top: 0px;
                border-left: 0px;
                border-right: 0px;
                border-bottom: 1px solid black;
                border-bottom-width: 100%;
                margin-bottom: 20px;
                margin-top: 5px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<div class="row">
  Book no.<input type="text" id="nop"name="s1">SL.NO <input type="text" id="Text1"name="s1">Admission Number<input type="text" id="Text2"name="s1">
    
</div>
<div class="row">
  1. Name of Pupil
  <input type="text" name="firstname" style="width:90%;">
</div>

<div class="row">
  2. Mother's Name
  <input type="text" id="Text3"name="mothersname" style="width:100%;">
</div>

<div class="row">
  3. Father's Name/ Gaurdian Name
  <input type="text" id="Text4"name="fgname" style="width:81%;">
</div>
  

<div class="row">
 4. Nationality
  <input type="text" id="Text5"name="Nationality"style="width:92%;">
</div>


<div class="row">
 5. Whether the candidate bolongs to Schedule Cast or Schedule Tribe or OBC
  <input type="text" id="Text6"name="scobc" style="width:58%;">
</div>

<div class="row">
 6. Date of First Admission in the School with class
  <input type="text" id="Text7"name="dfa" style="width:73%;">
</div>


<div class="row">
 7. Date of birth (in Christian Era) according to Admission Register (in Figures)
  <input type="text" id="Text8"name="dobe"style="width:58%;">
</div>


<div class="row">
 (In words)
  <input type="text" id="Text9"name="words1" style="width:94%;">
</div>


<div class="row">
 8. Class in which the pupil last Studied (In Figures)
   <input type="text" id="Text10"name="class" style="width:73%;">
</div>


<div class="row">
  (in Words)
  <input type="text" id="Text11"name="words2" style="width:94%;">
</div>

 
<div class="row">
 9. School/Board's Annual Examination last taken with result
   <input type="text" id="Text12"name="scboard" style="width:68%;">
</div>

<div class="row">
 10. Whether failed, if so once/twice int the same class
  <input type="text" id="Text13"name="failed" style="width:71%";>
</div>


<div class="row">
 11.Subject Studied 
  <input type="text" id="Text14"name="s1"> <input type="text" id="Text15"name="s2"> <input type="text" id="Text16"name="s2"> <input type="text" id="Text17"name="s2"> <input type="text" id="Text18"name="s2"> 
</div>

<div class="row">
 12. Whether qualified for promotion to the higher class<input type="text" id="Text19"name="s1">if so to which class <input type="text" id="Text20"name="s1">(in figures)<input type="text" id="Text21"name="s1">(in words)<input type="text" id="Text22"name="Nationality" style="width:100%";>
</div>

<div class="row">
 13. Month upto which the(pupil has paid) school dues/paid 
 <input type="text" id="Text23"name="Nationality" style="width:68%";>
</div>

<div class="row">
 14. Any fees Consession availed of, if so, the nature of such concession<input type="text" id="Text24"name="Nationality" style="width:62%"; > 
 </div>

<div class="row">
 15. Total number of working days<input type="text" id="Text25"name="Nationality" style="width:82%";> 
 </div>

<div class="row">
 16. Total number of working days present<input type="text" id="Text26"name="Nationality" style="width:78%";> 
 </div>

<div class="row">
 17. Whether the NCC cadet/Boy Scout/Girl Guide(details may be given)<input type="text" id="Text27"name="Nationality" style="width:62%";> 
 </div>

<div class="row">
 18. Games Played or extra-curricular activities in which the pupil usually took part(mentioned achievement level there in<input type="text" id="Text28"name="Nationality" style="width:36%";> 
 </div>

<div class="row">
 19. General Conduct<input type="text" id="Text29"name="Nationality" style="width:88%";> 
 </div>

<div class="row">
 20. Date of application for certificate<input type="text" id="Text30"name="Nationality" style="width:81%";> 
 </div>

<div class="row">
 21. Date of issues of certificate<input type="text" id="Text31"name="Nationality" style="width:83%";> 
 </div>

<div class="row">
 22. Reason for leaving the school<input type="text" id="Text32"name="Nationality" style="width:81%";> 
 </div>


<div class="row">
 23. Any other Remarks<input type="text" id="Text33"name="Nationality" style="width:86%";> 
 </div>

</div>
    </form>
</body>
</html>
