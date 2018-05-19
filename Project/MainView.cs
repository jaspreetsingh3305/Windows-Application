using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Project
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet3.VehicleType' table. You can move, or remove it, as needed.
            this.vehicleTypeTableAdapter.Fill(this.database1DataSet3.VehicleType);
            // TODO: This line of code loads data into the 'database1DataSet2.Model' table. You can move, or remove it, as needed.
            this.modelTableAdapter1.Fill(this.database1DataSet2.Model);
            // TODO: This line of code loads data into the 'database1DataSet1.Make' table. You can move, or remove it, as needed.
            this.makeTableAdapter1.Fill(this.database1DataSet1.Make);
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void viewsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void allVehiclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("Select m.Name AS Make, md.Name AS Model, Year, Price, SoldDate from Vehicle v INNER JOIN Make m ON v.MakeId = m.MakeId INNER JOIN Model md ON md.ModelId = v.ModelId ORDER BY Price DESC;", conn);
                DataTable dataSet = new DataTable();
                sda.Fill(dataSet);
                dataGridView2.DataSource = dataSet;
                dataGridView2.Columns[1].Width = 150;
            }
            ShowAllVehicles();
        }
        public void ShowAllVehicles()
        {
            AllVehicles.Visible = true;
            AddToVehicle.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToModelPanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void AllVehicles_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AllVehicles_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void NotSold_Paint(object sender, PaintEventArgs e)
        {

        }

        private void availableVeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(conString))
            {

                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("Select m.Name AS Make, md.Name AS Model, Year, Price from Vehicle v INNER JOIN Make m ON v.MakeId = m.MakeId INNER JOIN Model md ON md.ModelId = v.ModelId  WHERE SoldDate IS NULL ORDER BY Price DESC", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView3.DataSource = dt;
                dataGridView3.Columns[1].Width = 150;
            }

            ShowAvailableVehicles();
        }

        public void ShowAvailableVehicles()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = true;
            AddToVehicle.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void viewSoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(conString))
            {


                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("Select m.Name AS Make, md.Name AS Model, Year, Price, SoldDate from Vehicle v INNER JOIN Make m ON v.MakeId = m.MakeId INNER JOIN Model md ON md.ModelId = v.ModelId WHERE SoldDate is NOT NULL ORDER BY Price DESC;", conn);
                DataTable dataSet = new DataTable();
                sda.Fill(dataSet);
                dataGridView4.DataSource = dataSet;
                dataGridView4.Columns[1].Width = 150;
            }


            ShowSoldVehicles();


        }
        public void ShowSoldVehicles()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = true;
            AddToVehicle.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void financialReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            string query = "SELECT COUNT(*) FROM Vehicle WHERE SoldDate IS NOT NULL";

            SqlCommand myCommand = new SqlCommand(query, conn);
            SqlDataReader dataReader = myCommand.ExecuteReader();
            dataReader.Read();
            string vehiclesSold = dataReader.GetInt32(0).ToString();
            totalSoldLabel.Text = vehiclesSold;
            dataReader.Close();

            query = "SELECT COUNT(*) FROM Vehicle WHERE SoldDate IS NULL";
            myCommand = new SqlCommand(query, conn);
            dataReader = myCommand.ExecuteReader();
            dataReader.Read();
            string vehiclesRemaining = dataReader.GetInt32(0).ToString();
            totalRemainingLabel.Text = vehiclesRemaining;
            dataReader.Close();

            query = "SELECT SUM(Price) FROM Vehicle WHERE SoldDate IS NOT NULL";
            myCommand = new SqlCommand(query, conn);
            dataReader = myCommand.ExecuteReader();
            dataReader.Read();
            string incomeGained = dataReader.GetSqlMoney(0).ToString();
            incomeGainedLabel.Text = incomeGained;
            dataReader.Close();

            ShowFinancialReport();
            conn.Close();
        }
        public void ShowFinancialReport()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            AddToVehicle.Visible = false;
            FinancialReport.Visible = true;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void vehiclesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FinancialReport_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vehicleTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAddToVehicle();

        }
        public void ShowAddToVehicle()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = true;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void makeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAddToMakePanel();
        }

        public void ShowAddToMakePanel()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = true;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }


        private void modelTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAddToModelPanel();
        }
        public void ShowAddToModelPanel()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = true;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;

        }
        private void vehicleTypeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAddToVehicleTypePanel();
        }
        public void ShowAddToVehicleTypePanel()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            AddToVehicleTypePanel.Visible = true;
            AddToVehicleTypeForm.Visible = false;
        }


        private void addDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AddData_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadForm_Click(object sender, EventArgs e)
        {
        
            ShowAddToVehicleForm();
        }
        public void ShowAddToVehicleForm()
        {
            AddToVehicleForm.Visible = true;
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowAddToMakeForm();
        }
        public void ShowAddToMakeForm()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = true;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            ShowAddToModelForm();
        }
        public void ShowAddToModelForm()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = true;
            MainPanel.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = false;
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowAddToVehicleTypeForm();
        }
        public void ShowAddToVehicleTypeForm()
        {
            AllVehicles.Visible = false;
            NotSold.Visible = false;
            Sold.Visible = false;
            FinancialReport.Visible = false;
            AddToVehicle.Visible = false;
            AddToVehicleForm.Visible = false;
            AddToMakePanel.Visible = false;
            AddToMakeForm.Visible = false;
            AddToModelPanel.Visible = false;
            AddToModelForm.Visible = false;
            AddToVehicleTypePanel.Visible = false;
            AddToVehicleTypeForm.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(vehicleTypeNameTextbox.Text))
            {
                vehicleTypeErrorLabel.Text = "All the fields are required";
            }
            else
            {
                string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
                SqlConnection conn = new SqlConnection(conString);
                conn.Open();
                string query = "INSERT INTO VehicleType (Name)" +
                " VALUES (@Name)";

                SqlCommand myCommand = new SqlCommand(query, conn);
                myCommand.Parameters.AddWithValue("@Name", vehicleTypeNameTextbox.Text);
                myCommand.ExecuteNonQuery();
                conn.Close();
                vehicleTypeErrorLabel.Text = "";
                MessageBox.Show("Success. Your data is inserted");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CsvToDatabaseInsertion("VehicleType");
        }

        private void LoadDialouge_Click(object sender, EventArgs e)
        {
            CsvToDatabaseInsertion("Vehicle");
        }

        private void CsvToDatabaseInsertion(string tableName)
        {
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.Personal.ToString();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose a file";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True");
                    string filepath = openFileDialog1.FileName;
                    StreamReader sr = new StreamReader(filepath);
                    string line = sr.ReadLine();
                    string[] value = line.Split(',');
                    DataTable dt = new DataTable();
                    DataRow row;
                    foreach (string dc in value)
                    {
                        dt.Columns.Add(new DataColumn(dc));
                    }

                    while (!sr.EndOfStream)
                    {
                        value = sr.ReadLine().Split(',');
                        if (value.Length == dt.Columns.Count)
                        {
                            row = dt.NewRow();
                            row.ItemArray = value;
                            dt.Rows.Add(row);
                        }
                    }
                    SqlBulkCopy bc = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock);
                    bc.DestinationTableName = tableName;
                    bc.BatchSize = dt.Rows.Count;
                    con.Open();
                    bc.WriteToServer(dt);
                    bc.Close();
                    sr.Close();
                    con.Close();
                }
                MessageBox.Show("Data inserted successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show("Please format the csv file properly");
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            CsvToDatabaseInsertion("Model");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(makeTextBox.Text))
            {
                makeErrorLabel.Text = "All the fields are required";
            }
            else
            {
                string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
                SqlConnection conn = new SqlConnection(conString);
                conn.Open();
                string query = "INSERT INTO Make (Name)" +
                " VALUES (@Name)";

                SqlCommand myCommand = new SqlCommand(query, conn);
                myCommand.Parameters.AddWithValue("@Name", makeTextBox.Text);
                myCommand.ExecuteNonQuery();
                conn.Close();
                makeErrorLabel.Text = "";
                MessageBox.Show("Success. Your data is inserted");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CsvToDatabaseInsertion("Make");
        }

        private void AllVehicles_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InsertButton1_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            string query = "INSERT INTO Vehicle (MakeId, ModelId, Year, Price, SoldDate)" +
            " VALUES (@MakeId, @ModelId, @Year, @Price, @SoldDate)";

            SqlCommand myCommand = new SqlCommand(query, conn);
            myCommand.Parameters.AddWithValue("@MakeId", MakeCombobox1.SelectedValue.ToString());
            myCommand.Parameters.AddWithValue("@ModelId", ModelCombobox1.SelectedValue.ToString());
            myCommand.Parameters.AddWithValue("@Year", textBox4.Text);
            myCommand.Parameters.AddWithValue("@Price", textBox3.Text);
            myCommand.Parameters.AddWithValue("@SoldDate", dateTimePicker1.Value.Date);
            myCommand.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Success. Your data is inserted");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(colourTextBox.Text))
                {
                    errorLabel.Text = "All the fields are required";
                }
                else
                {
                    decimal engineSize = Math.Round(Convert.ToDecimal(engineSizeTextBox.Text), 1);
                    string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
                    SqlConnection conn = new SqlConnection(conString);
                    conn.Open();
                    string query = "INSERT INTO  Model(EngineSize, Name, NumberOfDoors, Colour, VehicleTypeId)" +
                    " VALUES (@EngineSize, @Name, @NumberOfDoors, @Colour, @VehicleTypeId)";
                    SqlCommand myCommand = new SqlCommand(query, conn);
                    myCommand.Parameters.AddWithValue("@EngineSize", engineSize);
                    myCommand.Parameters.AddWithValue("@Name", nameTextBox.Text);
                    myCommand.Parameters.AddWithValue("@NumberOfDoors", doorsNumericUpDown.Value);
                    myCommand.Parameters.AddWithValue("@Colour", colourTextBox.Text);
                    myCommand.Parameters.AddWithValue("@VehicleTypeId", vehicleTypeComboBox.SelectedValue);
                    myCommand.ExecuteNonQuery();
                    conn.Close();
                    errorLabel.Text = "";
                    MessageBox.Show("Success. Your data is inserted");
                }
            }
            catch (FormatException fe)
            {
                errorLabel.Text = "Please enter a valid format for the engine size";
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void ExportDataFromQuery(string query, string fileName)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            string filePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar
                + ".." + Path.DirectorySeparatorChar
                + ".." + Path.DirectorySeparatorChar
                + ".." + Path.DirectorySeparatorChar
                + "Export Files" + Path.DirectorySeparatorChar
                + fileName + ".csv";
            StreamWriter sw = new StreamWriter(filePath);
            object[] output = new object[sqlReader.FieldCount];

            for (int i = 0; i < sqlReader.FieldCount; i++)
                output[i] = sqlReader.GetName(i);

            sw.WriteLine(string.Join(",", output));

            while (sqlReader.Read())
            {
                sqlReader.GetValues(output);
                sw.WriteLine(string.Join(",", output));
            }

            sw.Close();
            sqlReader.Close();
            conn.Close();
            MessageBox.Show("Your data is exported in Export Files");
        }

        private void allDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "Select m.Name AS Make, md.Name AS Model, Year, Price, SoldDate from Vehicle v INNER JOIN Make m ON v.MakeId = m.MakeId INNER JOIN Model md ON md.ModelId = v.ModelId ORDER BY Price DESC";
            ExportDataFromQuery(query, "all_vehicles");
        }

        private void soldVehiclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "Select m.Name AS Make, md.Name AS Model, Year, Price, SoldDate from Vehicle v INNER JOIN Make m ON v.MakeId = m.MakeId INNER JOIN Model md ON md.ModelId = v.ModelId WHERE SoldDate is NOT NULL ORDER BY Price DESC";
            ExportDataFromQuery(query, "sold_vehicles");
        }

        private void availableVehiclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "Select m.Name AS Make, md.Name AS Model, Year, Price from Vehicle v INNER JOIN Make m ON v.MakeId = m.MakeId INNER JOIN Model md ON md.ModelId = v.ModelId  WHERE SoldDate IS NULL ORDER BY Price DESC";
            ExportDataFromQuery(query, "available_vehicles");
        }

        private void financialSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|Database1.mdf; Integrated Security = True";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            string query = "SELECT COUNT(*) FROM Vehicle WHERE SoldDate IS NOT NULL";
            SqlCommand myCommand = new SqlCommand(query, conn);
            SqlDataReader dataReader = myCommand.ExecuteReader();
            dataReader.Read();
            string filePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar
                + ".." + Path.DirectorySeparatorChar
                + ".." + Path.DirectorySeparatorChar
                + ".." + Path.DirectorySeparatorChar
                + "Export Files" + Path.DirectorySeparatorChar
                + "financial_report" + ".csv";
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("TotalVehiclesSold,TotalVehiclesRemaining,IncomeGained");
            string vehicleSold = dataReader.GetInt32(0).ToString();
            dataReader.Close();

            query = "SELECT COUNT(*) FROM Vehicle WHERE SoldDate IS NULL";
            myCommand = new SqlCommand(query, conn);
            dataReader = myCommand.ExecuteReader();
            dataReader.Read();
            string vehiclesRemaining = dataReader.GetInt32(0).ToString();
            dataReader.Close();

            query = "SELECT SUM(Price) FROM Vehicle WHERE SoldDate IS NOT NULL";
            myCommand = new SqlCommand(query, conn);
            dataReader = myCommand.ExecuteReader();
            dataReader.Read();
            string incomeGained = dataReader.GetSqlMoney(0).ToString();
            dataReader.Close();

            sw.WriteLine(vehicleSold + "," + vehiclesRemaining + "," + incomeGained);
            sw.Close();
            conn.Close();
            MessageBox.Show("Your file inserted");
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
