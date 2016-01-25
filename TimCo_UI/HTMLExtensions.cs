using System.Web.Mvc;
using TimCo_UI.Components.Hyperlink;

namespace TimCo_UI
{
	public static class HTMLExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="linkText"></param>
		/// <param name="actionName"></param>
		/// <param name="controllerName"></param>
		/// <param name="routeValues"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static HyperlinkBuilder ActionLinkEx(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
		{
			var link = new Hyperlink(helper);

			link.linkText = linkText;
			link.actionName = actionName;
			link.controllerName = controllerName;
			link.routeValues = routeValues;
			link.htmlAttributes = htmlAttributes;
			link.className = string.Empty;
			link.fragment = string.Empty;
			link.metaPolicyType = MetaPolicyTypes.None;
			link.useMicrodata = false;

			return new HyperlinkBuilder(link);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ajaxHelper">Класс, предоставляющий поддержку отображения HTML в AJAX сценариях.</param>
		/// <param name="linkText">Текст ссылки.</param>
		/// <param name="actionName">Название серверного Action метода контроллера, куда поступит запрос на обработку.</param>
		/// <param name="controllerName">Название контроллера, куда поступит запрос в поисках Action метода для обработки.</param>
		/// <param name="routeValues"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static HyperlinkBuilder ActionLinkEx(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
		{
			var link = new Hyperlink(ajaxHelper);

			link.linkText = linkText;
			link.actionName = actionName;
			link.controllerName = controllerName;
			link.routeValues = routeValues;
			link.htmlAttributes = htmlAttributes;
			link.className = string.Empty;
			link.fragment = string.Empty;
			//link.ajaxOptions = null;
			link.metaPolicyType = MetaPolicyTypes.None;
			link.useMicrodata = false;

			return new HyperlinkBuilder(link);
		}
	}
}
