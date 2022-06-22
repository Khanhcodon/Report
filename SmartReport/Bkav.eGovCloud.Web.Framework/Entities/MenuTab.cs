namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuTab
    {
        private MenuTab(string text, string action, string controller)
        {
            Text = text;
            Action = action;
            Controller = controller;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static MenuTab Create(string text, string action, string controller)
        {
            return new MenuTab(text, action, controller);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Controller { get; set; }
    }
}
