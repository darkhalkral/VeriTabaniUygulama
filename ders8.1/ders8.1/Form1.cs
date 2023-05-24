using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace ders8._1
{
    public partial class Form1 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        DataTable dt = new DataTable("Tablo");
        BindingSource bs = new BindingSource();
        Boolean eklebool, duzenlebool;
        string kimlik, ad, soyad;
        public Form1()
        {
            InitializeComponent();
        }

        void baglanti()
        {
            dt.Clear();
            con = new OleDbConnection("Provider=Microsoft.JET.Oledb.4.0; Data Source=C:\\Users\\halid\\source\\repos\\personel.mdb");
            da = new OleDbDataAdapter("Select * from ozluk", con);
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti();
        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            eklebool = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = new DialogResult();
            if (eklebool==true)
            {
                sonuc = MessageBox.Show("Kaydetmek İstiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
                if (sonuc == DialogResult.Yes)
                {
                    bs.EndEdit();
                    OleDbCommand ekle = new OleDbCommand("insert into ozluk(Kimlik,ad,soyad) values(" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ",'" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "')", con);
                    con.Open();
                    ekle.ExecuteNonQuery();
                    con.Close();
                    eklebool = false;
                }
                else
                {
                    bs.RemoveCurrent();
                    eklebool = false;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = new DialogResult();
            sonuc = MessageBox.Show("Silmek İstiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                OleDbCommand sil = new OleDbCommand("delete from ozluk where Kimlik=" + dataGridView1.CurrentRow.Cells[0].Value.ToString(), con);
                con.Open();
                sil.ExecuteNonQuery();
                con.Close();
                bs.RemoveCurrent();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.BeginEdit(true);
            duzenlebool = true;

            kimlik = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ad = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            soyad = dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (duzenlebool)
            {
                DialogResult sonuc = new DialogResult();
                sonuc = MessageBox.Show("Kaydetmek İstiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
                if (sonuc == DialogResult.Yes)
                {
                    OleDbCommand duzenle = new OleDbCommand("update ozluk set Kimlik=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ",ad='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "',soyad='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "' where Kimlik=" + dataGridView1.CurrentRow.Cells[0].Value.ToString(), con);
                    con.Open();
                    duzenle.ExecuteNonQuery();
                    con.Close();
                    duzenlebool = false;
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[0].Value = kimlik;
                    dataGridView1.CurrentRow.Cells[1].Value = ad;
                    dataGridView1.CurrentRow.Cells[2].Value = soyad;
                    duzenlebool = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
