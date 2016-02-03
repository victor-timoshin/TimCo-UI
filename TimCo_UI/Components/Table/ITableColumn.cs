using System;
using System.Web;
using System.Web.WebPages;

namespace TimCo_UI.Components.Table
{
	public interface ITableColumn<T> : ITableColumn, IColumn<T>, ISortableColumn<T>
	{
	}

	public interface ITableColumn : ISortableColumn
	{
	}

	public interface IColumn<T>
	{
		ITableColumn<T> Named(string name);

		ITableColumn<T> Titled(string title);

		//ITableColumn<T> RenderValueAs(Func<T, string> constraint);

		ITableColumn<T> RenderValueAs(Func<T, IHtmlString> constraint);

		ITableColumn<T> RenderValueAs(Func<T, Func<object, HelperResult>> constraint);

		ITableColumn<T> Format(string pattern);
	}

	public interface IColumn
	{
		string Name { get; set; }

		string Title { get; }

		ITableCell GetCell(object instance);
	}

	public interface ISortableColumn<T> : IColumn
	{
		/// <summary>
		/// Включаем / Отключаем сортировку
		/// </summary>
		/// <param name="sort"></param>
		/// <returns></returns>
		ITableColumn<T> Sortable(bool sort);
	}

	public interface ISortableColumn : IColumn
	{
		bool SortEnabled { get; }
	}
}