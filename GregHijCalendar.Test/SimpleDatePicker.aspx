<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimpleDatePicker.aspx.cs" Inherits="GregHijCalendar.Test.SimpleDatePicker" %>

<%@ Register Assembly="GregHijCalendar" Namespace="KhaledLabs" TagPrefix="cc1" %>









<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:GregHijCalendar ID="GregHijCalendar1" RegisterJQuery="true" runat="server" />  
    </div>
    </form>
</body>
</html>
