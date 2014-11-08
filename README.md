Introduction 
======
This is an ASP.NET custom DatePicker control. It allows you to switch between Hijri and Gregorian dates easily. 

**Features:** 

 1. Plug and Play. 
 2. Support Gregorian and Hijri calendars  Dates
 2. Allow the user to convert between Hijri / Gregorian dates.
 3. Customizable 
	 4. Change style layout 
	 5. change messages
	 6. Dual or Single Calendars. 
 7.  Validatable contorl using validation controls. 


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
              
4- Configure Style:
	
	add the following css classes to your style sheet to change the layout of the control:
	
	.calTbl: table layout class
	.dayBox: day textbox layout class
	.monthBox: month textbox layout class
	.yearBox: year textbox layout class

