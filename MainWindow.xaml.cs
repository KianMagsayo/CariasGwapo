using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CariasWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] names = new string[100];
        string[] addresses = new string[100];
        string[] teamname = new string[100];
        string[] totalwin = new string[100];


        char status = 'A';
        int index = 0;
        private int updatedIndex = -1;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtPlayerName.Text;
            string address = txtAddress.Text; ;
            string teamname = comboboxOrder.Text;

            string totalwin = "";
            if (rbOne.IsChecked == true) totalwin = "1";
            else if (rbTwo.IsChecked == true) totalwin = "2";
            else if (rbThree.IsChecked == true) totalwin = "3";
            else if (rbFour.IsChecked == true) totalwin = "4";
            else if (rbFive.IsChecked == true) totalwin = "5";
            else if (rbSix.IsChecked == true) totalwin = "6";
            else if (rbSeven.IsChecked == true) totalwin = "7";

            string data = $"{name} - {address}  =  {teamname} ={totalwin} ";                           


            string data1 = $"{name}";
            if (name == "")
            {
                MessageBox.Show("Please add player name", "Player Details", MessageBoxButton.OK);
                return;
            }

            string data2 = $"{address}";
            if (address == "")
            {
                MessageBox.Show("Please provide player address", "Player Details", MessageBoxButton.OK);
                return;
            }



            string data4 = $"{teamname}";
            if (teamname == "")
            {
                MessageBox.Show("Please Add Inforamtion", "Player Details", MessageBoxButton.OK);
                return;
            }

            string data5 = $"{totalwin}";
            if (totalwin == "")
            {
                MessageBox.Show("Please add Total Win", "Player Details", MessageBoxButton.OK);
                return;
            }

            SaveData(name, address, teamname, totalwin);
            ClearData();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            if (index >= 0)
            {
                txtPlayerName.Text = names[index];
                txtAddress.Text = addresses[index];
                comboboxOrder.Text = teamname[index];



                if (totalwin[index] == "1") rbOne.IsChecked = true;
                else if (totalwin[index] == "2") rbTwo.IsChecked = true;
                else if (totalwin[index] == "3") rbThree.IsChecked = true;
                else if (totalwin[index] == "4") rbFour.IsChecked = true;
                else if (totalwin[index] == "5") rbFive.IsChecked = true;
                else if (totalwin[index] == "6") rbSix.IsChecked = true;
                else if (totalwin[index] == "7") rbSeven.IsChecked = true;


                //para ma click  ang button nga gi butangan ninyo didtos interface
                btnDeleteData.IsEnabled = true;

                btnClose.IsEnabled = true;

                status = 'E';
                updatedIndex = index;
            }

        }

        private void ClearData()
        {
            txtPlayerName.Clear();
            txtAddress.Clear();

            comboboxOrder.SelectedIndex = 1;
            rbOne.IsChecked = false;
            rbTwo.IsChecked = false;
            rbThree.IsChecked = false;
            rbFour.IsChecked = false;
            rbFive.IsChecked = false;
        }

        private void SaveData(string n, string a, string o, string q)
        {
            if (status == 'A')
            {
                //save to array
                names[index] = n;
                addresses[index] = a;
                teamname[index] = o;
                totalwin[index] = q;

                RefreshGrid();

                //add sa datagrid
                dataGrid.Items.Add(new
                {
                    PlayerName = n,
                    Address = a,
                    TeamName = o,
                    TotalWin = q,

                });
                //increment index
                index++;

                MessageBox.Show("New Inforamtion successfully added!", "Player Detail", MessageBoxButton.OK);
            }
            else if (status == 'E' && updatedIndex >= 0)
            {
                //update data sa given index
                names[updatedIndex] = n;
                addresses[updatedIndex] = a;
                teamname[updatedIndex] = o;
                totalwin[updatedIndex] = q;


                //refresh datagrid
                RefreshGrid();

                //set sa default status ug updatedindex
                status = 'A';
                updatedIndex = -1;

                MessageBox.Show("Player Information successfully updated!", "Player Detail", MessageBoxButton.OK);
            }
        }

        private void btnDeleteData_Click_1(object sender, RoutedEventArgs e)
        {
            int deleteIndex = dataGrid.SelectedIndex;

            if (deleteIndex == -1)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            ShiftElements(deleteIndex);
            //decrement
            index--;

            RefreshGrid();
            //para ma disable ang btnDeleteData_Click1
            btnDeleteData.IsEnabled = false;
            ClearData();
            //               mao niy content sa   message |   | tas kani dre mao ni ang Heading or Title ba na
            MessageBox.Show("Player stats removed successfully!", "PLAYER STATS", MessageBoxButton.OK);
        }



        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void ShiftOrders(int orderIndex)
        {
            for (int i = orderIndex; i < index - 1; i++)
            {
                names[i] = names[i + 1];
                addresses[i] = addresses[i + 1];
                teamname[i] = teamname[i + 1];
                totalwin[i] = totalwin[i + 1];

            }
        }





        private void OrderDeliver()
        {
            txtPlayerName.Clear();
            txtAddress.Clear();
            comboboxOrder.SelectedIndex = -1;
            //TOTA WIN
            rbOne.IsChecked = false;
            rbTwo.IsChecked = false;
            rbThree.IsChecked = false;
            rbFour.IsChecked = false;
            rbFive.IsChecked = false;
        }

        private void Refresh()
        {
            dataGrid.Items.Clear();
            for (int i = 0; i < index; i++)
            {
                dataGrid.Items.Add(new
                {
                    PlayerName = names[i],
                    Address = addresses[i],
                    TeamName = teamname[i],
                    TotalWin = totalwin[i],


                });
            }

        }

        private void RefreshGrid()
        {
            dataGrid.Items.Clear();
            for (int i = 0; i < index; i++)
            {
                dataGrid.Items.Add(new
                {
                    PlayerName = names[i],
                    Address = addresses[i],
                    TeamName = teamname[i],
                    TotalWin = totalwin[i],

                });
            }
        }

        private void ShiftElements(int deletedIndex)
        {
            for (int i = deletedIndex; i < index; i++)
            {
                names[i] = names[i + 1];
                addresses[i] = addresses[i + 1];
                teamname[i] = teamname[i + 1];
                totalwin[i] = totalwin[i + 1];

            }
        }



    }
}
