<%
//js code build in server dynamically
%>

<script>
function saaaaaaa() {
    console.log(1);
    <% if(Request.Cookies["name"]!=null){ %>
        alert(1)
    <% } %>
    }
</script>

