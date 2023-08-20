using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;

namespace Dictionaries
{
    public class LayersClass
    {
        [CommandMethod("ListLayers")]
        public static void ListLayers()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                foreach (ObjectId lyID in lyTab)
                {
                    LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                    doc.Editor.WriteMessage("\nLayer Name: " + lytr.Name);
                }
                trans.Commit();
            }

        }

        [CommandMethod("CreateLayer")]
        public static void CreateLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (lyTab.Has("Misc")){
                    doc.Editor.WriteMessage("Layer already exist.");
                    trans.Abort();
                }
                else
                {
                    lyTab.UpgradeOpen();
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = "Misc";
                    ltr.Color = Color.FromColorIndex(ColorMethod.ByLayer, 1);
                    lyTab.Add(ltr);
                    trans.AddNewlyCreatedDBObject(ltr, true);

                    db.Clayer = lyTab["Misc"]; //define como a camada atual "Current Layer"

                    doc.Editor.WriteMessage("Layer[" + ltr.Name + "] was created successfully");
                }
                trans.Commit();
            }
        }

        [CommandMethod("UpdateLayer")]
        public static void UpdateLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    foreach(ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if(lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            lytr.Color = Color.FromColorIndex(ColorMethod.ByLayer, 2); //update the color

                            LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable; //update the linetype
                            if (ltTab.Has("Hidden") == true)
                            {
                                lytr.LinetypeObjectId = lyTab["Hidden"];
                            }
                            trans.Commit();
                            doc.Editor.WriteMessage("\nCompleted updatinf Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nSkipping layer [" + lytr.Name + "]");
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


        [CommandMethod("TurnOffLayer")]
        public static void TurnOffLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            lytr.IsOff = true; //turn the layer ON or OFF

                            trans.Commit();
                            doc.Editor.WriteMessage("\nCompleted updating Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nSkipping layer [" + lytr.Name + "]");
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


        [CommandMethod("FrozenLayer")]
        public static void FrozenLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            lytr.IsFrozen = true; //congela a layer

                            trans.Commit();
                            doc.Editor.WriteMessage("\nCompleted updating Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nSkipping layer [" + lytr.Name + "]");
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

        [CommandMethod("DeleteLayer")]
        public static void DeleteLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            lytr.Erase(true); //deleta layer

                            trans.Commit();
                            doc.Editor.WriteMessage("\nCompleted updating Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nSkipping layer [" + lytr.Name + "]");
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


        [CommandMethod("LockLayer")]
        public static void LockLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            lytr.IsLocked = true; //bloqueia a layer

                            trans.Commit();
                            doc.Editor.WriteMessage("\nCompleted updating Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nSkipping layer [" + lytr.Name + "]");
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

        [CommandMethod("SetLayerToObject")]
        public static void SetLayerToObject()
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

                    ln.Layer = "Cabinetry";

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
