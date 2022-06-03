namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[1].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(this).Show();
        }

        private void button2_Click(object sender, EventArgs e) => new Form2(this, true).Show();

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OpenFileDialog fbd = new OpenFileDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = File.OpenText(fbd.FileName))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] strs = sr.ReadLine().Split("#");
                            if (strs.Length > 1)
                                dataGridView1.Rows.Add(strs);
                        }
                        sr.Close();
                    }
                } catch 
                {
                    MessageBox.Show("Невозможно прочитать данные из файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult r = folderBrowserDialog.ShowDialog();
            if (r == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(folderBrowserDialog.SelectedPath + @"\data"))
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                sw.Write(row.Cells[i].Value.ToString() + "#");
                            }
                            sw.WriteLine();
                        }
                        sw.Close();
                    }
                }
                catch 
                {
                    MessageBox.Show("Невозможно сохранить данные в файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            button2.Enabled = !(e.RowIndex == 0);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            button2.Enabled = true;
        }
    }
}