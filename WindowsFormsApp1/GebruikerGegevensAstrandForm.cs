using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GebruikerGegevensAstrandForm : Form
    {
        private Astrand astrand;

        public GebruikerGegevensAstrandForm(Astrand a)
        {
            InitializeComponent();
            this.astrand = a;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(AgeTextBox.Text, out int age))
            {
                if (Double.TryParse(WeightBox.Text, out double weight))
                {
                    astrand.PrivateData(age, SexComboBox.Text, weight);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Not a valid number in Weight field");
                }
            }
            else
            {
                MessageBox.Show("Not a valid number in Age field");
            }
        }
    }
}
