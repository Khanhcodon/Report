<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Cấu hình mẫu cho loại hồ sơ</title>
    <style type="text/css">
        html, body, form {
            height: 100%;
            width: 100%;
        }

        .left-panel {
            height: 100%;
            width: 250px;
            float: left;
        }

        .right-panel {
            height: 100%;
            min-width: 600px;
            float: left;
        }

        .list-keyitem {
            color: blue;
            cursor: pointer;
            text-decoration: underline;
            margin-bottom: 5px;
        }
    </style>

        <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/jquery/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/objeditor.js"></script>
    <script type="text/javascript" src="../../../../Scripts/require.js"></script>
    <script src="../../../../Scripts/bkav.egov/resource/egov.resources.bindresource.min.js"></script>



    <script type="text/C#" runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            spCul.InnerText = Bkav.eGovCloud.Helper.LangHelper.GetAdminLangCode();
            txtContent.Content = (string)ViewBag.Contents;
            sFormName.InnerText = ViewBag.FormName;
            HidFormId.Value = ViewBag.FormId.ToString();
            //HidDoctypeId.Value = ViewBag.DoctypeId.ToString();
        }
    </script>


    <script type="text/javascript">
        $(function () {
            ResizeWindows();
            var cultureName = $("#spCul").text();
            $.getScript("../../../../Scripts/bkav.egov/resource/egov.resources." + cultureName + ".min.js", function () {
                $(document).bindResources(function () {
                    if (egov.resources.form && egov.resources.form.title) {
                        document.title = egov.resources.form.title;
                    }
                });
            })
        });

        function ResizeWindows() {
            var totalWidth = $("html").innerWidth();
            var leftWidth = $(".left-panel").innerWidth();
            $(".right-panel").width(totalWidth - leftWidth - 10);
        }
        // su kien khi window resize
        $(window).bind('resize', function () {
            ResizeWindows();
        });
    </script>
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function InsertKey(key) {
                var editor = window.$find("<%=txtContent.ClientID%>");
                editor.pasteHtml(key);
            }

            function Save() {
                var editor = window.$find("<%=txtContent.ClientID%>");
                var htmlContent = editor.get_html();
                var formId = $("#HidFormId").val();
                var token = $("input[name='__RequestVerificationToken']").val();
                $.ajax({
                    url: '<%: Url.Action("SaveTemplate", "Form") %>',
                    data: { content: escape(htmlContent), id: formId, __RequestVerificationToken: token },
                    type: "POST",
                    success: function (data) {
                        if (data) {
                            window.close();
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.statusText);
                    }
                });
            }
        </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form runat="server" method="post">
        <span style="display: none" id="spCul" runat="server"></span>
        <%=Html.AntiForgeryToken() %>
        <input type="hidden" id="HidFormId" runat="server" />
        <input type="hidden" id="HidDoctypeId" runat="server" />
        <telerik:RadScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <div class="left-panel">
            <span data-res="egov.resources.form.template"></span> <strong><span id="sFormName" runat="server"></span></strong>
            <br />
            <span data-res="egov.resources.form.insertspecialvalue"></span>
            <ul class="list-key">
                <li class="list-keyitem">
                    <a onclick="InsertKey('@account') " data-res="egov.resources.form.account"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@username') " data-res="egov.resources.form.currentusername"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@department') " data-res="egov.resources.form.currentdepartment"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@docfield')" data-res="egov.resources.form.docfieldname"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@doctype')" data-res="egov.resources.form.doctypename"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@doccode')" data-res="egov.resources.form.doccode"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@datereceive')" data-res="egov.resources.form.receivedate"></a>
                </li>
                <li class="list-keyitem">
                    <a onclick="InsertKey('@dateappoint')" data-res="egov.resources.form.appointdate"></a>
                </li>
            </ul>
            <input type="button" value="" data-res="egov.resources.buttons.save" runat="server" onclick="Save()" />
        </div>
        <div class="right-panel">
            <telerik:RadEditor ID="txtContent" Height="650px" Width="100%" ToolsFile="~/Toolbar.Xml"
                runat="server" Skin="Sitefinity" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd">
            </telerik:RadEditor>
        </div>
    </form>

</body>
</html>
