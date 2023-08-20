using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string choice = "";

            //using combobox
            choice = cboOptions.SelectedItem.ToString();
            ControlsDemoUtil cutil = new ControlsDemoUtil();
            if(choice == "Layer")
            {
                ArrayList layers = cutil.GetLayers();
                lstLayer.DataSource = layers;
                lstLineType.DataSource = null;
                lstTextStyle.DataSource = null;
                lblLayerCount.Text = "Layers count = " + layers.Count;
                lblTextStyleCount.Text = null;
                lblLinetypeCount.Text = null;
            }
            else if (choice == "TextStyle")
            {
                ArrayList textstyles = cutil.GetTextStyles();
                lstLayer.DataSource = null;
                lstLineType.DataSource = null;
                lstTextStyle.DataSource = textstyles;
                lblTextStyleCount.Text = "TextStyles count = " + textstyles.Count;
                lblLayerCount.Text = null;
                lblLinetypeCount.Text = null;
            }
            else if (choice == "Linetype")
            {
                ArrayList linetype = cutil.GetTextStyles();
                lstLayer.DataSource = null;
                lstLineType.DataSource = linetype;
                lstTextStyle.DataSource = null;
                lblLinetypeCount.Text = "LineType count = " + linetype.Count;
                lblLayerCount.Text = null;
                lblTextStyleCount.Text = null;
            }
            else if (choice == "All")
            {
                ArrayList linetype = cutil.GetTextStyles();                
                lstLineType.DataSource = linetype;                
                lblLinetypeCount.Text = "LineType count = " + linetype.Count;
                ArrayList textstyles = cutil.GetTextStyles();
                lstTextStyle.DataSource = textstyles;
                lblTextStyleCount.Text = "TextStyles count = " + textstyles.Count;
                ArrayList layers = cutil.GetLayers();
                lstLayer.DataSource = layers;
                lblLayerCount.Text = "Layers count = " + layers.Count;
            }
        }
    }
    }

