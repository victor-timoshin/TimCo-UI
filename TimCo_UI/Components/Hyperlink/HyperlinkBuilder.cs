using TimCo_UI.Components.Widget;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace TimCo_UI.Components.Hyperlink
{
	public class HyperlinkBuilder : WidgetBuilderBase<Hyperlink, HyperlinkBuilder>
	{
		private readonly Hyperlink component;

		/// <summary>
		/// Конструктор класса.
		/// </summary>
		/// <param name="component"></param>
		public HyperlinkBuilder(Hyperlink component)
			: base(component)
		{
			this.component = component;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="linkText">Текст ссылки.</param>
		/// <returns></returns>
		public HyperlinkBuilder SetLinkText(string linkText)
		{
			this.component.linkText = linkText;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionName">Название серверного Action метода контроллера, куда поступит запрос на обработку.</param>
		/// <returns></returns>
		public HyperlinkBuilder SetActionName(string actionName)
		{
			this.component.actionName = actionName;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="controllerName">Название контроллера, куда поступит запрос в поисках Action метода для обработки.</param>
		/// <returns></returns>
		public HyperlinkBuilder SetControllerName(string controllerName)
		{
			this.component.controllerName = controllerName;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="routeValues"></param>
		/// <returns></returns>
		public HyperlinkBuilder SetRouteValues(object routeValues)
		{
			this.component.routeValues = routeValues;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public HyperlinkBuilder SetClassName(string className)
		{
			this.component.className = className;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fragment"></param>
		/// <returns></returns>
		public HyperlinkBuilder SetFragment(string fragment)
		{
			this.component.fragment = fragment;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ajaxOptions"></param>
		/// <returns></returns>
		public HyperlinkBuilder SetAjaxOptions(AjaxOptions ajaxOptions)
		{
			this.component.ajaxOptions = ajaxOptions;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public HyperlinkBuilder SetHtmlAttributes(object htmlAttributes)
		{
			this.component.htmlAttributes = htmlAttributes;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public HyperlinkBuilder SetMetaPolicy(MetaPolicyTypes type)
		{
			this.component.metaPolicyType = type;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public HyperlinkBuilder UseMicrodata(bool value)
		{
			this.component.useMicrodata = value;
			return this;
		}
	}
}
