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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.JET.Oledb.4.0; Data Source=C:\\Users\\halid\\source\\repos\\personel.mdb");

            OleDbDataAdapter da1 = new OleDbDataAdapter("select * from ozluk", con);
            DataTable dt1 = new DataTable("ozluk");
            da1.Fill(dt1);

            OleDbDataAdapter da2 = new OleDbDataAdapter("select * from programlama", con);
            DataTable dt2 = new DataTable("programlama");
            da2.Fill(dt2);

            OleDbDataAdapter da3 = new OleDbDataAdapter("select * from ucret", con);
            DataTable dt3 = new DataTable("ucret");
            da3.Fill(dt3);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);

            DataRelation dr1 = new DataRelation("dil", dt1.Columns["Kimlik"], dt2.Columns["Kimlik"]);
            ds.Relations.Add(dr1);

            DataRelation dr2 = new DataRelation("ücret", dt2.Columns["Kimlik"], dt3.Columns["Kimlik"]);
            ds.Relations.Add(dr2);


            dataGrid1.DataSource = ds;

        }
    }
}
