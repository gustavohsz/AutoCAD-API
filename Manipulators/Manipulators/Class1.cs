using System;
using System.Security.Cryptography.X509Certificates;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace Manipulators
{
    public class Class1
    {

        [CommandMethod("SingleCopy")]
        public static void SingleCopy()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transsaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                    // Create a circle that is at 2,3 with a radius of 4.25
                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(2, 3, 0);
                        c1.Radius = 4.25;

                        // Add the new object to the block table record and the transaction
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        // Create a copy of the circle and change its radius
                        Circle c1Clone = c1.Clone() as Circle;
                        c1Clone.Radius = 1;

                        // Add the cloned circle
                        btr.AppendEntity(c1Clone);
                        trans.AddNewlyCreatedDBObject(c1Clone, true);
                    }

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

        [CommandMethod("MultipleCopy")]
        public static void MultipleCopy()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create a circle that is at (0,0,0) with a radius of 5
                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 5;

                        // Add the new object to the block table record and the transaction
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        // Create a circle that is at (0,0,0) with a radius of 7
                        using (Circle c2 = new Circle())
                        {
                            c2.Center = new Point3d(0, 0, 0);
                            c2.Radius = 7;

                            // Add the new object to the block table record and the transaction
                            btr.AppendEntity(c2);
                            trans.AddNewlyCreatedDBObject(c2, true);

                            // Add all the objects to clone
                            DBObjectCollection col = new DBObjectCollection();
                            col.Add(c1);
                            col.Add(c2);

                            foreach (Entity acEnt in col)
                            {
                                Entity ent;
                                ent = acEnt.Clone() as Entity;
                                ent.ColorIndex = 1;

                                // Create a matrix and move each copied entity 20 units
                                ent.TransformBy(Matrix3d.Displacement(new Vector3d(20, 0, 0)));

                                // Add the cloned object
                                btr.AppendEntity(ent);
                                trans.AddNewlyCreatedDBObject(ent, true);
                            }
                        }
                        // Save the new object to the database
                        trans.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("EraseObject")]
        public static void EraseObject()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create a lightweight polyline
                    using (Polyline acPoly = new Polyline())
                    {
                        acPoly.AddVertexAt(0, new Point2d(2, 4), 0, 0, 0);
                        acPoly.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
                        acPoly.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

                        // Add the new object to the block table record and the transaction
                        btr.AppendEntity(acPoly);
                        trans.AddNewlyCreatedDBObject(acPoly, true);

                        doc.SendStringToExecute("._zoom e ", false, false, false);

                        // Update the display and display an alert message
                        doc.Editor.Regen();
                        Application.ShowAlertDialog("Erase the newly added polyline.");

                        // Erase the polyline from the drawing
                        acPoly.Erase(true);
                    }

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

        [CommandMethod("MoveObject")]
        public static void MoveObject()
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
                    //criando um circulo que está no ponto 2,2 e com raio 0,5
                    using(Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(2, 2, 0);
                        c1.Radius = 0.5;

                    // criando uma matriz e movendo o circulo usando um vetor de (0,0,0) para (2,0,0)
                        Point3d startPt = new Point3d(0, 0, 0);
                        Vector3d destVector = startPt.GetVectorTo(new Point3d(2, 0, 0)); // movendo duas unidades para a direita

                        c1. TransformBy(Matrix3d.Displacement(destVector));

                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);
                        doc.SendStringToExecute("._zoom e ", false, false, false);
                        doc.SendStringToExecute("._regen ", false, false, false);

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

        [CommandMethod("MirrorObject")]
        public static void MirrorObject()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try{
                    //abrir o blocktable para leitura
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    //escreve no blocktable
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //criando uma lightweight poliline

                    using (Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0, new Point2d(1, 1), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(1, 2), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(2, 2), 0, 0, 0);
                        pl.AddVertexAt(3, new Point2d(3, 2), 0, 0, 0);
                        pl.AddVertexAt(4, new Point2d(4, 4), 0, 0, 0);
                        pl.AddVertexAt(5, new Point2d(4, 1), 0, 0, 0);

                        pl.SetBulgeAt(1, -1);

                        pl.Closed = true;

                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);

                        Polyline plMirror = pl.Clone() as Polyline;
                        plMirror.ColorIndex = 5;

                        //definir a linha de espelhametno
                        Point3d ptFrom = new Point3d(0,4.25,0);
                        Point3d ptTo = new Point3d(4, 4.25, 0);
                        Line3d ln = new Line3d(ptFrom, ptTo);

                        // espelhando o objeto pelo eixo X
                        plMirror.TransformBy(Matrix3d.Mirroring(ln));

                        btr.AppendEntity(plMirror);
                        trans.AddNewlyCreatedDBObject(plMirror, true);

                    }
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

        [CommandMethod("RotateObject")]
        public static void RotateObject()
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

                    using(Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0, new Point2d(1, 2), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(1, 3), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(2, 3), 0, 0, 0);
                        pl.AddVertexAt(3, new Point2d(3, 3), 0, 0, 0);
                        pl.AddVertexAt(4, new Point2d(4, 4), 0, 0, 0);
                        pl.AddVertexAt(5, new Point2d(4, 2), 0, 0, 0);

                        pl.Closed = true;

                        Matrix3d curUSCMatrix = doc.Editor.CurrentUserCoordinateSystem;
                        CoordinateSystem3d curUSC = curUSCMatrix.CoordinateSystem3d;

                        //rotaciona a polilinha 45 graus, entorno do eixo z do atual UCS
                        //usando como ponto base (4,4.25,0)

                        pl.TransformBy(Matrix3d.Rotation(0.7854, curUSC.Zaxis, new Point3d(4, 4.25, 0)));

                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                    }


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

        [CommandMethod("ScaleObject")]
        public static void ScaleObject()
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

                    using (Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0, new Point2d(1, 2), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(1, 3), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(2, 3), 0, 0, 0);
                        pl.AddVertexAt(3, new Point2d(3, 3), 0, 0, 0);
                        pl.AddVertexAt(4, new Point2d(4, 4), 0, 0, 0);
                        pl.AddVertexAt(5, new Point2d(4, 2), 0, 0, 0);

                        pl.Closed = true;

                        pl.TransformBy(Matrix3d.Scaling(0.5, new Point3d(4, 4.25, 0)));
                        
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);
                    }


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

        /// 
        /// exercises
        /// 

        [CommandMethod("CopyX")]
        public static void CopyExercise()
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

                    using(Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 1;
                        c1.ColorIndex = 1;

                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        using(Circle c2 = new Circle())
                        {
                            c2.Center = new Point3d(10, 10, 0);
                            c2.Radius = 2;

                            btr.AppendEntity(c2);
                            trans.AddNewlyCreatedDBObject(c2, true);

                            using (Circle c3 = new Circle())
                            {
                                c3.Center = new Point3d(30, 30, 0);
                                c3.Radius = 5;
                                c3.ColorIndex = 5;

                                btr.AppendEntity(c3);
                                trans.AddNewlyCreatedDBObject(c3, true);

                                DBObjectCollection col = new DBObjectCollection();
                                col.Add(c1);
                                col.Add(c2);
                                col.Add(c3);

                                foreach (Circle acEnt in col)
                                {
                                    Circle ent;
                                    ent = acEnt.Clone() as Circle;
                                    
                                    if(ent.Radius == 2)
                                    {
                                        ent.ColorIndex = 3;
                                        ent.Radius = 10;
                                    }
                                                                        
                                    // Add the cloned object
                                    btr.AppendEntity(ent);
                                    trans.AddNewlyCreatedDBObject(ent, true);
                                }
                            }
                        }
                    }

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

        [CommandMethod("EraseX")]
        public static void EraseExercise()
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

                    using (Line ln = new Line())
                    {
                        ln.StartPoint = new Point3d(0, 0, 0);
                        ln.EndPoint = new Point3d(10, 10, 0);
                        
                        btr.AppendEntity(ln);
                        trans.AddNewlyCreatedDBObject(ln, true);

                        using (Circle c1 = new Circle())
                        {
                            c1.Center = new Point3d(0, 0, 0);
                            c1.Radius = 5;

                            btr.AppendEntity(c1);
                            trans.AddNewlyCreatedDBObject(c1, true);

                            Polyline pl = new Polyline();                            
                            pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                            pl.AddVertexAt(1, new Point2d(-10, 10), 0, 0, 0);
                            pl.AddVertexAt(2, new Point2d(20, -20), 0, 0, 0);

                            btr.AppendEntity(pl);
                            trans.AddNewlyCreatedDBObject(pl, true);

                            DBObjectCollection col = new DBObjectCollection();
                            col.Add(ln);
                            col.Add(c1);
                            col.Add(pl);

                            foreach (Entity ent in col)
                            {
                                if (ent is Line) ent.ColorIndex = 2;
                                else if (ent is Polyline) ent.ColorIndex = 3;
                                else if (ent is Circle) ent.Erase(true);

                            }                            
                        }
                    }

                    

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

        [CommandMethod("MoveX")]
        public static void MoveExercise()
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

                    string txt = "Move Me";
                    Point3d insPt = new Point3d(0, 0, 0);
                    using (MText mtx = new MText())
                    {
                        mtx.Contents = txt;
                        mtx.Height = 5;
                        mtx.Location = insPt;
                        mtx.ColorIndex = 3;

                        MText mtx2 = new MText();
                        mtx2.Contents = "Don't move me";
                        mtx2.Height = 5;
                        mtx2.Location = insPt;
                        mtx2.ColorIndex = 2;

                        MText mtx3 = new MText();
                        mtx3.Contents = "Don't move me either";
                        mtx3.Height = 5;
                        mtx3.Location = insPt;
                        mtx3.ColorIndex = 1;                                            

                        DBObjectCollection col = new DBObjectCollection();
                        col.Add(mtx);
                        col.Add(mtx2);
                        col.Add(mtx3);
                                                
                        foreach (MText mText in col)
                        {
                           
                            if (mText.Text.ToLower() == "move me")
                            {
                                Point3d startPt = new Point3d(0, 0, 0);
                                Vector3d destVector = startPt.GetVectorTo(new Point3d(50, 50, 0));
                                mText.TransformBy(Matrix3d.Displacement(destVector));
                            }
                            btr.AppendEntity(mText);
                            trans.AddNewlyCreatedDBObject(mText, true);
                        }

                        //btr.AppendEntity(mtx);
                        //trans.AddNewlyCreatedDBObject(mtx, true);
                        //btr.AppendEntity(mtx2);
                        //trans.AddNewlyCreatedDBObject(mtx2, true);
                        //btr.AppendEntity(mtx3);
                        //trans.AddNewlyCreatedDBObject(mtx3, true);

                    }

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

        [CommandMethod("MirrorX")]
        public static void Mirrored()
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
                        Point3d insPosition = new Point3d(0, 0, 0);
                        mtx.Contents = "Mirrored";
                        mtx.Height = 3;
                        mtx.ColorIndex = 1;
                        mtx.Location = insPosition;

                        Point3d insPosition2 = new Point3d(-10, 0, 0);
                        MText mtx2 = new MText();
                        mtx2.Contents = "Mirrored";
                        mtx2.Height = 5;
                        mtx2.ColorIndex = 2;
                        mtx2.Location = insPosition2;

                        MText mtx3 = new MText();
                        mtx3.Contents = "Mirrored";
                        mtx3.Height = 5;
                        mtx3.ColorIndex = 2;
                        mtx3.Location = insPosition2;                                                                     

                        DBObjectCollection col = new DBObjectCollection();

                        col.Add(mtx);
                        col.Add(mtx2);
                        col.Add(mtx3);

                        foreach (MText mtext in col)
                        {
                            if (mtext.Height == 3 && mtext.ColorIndex == 1)
                            {
                                Point3d ptFrom = new Point3d(0, 15, 0);
                                Point3d ptTo = new Point3d(20, 15, 0);
                                Line3d ln = new Line3d(ptFrom, ptTo);
                                mtext.TransformBy(Matrix3d.Mirroring(ln));
                            }
                            btr.AppendEntity(mtext);
                            trans.AddNewlyCreatedDBObject(mtext,true); 
                        }
                    }

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

        [CommandMethod("RotateX")]
        public static void RotateExercise()
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

                    using (MText mtx = new MText())
                    {
                        Point3d insPoint = new Point3d(10,10,0);
                        mtx.Location = insPoint;
                        mtx.Contents = "Rotating MText";
                        mtx.Height = 5;

                        MText mtxClone = mtx.Clone() as MText;

                        mtxClone.ColorIndex = 1;
                        mtxClone.Contents = "Rotated MText";

                        Matrix3d curUSCMatrix = doc.Editor.CurrentUserCoordinateSystem;
                        CoordinateSystem3d curUSC = curUSCMatrix.CoordinateSystem3d;

                        mtxClone.TransformBy(Matrix3d.Rotation((30*3.1415/180),curUSC.Zaxis, new Point3d(0,0,0)));    

                        btr.AppendEntity(mtx);
                        trans.AddNewlyCreatedDBObject(mtx, true);
                        btr.AppendEntity(mtxClone);
                        trans.AddNewlyCreatedDBObject(mtxClone, true);

                    }

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

        [CommandMethod("ScaleX")]
        public static void ScaleExercise()
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

                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 5;

                        Circle c2 = new Circle();
                        c2.Center = new Point3d(10, 0, 0);
                        c2.Radius = 2.5;
                        c2.ColorIndex = 1;

                        Circle c3 = new Circle();
                        c3.Center = new Point3d(20, 0, 0);
                        c3.Radius = 5;

                        DBObjectCollection col = new DBObjectCollection();
                        col.Add(c1);
                        col.Add(c2);
                        col.Add(c3);

                        foreach (Circle c in col)
                        {
                            if(c.Radius == 2.5 && c.ColorIndex == 1)
                            {
                                c.TransformBy(Matrix3d.Scaling(4, new Point3d(10,0,0)));
                            }

                            btr.AppendEntity(c);
                            trans.AddNewlyCreatedDBObject(c, true);
                        }

                    }


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
