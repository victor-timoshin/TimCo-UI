using TimCo_UI.Components.Widget;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.Web.UI;

namespace TimCo_UI.Components.Hyperlink
{
	public class Hyperlink : WidgetBase, IHyperlink
	{
		private readonly ViewContext _viewContext = null;
		private readonly RouteCollection _routeCollection = null;

		/// <summary>
		/// Конструктор класса.
		/// </summary>
		public Hyperlink(HtmlHelper htmlHelper)
		{
			_viewContext = htmlHelper.ViewContext;
			_routeCollection = htmlHelper.RouteCollection;
		}

		/// <summary>
		/// Конструктор класса.
		/// </summary>
		public Hyperlink(AjaxHelper htmlHelper)
		{
			_viewContext = htmlHelper.ViewContext;
			_routeCollection = htmlHelper.RouteCollection;
		}

		#region Implementation of IHyperlink

		public string linkText { get; set; }
		public string actionName { get; set; }
		public string controllerName { get; set; }
		public object routeValues { get; set; }
		public string className { get; set; }
		public string fragment { get; set; }
		public AjaxOptions ajaxOptions { get; set; }
		public object htmlAttributes { get; set; }
		public MetaPolicyTypes metaPolicyType { get; set; }
		public bool useMicrodata { get; set; }

		#endregion

		#region Implementation of WidgetBase

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void WriteHtml(HtmlTextWriter writer)
		{
			if (string.IsNullOrWhiteSpace(controllerName))
				controllerName = _viewContext.RequestContext.RouteData.GetRequiredString("controller");

			UrlHelper urlHelper = new UrlHelper(_viewContext.RequestContext, _routeCollection);
			var url = urlHelper.Action(actionName, controllerName, routeValues);
			if (!string.IsNullOrWhiteSpace(fragment))
				url = string.Format("{0}#{1}", url, fragment);

			writer.AddAttribute("href", url);

			RouteValueDictionary attributes = new RouteValueDictionary(AnonymousObjectToHtmlAttributes(htmlAttributes));
			foreach (var attribute in attributes)
				writer.AddAttribute(attribute.Key, attribute.Value.ToString());

			if (!string.IsNullOrWhiteSpace(className))
				writer.AddAttribute("class", className);

			if (ajaxOptions != null)
			{
				RouteValueDictionary ajaxAttributes = new RouteValueDictionary(ajaxOptions.ToUnobtrusiveHtmlAttributes());

				foreach (var attribute in ajaxAttributes)
					writer.AddAttribute(attribute.Key, attribute.Value.ToString());
			}

			if (metaPolicyType == MetaPolicyTypes.Follow)
				writer.AddAttribute("rel", "follow");
			else if (metaPolicyType == MetaPolicyTypes.NoFollow)
				writer.AddAttribute("rel", "nofollow");

			if (useMicrodata)
				writer.AddAttribute("itemprop", "url"); // URL элемента навигации.

			writer.RenderBeginTag(HtmlTextWriterTag.A);

			if (useMicrodata)
			{
				writer.AddAttribute("itemprop", "title");
				writer.RenderBeginTag(HtmlTextWriterTag.Span); // Название элемента навигации.
				writer.WriteLine(linkText);
				writer.RenderEndTag(); // Span
			}
			else
				writer.WriteLine(linkText);

			writer.RenderEndTag(); // A
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		private static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
		{
			RouteValueDictionary result = new RouteValueDictionary();
			if (htmlAttributes != null)
			{
				foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(htmlAttributes))
					result.Add(property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
			}

			return result;
		}

		#endregion
	}
}