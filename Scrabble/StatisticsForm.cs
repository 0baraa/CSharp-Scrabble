using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble
{
	//This form displays statistics for player rankings and previous game info.
	//User can change how the data is sorted.
    public partial class StatisticsForm : Form
    {
		bool firstChange = true;
		public StatisticsForm()
        {
            InitializeComponent();

			playerComboBox.Items.Add("Highest score in one game");
			playerComboBox.Items.Add("Number of games played");
			playerComboBox.Items.Add("Number of games won");
			playerComboBox.Items.Add("Highest score in one turn");

			gamesComboBox.Items.Add("Most recent");
			gamesComboBox.Items.Add("Least recent");
			gamesComboBox.Items.Add("Highest score in one turn");

			updateDataGrids();

		}

		private void btReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void playerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
			string selection = playerComboBox.Text;
			updateDataGrids(1, selection);

		}

		private void gamesComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			string selection = gamesComboBox.Text;
			updateDataGrids(2, null, selection);
		}

		private void updateDataGrids(int mode = 0, string playerSelection = null, string gameSelection = null)
        {
			DataTable[] statsTables = DatabaseConnection.loadDataGrid(firstChange, mode, playerSelection, gameSelection);
			DataTable playertable = statsTables[0];
			DataTable gametable = statsTables[1];



			if (firstChange == true)
			{
				playerGrid.DataSource = playertable;
				playerGrid.Show();

				gameGrid.DataSource = gametable;
				gameGrid.Show();

				firstChange = false;
			}
			
			if (mode == 1)
			{
				playerGrid.DataSource = playertable;
				playerGrid.Show();
			}

			if (mode == 2)
			{
				gameGrid.DataSource = gametable;
				gameGrid.Show();
			}
		}

    }
}
