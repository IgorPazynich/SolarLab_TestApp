using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace ToDo
{
    public partial class Form1 : Form
    {
        ToDoContext db;
        public Form1()
        {
            InitializeComponent();
            db = new ToDoContext();
            db.Goals.Load();

            dataGridView1.DataSource = db.Goals.Local.ToBindingList();
        }

        //Добавить
        private void button1_Click(object sender, EventArgs e)
        {
            GoalForm goalForm = new GoalForm();
            DialogResult result = goalForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Goal goal = new Goal();
            goal.Name = goalForm.textBox1.Text;
            goal.Note = goalForm.textBox2.Text;
            goal.DateTimeToDo = DateTime.ParseExact(goalForm.dateTimePicker1.Text,"dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            db.Goals.Add(goal);
            db.SaveChanges();
            MessageBox.Show("Новая задача добавлена");
        }

        //Редактировать
        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Goal goal = db.Goals.Find(id);
                GoalForm goalForm = new GoalForm();
                goalForm.textBox1.Text = goal.Name;
                goalForm.textBox2.Text = goal.Note;
                goalForm.dateTimePicker1.Text = goal.DateTimeToDo.ToString();

                DialogResult result = goalForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                goal.Name = goalForm.textBox1.Text;
                goal.Note = goalForm.textBox2.Text;
                goal.DateTimeToDo = DateTime.ParseExact(goalForm.dateTimePicker1.Text, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Задача обновлена");
            }
        }

        //Удалить
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Goal goal = db.Goals.Find(id);
                db.Goals.Remove(goal);
                db.SaveChanges();

                MessageBox.Show("Задача удалена");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'toDoListDataSet.Goals' table. You can move, or remove it, as needed.
            this.goalsTableAdapter.Fill(this.toDoListDataSet.Goals);

        }
    }
}
