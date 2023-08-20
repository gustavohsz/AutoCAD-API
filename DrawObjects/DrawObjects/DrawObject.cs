using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace DrawObjects
{
    public class DrawObject
    {
        [CommandMethod("DrawLine")]
        public void DrawLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {                
                try
                { 
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    ed.WriteMessage("\nDrawing a Line object:");
                    Point3d pt1 = new Point3d(0, 0, 0);
                    Point3d pt2 = new Point3d(100, 100, 0);
                    Line ln = new Line(pt1, pt2);
                    ln.ColorIndex = 1;
                    btr.AppendEntity(ln);
                    trans.AddNewlyCreatedDBObject(ln, true);
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }            
        }

        [CommandMethod("DrawMText")]
        public void DrawMText()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    edt.WriteMessage("Drawing MText Exercise!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Specify the MText's parameters (i.e. textstring and insertionPoint)
                    string txt = "Hello AutoCAD from C#!";
                    Point3d insPt = new Point3d(200, 200, 0);
                    using (MText mtx = new MText())
                    {
                        mtx.Contents = txt;
                        mtx.Location = insPt;

                        btr.AppendEntity(mtx);
                        trans.AddNewlyCreatedDBObject(mtx, true);
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }


        [CommandMethod("DrawCircle")]
        public void DrawCircle()
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    doc.Editor.WriteMessage("Drawing a Circle!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Specify the Circle's parameters (i.e. centerpoint, radius, etc. etc.)
                    Point3d centerPt = new Point3d(100, 100, 0);
                    double circleRad = 100;
                    using (Circle circle = new Circle())
                    {
                        circle.Radius = circleRad;
                        circle.Center = centerPt;

                        btr.AppendEntity(circle);
                        trans.AddNewlyCreatedDBObject(circle, true);
                    }

                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DrawCirclesVert")]
        public void DrawCirclesVert()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    doc.Editor.WriteMessage("Drawing vertical Circles!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Specify the Circle's parameters (i.e. centerpoint, radius, etc. etc.)
                    double circleRad = 100;
                    int y = 100;
                    for (int i = 0; i < 10; i++)
                    {
                        Point3d centerPt = new Point3d(100, y, 0);
                        using (Circle circle = new Circle())
                        {
                            circle.Radius = circleRad;
                            circle.Center = centerPt;

                            btr.AppendEntity(circle);
                            trans.AddNewlyCreatedDBObject(circle, true);
                        }
                        y = y + 50;
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DrawCirclesHor")]
        public void DrawCirclesHor()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    doc.Editor.WriteMessage("Drawing horizontal Circles!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Specify the Circle's parameters (i.e. centerpoint, radius, etc. etc.)
                    double circleRad = 100;
                    int x = 100;
                    for (int i = 0; i < 10; i++)
                    {
                        Point3d centerPt = new Point3d(x, 100, 0);
                        using (Circle circle = new Circle())
                        {
                            circle.Radius = circleRad;
                            circle.Center = centerPt;

                            btr.AppendEntity(circle);
                            trans.AddNewlyCreatedDBObject(circle, true);
                        }
                        x += 50;
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DrawArc")]
        public void DrawArc()
        {
            // Get the drawing document and the database object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Create the transaction object inside the using block
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Get the BlockTable object
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create the Arc
                    Point3d centerPt = new Point3d(10.0, 10.0, 0);
                    double arcRad = 20.0;
                    double startAngle = 1.0;
                    double endAngle = 3.0;
                    Arc arc = new Arc(centerPt, arcRad, startAngle, endAngle);                
                    // Set the default properties
                    arc.SetDatabaseDefaults();
                    btr.AppendEntity(arc);
                    trans.AddNewlyCreatedDBObject(arc, true);

                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }


        [CommandMethod("DrawPline")]
        public void DrawPline()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    doc.Editor.WriteMessage("Drawing a 2D Polyline!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Specify the Polyline's coordinates
                    Polyline pl = new Polyline();                    
                    pl.SetDatabaseDefaults();
                    pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                    pl.AddVertexAt(1, new Point2d(10, 10), 0, 0, 0);
                    pl.AddVertexAt(2, new Point2d(20, 20), 0, 0, 0);
                    pl.AddVertexAt(3, new Point2d(30, 30), 0, 0, 0);
                    pl.AddVertexAt(4, new Point2d(40, 40), 0, 0, 0);
                    pl.AddVertexAt(5, new Point2d(50, 50), 0, 0, 0);

                    // Add the new Polyline in the block table record and the transaction
                    pl.SetDatabaseDefaults();
                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);

                    // Save the new object to the database
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }
    }
}
