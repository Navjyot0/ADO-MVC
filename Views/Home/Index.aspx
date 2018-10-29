<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Home | Index</title>
</head>
<body>
    <div>
        <b>Countries</b>
        <ul>
            <% foreach (string Country in Model)%>
            <% { %>
            <li><%= Country%></li>
            <% } %>
        </ul>
        <b>States</b>
        <ul>
            <% foreach (string State in (List<string>)ViewData["State"])%>
            <% { %>
            <li>
                <%= State%></li>
            <% } %>
        </ul>
    </div>
</body>
</html>
