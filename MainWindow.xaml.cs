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

namespace CariasWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] names = new string[100];
        string[] addresses = new string[100];
        string[] order = new string[100];
        string[]quantity = new string[100];
      

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
            string order = comboboxOrder.Text;



            string quantity = "";
            if (rbOne.IsChecked == true) quantity = "1";
            else if (rbTwo.IsChecked == true) quantity = "2";
            else if (rbThree.IsChecked == true) quantity = "3";
            else if (rbFour.IsChecked == true) quantity = "4";
            else if (rbFive.IsChecked == true) quantity = "5";
            else if (rbSix.IsChecked == true) quantity = "6";
            else if (rbSeven.IsChecked == true) quantity = "7";

            string data = $"{name} - {address}  =  {order} ={quantity} ";

            //pag gamit ramog TryParse para dle mag crash para ma convert ang string to integer since nag gamit mankog textbox dha sa price nga field



            string data1 = $"{name}";
            if (name == "")
            {
                MessageBox.Show("Please add customer name", "Customer Details", MessageBoxButton.OK);
                return;
            }

            string data2 = $"{address}";
            if (address == "")
            {
                MessageBox.Show("Please provide customer address", "Customer Details", MessageBoxButton.OK);
                return;
            }



            string data4 = $"{order}";
            if (order == "")
            {
                MessageBox.Show("Please add order", "Customer Details", MessageBoxButton.OK);
                return;
            }

            string data5 = $"{quantity}";
            if (quantity == "")
            {
                MessageBox.Show("Please add quantity", "Customer Details", MessageBoxButton.OK);
                return;
            }

            SaveData(name, address, order, quantity);
            ClearData();
            OrderDeliver();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            if (index >= 0)
            {
                txtPlayerName.Text = names[index];
                txtAddress.Text = addresses[index];
                comboboxOrder.Text = order[index];
                


                if (quantity[index] == "1") rbOne.IsChecked = true;
                else if (quantity[index] == "2") rbTwo.IsChecked = true;
                else if (quantity[index] == "3") rbThree.IsChecked = true;
                else if (quantity[index] == "4") rbFour.IsChecked = true;
                else if (quantity[index] == "5") rbFive.IsChecked = true;
                else if (quantity[index] == "6") rbSix.IsChecked = true;
                else if (quantity[index] == "7") rbSeven.IsChecked = true;


                //para ma click ninyo ang button nga gi butangan ninyo didtos interface
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

            comboboxOrder.SelectedIndex = -1;
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
                order[index] = o;
                quantity[index] = q;
               
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

                MessageBox.Show("New order successfully added!", "Customer Detail", MessageBoxButton.OK);
            }
            else if (status == 'E' && updatedIndex >= 0)
            {
                //update data sa given index
                names[updatedIndex] = n;
                addresses[updatedIndex] = a;              
                order[updatedIndex] = o;
                quantity[updatedIndex] = q;
                
              
                //refresh datagrid
                RefreshGrid();

                //set sa default status ug updatedindex
                status = 'A';
                updatedIndex = -1;

                MessageBox.Show("Customer order successfully updated!", "Customer Detail", MessageBoxButton.OK);
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
            //               mao niy content sa inyoang message |   | tas kani dre mao ni ang Heading or Title ba na
            MessageBox.Show("Customer orders removed successfully!", "CUSTOMER ORDER", MessageBoxButton.OK);
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
               order[i] = order[i + 1];
                quantity[i] = quantity[i + 1];
               
            }
        }





        private void OrderDeliver()
        {
            txtPlayerName.Clear();
            txtAddress.Clear();
            comboboxOrder.SelectedIndex = -1;
            //Quantity
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
                   
                    TeamName= order[i],
                    TotalWin = quantity[i],
                    
                    
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
                    TeamName = order[i],
                    TotalWin = quantity[i],
                   
                });
            }
        }

        private void ShiftElements(int deletedIndex)
        {
            for (int i = deletedIndex; i < index; i++)
            {
                names[i] = names[i + 1];
                addresses[i] = addresses[i + 1];
                order[i] = order[i + 1];
                quantity[i] = quantity[i + 1];
               
            }
        }



    }

}