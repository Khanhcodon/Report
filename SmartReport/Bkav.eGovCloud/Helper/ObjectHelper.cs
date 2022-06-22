using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Helper
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Clone 1 đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="source">Đối tượng clone</param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        /// <summary>
        /// Gộp thông tin 2 đối tượng
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void CopyValues<T>(T target, T source)
        {
            var result = target;
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                try
                {
                    var value = prop.GetValue(source, null);
                    if (value != null)
                    {
                        prop.SetValue(target, value, null);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }

    public static class Render
    {
        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                    controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
    public class Pager //<T> : List<T>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        //IQueryable<T> dataSource,
        public Pager( int totalItems, int? page, int pageSize = 10)
        {
            try
            {
                var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
                var currentPage = page ?? 1;
                var startPage = currentPage - 5;
                var endPage = currentPage + 4;
                if (startPage <= 0)
                {
                    endPage -= (startPage - 1);
                    startPage = 1;
                }
                if (endPage > totalPages)
                {
                    endPage = totalPages;
                    if (endPage > 10)
                    {
                        startPage = endPage - 9;
                    }
                }
                TotalCount = totalItems;
                CurrentPage = currentPage;
                PageSize = pageSize;
                StartPage = startPage;
                EndPage = endPage;
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
                //this.AddRange(dataSource);
            }
            catch
            {
                // ignored
            }
        }
    }
}