using System.IO;
using System.Web;
using System.Web.UI;

namespace TimCo_UI.Components.Widget
{
	public abstract class WidgetBuilderBase<TViewComponent, TBuilder> : IHtmlString
		where TViewComponent : WidgetBase
		where TBuilder : WidgetBuilderBase<TViewComponent, TBuilder>
	{
		protected internal TViewComponent Component { get; set; }

		/// <summary>
		/// Конструктор класса.
		/// </summary>
		/// <param name="component"></param>
		public WidgetBuilderBase(TViewComponent component)
		{
			Component = component;
		}

		#region Implementation of IHtmlString

		/// <summary>
		/// 
		/// </summary>
		/// <returns>HTML-кодированная строка.</returns>
		public string ToHtmlString()
		{
			StringWriter stringWriter = new StringWriter();
			Component.WriteHtml(new HtmlTextWriter(stringWriter));

			return stringWriter.ToString();
		}

		#endregion
	}
}