<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WithJquery.aspx.cs" Inherits="GregHijCalendar.Test.SimpleDatePicker" %>

<%@ Register Assembly="GregHijCalendar" Namespace="KhaledLabs" TagPrefix="cc1" %>









<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:GregHijCalendar ID="GregHijCalendar1"  runat="server" />  
    </div>
    </form>
</body>
</html>
