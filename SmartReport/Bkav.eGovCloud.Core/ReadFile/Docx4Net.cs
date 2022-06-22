using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Words.NET;
using System.Drawing;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// 
    /// </summary>
    public class Docx4Net
    {

        private static Random rand = new Random();

        /// <summary>
        /// 
        /// </summary>
        public static void ExportDocx(string pathTemplate, DataTable data, string fileName)
        {
            using (DocX document = DocX.Load(pathTemplate))
            {
                // get the table with caption "GROCERY_LIST" from the document.
                var rowTemplate = 1;
                var groceryListTable = document.Tables.FirstOrDefault(t => t.TableCaption == "GROCERY_LIST");
                var templateRow = document.Paragraphs.FirstOrDefault(t => t.Text.Contains("{TemplateRow}"));
                var paragAppendChartBar = document.Paragraphs.FirstOrDefault(t => t.Text.Contains("{ReportChartBar}"));
                var paragAppendChartLine = document.Paragraphs.FirstOrDefault(t => t.Text.Contains("{ReportChartLine}"));
                var paragAppendChartPie = document.Paragraphs.FirstOrDefault(t => t.Text.Contains("{ReportChartPie}"));
                if (templateRow != null)
                {
                    var countTxt = templateRow.Text.Replace("{TemplateRow}=", "");
                    var count = 1;
                    var countRowTemp = int.TryParse(countTxt, out count);
                    if (countRowTemp)
                    {
                        rowTemplate = count;
                    }

                    templateRow.Remove(false);
                }

                // Create a bar chart.
                if (paragAppendChartBar != null)
                {
                    paragAppendChartBar.ReplaceText("{ReportChartBar}", "");
                    var barchart = renderBarChart(data);
                    document.InsertChartAfterParagraph(barchart, paragAppendChartBar);
                }
                if (paragAppendChartLine != null)
                {
                    paragAppendChartLine.ReplaceText("{ReportChartLine}", "");
                    var barchart = renderLineChart(data);
                    document.InsertChartAfterParagraph(barchart, paragAppendChartLine);
                }

                if (paragAppendChartPie != null)
                {
                    paragAppendChartPie.ReplaceText("{ReportChartPie}", "");
                    var barchart = renderPieChart(data);
                    document.InsertChartAfterParagraph(barchart, paragAppendChartPie);
                }

                if (groceryListTable == null)
                {
                    Console.WriteLine("\tError, couldn't find table with caption GROCERY_LIST in current document.");
                }
                else
                {
                    if (groceryListTable.RowCount > 1)
                    {
                        // Get the row pattern of the second row.
                        var rowPattern = groceryListTable.Rows[rowTemplate];


                        foreach (DataRow row in data.Rows)
                        {
                            var newItem = groceryListTable.InsertRow(rowPattern, groceryListTable.RowCount - 1);
                            foreach (DataColumn column in data.Columns)
                            {
                                if (row[column] != null) // This will check the null values also (if you want to check).
                                {
                                    newItem.ReplaceText(string.Format("%{0}%", column), (string)row[column]);
                                }
                            }
                        }

                        rowPattern.Remove();
                    }
                }

                document.SaveAs(fileName);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataChart"></param>
        /// <returns></returns>
        public static BarChart renderBarChart(DataTable dataChart)
        {
            var c = new BarChart();
            c.AddLegend(ChartLegendPosition.Left, false);
            c.BarDirection = BarDirection.Bar;
            c.BarGrouping = BarGrouping.Standard;
            c.GapWidth = 200;
            foreach (DataRow row in dataChart.Rows)
            {
                var s1 = new Series((string)row[0]);
                s1.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)); ;
                    var objects = new List<object>();
                    var columnFirst = true;
                foreach (DataColumn column in dataChart.Columns)
                {
                    float a = 0;
                    if (row[column] != null && float.TryParse((string)row[column], out a)) // This will check the null values also (if you want to check).
                    {
                        if (!columnFirst)
                        {
                            objects.Add(new { Key = column, Value = (string)row[column] });
                        }
                        columnFirst = false;
                    }
                }
                s1.Bind(objects, "Key", "Value");
                c.AddSeries(s1);
            }

            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataChart"></param>
        /// <returns></returns>
        public static LineChart renderLineChart(DataTable dataChart)
        {
            var c = new LineChart();
            c.AddLegend(ChartLegendPosition.Left, false);
            foreach (DataRow row in dataChart.Rows)
            {
                var s1 = new Series((string)row[0]);
                s1.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                var objects = new List<object>();
                var columnFirst = true;
                foreach (DataColumn column in dataChart.Columns)
                {
                    float a = 0;
                    if (row[column] != null && float.TryParse((string)row[column], out a)) // This will check the null values also (if you want to check).
                    {
                        if (!columnFirst)
                        {
                            objects.Add(new { Key = column, Value = (string)row[column] });
                        }
                        columnFirst = false;
                    }
                }
                s1.Bind(objects, "Key", "Value");
                c.AddSeries(s1);
            }

            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataChart"></param>
        /// <returns></returns>
        public static PieChart renderPieChart(DataTable dataChart)
        {
            var c = new PieChart();
            c.AddLegend(ChartLegendPosition.Left, false);
                var s1 = new Series("Chart");
                var objects = new List<object>();
            foreach (DataRow row in dataChart.Rows)
            {
                //s1.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)); ;
                objects.Add(new { Key = (string)row[0], Value = (string)row[1] });

            }
            s1.Bind(objects, "Key", "Value");

            c.AddSeries(s1);

            return c;
        }
    }
}
