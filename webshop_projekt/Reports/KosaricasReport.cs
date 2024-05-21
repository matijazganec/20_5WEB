﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using webshop_projekt.Controllers;
using webshop_projekt.Models;

namespace webshop_projekt.Reports
{
    public class KosaricasReport
    {
        public byte[] Podaci { get; set; }

        private PdfPCell GenerirajCeliju(string sadrzaj, Font font, BaseColor boja, bool wrap)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(sadrzaj,font));
            c1.BackgroundColor = boja;
            c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            c1.Padding = 5;
            c1.NoWrap = wrap;
            c1.Border = Rectangle.BOTTOM_BORDER;
            c1.BorderColor = BaseColor.LIGHT_GRAY;
            return c1;
        }

        public void ListaKosarica(List<KosaricaItem> kosaricas)
        {
            BaseFont bfontZaglavlje = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            BaseFont bfontText = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            BaseFont bfontPodnozje = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1250, false);

            Font fontZaglavlje = new Font(bfontZaglavlje, 12, Font.NORMAL, BaseColor.DARK_GRAY);
            Font fontZaglavljeBold = new Font(bfontZaglavlje, 12, Font.BOLD, BaseColor.DARK_GRAY);
            Font fontNaslov = new Font(bfontText, 14, Font.BOLD, BaseColor.DARK_GRAY);
            Font fontTablicaZaglavlje = new Font(bfontText, 10, Font.BOLD, BaseColor.WHITE);
            Font fontText = new Font(bfontText, 10, Font.NORMAL, BaseColor.BLACK);

            BaseColor tPozadinaZaglavlje = new BaseColor(121, 65, 11);
            BaseColor tPozadinaSadrzaj = new BaseColor(255,255,255);

            using(MemoryStream mstream = new MemoryStream())
            {
                using(Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50))
                {
                    PdfWriter.GetInstance(pdfDokument, mstream).CloseStream = false;
                    pdfDokument.Open();
                    PdfPTable tZaglavlje = new PdfPTable(2);
                    tZaglavlje.HorizontalAlignment = Element.ALIGN_LEFT;
                    tZaglavlje.DefaultCell.Border = Rectangle.NO_BORDER;
                    tZaglavlje.WidthPercentage = 100f;
                    float[] sirinaKolonaZag = new float[] { 1f, 3f };
                    tZaglavlje.SetWidths(sirinaKolonaZag);

                    Paragraph info = new Paragraph();
                    info.Alignment = Element.ALIGN_RIGHT;
                    info.SetLeading(0, 1.2f);
                    info.Add(new Chunk("PAUP Web Shop \n", fontZaglavljeBold));
                    info.Add(new Chunk("matija.zganec@mev.hr", fontZaglavlje));

                    PdfPCell cInfo = new PdfPCell();
                    cInfo.AddElement(info);
                    cInfo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cInfo.VerticalAlignment = Element.ALIGN_TOP;
                    cInfo.Border = Rectangle.NO_BORDER;
                    tZaglavlje.AddCell(cInfo);

                    pdfDokument.Add(tZaglavlje);

                    Paragraph pNaslov  = new Paragraph("Popis proizvoda", fontNaslov);
                    pNaslov.Alignment = Element.ALIGN_CENTER;
                    pNaslov.SpacingBefore = 20;
                    pNaslov.SpacingAfter = 20;
                    pdfDokument.Add(pNaslov);

                    PdfPTable t = new PdfPTable(4);
                    t.WidthPercentage = 100;
                    t.SetWidths(new float[] { 1,1,1,1});

                    t.AddCell(GenerirajCeliju("Red. br.", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Naziv proizvoda", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Količina", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Cijena proizvoda", fontTablicaZaglavlje, tPozadinaZaglavlje, true));

                    int i = 1;
                    foreach(KosaricaItem k in kosaricas)
                    {
                        t.AddCell(GenerirajCeliju(i.ToString()+".", fontText, tPozadinaSadrzaj, true));
                        t.AddCell(GenerirajCeliju(k.Naziv, fontText, tPozadinaSadrzaj, true));
                        t.AddCell(GenerirajCeliju(k.Kolicina.ToString(), fontText, tPozadinaSadrzaj, true));
                        t.AddCell(GenerirajCeliju(k.Cijena.ToString() + ".00€", fontText, tPozadinaSadrzaj, true));
                        i++;
                    }
                    decimal ukupno = 0.00m;
                    foreach(KosaricaItem k in kosaricas)
                    {
                        ukupno += k.Cijena;
                    }
                    t.AddCell(GenerirajCeliju("", fontText, tPozadinaSadrzaj, true));
                    t.AddCell(GenerirajCeliju("", fontText, tPozadinaSadrzaj, true));
                    t.AddCell(GenerirajCeliju("Ukupno:", fontText, tPozadinaSadrzaj, true));
                    t.AddCell(GenerirajCeliju(ukupno.ToString() + "€", fontText, tPozadinaSadrzaj, true));

                    pdfDokument.Add(t);

                    pNaslov = new Paragraph("Čakovec, " + System.DateTime.Now.ToString("dd.MM.yyyy"), fontText);
                    pNaslov.Alignment = Element.ALIGN_RIGHT;
                    pNaslov.SpacingBefore = 30;
                    pdfDokument.Add(pNaslov);
                }

                Podaci = mstream.ToArray();

                using(var reader = new PdfReader(Podaci))
                {
                    using (var ms = new MemoryStream())
                    {
                        using(var stamper = new PdfStamper(reader, ms))
                        {
                            int PageCount = reader.NumberOfPages;
                            for(int i = 1; i <= PageCount; i++) {
                                Rectangle pageSize = reader.GetPageSize(i);
                                PdfContentByte canvas = stamper.GetOverContent(i);

                                canvas.BeginText();
                                canvas.SetFontAndSize(bfontPodnozje, 10);

                                canvas.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, $"Stranica {i} / {PageCount}", pageSize.Right - 50, 30, 0);
                                canvas.EndText();
                            }
                        }
                        Podaci = ms.ToArray();
                    } 
                }
            }
        }
    }
}