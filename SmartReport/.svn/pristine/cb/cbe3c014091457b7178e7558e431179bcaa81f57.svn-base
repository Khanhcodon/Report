<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <link href="../../../../Content/themes/base/jquery.ui.all.css" rel="stylesheet" />
    <title>Cấu hình mẫu phiếu in</title>
    <style type="text/css">
        .div-keys {
            width: 200px;
            float: left;
        }

        .div-editor {
            width: 950px;
            float: left;
            margin-left: 17px;
        }

        .div-keys select {
            width: 100%;
            border: none;
            margin-bottom: 10px;
            font-weight: bold;
        }

        .div-note {
            padding-left: 217px;
            margin-bottom: 10px;
            font-weight: bold;
        }

        .config ul {
            -webkit-padding-start: 0;
            -webkit-margin-before: 0;
            font-size: 13px;
        }

            .config ul li {
                cursor: pointer;
            }

                .config ul li:hover {
                    color: #3939f1;
                }

        .config #accordion h3 {
            padding-left: 5px;
            color: #c43434;
            font-weight: bold;
            background: transparent;
            border-radius: 0px;
            border: none;
            font-size:15px;
        }

        .config #accordion div {
            height: 285px;
            border: none;
        }

        .config .ui-accordion .ui-accordion-header .ui-icon {
            left: -0.5em;
        }

        .btn-save {
            padding: 8px 15px;
            background: #3939f1;
            color: white;
        }
    </style>

    <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/jquery/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/jquery/browser/jquery.browser.js"></script>
    <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/jquery/jquery-ui-1.8.22.modified.min.js"></script>
    <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/objeditor.js"></script>
    <script type="text/javascript" src="../../../../Scripts/bkav.egov/libs/underscore/underscore-1.8.3.min.js"></script>
    <script type="text/javascript" src="../../../../Scripts/require.js"></script>

    <script src="../../../../Scripts/bkav.egov/resource/egov.resources.bindresource.js"></script>
    <script type="text/C#" runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            spCul.InnerText = Bkav.eGovCloud.Helper.LangHelper.GetAdminLangCode();
            txtContent.Content = (string)Model.Content;
        }
    </script>

    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function insertKey(key) {
                var editor = window.$find("<%=txtContent.ClientID%>");
                editor.pasteHtml(key);
            }

            function Save() {
                var editor = window.$find("<%=txtContent.ClientID%>");
                var htmlContent = editor.get_html();
                var templateId = $("#TemplateId").val();
                var token = $("input[name='__RequestVerificationToken']").val();
                $.ajax({
                    url: '<%: Url.Action("SaveTemplate", "Template") %>',
                    data: { content: htmlContent, templateId: templateId, __RequestVerificationToken: token },
                    type: "POST",
                    success: function (data) {
                        if (data.success) {
                            window.close();
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.statusText);
                    }
                });
            }

            var existDoctype = JSON.parse('<%=Html.Raw(string.IsNullOrEmpty(ViewBag.ExistDoctype) ? "[]" : ViewBag.ExistDoctype) %>');
        </script>
    </telerik:RadCodeBlock>

    <script type="text/javascript">
        $(function () {
            checkExistDoctype();

            var cultureName = $("#spCul").text();
            $.getScript("../../../../Scripts/release/bkav.egov/resource/egov.resources." + cultureName + ".min.js", function () {
                $(document).bindResources(function () {
                    if (egov.resources.form && egov.resources.form.title) {
                        document.title = egov.resources.form.title;
                    }
                });
            })

            $("#config #accordion").accordion({
                collapsible: true
            });
        });

        function getKeys() {
            var formId = $("#Form").val();
            if (formId != "") {
                $.ajax({
                    url: '../GetKeys/Template',
                    data: { formId: formId },
                    success: function (result) {
                        if (result.success) {
                            var listKeys = $("<ul>");
                            $("#formKeyTemp").tmpl(JSON.parse(result.success)).appendTo(listKeys);
                            $("#formKeys").html(listKeys);
                        }
                    }
                });
            }
        }

        function getDoctypes() {
            var docfieldId = $("#Docfield").val();
            if (docfieldId > 0) {
                $.ajax({
                    url: '../GetDocTypes/Template',
                    data: { docfieldId: docfieldId },
                    success: function (result) {
                        if (result.success) {
                            $("#DoctypeId option[value!= '']").remove();
                            var doctypes = JSON.parse(result.success);
                            for (var i = 0; i < doctypes.length; i++) {
                                var $option = $("<option>").val(doctypes[i].Value).text(doctypes[i].Text);
                                $("#DoctypeId").append($option);
                            }
                            checkExistDoctype();
                        }
                    }
                });
            }
        }

        function getForms() {
            var doctypeId = $("#DoctypeId").val();
            if (doctypeId != "") {
                $.ajax({
                    url: '../GetForms/Template',
                    data: { doctypeId: doctypeId },
                    success: function (result) {
                        if (result.success) {
                            $("#Form option[value!= '']").remove();
                            var doctypes = JSON.parse(result.success);
                            for (var i = 0; i < doctypes.length; i++) {
                                var $option = $("<option>").val(doctypes[i].Value).text(doctypes[i].Text);
                                $("#Form").append($option);
                            }
                        }
                    }
                });
            }
        }

        function checkExistDoctype() {
            if ($("form").attr("name") == "AddChild") {
                $("#DoctypeId option").each(function () {
                    var doctypeId = $(this).val();
                    if (_.find(existDoctype, function (e) { return e == doctypeId; })) {
                        $(this).attr("disabled", "disabled");
                    }
                });
            }
        }
    </script>
</head>
<body>
    <span style="display: none" id="spCul" runat="server"></span>
    <telerik:RadScriptManager runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <form name="Config" method="post" action="Config" class="form">
        <input type="hidden" id="TemplateId" name="TemplateId" value="<%= Model.TemplateId %>" />
        <%=Html.AntiForgeryToken() %>
        <div class="config" id="config">
            <div class="div-keys">
                <%= Html.DropDownList("Docfield", null, (ViewBag.Docfield.Count != 1 ? "Lĩnh vực" : null), new { onchange = "getDoctypes()" }) %>
                <%= Html.DropDownList("DoctypeId", null, (ViewBag.DoctypeId.Count != 1 ? "Chọn loại hồ sơ" : null), new { onchange = "getForms()" }) %>
                <%= Html.DropDownList("Form", null, "Chọn", new { onchange = "getKeys()" }) %>
                <div id="accordion">
                    <h3 data-res="egov.resources.template.specialkey"></h3>
                    <div class="special-keys">
                        <ul id="specialKeys">
                            <% foreach (var key in (ViewBag.SpecialKeys as IEnumerable<SelectListItem>)) %>
                            <% { %>
                            <li>
                                <span onclick="insertKey('<%= key.Value%>')" title="<%= key.Text%>"><%= key.Text%></span>
                            </li>
                            <% } %>
                        </ul>
                    </div>
                    <h3 data-res="egov.resources.template.dbkey"></h3>
                    <div class="db-keys">
                        <ul id="dbKeys">
                            <% foreach (var key in (ViewBag.DbKeys as IEnumerable<SelectListItem>)) %>
                            <% {  %>
                            <li>
                                <span onclick="insertKey('<%= key.Value%>')" title="<%= key.Text%>"><%= key.Text%></span>
                            </li>
                            <% } %>
                        </ul>
                    </div>
                    <h3 data-res="egov.resources.template.questionkey"></h3>
                    <div class="question-keys">
                        <ul id="questionKeys">
                            <% foreach (var key in (ViewBag.QuestionKeys as IEnumerable<SelectListItem>)) %>
                            <% {  %>
                            <li>
                                <span onclick="insertKey('<%= key.Value%>')" title="<%= key.Text%>"><%= key.Text%></span>
                            </li>
                            <% } %>
                        </ul>
                    </div>

                     <h3 data-res="egov.resources.template.documentOnlineKey"></h3>
                    <div class="documentOnline-keys">
                        <ul id="documentOnlineKeys">
                            <% foreach (var key in (ViewBag.DocumentOnlineKeys as IEnumerable<SelectListItem>)) %>
                            <% {  %>
                            <li>
                                <span onclick="insertKey('<%= key.Value%>')" title="<%= key.Text%>"><%= key.Text%></span>
                            </li>
                            <% } %>
                        </ul>
                    </div>

                    <h3 data-res="egov.resources.template.commonKey"></h3>
                    <div class="common-keys">
                        <ul id="commonKeys">
                            <% foreach (var key in (ViewBag.CommonKeys as IEnumerable<SelectListItem>)) %>
                            <% {  %>
                            <li>
                                <span onclick="insertKey('<%= key.Value%>')" title="<%= key.Text%>"><%= key.Text%></span>
                            </li>
                            <% } %>
                        </ul>
                    </div>
                    <h3 data-res="egov.resources.template.keyfromform"></h3>
                    <div id="formKeys">
                    </div>
                </div>
            </div>
            <div class="div-editor">
                <div>
                    <input type="button" value="Lưu" data-res="egov.resources.common.saveBtn" runat="server" onclick="Save()" class="btn-save" />
                </div>
                <telerik:RadEditor ID="txtContent" Height="700px" Width="100%" ToolsFile="~/Toolbar.Xml"
                    runat="server" Skin="Sitefinity" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd">
                </telerik:RadEditor>

            </div>
        </div>
    </form>
</body>
</html>
