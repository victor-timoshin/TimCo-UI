using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;
using TimCo_UI.Components.Widget;

namespace TimCo_UI.Components.Table
{
	public class Table<TModel> : WidgetBase, ITable where TModel : class
	{
		private readonly ITableColumnBuilder<TModel> _columnBuilder;
		private readonly ITableColumnContainer<TModel> _columnsContainer;

		protected IEnumerable<TModel> _dataSource;

		/// <summary>
		/// Конструктор класса.
		/// </summary>
		/// <param name="dataSource"></param>
		public Table(IEnumerable<TModel> dataSource)
		{
			_columnBuilder = new TableColumnBuilder<TModel>(this);
			_columnsContainer = new TableColumnContainer<TModel>(_columnBuilder);

			_dataSource = dataSource;
		}

		public ITableColumnContainer<TModel> Columns
		{
			get { return _columnsContainer; }
		}

		public IEnumerable<TModel> DataSource
		{
			get { return _dataSource; }
		}

		#region WidgetBase

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void WriteHtml(HtmlTextWriter writer)
		{
			RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
			foreach (var attribute in routeValues)
				writer.AddAttribute(attribute.Key, attribute.Value.ToString());

			writer.AddAttribute("role", TableName);
			writer.RenderBeginTag(HtmlTextWriterTag.Table);

			#region Head

			writer.AddAttribute("role", "rowgroup");
			writer.RenderBeginTag(HtmlTextWriterTag.Thead);

			writer.AddAttribute("role", "row");
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);

			foreach (var column in Columns)
			{
				writer.AddAttribute("role", "columnheader");
				//writer.AddAttribute("data-role", "sorter");
				writer.RenderBeginTag(HtmlTextWriterTag.Th);
				writer.Write("<a href='#'>{0}</a>", column.Title);
				writer.RenderEndTag(); // Th
			}

			writer.RenderEndTag(); // Tr

			writer.RenderEndTag(); // Thead

			#endregion

			#region Body

			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

			if (Orientation == OrientationTypes.Horizontal)
			{
				foreach (var item in DataSource)
				{
					if (!string.IsNullOrEmpty(RowId))
						writer.AddAttribute("id", item.GetType().GetProperty(RowId).GetValue(item, null).ToString());

					if (!string.IsNullOrEmpty(RowClass))
					{
						var isRead = item.GetType().GetProperty(RowClass).GetValue(item, null).ToString();
						writer.AddAttribute("class", (isRead == "True") ? "read" : "unread");
					}

					writer.AddAttribute("role", "row");
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);

					foreach (var column in Columns)
					{
						ITableCell cell = column.GetCell(item);

						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						writer.Write(cell.Value);
						writer.RenderEndTag(); // Td
					}

					writer.RenderEndTag(); // Tr
				}
			}
			else
			{
				ItemRenderGroup(writer);
			}

			writer.RenderEndTag(); // Tbody

			#endregion

			#region Foot

			if (IsPageable)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);

				writer.RenderBeginTag(HtmlTextWriterTag.Tr);

				writer.AddAttribute("colspan", Enumerable.Count(Columns).ToString());
				writer.RenderBeginTag(HtmlTextWriterTag.Td);

				writer.AddAttribute("class", "table-pager-wrapper");
				writer.AddAttribute("data-role", "pager");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);


				writer.WriteLine("<span class='table-pager-prev'><i class='glyphicon glyphicon-chevron-left'></i> Prev</span>");
				writer.WriteLine("<span class='table-pager-info'>Showing {0} of {1} items</span>", Enumerable.Count(DataSource), Enumerable.Count(DataSource));
				writer.WriteLine("<span class='table-pager-next'>Next <i class='glyphicon glyphicon-chevron-right'></i></span>");


				writer.RenderEndTag(); // Div

				writer.RenderEndTag(); // Td

				writer.RenderEndTag(); // Tr

				writer.RenderEndTag(); // Tfoot
			}

			#endregion

			writer.RenderEndTag(); // Table
		}

		#endregion

		#region Implementation of ITable

		ITableColumnContainer ITable.Columns { get { return Columns; } }
		public OrientationTypes Orientation { get; set; }
		public string TableName { get; set; }
		public string RowId { get; set; }
		public string RowClass { get; set; }
		public bool IsPageable { get; set; }
		public object htmlAttributes { get; set; }

		#endregion

		#region Private methods

		internal void ItemRenderGroup(HtmlTextWriter writer)
		{
			for (int i = 0; i < Columns.Count(); i++)
				ItemRender(writer, i);
		}

		internal void ItemRender(HtmlTextWriter writer, int index)
		{
			writer.AddAttribute("role", "row");
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);

			foreach (var item in DataSource)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);

				ITableCell cell = Columns.ElementAt(index).GetCell(item);
				writer.Write(cell.Value);
				writer.RenderEndTag(); // Td
			}

			writer.RenderEndTag(); // Tr
		}

		#endregion
	}
}