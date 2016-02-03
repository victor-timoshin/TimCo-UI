using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;

namespace TimCo_UI.Components.Table
{
	public abstract class TableColumnBase<T> : ITableColumn<T>
	{
		public string Name { get; set; }

		public string Title { get; set; }

		public bool SortEnabled { get; set; }

		protected Func<T, string> ValueConstraint;

		protected string ValuePattern;

		#region Implementation of ITableColumn<T>

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public ITableColumn<T> Named(string name)
		{
			Name = name;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public ITableColumn<T> Titled(string title)
		{
			Title = title;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pattern"></param>
		/// <returns></returns>
		public ITableColumn<T> Format(string pattern)
		{
			ValuePattern = pattern;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="constraint"></param>
		/// <returns></returns>
		//public ITableColumn<T> RenderValueAs(Func<T, string> constraint)
		//{
		//    ValueConstraint = constraint;
		//    return this;
		//}

		public ITableColumn<T> RenderValueAs(Func<T, IHtmlString> constraint)
		{
			Func<T, string> valueContraint = a => constraint(a).ToHtmlString();
			ValueConstraint = valueContraint;
			return this;
		}

		public ITableColumn<T> RenderValueAs(Func<T, Func<object, HelperResult>> constraint)
		{
			Func<T, string> valueContraint = a => constraint(a)(null).ToHtmlString();
			ValueConstraint = valueContraint;
			return this;
		}

		#endregion

		public abstract ITableCell GetCell(object instance);

		public abstract ITableCell GetValue(T instance);

		public abstract ITableColumn<T> Sortable(bool sort);
	}
}