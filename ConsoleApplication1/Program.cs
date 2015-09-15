using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ClassLibrary1;
using Microsoft.Office.Interop.Word;

namespace ConsoleApplication1
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //new SimpleMefDemo().Run();

            //new Notifier().Notify("AAAAAAAAAA");

            new WordToPdf().ConvertToPdf(@"D:\Doc1.docx");


            //wordTest(@"D:\WordTemplate1.dotx");

            //Console.WriteLine("press any key to continue......");
            //Console.ReadKey();
        }

        private static void wordTest(string docPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(docPath);

            var application = new Microsoft.Office.Interop.Word.Application();
            var document = application.Documents.Open(docPath);

            var wt = new WordTemplate();

            var tbl0 = wt.findTable(document, "Company List");
            var tbl1 = wt.findTable(document, "Company List1");
            var tbl2 = wt.getTableByBookmarkName(document, "CompanyList");
            var tbl3 = wt.getTableByBookmarkName(document, "CompanyList2");

            //int a = 1;
        }
    }

    public class WordToPdf
    {
        public void ConvertToPdf(string docPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(docPath);

            var application = new Microsoft.Office.Interop.Word.Application();

            Object filename = docPath;
            Object confirmConversions = Type.Missing;
            Object readOnly = Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = Type.Missing;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object visible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = Type.Missing;

            var document = application.Documents.Open(ref filename,
                ref confirmConversions, ref readOnly, ref addToRecentFiles,
                ref passwordDocument, ref passwordTemplate, ref revert,
                ref writePasswordDocument, ref writePasswordTemplate,
                ref format, ref encoding, ref visible, ref openConflictDocument,
                ref openAndRepair, ref documentDirection, ref noEncodingDialog);

            document.ExportAsFixedFormat(string.Format(@"D:\{0}-{1}.pdf", fileName, Guid.NewGuid()),
                Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

            Object saveChanges = Type.Missing;
            Object originalFormat = Type.Missing;
            Object routeDocument = Type.Missing;

            document.Close(ref saveChanges, ref originalFormat, ref routeDocument);
            application.Quit(ref saveChanges, ref originalFormat, ref routeDocument);

        }

        //public Microsoft.Office.Interop.Word.Document WordDocument { get; set; }
    }

    public class WordTemplate
    {
        public WordTemplate()
        {
        }

        public Table findTable(Document doc, string searchText)
        {
            if (doc.Tables.Count > 0)
            {
                int iCount = 1;
                Table tbl;
                do
                {
                    tbl = (Table)doc.Tables[iCount];
                    //Select first cell and check it's value
                    string header = tbl.Cell(1, 1).Range.Text;
                    if (header.Contains(searchText))
                    {
                        return tbl;
                    }
                    iCount++;
                    Marshal.ReleaseComObject(tbl);
                } while (iCount < doc.Tables.Count + 1);
            }
            return null;
        }

        public Table getTableByBookmarkName(Document doc, string bookmarkName)
        {
            Table tbl = doc.Bookmarks[bookmarkName].Range.Tables[1];
            if (tbl != null)
                return tbl;
            return null;
        }

    }
}
