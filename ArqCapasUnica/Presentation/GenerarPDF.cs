using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Presentation
{
    internal class GenerarPDF
    {

        SqlConnection connection = new SqlConnection("server=DESKTOP-8NTIIEU ; database=prueba ; integrated security = true");
        SqlCommand command;

        String consulta;
        /*Document document;
        FileStream fs;
        iTextSharp.text.Font n10;
        iTextSharp.text.Font n12;
        iTextSharp.text.Font ft;
    

        private void initializePDF() 
        {
            document = new Document(iTextSharp.text.PageSize.A4);

            PdfWriter pw = PdfWriter.GetInstance(document, fs);
            pw.PageEvent = new HeaderFooter();

            document.Open();
            BaseFont bfttimes = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            BaseFont bft = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, false);

            iTextSharp.text.Font n10 = new iTextSharp.text.Font(bfttimes, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font n12 = new iTextSharp.text.Font(bfttimes, 12f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font ft = new iTextSharp.text.Font(bft, 8f, iTextSharp.text.Font.NORMAL);
        }*/

        public void Generar() 
        {
            connection.Open();

            FileStream fs = new FileStream(@"C:\Users\Estilos\Desktop\Proyecto ALGORITMO\Resultados admisión 2022.pdf", FileMode.Create);
            Document document = new Document(iTextSharp.text.PageSize.A4);

            PdfWriter pw = PdfWriter.GetInstance(document, fs);
            pw.PageEvent = new HeaderFooter();

            document.Open();
            BaseFont bfttimes = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            BaseFont bft = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, false);

            iTextSharp.text.Font n10 = new iTextSharp.text.Font(bfttimes, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font n12 = new iTextSharp.text.Font(bfttimes, 12f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font ft = new iTextSharp.text.Font(bft, 8f, iTextSharp.text.Font.NORMAL);


            

            for (int i = 1; i <= 40; i++) 
            {
                consulta = "SELECT COUNT(*) FROM POSTULANTE WHERE ESPECIALIDAD = " + i;
                command = new SqlCommand(consulta, connection);
                int n = (int)command.ExecuteScalar();


                if (n != 0) 
                {
                    consulta = "SELECT nombreEspecialidad from Especialidad WHERE IDESPECIALIDAD = " + i;
                    command = new SqlCommand(consulta, connection);

                    String carrera = Convert.ToString(command.ExecuteScalar());
                    //GenerarTabla(i, n, carrera);

                    document.Add(new Paragraph("CARRERA: " + carrera, n10));
                    document.Add(new Paragraph("MODALIDAD: ORDINARIO"));
                    document.Add(Chunk.NEWLINE);
                    var tbl = new PdfPTable(new float[] { 5f, 7f, 15f, 7f, 5f, 10f }) { WidthPercentage = 100f };
                    var c7 = new PdfPCell(new Phrase("N° Ord.", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                    var c8 = new PdfPCell(new Phrase("CODIGO", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                    var c9 = new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                    var c10 = new PdfPCell(new Phrase("PUNTAJE", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                    var c11 = new PdfPCell(new Phrase("MERITO", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                    var c12 = new PdfPCell(new Phrase("CONDICION", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                    tbl.AddCell(c7);
                    tbl.AddCell(c8);
                    tbl.AddCell(c9);
                    tbl.AddCell(c10);
                    tbl.AddCell(c11);
                    tbl.AddCell(c12);

                    for (int j = 1; j <= n; j++)
                    {
                        consulta = "SELECT puntaje FROM (SELECT ROW_NUMBER() OVER (ORDER BY pun.PUNTAJE DESC) AS Orden, pos.PUNTAJE FROM Postulante pos INNER JOIN PUNTAJE " +
                            "pun ON pos.Puntaje = pun.IdPuntaje WHERE pos.Especialidad = " + i + ") POSTULANTE  WHERE ORDEN = " + j;

                        command = new SqlCommand(consulta, connection);
                        String cod = Convert.ToString(command.ExecuteScalar());


                        consulta = "SELECT NombresComp FROM (SELECT ROW_NUMBER() OVER (ORDER BY pun.PUNTAJE DESC) AS Orden, CONCAT_WS(' ', pos.ApePaterno, " +
                            "pos.ApeMaterno, pos.Nombre) NombresComp FROM Postulante pos INNER JOIN PUNTAJE pun ON " +
                            "pos.Puntaje = pun.IdPuntaje WHERE pos.Especialidad = " + i + ") POSTULANTE WHERE ORDEN = " + j;

                        command = new SqlCommand(consulta, connection);
                        String nomb = Convert.ToString(command.ExecuteScalar());


                        consulta = "SELECT puntaje FROM (SELECT ROW_NUMBER() OVER (ORDER BY pun.PUNTAJE DESC) AS Orden, pun.PUNTAJE FROM Postulante pos INNER JOIN PUNTAJE " +
                            "pun ON pos.Puntaje = pun.IdPuntaje WHERE pos.Especialidad = " + i + ") POSTULANTE  WHERE ORDEN = " + j;

                        command = new SqlCommand(consulta, connection);
                        String pntj = Convert.ToString(command.ExecuteScalar());

                        c7 = new PdfPCell(new Phrase(j.ToString(), ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                        c8 = new PdfPCell(new Phrase(cod, ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                        c9 = new PdfPCell(new Phrase(nomb, ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_JUSTIFIED, BorderColor = BaseColor.BLACK, Padding = 3f };
                        c10 = new PdfPCell(new Phrase(pntj, ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                        c11 = new PdfPCell(new Phrase(j.ToString(), ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                        c12 = new PdfPCell(new Phrase("INGRESO", ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                        tbl.AddCell(c7);
                        tbl.AddCell(c8);
                        tbl.AddCell(c9);
                        tbl.AddCell(c10);
                        tbl.AddCell(c11);
                        tbl.AddCell(c12);
                    }
                    document.Add(tbl);
                    document.NewPage();
                }
            }

            document.Close();
            connection.Close();

            MessageBox.Show("","HECHO");
        }

        /*
        private void GenerarTabla(int especialidad, int n, String carrera) 
        {
            document.Add(new Paragraph("CARRERA: " + carrera, n10));
            document.Add(new Paragraph("MODALIDAD: ORDINARIO"));
            document.Add(Chunk.NEWLINE);
            var tbl = new PdfPTable(new float[] { 5f, 7f, 15f, 7f, 5f, 10f }) { WidthPercentage = 100f };
            var c7 = new PdfPCell(new Phrase("N° Ord.", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
            var c8 = new PdfPCell(new Phrase("CODIGO", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
            var c9 = new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
            var c10 = new PdfPCell(new Phrase("PUNTAJE", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
            var c11 = new PdfPCell(new Phrase("MERITO", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
            var c12 = new PdfPCell(new Phrase("CONDICION", n10)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
            tbl.AddCell(c7);
            tbl.AddCell(c8);
            tbl.AddCell(c9);
            tbl.AddCell(c10);
            tbl.AddCell(c11);
            tbl.AddCell(c12);

            for (int i = 1; i <= n; i++)
            {
                consulta = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY pun.PUNTAJE DESC) AS Orden, pos.PUNTAJE FROM Postulante pos INNER JOIN PUNTAJE " +
                    "pun ON pos.Puntaje = pun.IdPuntaje WHERE pos.Especialidad = " + especialidad + ") POSTULANTE  WHERE ORDEN = " + i;
              
                command = new SqlCommand(consulta, connection);
                String cod = Convert.ToString(command.ExecuteScalar());


                consulta = "SELECT * FROM(SELECT ROW_NUMBER() OVER (ORDER BY pun.PUNTAJE DESC) AS Orden, CONCAT_WS(' ', pos.ApePaterno, pos.ApeMaterno, " +
                    "pos.Nombre) Nombres FROM Postulante pos INNER JOIN PUNTAJE pun ON pos.Puntaje = pun.IdPuntaje WHERE pos.Especialidad = " + especialidad + ") " +
                    "POSTULANTE WHERE ORDEN = " + i;

                command = new SqlCommand(consulta, connection);
                String nomb = Convert.ToString(command.ExecuteScalar());


                consulta = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY pun.PUNTAJE DESC) AS Orden, pun.PUNTAJE FROM Postulante pos INNER JOIN PUNTAJE " +
                    "pun ON pos.Puntaje = pun.IdPuntaje WHERE pos.Especialidad = " + especialidad + ") POSTULANTE  WHERE ORDEN = " + i;

                command = new SqlCommand(consulta, connection);
                String pntj = Convert.ToString(command.ExecuteScalar());

                c7 = new PdfPCell(new Phrase(i.ToString(), ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                c8 = new PdfPCell(new Phrase(cod, ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                c9 = new PdfPCell(new Phrase(nomb, ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_JUSTIFIED, BorderColor = BaseColor.BLACK, Padding = 3f };
                c10 = new PdfPCell(new Phrase(pntj, ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                c11 = new PdfPCell(new Phrase(i.ToString(), ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                c12 = new PdfPCell(new Phrase("INGRESO", ft)) { BorderWidthBottom = 1f, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = BaseColor.BLACK, Padding = 3f };
                tbl.AddCell(c7);
                tbl.AddCell(c8);
                tbl.AddCell(c9);
                tbl.AddCell(c10);
                tbl.AddCell(c11);
                tbl.AddCell(c12);
            }

            document.Add(tbl);
            document.NewPage();
        }*/


        class HeaderFooter : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                //base.OnEndPage(writer, document);
                //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Path.Combine("logo.jpg"));
                BaseFont bfttimes = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                iTextSharp.text.Font n10 = new iTextSharp.text.Font(bfttimes, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font n12 = new iTextSharp.text.Font(bfttimes, 12f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance(Path.Combine("logo.jpg"));
                logo2.ScalePercent(25f);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Path.Combine("descarga.jpg"));
                logo.ScalePercent(40f);


                var tb1 = new PdfPTable(new float[] { 20f, 60f, 20f }) { WidthPercentage = 100f };
                var c1 = new PdfPCell(logo2) { Rowspan = 4 };
                var c2 = new PdfPCell(new Phrase("UNIVERSIDAD NACIONAL ''SAN LUIS GONZAGA''", n12));
                var c3 = new PdfPCell(logo) { Rowspan = 4 };
                c1.Border = 0;
                c2.Border = 0;
                c3.Border = 0;
                c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tb1.AddCell(c1);
                tb1.AddCell(c2);
                tb1.AddCell(c3);
                c1 = new PdfPCell(new Phrase("", n12));
                c2 = new PdfPCell(new Phrase("COMISION EJECUTIVA CENTRAL DE ADMISION", n12));
                c3 = new PdfPCell(new Phrase("", n10));
                c1.Border = 0;
                c2.Border = 0;
                c3.Border = 0;
                c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tb1.AddCell(c2);
                c1 = new PdfPCell(new Phrase("", n10));
                c2 = new PdfPCell(new Phrase("EXAMEN DE ADMISION 2022", n12));
                c3 = new PdfPCell(new Phrase("", n10));
                c1.Border = 0;
                c2.Border = 0;
                c3.Border = 0;
                c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tb1.AddCell(c2);
                c1 = new PdfPCell(new Phrase("", n10));
                c2 = new PdfPCell(new Phrase("Calle las Palmeras N°187 - Urb. San Jose", n10));
                c3 = new PdfPCell(new Phrase("", n10));
                c1.Border = 0;
                c2.Border = 0;
                c3.Border = 0;
                c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tb1.AddCell(c2);
                document.Add(tb1);
                //Chunk linea=new Chunk(iTextSharp.text.pdf.draw.LineSeparator(5f,100f,BaseColor.RED,Element.ALIGN_CENTER,50f));
                var line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, 0f);
                document.Add(new Chunk(line));
                //ima
                document.Add(Chunk.NEWLINE);
                iTextSharp.text.Font n4 = new iTextSharp.text.Font(bfttimes, 14f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


            }
        }
    }
}
