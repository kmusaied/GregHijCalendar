<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullProperties.aspx.cs" Inherits="GregHijCalendar.Test.FullProperties" %>


<%@ Register Assembly="GregHijCalendar" Namespace="KhaledLabs" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:GregHijCalendar ID="GregHijCalendar1" 
             RegisterJQuery ="false"
             DefaultCalendar ="Hijri"
             DualCalendar ="true"
             GregorianCalendarCaption ="ميلادي"
             HijriCalendarCaption ="هجري"
             InvalidDateErrorMessage="تاريخ غير صالح"
              runat="server" />  
    </div>
    </form>
</body>
</html>
