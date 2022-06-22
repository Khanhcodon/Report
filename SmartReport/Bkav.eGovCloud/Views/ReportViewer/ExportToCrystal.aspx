<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="CrystalDecisions.CrystalReports.Engine, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.CrystalReports.Engine" TagPrefix="R" %>
<%@ Register Assembly="CrystalDecisions.Shared, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Shared" TagPrefix="S" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            var report = new ReportDocument();
            report.Load(Session["ReportPath"] as string);
            var dataSource = Session["ReportSource"] as System.Data.DataSet;
            report.SetDataSource(dataSource);
            crViewer.ReportSource = report;
            // crViewer.DisplayGroupTree = false;
            crViewer.HasCrystalLogo = false;
            crViewer.DataBind();
        }
    </script>
    <title>ExportToCrystal</title>
</head>
<body>
    <form id="Form1" runat="server">
        <div>
            <label runat="server" id="aaa"></label>
            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"></CR:CrystalReportViewer>
        </div>
    </form>
</body>
</html>
