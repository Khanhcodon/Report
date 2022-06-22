using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : HtmlExtensions; - public - Framework
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : 1 helper mở rộng cho HtmlHelper
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <param name="modelItems"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckboxListForEnum<T>(this HtmlHelper html, string name, T modelItems) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (T item in Enum.GetValues(typeof(T)).Cast<T>())
            {
                TagBuilder builder = new TagBuilder("input");
                long targetValue = Convert.ToInt64(item);
                long flagValue = Convert.ToInt64(modelItems);

                var text = item.ToString();
                Type type = item.GetType();
                MemberInfo[] memInfo = type.GetMember(item.ToString());
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        text = ((DescriptionAttribute)attrs[0]).Description;
                    }
                }

                if ((targetValue & flagValue) == targetValue)
                    builder.MergeAttribute("checked", "checked");
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", targetValue.ToString());
                builder.MergeAttribute("name", name);
                builder.InnerHtml = text;
                var li = string.Format("<li><label>{0}</label></li>", builder.ToString(TagRenderMode.Normal));
                sb.Append(li);
            }
            sb.Append("</ul>");
            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue));
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="htmlAttributes">Các thuộc tính html của dropdown</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, object htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), htmlAttributes);
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="htmlAttributes">Các thuộc tính html của dropdown</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, IDictionary<string, object> htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), htmlAttributes);
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="optionLabel">Giá trị đầu tiên trong dropdown (VD: Mời bạn chọn...)</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string optionLabel, string selectedValue)
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), optionLabel);
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="optionLabel">Giá trị đầu tiên trong dropdown (VD: Mời bạn chọn...)</param>
        /// <param name="htmlAttributes">Các thuộc tính html của dropdown</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string optionLabel, IDictionary<string, object> htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), optionLabel, htmlAttributes);
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="optionLabel">Giá trị đầu tiên trong dropdown (VD: Mời bạn chọn...)</param>
        /// <param name="htmlAttributes">Các thuộc tính html của dropdown</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string optionLabel, object htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), optionLabel, htmlAttributes);
        }

        /// <summary>
        /// Tạo dropdown list từ 1 Enum
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="name">Tên control</param>
        /// <param name="enumType">Kiểu enum</param>
        /// <param name="optionLabel">Giá trị đầu tiên trong dropdown (VD: Mời bạn chọn...)</param>
        /// <param name="htmlAttributes">Các thuộc tính html của dropdown</param>
        /// <param name="selectedValue">Giá trị sẽ được chọn sẵn</param>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString DropDownListEnum(this HtmlHelper htmlHelper, string name, Type enumType, string optionLabel, object htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownList(name, GetListEnum(enumType, selectedValue), optionLabel, htmlAttributes);
        }

        private static IEnumerable<SelectListItem> GetListEnum(Type enumeration, string selectedValue)
        {
            if (!enumeration.IsEnum)
            {
                throw new ArgumentException(@"passed type must be of Enum type", "enumeration");
            }
            return Enum.GetNames(enumeration)
                        .Select(member => new SelectListItem
                        {
                            Value = member,
                            Text = RetrieveDescription(member, enumeration) ?? member,
                            Selected = selectedValue == member
                        });
        }

        /// <summary>
        /// Lấy ra mô tả của từng thuộc tính trong enum
        /// </summary>
        /// <param name="name">Tên thuộc tính</param>
        /// <param name="enumeration">Kiểu enum</param>
        /// <returns></returns>
        public static String RetrieveDescription(string name, Type enumeration)
        {
            var mi = enumeration.GetMember(name);
            if (mi.Length > 0)
            {
                var attributes = mi[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return null;
        }

        /// <summary>
        /// Tạo label có gợi ý
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="displayHint">Hiển thị gợi ý: true và ngược lại: false</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString LabelHintFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool displayHint = true)
        {
            var result = new StringBuilder();
            if (displayHint)
            {
                var hint = GetHint(expression);
                if (string.IsNullOrWhiteSpace(hint))
                {
                    hint = htmlHelper.GetDisplayName(expression);
                }
                result.Append(htmlHelper.Hint(hint));
                result.Append("&nbsp;");
            }
            result.Append(htmlHelper.LabelFor(expression));
            return MvcHtmlString.Create(result.ToString());
        }

        /// <summary>
        /// Tạo label có thêm thông tin bắt buộc nhập (tự phát hiện) và hiển thị gợi ý
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <param name="displayHint">Hiển thị gợi ý: true và ngược lại: false</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString EgovLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool displayHint = false)
        {
            var result = new StringBuilder();
            if (displayHint)
            {
                var hint = GetHint(expression);
                if (string.IsNullOrWhiteSpace(hint))
                {
                    hint = htmlHelper.GetDisplayName(expression);
                }
                result.Append(htmlHelper.Hint(hint));
                result.Append("&nbsp;");
            }
            result.Append(htmlHelper.LabelFor(expression));

            Func<string, ModelMetadata, IEnumerable<ModelClientValidationRule>> clientValidationRuleFactory =
                (name, metadata) => ModelValidatorProviders.Providers.GetValidators(
                                        metadata ??
                                        ModelMetadata.FromStringExpression(name, htmlHelper.ViewData), htmlHelper.ViewContext)
                                            .SelectMany(v => v.GetClientValidationRules());
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var propertyName = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            var clientRules = clientValidationRuleFactory(propertyName, metaData);
            if (clientRules.Any(rule => rule.ValidationType.Equals("required")))
            {
                result.Append("&nbsp;<span class='spanRequire'>*</span>");
            }

            return MvcHtmlString.Create(result.ToString());
        }

        /// <summary>
        /// Tạo label có thêm thông tin bắt buộc nhập
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="expression">Tên thuộc tính trong model</param>
        /// <typeparam name="TModel">Kiểu của model</typeparam>
        /// <typeparam name="TProperty">Kiểu của thuộc tính</typeparam>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString LabelRequireFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var result = new StringBuilder();
            result.Append(htmlHelper.LabelFor(expression));
            result.Append("&nbsp;<span class='spanRequire'>*</span>");
            return MvcHtmlString.Create(result.ToString());
        }

        private static string GetHint<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var result = string.Empty;
            var body = (MemberExpression)expression.Body;
            var propertyName = body.Member.Name;
            var objProperty = typeof(TModel).GetProperty(propertyName);
            var attrs = objProperty.GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (!(attr is LocalizationDisplayName)) continue;

                var displayNameAttribute = attr as LocalizationDisplayName;
                result = displayNameAttribute.Hint;
            }
            return result;
        }

        private static string GetDisplayName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metaData.DisplayName ?? (metaData.PropertyName ?? System.Web.Mvc.ExpressionHelper.GetExpressionText(expression));
            return value;
        }

        /// <summary>
        /// Tạo gợi ý cho từng label (Mô tả chi tiết về trường dữ liệu đó giúp người dùng hiểu rõ hơn)
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="text">Nội dung</param>
        /// <returns>Chuỗi html</returns>
        public static MvcHtmlString Hint(this HtmlHelper htmlHelper, string text)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", ResolveUrl(htmlHelper, "~/Content/Images/help.png").ToHtmlString());
            builder.MergeAttribute("alt", text);
            builder.MergeAttribute("title", text);
            return MvcHtmlString.Create(builder.ToString());
        }

        /// <summary>
        /// Lấy ra đường dẫn tới các file trong hệ thống (Thực chất chính là Url.Content)
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="url">Đường dẫn</param>
        /// <returns>Đường dẫn chính xác</returns>
        public static MvcHtmlString ResolveUrl(this HtmlHelper htmlHelper, string url)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return MvcHtmlString.Create(urlHelper.Content(url));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="tabs"></param>
        /// <returns></returns>
        public static MvcHtmlString Tabs(this HtmlHelper helper, IEnumerable<MenuTab> tabs)
        {
            var route = helper.ViewContext.RequestContext.RouteData;
            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");
            var menu = "\n\n<ul id=\"menu\">";

            foreach (var tab in tabs)
            {
                if (controller == tab.Controller && action == tab.Action)
                    menu += "\n\t<li>" + helper.ActionLink(tab.Text, tab.Action,
                    tab.Controller, new { @class = "selected" }) + "</li>";
                else
                    menu += "\n\t<li>" + helper.ActionLink(tab.Text,
                    tab.Action, tab.Controller) + "</li>";
            }
            menu += "\n</ul>\n\n";
            return MvcHtmlString.Create(menu);
        }

        /// <summary>
        /// Trả về html menu từ site map
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString SiteMapMenu(this HtmlHelper htmlHelper)
        {
            var sb = new StringBuilder();
            sb.Append("<ul class='menu'>");

            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                sb.Append(RenderMenuNode(node, true));
            }

            sb.AppendLine("</ul>");

            return MvcHtmlString.Create(sb.ToString());
        }


        /// <summary>
        /// Trả về html menu từ site map
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString SiteMapMenuNavbar(this HtmlHelper htmlHelper)
        {
            var sb = new StringBuilder();
            sb.Append("<ul class='nav'>");

            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                sb.Append(RenderMenuNodeNavbar(node, true));
            }

            sb.AppendLine("</ul>");

            return MvcHtmlString.Create(sb.ToString());
        }

        private static string RenderMenuNode(SiteMapNode node, bool isRoot = false)
        {
            var sb = new StringBuilder();

            if (SiteMap.CurrentNode == node)
            {
                sb.AppendLine("<li class='selectedMenuItem'>");
            }
            else
            {
                sb.AppendLine("<li>");
            }
            var imageLink = node["image"].ToString();
            if (string.IsNullOrEmpty(imageLink))
            {
                imageLink = "clear.gif";
            }
            var rootClass = string.Empty;
            if (isRoot)
            {
                rootClass = "root";
            }
            sb.Append(string.Format("<i class='{1} {0}'></i>", imageLink, rootClass));
            sb.AppendFormat("<a href='{0}' class='{2}'>{1}</a>", new string[] { node.Url, node.Title, rootClass });
            if (node.ChildNodes.Count > 0)
            {
                sb.Append("<ul class='children'>");
                foreach (SiteMapNode child in node.ChildNodes)
                {
                    sb.Append(RenderMenuNode(child));
                }
                sb.Append("</ul>");
            }
            sb.AppendLine("</li>");
            return sb.ToString();
        }

        private static string RenderMenuNodeNavbar(SiteMapNode node, bool isRoot = false)
        {
            var sb = new StringBuilder();
            var listItemElement = isRoot ? "<li class='dropdown {0}'>" : "<li class='{0}'>";
            var className = node["class"].ToString();
            sb.AppendLine(string.Format(listItemElement, className));

            if (className.Equals("divider"))
            {
                sb.AppendLine("</li>");
                return sb.ToString();
            }

            var imageLink = node["image"].ToString();

            if (string.IsNullOrEmpty(imageLink))
            {
                imageLink = "clear.gif";
            }

            sb.AppendFormat("<a href='{0}' class='dropdown-toggle' data-target='#'>", isRoot ? "#" : node.Url);
            sb.AppendFormat("<i class='fa {0}'></i>", imageLink);
            if (isRoot)
            {
                sb.AppendFormat("<span class='navbar-label'>{0}</span></a>", node.Title);
            }
            else
            {
                sb.AppendFormat("{0}</a>", node.Title);
            }

            if (node.ChildNodes.Count > 0)
            {
                sb.Append("<ul class='dropdown-menu'>");
                foreach (SiteMapNode child in node.ChildNodes)
                {
                    sb.Append(RenderMenuNodeNavbar(child));
                }
                sb.Append("</ul>");
            }

            sb.AppendLine("</li>");
            return sb.ToString();
        }
    }
}
