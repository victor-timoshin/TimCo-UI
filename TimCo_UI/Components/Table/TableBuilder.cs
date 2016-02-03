using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimCo_UI.Components.Widget;

namespace TimCo_UI.Components.Table
{
	public class TableBuilder<TModel> : WidgetBuilderBase<Table<TModel>, TableBuilder<TModel>> where TModel : class
	{
		private readonly Table<TModel> _component;

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="component"></param>
		public TableBuilder(Table<TModel> component)
			: base(component)
		{
			_component = component;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="columnBuilder"></param>
		/// <returns></returns>
		public TableBuilder<TModel> Columns(Action<ITableColumnContainer<TModel>> columnBuilder)
		{
			columnBuilder(_component.Columns);
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public TableBuilder<TModel> Orientation(OrientationTypes type)
		{
			_component.Orientation = type;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public TableBuilder<TModel> Named(string tableName)
		{
			_component.TableName = tableName;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public TableBuilder<TModel> RowId(string rowId)
		{
			_component.RowId = rowId;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rowClass"></param>
		/// <returns></returns>
		public TableBuilder<TModel> RowClass(string rowClass)
		{
			_component.RowClass = rowClass;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="isPageable"></param>
		/// <returns></returns>
		public TableBuilder<TModel> IsPageable(bool isPageable)
		{
			_component.IsPageable = isPageable;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public TableBuilder<TModel> HtmlAttibutes(object htmlAttributes)
		{
			_component.htmlAttributes = htmlAttributes;
			return this;
		}
	}
}