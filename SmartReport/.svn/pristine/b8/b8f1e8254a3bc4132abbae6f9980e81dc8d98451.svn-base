using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// 
    /// </summary>
    public class RectAndText
    {
        /// <summary>
        /// 
        /// </summary>
        public iTextSharp.text.Rectangle Rect;

        /// <summary>
        /// 
        /// </summary>
        public String Text;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        public RectAndText(iTextSharp.text.Rectangle rect, String text)
        {
            this.Rect = rect;
            this.Text = text;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomTextExtractionStrategy : LocationTextExtractionStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RectAndText> myPoints = new List<RectAndText>();

        /// <summary>
        /// 
        /// </summary>
        public String TextToSearchFor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Globalization.CompareOptions CompareOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textToSearchFor"></param>
        /// <param name="compareOptions"></param>
        public CustomTextExtractionStrategy(String textToSearchFor, System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
        {
            this.TextToSearchFor = textToSearchFor;
            this.CompareOptions = compareOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderInfo"></param>
        public override void RenderText(TextRenderInfo renderInfo)
        {
            base.RenderText(renderInfo);

            //See if the current chunk contains the text
            var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(renderInfo.GetText(), this.TextToSearchFor, this.CompareOptions);

            //If not found bail
            if (startPosition < 0)
            {
                return;
            }

            //Grab the individual characters
            var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take(this.TextToSearchFor.Length).ToList();

            //Grab the first and last character
            var firstChar = chars.First();
            var lastChar = chars.Last();


            //Get the bounding box for the chunk of text
            var bottomLeft = firstChar.GetDescentLine().GetStartPoint();
            var topRight = lastChar.GetAscentLine().GetEndPoint();

            //Create a rectangle from it
            var rect = new iTextSharp.text.Rectangle(
                                                    bottomLeft[Vector.I1],
                                                    bottomLeft[Vector.I2],
                                                    topRight[Vector.I1],
                                                    topRight[Vector.I2]
                                                    );

            //Add this to our main collection
            this.myPoints.Add(new RectAndText(rect, this.TextToSearchFor));
        }
    }
}
