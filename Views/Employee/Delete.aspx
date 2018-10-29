<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcApplication.Model.Employee>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delete</title>
</head>
<body>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Employee Details</legend>
        <div class="display-label">Id : <%: Model.Id %></div>
        <div class="display-label">FirstName : <%: Model.FirstName %></div>
        <div class="display-label">LastName : <%: Model.LastName %></div>
        <div class="display-label">Age : <%: Model.Age %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>
</body>
</html>

