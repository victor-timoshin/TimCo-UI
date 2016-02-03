using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TimCo_UI.Components.Hyperlink;
using TimCo_UI.Components.Table;

namespace TimCo_UI
{
	public static class HTMLExtensions
	{
		#region ActionLink

		/// <summary>
		/// 
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="linkText">Текст гиперссылки.</param>
		/// <param name="actionName">Название серверного Action метода контроллера, куда поступит запрос на обработку.</param>
		/// <param name="controllerName">Название контроллера, куда поступит запрос в поисках Action метода для обработки.</param>
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
		/// <param name="linkText">Текст гиперссылки.</param>
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

		#endregion

		#region DropDownList

		private static readonly SelectListItem[] _singleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelMetadata"></param>
		/// <returns></returns>
		private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
		{
			Type realModelType = modelMetadata.ModelType;
			Type underlyingType = Nullable.GetUnderlyingType(realModelType);

			if (underlyingType != null)
				realModelType = underlyingType;

			return realModelType;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetEnumDescription<TEnum>(TEnum value)
		{
			FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if ((attributes != null) && (attributes.Length > 0))
				return attributes[0].Description;
			else
				return value.ToString();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="htmlHelper"></param>
		/// <param name="expression"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownListForEx<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			Type enumType = GetNonNullableModelType(metadata);
			IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

			IEnumerable<SelectListItem> items = from value in values select new SelectListItem
			{
				Text = GetEnumDescription(value),
				Value = value.ToString(),
				Selected = value.Equals(metadata.Model)
			};

			if (metadata.IsNullableValueType)
				items = _singleEmptyItem.Concat(items);

			return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="htmlHelper"></param>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownListForEx<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
		{
			return EnumDropDownListForEx(htmlHelper, expression, null);
		}

		#endregion

		#region Table

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="helper"></param>
		/// <param name="dataSource"></param>
		/// <returns></returns>
		public static TableBuilder<T> TableEx<T>(this HtmlHelper helper, IEnumerable<T> dataSource)
			where T : class
		{
			var table = new Table<T>(dataSource);
			table.Orientation = OrientationTypes.Horizontal;
			table.IsPageable = false;
			return new TableBuilder<T>(table);
		}

		#endregion
	}
}