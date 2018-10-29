<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcApplication.Model.Employee>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee</title>
</head>
<body>
    <% if (Model.Id != 0)%>
    <% { %>
    <fieldset>
        <legend>Employee Details</legend>
        Id : <%: Model.Id%> <br />
        FirstName : <%: Model.FirstName%><br />
        LastName : <%: Model.LastName%><br />
        Age : <%: Model.Age%>
    </fieldset>
    <% } %>
    <% else %>
    <% { %>
            <label> No Employee Present with Id=<b><%=Url.RequestContext.RouteData.Values["id"]%></b></label>
    <% } %>
    <% using (Html.BeginForm()) { %>
    <p>
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
    <% } %>
</body>
</html>

