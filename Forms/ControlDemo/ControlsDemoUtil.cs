using System;
using System.Collections;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace ControlDemo
{
    
    public class ControlsDemoUtil
    {
        [CommandMethod("Demo")]
        public void Demo()
        {
            MainForm mf = new MainForm();
            mf.Show();  
        }

        public ArrayList GetLayers()
        {
            ArrayList layers = new ArrayList();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;


            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                foreach(var ly in lyTab)
                {
                    LayerTableRecord lytr = trans.GetObject(ly, OpenMode.ForRead) as LayerTableRecord;
                    layers.Add(lytr.Name);
                }
            }
            return layers;
        }

        public ArrayList GetLineTypes()
        {
            ArrayList lineTypes = new ArrayList();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;

                foreach (var lt in ltTab)
                {
                    LinetypeTableRecord lttr = trans.GetObject(lt, OpenMode.ForRead) as LinetypeTableRecord;
                    lineTypes.Add(lttr.Name);
                }
            }
            return lineTypes;
        }

        public ArrayList GetTextStyles()
        {
            ArrayList textStyles = new ArrayList();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;


            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                TextStyleTable stTab = trans.GetObject(db.TextStyleTableId, OpenMode.ForRead) as TextStyleTable;

                foreach (var st in stTab)
                {
                    TextStyleTableRecord sttr = trans.GetObject(st, OpenMode.ForRead) as TextStyleTableRecord;
                    textStyles.Add(sttr.Name);
                }
            }
            return textStyles;
        }


    }
}
