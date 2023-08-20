using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace Dictionaries
{
    public class LineTypesClass
    {

        [CommandMethod("ListLineTypes")]
        public void ListLineTypes()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                foreach(ObjectId ltID in ltTab)
                {
                    LinetypeTableRecord lttr = trans.GetObject(ltID, OpenMode.ForRead) as LinetypeTableRecord;
                    doc.Editor.WriteMessage("\nLinetype name: " + lttr.Name);
                }
                trans.Commit();
            }
        }


        [CommandMethod("LoadLineType")]
        public static void LoadLineType()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    string ltName = "CENTER";
                    if (ltTab.Has(ltName))
                    {
                        doc.Editor.WriteMessage("Linetype already exist");
                        trans.Abort();
                    }
                    else
                    {
                        db.LoadLineTypeFile(ltName, "acad.lin"); //load the center linetype
                        doc.Editor.WriteMessage("Linetype [" + ltName + "] was created");
                        trans.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
            }
        }


        [CommandMethod("SetCurrentLineType")]
        public static void SetCurrentLineType()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    string ltName = "DASHED2";

                    if (ltTab.Has(ltName)){
                        db.Celtype = ltTab[ltName];
                        doc.Editor.WriteMessage("Linetype [" + ltName + "] is now the current LineType");
                    }
                                        
                    trans.Commit();
                
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DeleteLineType")]
        public static void DeleteLineType()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    db.Celtype = ltTab["ByLayer"];

                    foreach(ObjectId ltID in ltTab)
                    {
                        LinetypeTableRecord lttr = trans.GetObject(ltID, OpenMode.ForRead) as LinetypeTableRecord;
                        if(lttr.Name == "DASHED2")
                        {
                            lttr.UpgradeOpen();

                            lttr.Erase(true);

                            trans.Commit();
                            doc.Editor.WriteMessage("\nlineType deleted");
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
            }
        }


        [CommandMethod("SetLinetypeToObject")]
        public static void SetLinetypeToObject()
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

                    Point3d pt1 = new Point3d(0, 0, 0);
                    Point3d pt2 = new Point3d(100, 100, 0);
                    Line ln = new Line(pt1, pt2);

                    ln.Linetype = "HIDDEN"; //set the linetype

                    btr.AppendEntity(ln);
                    trans.AddNewlyCreatedDBObject(ln, true);

                    doc.SendStringToExecute("._zoom e ", false, false, false);
                    doc.SendStringToExecute("._regen ", false, false, false);
                    trans.Commit();
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
