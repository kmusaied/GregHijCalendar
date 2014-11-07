Introduction 
======
This is an ASP.NET custom DatePicker control. It allows you to swich between Hijri and Gergorain dates easly. 


Getting Started 
========

1- Install package from nuget (https://www.nuget.org/packages/GregHijCalendar/)

2- Register control 

    <%@ Register Assembly="GregHijCalendar" Namespace="KhaledLabs" TagPrefix="cc1" %>
    
3- Add Control 

    <cc1:GregHijCalendar ID="GregHijCalendar1" 
             RegisterJQuery ="false"
             DefaultCalendar ="Hijri"
             DualCalendar ="true"
             GregorianCalendarCaption ="G"
             HijriCalendarCaption ="H"
             InvalidDateErrorMessage="Invalid Date"
              runat="server" />  
              


