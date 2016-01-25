using TimCo_UI.Components.Widget;
using System.Web.Mvc.Ajax;

namespace TimCo_UI.Components.Hyperlink
{
	public interface IHyperlink : IHtmlAttributesContainer
	{
		string linkText { get; set; }
		string actionName { get; set; }
		string controllerName { get; set; }
		object routeValues { get; set; }
		string className { get; set; }
		string fragment { get; set; }
		AjaxOptions ajaxOptions { get; set; }
		MetaPolicyTypes metaPolicyType { get; set; }
		bool useMicrodata { get; set; }
	}
}
