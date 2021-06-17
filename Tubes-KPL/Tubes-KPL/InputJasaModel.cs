﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_KPL
{
    class InputJasaModel
    {
        public String namaToko, namaJasa, deskripsi;
        public int hargaPerPaket, jumlahPaket;

        public InputJasaModel(string namaToko, string namaJasa, int hargaPerPaket, int jumlahPaket, string deskripsi)
        {

            Debug.Assert(jumlahPaket >= 2, "Jumlah Paket harus lebih dari 2");
            Debug.Assert(hargaPerPaket >= 5000, "Harga barang harus lebih dari 5000");
            this.namaToko = namaToko;
            this.namaJasa = namaJasa;
            this.deskripsi = deskripsi;
            this.hargaPerPaket = hargaPerPaket;
            this.jumlahPaket = jumlahPaket;
        }

        public InputJasaModel()
        {

        }

        public void setNamaToko(string namaToko)
        {
            this.namaToko = namaToko;
        }
        public String getNamaToko()
        {
            return this.namaToko;
        }
        public void setNamaJasa(string namaJasa)
        {
            this.namaJasa = namaJasa;
        }
        public String getNamaJasa()
        {
            return this.namaJasa;
        }
        public void setHarga(int hargaPerPaket)
        {
            this.hargaPerPaket = hargaPerPaket;
        }
        public int getHarga()
        {
            return this.hargaPerPaket;
        }
        public void setDeskripsi(String deskripsi)
        {
            this.deskripsi = deskripsi;
        }
        public String getDeskripsi()
        {
            return this.deskripsi;
        }

        public void setJumlahPaket(int jumlahPaket)
        {
            this.jumlahPaket = jumlahPaket;
        }
        public int getJumlahPaket()
        {
            return this.jumlahPaket;
        }
    }
}
