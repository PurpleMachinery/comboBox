using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace combobox
{
    public partial class Form1 : Form
    {
        private SqlConnection conn = new SqlConnection("Password=info211;Persist Security Info=True;User ID=sa;Initial Catalog=combobox;Data Source=LAB-08-07");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "insert into clientes (nome, endereco, fk_estado) values ('" + txtNome.Text + "','" + txtEndereco.Text + "'," + cmbEstado.SelectedValue + ")";
            SqlCommand comando = new SqlCommand(sql, conn);
            conn.Open();
            comando.ExecuteNonQuery();
            conn.Close();
            atualizar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtEndereco.Text = "";
            txtNome.Text = "";
        }

        private void atualizar()
        {
            String sql = "busca";
            conn.Open();
            SqlDataAdapter dd = new SqlDataAdapter(sql, conn);
            DataSet set = new DataSet();
            dd.Fill(set);
            conn.Close();
            dataGridAll.DataSource = set.Tables[0];
            dataGridAll.Columns[0].Visible = false;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            String sql = "select * from estados";
            conn.Open();
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet set = new DataSet();
            data.Fill(set);
            conn.Close();
            cmbEstado.DataSource = set.Tables[0];
            cmbEstado.DisplayMember = "nome";
            cmbEstado.ValueMember = "pk_id";
            atualizar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String sql = "update clientes set nome = '" + txtNome.Text + "', endereco = '" + txtEndereco.Text + "', fk_estado = '" + cmbEstado.SelectedValue + "' where pk_id = " + txtId.Text;
            SqlCommand comando = new SqlCommand(sql, conn);
            conn.Open();
            comando.ExecuteNonQuery();
            conn.Close();
            atualizar();
        }

        private void dataGridAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = "" + dataGridAll.Rows[e.RowIndex].Cells[0].Value;
            txtNome.Text = "" + dataGridAll.Rows[e.RowIndex].Cells[1].Value;
            txtEndereco.Text = "" + dataGridAll.Rows[e.RowIndex].Cells[2].Value;
            cmbEstado.Text = "" + dataGridAll.Rows[e.RowIndex].Cells[3].Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String sql = "delete from clientes where pk_id = " + txtId.Text;
            SqlCommand comando = new SqlCommand(sql, conn);
            conn.Open();
            comando.ExecuteNonQuery();
            conn.Close();
            atualizar();
        }
    }
}
