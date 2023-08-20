using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using System.CodeDom;
using Autodesk.AutoCAD.GraphicsInterface;

namespace Dictionaries
{
    public class StylesClass
    {
        [CommandMethod("ListStyles")]
        public void ListStyles()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                TextStyleTable stTab = trans.GetObject(db.TextStyleTableId, OpenMode.ForRead) as TextStyleTable;
                foreach(ObjectId stID in stTab)
                {
                    TextStyleTableRecord tstr = trans.GetObject(stID, OpenMode.ForRead) as TextStyleTableRecord;
                    doc.Editor.WriteMessage("\nStyle name: " + tstr.Name);
                }
                trans.Commit();
            }

        }

        [CommandMethod("UpdateCurrentTextStyleFont")]
        public void UpdateCurrentTextStyleFont()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
               // TextStyleTable stTab = trans.GetObject(db.TextStyleTableId, OpenMode.ForRead) as TextStyleTable;
                TextStyleTableRecord tstr = trans.GetObject(db.Textstyle, OpenMode.ForWrite) as TextStyleTableRecord;
                FontDescriptor font = tstr.Font; //get the current font setting

                FontDescriptor newFont = new FontDescriptor("ARCHITECT", font.Bold, font.Italic, font.CharacterSet, font.PitchAndFamily); //update the test style's typeface with ARCHITECT
                tstr.Font = newFont;

                doc.Editor.Regen();
                    
                trans.Commit();
            }

        }

        [CommandMethod("SetCurrentTextStyle")]
        public void SetCurrentTextStyle()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                TextStyleTable stTab = trans.GetObject(db.TextStyleTableId, OpenMode.ForRead) as TextStyleTable;

                foreach(ObjectId stID in stTab)
                {
                    TextStyleTableRecord tstr = trans.GetObject(stID, OpenMode.ForRead) as TextStyleTableRecord;
                    if(tstr.Name == "ARCHITECT")
                    {
                        Application.SetSystemVariable("TEXTSTYLE", "ARCHITECT");
                        doc.Editor.WriteMessage("\nStyle name: " + tstr.Name + " is now the default TextStyle\n");

                        trans.Commit();
                        break;
                    }
                }                      
                doc.Editor.Regen();
            }
        }

        [CommandMethod("SetTextStyleToObject")]
        public void SetTextStyleToObject()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //abrir o blocktable para leitura
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    //escreve no blocktable
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using(MText mtx = new MText())
                    {
                        Point3d insPt = new Point3d(0, 0, 0);
                        mtx.Contents = "Hello AutoCAD";
                        mtx.TextHeight = 9;
                        mtx.Location = insPt;

                        mtx.TextStyleId = db.Textstyle;

                        btr.AppendEntity(mtx);
                        trans.AddNewlyCreatedDBObject(mtx, true);
                        trans.Commit();
                    }

                    doc.SendStringToExecute("._zoom e ", false, false, false);
                    doc.SendStringToExecute("._regen ", false, false, false);
                    
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error: " + ex.Message);
                    trans.Abort();

                }
            }
        }


    }
}
