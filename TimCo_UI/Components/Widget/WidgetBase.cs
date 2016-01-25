using System.Web.UI;

namespace TimCo_UI.Components.Widget
{
	/// <summary>
	/// Абстрактный класс виджета.
	/// </summary>
	public abstract class WidgetBase : IWidget
	{
		/// <summary>
		/// Абстрактный метод отрисовки виджета для клиента.
		/// </summary>
		/// <param name="writer">Html разметка.</param>
		public abstract void WriteHtml(HtmlTextWriter writer);
	}
}