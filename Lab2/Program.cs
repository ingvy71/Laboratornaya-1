using System;
using System.Collections.Generic;
using System.Text;

namespace VisitorPatternExample
{
    
    public interface IDocumentElement
    {
        void Accept(IVisitor visitor);
    }

    
    public class Paragraph : IDocumentElement
    {
        public string Text { get; }

        public Paragraph(string text)
        {
            Text = text;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Image : IDocumentElement
    {
        public string Url { get; }

        public Image(string url)
        {
            Url = url;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Table : IDocumentElement
    {
        public string[,] Data { get; }

        public Table(string[,] data)
        {
            Data = data;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    
    public interface IVisitor
    {
        void Visit(Paragraph paragraph);
        void Visit(Image image);
        void Visit(Table table);
        string GetResult();
    }

    
    public class HtmlVisitor : IVisitor
    {
        private StringBuilder _builder = new StringBuilder();

        public void Visit(Paragraph paragraph)
        {
            _builder.AppendLine($"<p>{paragraph.Text}</p>");
        }

        public void Visit(Image image)
        {
            _builder.AppendLine($"<img src=\"{image.Url}\" />");
        }

        public void Visit(Table table)
        {
            _builder.AppendLine("<table>");
            for (int i = 0; i < table.Data.GetLength(0); i++)
            {
                _builder.AppendLine("<tr>");
                for (int j = 0; j < table.Data.GetLength(1); j++)
                {
                    _builder.AppendLine($"<td>{table.Data[i, j]}</td>");
                }
                _builder.AppendLine("</tr>");
            }
            _builder.AppendLine("</table>");
        }

        public string GetResult()
        {
            return _builder.ToString();
        }
    }

    
    public class MarkdownVisitor : IVisitor
    {
        private StringBuilder _builder = new StringBuilder();

        public void Visit(Paragraph paragraph)
        {
            _builder.AppendLine(paragraph.Text);
            _builder.AppendLine();
        }

        public void Visit(Image image)
        {
            _builder.AppendLine($"![image]({image.Url})");
        }

        public void Visit(Table table)
        {
            int rows = table.Data.GetLength(0);
            int cols = table.Data.GetLength(1);

            
            for (int j = 0; j < cols; j++)
            {
                _builder.Append($"| {table.Data[0, j]} ");
            }
            _builder.AppendLine("|");

            
            for (int j = 0; j < cols; j++)
            {
                _builder.Append("|---");
            }
            _builder.AppendLine("|");

            
            for (int i = 1; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _builder.Append($"| {table.Data[i, j]} ");
                }
                _builder.AppendLine("|");
            }
        }

        public string GetResult()
        {
            return _builder.ToString();
        }
    }

    
    public class Document
    {
        private List<IDocumentElement> _elements = new List<IDocumentElement>();

        public void AddElement(IDocumentElement element)
        {
            _elements.Add(element);
        }

        public string Export(IVisitor visitor)
        {
            foreach (var element in _elements)
            {
                element.Accept(visitor);
            }
            return visitor.GetResult();
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document();

            doc.AddElement(new Paragraph("Hello World"));
            doc.AddElement(new Image("https://picsum.photos/200"));

            string[,] tableData = {
                { "Name", "Age" },
                { "Alice", "25" },
                { "Bob", "30" }
            };

            doc.AddElement(new Table(tableData));

            
            var htmlVisitor = new HtmlVisitor();
            string html = doc.Export(htmlVisitor);

            Console.WriteLine("=== HTML ===");
            Console.WriteLine(html);

            
            var mdVisitor = new MarkdownVisitor();
            string markdown = doc.Export(mdVisitor);

            Console.WriteLine("=== Markdown ===");
            Console.WriteLine(markdown);
        }
    }
}