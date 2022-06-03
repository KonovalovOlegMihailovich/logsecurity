using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form2 : Form
    {
        private Form1 f1;
        private bool ins = false;
        public Form2(Form1 form, bool insert = false)
        {
            ins = insert;
            f1 = form;
            f1.Enabled = false;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!ins)
                dateTimePicker1.MinDate = DateTime.Now;
            else
            {
                try
                {
                    //Получаем данные из выделенной строчки и раскидываем их по текстбоксам, для того чтобы пользователь видел данные и мог их изменить.
                    var r = f1.dataGridView1.SelectedRows[0].Cells;
                    dateTimePicker1.Value = DateTime.Parse(r[0].Value.ToString());
                    textBox1.Text = r[1].Value.ToString();
                    textBox2.Text = r[2].Value.ToString();
                    textBox3.Text = r[3].Value.ToString();
                    textBox4.Text = r[4].Value.ToString();
                    textBox5.Text = r[5].Value.ToString();
                    textBox6.Text = r[6].Value.ToString();
                }
                catch
                {

                }
            }
        }

        private void textBox6_validing(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            try
            {
                DateTime.Parse(t.Text);
            }
            catch (FormatException)
            {
                t.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Обработка нежелательных ситуаций с пустыми полями.
            string errorstr = "";
            if (textBox1.Text.Length <= 10)
                errorstr = "Поле ФИО не должно иметь длину менее 10 символов";
            else if (textBox2.Text.Length != 4)
                errorstr = "Поле Год рождения не может быть менеше или больеш 4-ёх символов";
            else if (textBox3.Text.Length <= 10)
                errorstr = "Поле Должность не должно иметь длину менее 10 символов";
            else if (textBox4.Text.Length < 2)
                errorstr = "Поле Подразделение не должно иметь длину не менее 2 символов";
            if (errorstr != "")
            {
                MessageBox.Show(errorstr, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ins)
                f1.dataGridView1.Rows.Remove(f1.dataGridView1.SelectedRows[0]);
            f1.dataGridView1.Rows.Add(dateTimePicker1.Value.ToShortDateString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e) => f1.Enabled = true;
    }
}
