/*
    MD5: Calculate a checksum when you drop a file on the application
    Copyright (C) 2011  Mike Bos

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace MD5Sum
{
    public delegate void ShowFilePostionHandler(int postion);


    public partial class frmMD5 : Form
    {
        public frmMD5()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stsLabel.Text = "Drop file to begin.";
        }

        private void frmMD5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }

        private void frmMD5_DragDrop(object sender, DragEventArgs e)
        {
            stsLabel.Text = "Calculating MD5";
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string fn = files[0];
            if (File.Exists(fn))
            {
                stsLabel.Text = "Calculating MD5";
                txtMD5.Text = GetMD5HashFromFile(fn).ToUpper();
                stsLabel.Text = "Drop file to begin";
            }
        }

        protected string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }



  
    }

 
}