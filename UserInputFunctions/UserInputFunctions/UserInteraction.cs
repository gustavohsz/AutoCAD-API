using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Colors;

namespace UserInputFunctions
{
    public class UserInteraction
    {
   
            [CommandMethod("GetName")] //Atributo que define que este método pode ser chamado como um comando em AutoCAD
            public void GetNameUsingGetString()
            {
                //Obtém o documento atual e o editor a partir do objeto Application
                Document doc = Application.DocumentManager.MdiActiveDocument;
                Editor edt = doc.Editor;

                //Configura as opções do prompt para o usuário, no caso, "Enter your name:" e permite espaços na resposta
                PromptStringOptions prompt = new PromptStringOptions("Enter your name: ");
                prompt.AllowSpaces = true;

                //Exibe o prompt para o usuário e aguarda a resposta, que será armazenada em result
                PromptResult result = edt.GetString(prompt);

                //Verifica se a resposta foi recebida com sucesso
                if (result.Status == PromptStatus.OK)
                {
                    //Se sim, armazena a resposta do usuário em uma variável string
                    string name = result.StringResult;

                    //Exibe uma mensagem na linha de comando do AutoCAD com o nome do usuário
                    edt.WriteMessage("Hello there: " + name);

                    //Exibe uma janela de alerta com o nome do usuário
                    Application.ShowAlertDialog("Your name is: " + name);
                }
                else
                {
                    //Se o usuário não forneceu uma resposta, exibe uma mensagem na linha de comando e uma janela de alerta informando que nenhum nome foi inserido
                    edt.WriteMessage("No name entered.");
                    Application.ShowAlertDialog("No name entered.");
                }

            }

        [CommandMethod("SetLayerUsingGetString")]
        public void SetLayerUsingGetString()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;


            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                PromptStringOptions prompt = new PromptStringOptions("Enter the layer name: ");
                prompt.AllowSpaces = true;
                PromptResult result = edt.GetString(prompt);
                if (result.Status == PromptStatus.OK)
                {
                    string layerName = result.StringResult;

                    if(lyTab.Has(layerName) == true)
                    {
                        db.Clayer = lyTab[layerName];
                        trans.Commit();
                    }
                    else
                    {
                        Application.ShowAlertDialog("The layer "+layerName + " you entered does not exist.");
                    }
                }
                else
                {
                    Application.ShowAlertDialog("No name entered.");
                }
            }            
        }

        [CommandMethod("CreateLineUsingGetPoint")]
        public void CreateLineUsingGetPoint()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            PromptPointOptions ppo = new PromptPointOptions("Escolha o ponto inicial: ");
            PromptPointResult ppr = edt.GetPoint(ppo);
            Point3d startPt = ppr.Value;

            ppo = new PromptPointOptions("Pick end point: ");
            ppo.UseBasePoint = true;
            ppo.BasePoint = startPt;
            ppr = edt.GetPoint(ppo);
            Point3d endPt = ppr.Value;

            if(startPt == null || endPt == null)
            {
                edt.WriteMessage("Invalid point.");
                return;
            }
            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //abrir o blocktable para leitura
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    //escreve no blocktable
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Line ln = new Line(startPt, endPt);
                    ln.SetDatabaseDefaults();

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
        [CommandMethod("GetDistanceBetweenTwoPoints")]
        public void GetDistanceBetweenTwoPoints()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;            
            Editor edt = doc.Editor;

            PromptDoubleResult pdr;
            pdr = edt.GetDistance("Pick two points to get the distance");
            Application.ShowAlertDialog("\nDistance between points: " + pdr.Value.ToString());

        }

        [CommandMethod("DrawObjectUsingGetKeyWords")]
        public void DrawObjectUsingGetKeyWords()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            PromptKeywordOptions pko = new PromptKeywordOptions("");
            pko.Message = "\nO que você gostaria de desenhar?";
            pko.Keywords.Add("Line");
            pko.Keywords.Add("Circle");
            pko.Keywords.Add("Mtext");
            pko.AllowNone = false;

            PromptResult res = doc.Editor.GetKeywords(pko);
            string answer = res.StringResult;
            if (answer != null)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;


                    switch (answer)
                    {
                        case "Line":
                            Point3d pt1 = new Point3d(0, 0, 0);
                            Point3d pt2 = new Point3d(100, 100, 0);
                            Line ln = new Line(pt1, pt2);
                            btr.AppendEntity(ln);
                            trans.AddNewlyCreatedDBObject(ln, true);
                            break;
                        case "Circle":
                            Point3d cenPt = new Point3d(0, 0, 0);
                            Circle cir = new Circle();
                            cir.Center = cenPt;
                            cir.Radius = 10;
                            cir.ColorIndex = 1;
                            btr.AppendEntity(cir);
                            trans.AddNewlyCreatedDBObject(cir, true);
                            break;
                        case "Mtext":
                            Point3d insPt = new Point3d(100, 100, 0);
                            MText mtx = new MText();
                            mtx.Contents = "Olá";
                            mtx.Location = insPt;
                            mtx.TextHeight = 10;
                            mtx.ColorIndex = 2;
                            btr.AppendEntity(mtx);
                            trans.AddNewlyCreatedDBObject(mtx, true);
                            break;
                        default:
                            edt.WriteMessage("No option selected.");
                            break;

                    }
                    trans.Commit();
                }

            }


        }

    }
}
