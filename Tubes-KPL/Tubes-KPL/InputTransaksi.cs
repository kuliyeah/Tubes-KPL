﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Tubes_KPL
{
    public partial class InputTransaksi : Form
    {       
        DataTable dtTransaksi, dtJasa;
        DateTime tglSekarang = DateTime.Now;
        private string pathDir = Environment.CurrentDirectory;
        private string pathTransaksi = @"\InputTransaksi.json";
        private string pathJasa= @"\InputJasa.json";

        public InputTransaksi()
        {
            InitializeComponent();

            try //jika file JSON sudah ada maka akan membaca data tersebut
            {
                dtTransaksi = ReadFromJson<DataTable>(pathDir + pathTransaksi);
            }
            catch //jika file JSON belum ada maka akan membuat file JSON
            {
                //membuat tabel data
                dtTransaksi = new DataTable();
                dtTransaksi.Columns.Add("Tanggal");
                dtTransaksi.Columns.Add("ID Transaksi");
                dtTransaksi.Columns.Add("ID Jasa");
                dtTransaksi.Columns.Add("Deskripsi");
                dtTransaksi.Columns.Add("Berat");
                dtTransaksi.Columns.Add("Ongkir");
                dtTransaksi.Columns.Add("Total Bayar");

                SaveToJson<DataTable>(dtTransaksi, pathDir + pathTransaksi);
            }

            //tampilkan data dari InputTransaksi.json
            dataGridTransaksi.DataSource = dtTransaksi;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setEditEnabled(false);
            btnNew.Enabled = true;
            textTanggal.Text = tglSekarang.ToString();
            setComboboxNamaJasa();
        }
        private void setEditEnabled(bool stat)
        {
            btnSimpan.Enabled = stat;
            btnBatal.Enabled = true;
            btnNew.Enabled = stat;
            comboBoxNamaJasa.Enabled = stat;
            textBerat.Enabled = stat;
            textDeskripsi.Enabled = stat;
            textIDTransaksi.Enabled = stat;
            textOngkir.Enabled = stat;
            textTotal.Enabled = stat;
            textTanggal.Enabled = false;
        }
        private void clearText()
        {
            textIDTransaksi.Text = "";
            textBerat.Text = "";
            textOngkir.Text = "";
            textTotal.Text = "";
            textDeskripsi.Text = "";
            comboBoxNamaJasa.Text = "";
        }

        private int cariHargaJasa()
        {
            dtJasa = ReadFromJson<DataTable>(pathDir + pathJasa);
            int harga = Int32.Parse(dtJasa.Rows[comboBoxNamaJasa.SelectedIndex][2].ToString());
            /*
            for (int i = 0; i < dtJasa.Rows.Count; i++) {
                if(dtJasa.Rows[i][2].ToString() == comboBoxNamaJasa.Text)
                {
                    harga = Int32.Parse(dtJasa.Rows[i][3].ToString());
                }
            }*/

            return harga;
        }

        public static T ReadFromJson<T>(string path)
        {
            string json = File.ReadAllText(path);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static void SaveToJson<T>(T obj, string path)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        private int hitungTotal(int berat, int harga, int ongkir)
        {
            int total = berat * harga + ongkir;
            return total;
        }

        private void setComboboxNamaJasa()
        {
            try //jika file JSON sudah ada maka akan membaca data tersebut
            {
                dtJasa = ReadFromJson<DataTable>(pathDir + pathJasa);
            }
            catch //jika file JSON belum ada maka akan membuat file JSON
            {
                //membuat tabel data
                dtJasa = new DataTable();
                dtJasa.Columns.Add("Nama Toko");
                dtJasa.Columns.Add("Nama Jasa");
                dtJasa.Columns.Add("Harga");
                dtJasa.Columns.Add("Jumlah Paket");
                dtJasa.Columns.Add("Deskripsi Jasa");

                SaveToJson<DataTable>(dtJasa, pathDir + pathJasa);
            }

            comboBoxNamaJasa.DataSource = dtJasa;
            comboBoxNamaJasa.DisplayMember = "Nama Jasa";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearText();
            setEditEnabled(true);
            btnNew.Enabled = false;
            textTotal.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            List<InputTransaksiModel> transaksi = new List<InputTransaksiModel>();

            //set button
            setEditEnabled(false);
            btnNew.Enabled = true;

            //set dan ambil nilai dari input user
            int idTransaksi = Int32.Parse(textIDTransaksi.Text);
            String namaJasa = comboBoxNamaJasa.Text;
            String deskripsi = textDeskripsi.Text;
            int berat = Int32.Parse(textBerat.Text);
            int ongkir = Int32.Parse(textOngkir.Text);
            int totalBayar = hitungTotal(berat, cariHargaJasa(), ongkir);
            textTotal.Text = totalBayar.ToString();

            //masukan data kedalam list
            transaksi.Add(new InputTransaksiModel(tglSekarang, idTransaksi, namaJasa, deskripsi,
                berat, ongkir, totalBayar));

            //isi tabel data dengan data dari list
            for (int i = 0; i < transaksi.Count; i++)
            {
                dtTransaksi.Rows.Add(
                    transaksi[i].getTanggal().ToString(),
                    transaksi[i].getIdTransaksi().ToString(),
                    transaksi[i].getNamaJasa().ToString(),
                    transaksi[i].getDeskripsiCucian().ToString(),
                    transaksi[i].getBeratCucian().ToString(),
                    transaksi[i].getOngkir().ToString(),
                    transaksi[i].getTotalBayar().ToString()
                    );
            }

            //tampilkan data ke UI
            dataGridTransaksi.DataSource = dtTransaksi;

            //simpan dan update data ke JSON
            SaveToJson<DataTable>(dtTransaksi, pathDir + pathTransaksi);
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            clearText(); 
            setEditEnabled(false);
            btnNew.Enabled = true;
            this.Hide();
            new Dashboard().Show();
        }

    }
}
