using System;
using System.Windows.Forms;


namespace Scrabble
{
    //Form that allows user to add custom words to custom dictionary
    public partial class CustomWordsForm : Form
    {
        public CustomWordsForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Adds custom word to database when the Add button is clicked
        private void addWordButton_Click(object sender, EventArgs e)
        {
            DatabaseConnection.AddCustomWord(customWordTxtBox, definitionTxtBox, infoLabel);
        }
    }
}
